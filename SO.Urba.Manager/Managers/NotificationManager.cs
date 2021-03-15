using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

using SO.Urba.Models.ValueObjects;
using SO.Utility.Helpers;
using SO.Urba.DbContexts;

namespace SO.Urba.Managers
{
    public class NotificationManager
    {
        public void sendReferralNotification(IEnumerable<int> referralIds, bool isUpdate = false)
        {
            bool success = false;

            using (var db = new MainDb())
            {
                var groupedClients = db.referrals
                    .Include(c => c.client)
                    .Include(ci => ci.client.contactInfo)
                    .Include(cct => cct.companyCategoryType)
                    .Include(c => c.company)
                    .Include(ci => ci.company.contactInfo)
                    .Where(e => referralIds.Contains(e.referralId))
                    .ToList()
                    .GroupBy(c => c.client)
                    .Select(g => new
                    {
                        Contacts = g.Key.contactInfo,
                        Referrals = g.Select(c => c)
                    });

                string subject = (isUpdate ? "Referral Update" : "New Referral");

                foreach (var client in groupedClients)
                {
                    StringBuilder clientBody = new StringBuilder();

                    foreach (var referral in client.Referrals)
                    {
                        if (referral.client.contactInfo.preferredContactMethod == ContactMethod.Email && !string.IsNullOrWhiteSpace(referral.client.contactInfo.email))
                        {
                            if (client.Referrals.Count() > 1)
                            {
                                clientBody.AppendLine("----------------------------------------");
                                clientBody.AppendLine(string.Format("Referral #{0}", client.Referrals.ToList().IndexOf(referral) + 1));
                            }

                            clientBody.AppendLine("Type: " + referral.companyCategoryType.name);
                            clientBody.AppendLine();
                            clientBody.AppendLine("Company Name: " + referral.company.name);
                            clientBody.AppendLine("Contact Person: " + referral.company.contactInfo.fullname);
                            clientBody.AppendLine("Cell Phone: " + referral.company.contactInfo.cell);
                            clientBody.AppendLine("Home Phone: " + referral.company.contactInfo.homePhone);
                            clientBody.AppendLine("Work Phone: " + referral.company.contactInfo.workPhone);
                            clientBody.AppendLine();
                            clientBody.AppendLine("Referral Date: " + referral.referralDate);
                            clientBody.AppendLine("Description: " + referral.description);
                            clientBody.AppendLine("Quote: " + referral.quote);

                            if (client.Referrals.Count() > 1)
                            {
                                clientBody.AppendLine("----------------------------------------");
                                clientBody.AppendLine();
                            }
                        }

                        if (referral.company.contactInfo.preferredContactMethod == ContactMethod.Email && !string.IsNullOrWhiteSpace(referral.company.contactInfo.email))
                        {
                            StringBuilder providerBody = new StringBuilder();
                            providerBody.AppendLine("Client Name: " + referral.client.contactInfo.fullname);
                            providerBody.AppendLine("Client Address: " + referral.client.contactInfo.fullAddress);
                            providerBody.AppendLine("Cell Phone: " + referral.client.contactInfo.cell);
                            providerBody.AppendLine("Home Phone: " + referral.client.contactInfo.homePhone);
                            providerBody.AppendLine("Work Phone: " + referral.client.contactInfo.workPhone);
                            providerBody.AppendLine();
                            providerBody.AppendLine("Referral Date: " + referral.referralDate);
                            providerBody.AppendLine("Description: " + referral.description);
                            providerBody.AppendLine("Quote: " + referral.quote);

                            EmailHelper.SendEmail(
                                new MailAddress(referral.company.contactInfo.email, referral.company.contactInfo.fullname),
                                subject,
                                providerBody.ToString());
                        }
                    }
                    if (client.Contacts.email != null)
                    {
                        success = EmailHelper.SendEmail(
                            new MailAddress(client.Contacts.email, client.Contacts.fullname),
                            subject,
                            clientBody.ToString());

                        if (success)
                        {
                            foreach (var referral in client.Referrals)
                            {
                                referral.notificationStatus = NotificationStatus.Sent;
                            }
                            db.SaveChanges();
                        }
                    }
                }
            }
        }
    }
}
