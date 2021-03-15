using SO.Urba.Web;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SO.Urba.Web.Controllers
{
    [AllowAnonymous]
    public class HTMLController : Controller
    {
        // GET: HTML
        public ActionResult index()
        {
            HomePageContent objtable = new HomePageContent();
            try
            {
                UrbaEntityModel objentity = new UrbaEntityModel();

                var data = (from customer in objentity.HomePageContents.Where(y => y.PageName == "Home Page").Take(10).OrderByDescending(x => x.ID)
                            select customer).ToList();
                if (data != null && data.Count > 0)
                {
                    for (int i = 0; i < data.Count; i++)
                    {
                        if (data[i].Section1 != null && data[i].Section1 != "undefined")
                        {
                            objtable.Section1 = data[0].Section1;
                            objtable.Section2 = data[0].Section2;
                            objtable.Section3 = data[0].Section3;
                            objtable.Section4 = data[0].Section4;
                            objtable.Section5 = data[0].Section5;
                            objtable.Section6 = data[0].Section6;
                            objtable.FooterSection1 = data[0].FooterSection1;
                            objtable.FooterSection2 = data[0].FooterSection2;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex) { }
            return View(objtable);
        }
        public ActionResult about_us()
        {
            HomePageContent objtable = new HomePageContent();
            try
            {
                UrbaEntityModel objentity = new UrbaEntityModel();
                var data = (from customer in objentity.HomePageContents.Where(y => y.PageName == "About Us").Take(10).OrderByDescending(x => x.ID)
                            select customer).ToList();
                if (data != null && data.Count > 0)
                {
                    for (int i = 0; i < data.Count; i++)
                    {
                        if (data[i].Section1 != null && data[i].Section1 != "undefined")
                        {
                            objtable.Section1 = data[0].Section1;
                            objtable.Section2 = data[0].Section2;
                            objtable.Section3 = data[0].Section3;
                            objtable.Section4 = data[0].Section4;
                            objtable.Section5 = data[0].Section5;
                            objtable.Section6 = data[0].Section6;
                            objtable.FooterSection1 = data[0].FooterSection1;
                            objtable.FooterSection2 = data[0].FooterSection2;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex) { }
            return View(objtable);
        }
        public ActionResult contact_us()
        {
            HomePageContent objtable = new HomePageContent();
            try
            {
                UrbaEntityModel objentity = new UrbaEntityModel();
                var data = (from customer in objentity.HomePageContents.Where(y => y.PageName == "Contact Us").Take(10).OrderByDescending(x => x.ID)
                            select customer).ToList();
                if (data != null && data.Count > 0)
                {
                    for (int i = 0; i < data.Count; i++)
                    {
                        if (data[i].Section1 != null && data[i].Section1 != "undefined")
                        {

                            objtable.Section1 = data[0].Section1;
                            objtable.Section2 = data[0].Section2;
                            objtable.Section3 = data[0].Section3;
                            //  objtable.Section4 = data[0].Section4;
                            // objtable.Section5 = data[0].Section5;
                            //    objtable.Section6 = data[0].Section6;
                            objtable.FooterSection1 = data[0].FooterSection1;
                            objtable.FooterSection2 = data[0].FooterSection2;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex) { }
            return View(objtable);
        }
        public ActionResult Blog()
        {
            HomePageContent objtable = new HomePageContent();
            try
            {
                UrbaEntityModel objentity = new UrbaEntityModel();
                var data = (from customer in objentity.HomePageContents.Where(y => y.PageName == "Blog").Take(10).OrderByDescending(x => x.ID)
                            select customer).ToList();
                if (data != null && data.Count > 0)
                {
                    for (int i = 0; i < data.Count; i++)
                    {
                        if (data[i].Section1 != null && data[i].Section1 != "undefined")
                        {

                            objtable.Section1 = data[i].Section1;
                            objtable.Section2 = data[i].Section2;
                            objtable.Section3 = data[i].Section3;
                            objtable.Section4 = data[i].Section4;
                            objtable.Section5 = data[i].Section5;
                            objtable.Section6 = data[i].Section6;
                            objtable.FooterSection1 = data[i].FooterSection1;
                            objtable.FooterSection2 = data[i].FooterSection2;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex) { }
            return View(objtable);
        }
        public ActionResult Blog_1()
        {
            HomePageContent objtable = new HomePageContent();
            try
            {
                UrbaEntityModel objentity = new UrbaEntityModel();
                var data = (from customer in objentity.HomePageContents.Where(y => y.PageName == "Blog1").Take(10).OrderByDescending(x => x.ID)
                            select customer).ToList();
                if (data != null && data.Count > 0)
                {
                    for (int i = 0; i < data.Count; i++)
                    {
                        if (data[i].Section1 != null && data[i].Section1 != "undefined")
                        {

                            objtable.Section1 = data[i].Section1;
                            objtable.Section2 = data[i].Section2;
                            objtable.Section3 = data[i].Section3;
                            objtable.Section4 = data[i].Section4;
                            objtable.Section5 = data[i].Section5;
                            objtable.Section6 = data[i].Section6;
                            objtable.FooterSection1 = data[i].FooterSection1;
                            objtable.FooterSection2 = data[i].FooterSection2;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex) { }
            return View(objtable);
        }
        public ActionResult Blog_2()
        {
            HomePageContent objtable = new HomePageContent();
            try
            {
                UrbaEntityModel objentity = new UrbaEntityModel();
                var data = (from customer in objentity.HomePageContents.Where(y => y.PageName == "Blog2").Take(10).OrderByDescending(x => x.ID)
                            select customer).ToList();
                if (data != null && data.Count > 0)
                {
                    for (int i = 0; i < data.Count; i++)
                    {
                        if (data[i].Section1 != null && data[i].Section1 != "undefined")
                        {

                            objtable.Section1 = data[i].Section1;
                            objtable.Section2 = data[i].Section2;
                            objtable.Section3 = data[i].Section3;
                            objtable.Section4 = data[i].Section4;
                            objtable.Section5 = data[i].Section5;
                            objtable.Section6 = data[i].Section6;
                            objtable.FooterSection1 = data[i].FooterSection1;
                            objtable.FooterSection2 = data[i].FooterSection2;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex) { }
            return View(objtable);
        }
        public ActionResult Blog_3()
        {
            HomePageContent objtable = new HomePageContent();
            try
            {
                UrbaEntityModel objentity = new UrbaEntityModel();
                var data = (from customer in objentity.HomePageContents.Where(y => y.PageName == "Blog3").Take(10).OrderByDescending(x => x.ID)
                            select customer).ToList();
                if (data != null && data.Count > 0)
                {
                    for (int i = 0; i < data.Count; i++)
                    {
                        if (data[i].Section1 != null && data[i].Section1 != "undefined")
                        {

                            objtable.Section1 = data[i].Section1;
                            objtable.Section2 = data[i].Section2;
                            objtable.Section3 = data[i].Section3;
                            objtable.Section4 = data[i].Section4;
                            objtable.Section5 = data[i].Section5;
                            objtable.Section6 = data[i].Section6;
                            objtable.FooterSection1 = data[i].FooterSection1;
                            objtable.FooterSection2 = data[i].FooterSection2;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex) { }
            return View(objtable);
        }
        public ActionResult Terms()
        {
            return View();
        }
        public ActionResult Privacy()
        {
            return View();
        }
        public ActionResult Testimonials()
        {
            HomePageContent objtable = new HomePageContent();
            try
            {
                UrbaEntityModel objentity = new UrbaEntityModel();
                var data = (from customer in objentity.HomePageContents.Where(y => y.PageName == "Testimonials").Take(10).OrderByDescending(x => x.ID)
                            select customer).ToList();
                if (data != null && data.Count > 0)
                {
                    for (int i = 0; i < data.Count; i++)
                    {
                        if (data[i].Section1 != null && data[i].Section1 != "undefined")
                        {

                            objtable.Section1 = data[i].Section1;
                            objtable.Section2 = data[i].Section2;
                            objtable.Section3 = data[i].Section3;
                            objtable.Section4 = data[i].Section4;
                            objtable.Section5 = data[i].Section5;
                            //    objtable.Section6 = data[0].Section6;
                            objtable.FooterSection1 = data[i].FooterSection1;
                            objtable.FooterSection2 = data[i].FooterSection2;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex) { }
            return View(objtable);
        }
        public ActionResult FAQ()
        {
            HomePageContent objtable = new HomePageContent();
            try
            {
                UrbaEntityModel objentity = new UrbaEntityModel();
                var data = (from customer in objentity.HomePageContents.Where(y => y.PageName == "FAQ").Take(10).OrderByDescending(x => x.ID)
                            select customer).ToList();
                if (data != null && data.Count > 0)
                {
                    for (int i = 0; i < data.Count; i++)
                    {
                        if (data[i].Section1 != null && data[i].Section1 != "undefined")
                        {

                            objtable.Section1 = data[i].Section1;
                            objtable.Section2 = data[i].Section2;
                            objtable.Section3 = data[i].Section3;
                            //  objtable.Section4 = data[0].Section4;
                            // objtable.Section5 = data[0].Section5;
                            //    objtable.Section6 = data[0].Section6;
                            objtable.FooterSection1 = data[i].FooterSection1;
                            objtable.FooterSection2 = data[i].FooterSection2;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex) { }
            return View(objtable);
        }
        // Send email functionality
        public JsonResult SendNewMessage()
        {
            bool res = false;
            string firstname = Request.Params["firstname"];
            string lastname = Request.Params["lastname"];
            string address = Request.Params["address"];
            string telephone = Request.Params["telephone"];
            string Message = Request.Params["Message"];
            string reasonForContacting = Request.Params["select"];
            if (reasonForContacting != "null")
            {
                int selectedOption = int.Parse(reasonForContacting);
                if (selectedOption == 00)
                { reasonForContacting = "Information";}
                else if (selectedOption == 01)
                {reasonForContacting = "Service";}
                else { reasonForContacting = "Phone"; }
            }
            String Body = "You have just recieved a contact us message from " + firstname + " " + lastname;
            Body = Body + "<br> Email address: " + address + "<br> Phone Number: " + (telephone != null ? telephone : "N/A") + "<br>" + (reasonForContacting != "null" ? "Purpose of contacting Urban Referral Network: " + reasonForContacting : "N/A") + "<br>Message: " + (Message != null ? Message : "N/A");
            res = SendMail(Body, address);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public bool SendMail(string Body, string email)
        {
            try
            { //public MailMessage(string from, string to, string subject, string body);
                // Mail Settings ---------------------------------------------------------------
                MailAddress to = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["AdminEmail"]);
                MailAddress from = new MailAddress(email);

                MailMessage message = new MailMessage(from, to);
                //var message = new MailMessage();
                //message.From = new MailAddress(email);
                message.From = from;
            
                //message.To.Add(new MailAddress(System.Configuration.ConfigurationManager.AppSettings["AdminEmail"]).ToString()); // replace with receiver's email id   

                message.Subject = "Message From Contact Us Form";
                message.Body = Body;
                message.IsBodyHtml = true;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.ReplyToList.Add(new MailAddress(from.ToString()));
                // SMTP Settings ---------------------------------------------------------------
                //SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["smtpHost"], int.Parse(ConfigurationManager.AppSettings["smtpPort"].ToString()));

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(new MailAddress(System.Configuration.ConfigurationManager.AppSettings["AdminEmail"]).ToString(), "Quranlover99");
                //client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["smtpUser"], ConfigurationManager.AppSettings["smtpPass"]);
                
                client.EnableSsl = true;
                client.Timeout = 100000;
                client.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //public async Task<ActionResult> contact_us(FormCollection form)
        //{
        //    return RedirectToAction("index");
        //}
        //private async Task<string> SendEmail(string name, string email, string messages, string phone)
        //{
        //    var message = new MailMessage();
        //    message.To.Add(new MailAddress("abc@xyz.com")); // replace with receiver's email id   
        //    message.From = new MailAddress("xyz@abc.com"); // replace with sender's email id   
        //    message.Subject = "Message From" + email;
        //    message.Body = "Name: " + name + "\nFrom: " + email + "\nPhone: " + phone + "\n" + messages;
        //    message.IsBodyHtml = true;
        //    using (var smtp = new SmtpClient())
        //    {
        //        var credential = new NetworkCredential
        //        {
        //            UserName = "xyz@abc.com", // replace with sender's email id   
        //            Password = "*****" // replace with password   
        //        };
        //        smtp.Credentials = credential;
        //        smtp.Host = "smtp-mail.outlook.com";
        //        smtp.Port = 587;
        //        smtp.EnableSsl = true;
        //        await smtp.SendMailAsync(message);
        //        return "sent";
        //    }
        //}
        public ActionResult how_it_work()
        {
            HomePageContent objtable = new HomePageContent();
            try
            {
                UrbaEntityModel objentity = new UrbaEntityModel();
                var data = (from customer in objentity.HomePageContents.Where(y => y.PageName == "How it Work").Take(10).OrderByDescending(x => x.ID)
                            select customer).ToList();
                if (data != null && data.Count > 0)
                {
                    for (int i = 0; i < data.Count; i++)
                    {
                        if (data[i].Section1 != null && data[i].Section1 != "undefined")
                        {
                            objtable.Section1 = data[i].Section1;
                            objtable.Section2 = data[i].Section2;
                            objtable.Section3 = data[i].Section3;
                            objtable.Section4 = data[i].Section4;
                            objtable.Section5 = data[i].Section5;
                            //    objtable.Section6 = data[0].Section6;
                            objtable.FooterSection1 = data[i].FooterSection1;
                            objtable.FooterSection2 = data[i].FooterSection2;
                            // return PartialView("_OurService", objtable);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex) { }
            return View(objtable);
        }
        public ActionResult our_services()
        {
            HomePageContent objtable = new HomePageContent();
            try
            {
                UrbaEntityModel objentity = new UrbaEntityModel();
                var data = (from customer in objentity.HomePageContents.Where(y => y.PageName == "Service").Take(10).OrderByDescending(x => x.ID)
                            select customer).ToList();
                if (data != null && data.Count > 0)
                {
                    for (int i = 0; i < data.Count; i++)
                    {
                        if (data[i].Section1 != null && data[i].Section1 != "undefined")
                        {
                            objtable.Section1 = data[i].Section1;
                            objtable.Section2 = data[i].Section2;
                            objtable.Section3 = data[i].Section3;
                            objtable.Section4 = data[i].Section4;
                            objtable.Section5 = data[i].Section5;
                            //    objtable.Section6 = data[0].Section6;
                            objtable.FooterSection1 = data[i].FooterSection1;
                            objtable.FooterSection2 = data[i].FooterSection2;
                            // return PartialView("_OurService", objtable);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex) { }
            return View(objtable);
        }
        //[HttpPost]
        //public ActionResult contact_us()
        //{

        //}

        //public ActionResult Login()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult Login(UserLogin login)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var username = ConfigurationManager.AppSettings["Username"];
        //        var password = ConfigurationManager.AppSettings["Password"];
        //        if (login.Username == username && login.Password == password)
        //        {
        //            Session["Username"] = username;
        //            return RedirectToAction("TestPage");
        //        }
        //    }
        //    return View();
        //}

        public ActionResult CMS()
        {
            //try
            //{
            //    if (Session["Username"] != null)
            //    {
            return View();
            //    }
            //}
            //catch (Exception ex) { }
            //return RedirectToAction("UserLogin", "Admin");
        }

        [HttpGet]
        public ActionResult GetWords(string selectedItem)
        {
            try
            {
                if (selectedItem == "1")
                {
                    UrbaEntityModel objentity = new UrbaEntityModel();
                    var data = (from customer in objentity.HomePageContents.Where(y => y.PageName == "Home Page").Take(10).OrderByDescending(x => x.ID)
                                select customer).ToList();
                    if (data != null && data.Count > 0)
                    {
                        for (int i = 0; i < data.Count; i++)
                        {
                            if (data[i].Section1 != null && data[i].Section1 != "undefined")
                            {
                                HomePageContent objtable = new HomePageContent();
                                objtable.Section1 = data[i].Section1;
                                objtable.Section2 = data[i].Section2;
                                objtable.Section3 = data[i].Section3;
                                objtable.Section4 = data[i].Section4;
                                objtable.Section5 = data[i].Section5;
                                objtable.Section6 = data[i].Section6;
                                objtable.FooterSection1 = data[i].FooterSection1;
                                objtable.FooterSection2 = data[i].FooterSection2;
                                return PartialView("_HomeTemplate", objtable);
                            }
                        }
                    }
                    return PartialView("_HomeTemplate");
                }
                else if (selectedItem == "2")
                {
                    UrbaEntityModel objentity = new UrbaEntityModel();
                    var data = (from customer in objentity.HomePageContents.Where(y => y.PageName == "About Us").Take(10).OrderByDescending(x => x.ID)
                                select customer).ToList();
                    if (data != null && data.Count > 0)
                    {
                        for (int i = 0; i < data.Count; i++)
                        {
                            if (data[i].Section1 != null && data[i].Section1 != "undefined")
                            {
                                HomePageContent objtable = new HomePageContent();
                                objtable.Section1 = data[i].Section1;
                                objtable.Section2 = data[i].Section2;
                                objtable.Section3 = data[i].Section3;
                                objtable.Section4 = data[i].Section4;
                                objtable.Section5 = data[i].Section5;
                                objtable.Section6 = data[i].Section6;
                                objtable.FooterSection1 = data[i].FooterSection1;
                                objtable.FooterSection2 = data[i].FooterSection2;
                                return PartialView("_About_UsTemplate", objtable);
                            }
                        }
                    }

                    return PartialView("_About_UsTemplate");
                }
                else if (selectedItem == "3")
                {
                    UrbaEntityModel objentity = new UrbaEntityModel();
                    var data = (from customer in objentity.HomePageContents.Where(y => y.PageName == "Service").Take(10).OrderByDescending(x => x.ID)
                                select customer).ToList();
                    if (data != null && data.Count > 0)
                    {
                        for (int i = 0; i < data.Count; i++)
                        {
                            if (data[i].Section1 != null && data[i].Section1 != "undefined")
                            {
                                HomePageContent objtable = new HomePageContent();
                                objtable.Section1 = data[i].Section1;
                                objtable.Section2 = data[i].Section2;
                                objtable.Section3 = data[i].Section3;
                                objtable.Section4 = data[i].Section4;
                                objtable.Section5 = data[i].Section5;
                                //    objtable.Section6 = data[0].Section6;
                                objtable.FooterSection1 = data[i].FooterSection1;
                                objtable.FooterSection2 = data[i].FooterSection2;
                                return PartialView("_OurService", objtable);
                            }
                        }
                    }
                    return PartialView("_OurService");
                }
                else if (selectedItem == "4")
                {
                    UrbaEntityModel objentity = new UrbaEntityModel();
                    var data = (from customer in objentity.HomePageContents.Where(y => y.PageName == "How it Work").Take(10).OrderByDescending(x => x.ID)
                                select customer).ToList();
                    if (data != null && data.Count > 0)
                    {
                        for (int i = 0; i < data.Count; i++)
                        {
                            if (data[i].Section1 != null && data[i].Section1 != "undefined")
                            {
                                HomePageContent objtable = new HomePageContent();
                                objtable.Section1 = data[i].Section1;
                                objtable.Section2 = data[i].Section2;
                                objtable.Section3 = data[i].Section3;
                                objtable.Section4 = data[i].Section4;
                                objtable.Section5 = data[i].Section5;
                                //    objtable.Section6 = data[0].Section6;
                                objtable.FooterSection1 = data[i].FooterSection1;
                                objtable.FooterSection2 = data[i].FooterSection2;
                                return PartialView("_How_itwork", objtable);
                            }
                        }
                    }
                    return PartialView("_How_itwork");
                }
                else if (selectedItem == "5")
                {
                    UrbaEntityModel objentity = new UrbaEntityModel();
                    var data = (from customer in objentity.HomePageContents.Where(y => y.PageName == "FAQ").Take(10).OrderByDescending(x => x.ID)
                                select customer).ToList();
                    if (data != null && data.Count > 0)
                    {
                        for (int i = 0; i < data.Count; i++)
                        {
                            if (data[i].Section1 != null && data[i].Section1 != "undefined")
                            {
                                HomePageContent objtable = new HomePageContent();
                                objtable.Section1 = data[i].Section1;
                                objtable.Section2 = data[i].Section2;
                                objtable.Section3 = data[i].Section3;
                                //  objtable.Section4 = data[0].Section4;
                                // objtable.Section5 = data[0].Section5;
                                //    objtable.Section6 = data[0].Section6;
                                objtable.FooterSection1 = data[i].FooterSection1;
                                objtable.FooterSection2 = data[i].FooterSection2;
                                return PartialView("_FAQ", objtable);
                            }
                        }
                    }
                    return PartialView("_FAQ");
                }
                else if (selectedItem == "6")
                {
                    UrbaEntityModel objentity = new UrbaEntityModel();
                    var data = (from customer in objentity.HomePageContents.Where(y => y.PageName == "Contact Us").Take(10).OrderByDescending(x => x.ID)
                                select customer).ToList();
                    if (data != null && data.Count > 0)
                    {
                        for (int i = 0; i < data.Count; i++)
                        {
                            if (data[i].Section1 != null && data[i].Section1 != "undefined")
                            {
                                HomePageContent objtable = new HomePageContent();
                                objtable.Section1 = data[i].Section1;
                                objtable.Section2 = data[i].Section2;
                                objtable.Section3 = data[i].Section3;
                                //  objtable.Section4 = data[0].Section4;
                                // objtable.Section5 = data[0].Section5;
                                //    objtable.Section6 = data[0].Section6;
                                objtable.FooterSection1 = data[i].FooterSection1;
                                objtable.FooterSection2 = data[i].FooterSection2;
                                return PartialView("_ContactUs", objtable);
                            }
                        }
                    }
                    return PartialView("_ContactUs");
                }
                else if (selectedItem == "7")
                {
                    UrbaEntityModel objentity = new UrbaEntityModel();
                    var data = (from customer in objentity.HomePageContents.Where(y => y.PageName == "Blog").Take(10).OrderByDescending(x => x.ID)
                                select customer).ToList();
                    if (data != null && data.Count > 0)
                    {
                        for (int i = 0; i < data.Count; i++)
                        {
                            if (data[i].Section1 != null && data[i].Section1 != "undefined")
                            {
                                HomePageContent objtable = new HomePageContent();
                                objtable.Section1 = data[i].Section1;
                                objtable.Section2 = data[i].Section2;
                                objtable.Section3 = data[i].Section3;
                                objtable.Section4 = data[i].Section4;
                                objtable.Section5 = data[i].Section5;
                                objtable.Section6 = data[i].Section6;
                                objtable.FooterSection1 = data[i].FooterSection1;
                                objtable.FooterSection2 = data[i].FooterSection2;
                                return PartialView("_Blog", objtable);
                            }
                        }
                    }
                    return PartialView("_Blog");
                }
                else if (selectedItem == "8")
                {
                    UrbaEntityModel objentity = new UrbaEntityModel();
                    var data = (from customer in objentity.HomePageContents.Where(y => y.PageName == "Blog1").Take(10).OrderByDescending(x => x.ID)
                                select customer).ToList();
                    if (data != null && data.Count > 0)
                    {
                        for (int i = 0; i < data.Count; i++)
                        {
                            if (data[i].Section1 != null && data[i].Section1 != "undefined")
                            {
                                HomePageContent objtable = new HomePageContent();
                                objtable.Section1 = data[i].Section1;
                                objtable.Section2 = data[i].Section2;
                                objtable.Section3 = data[i].Section3;
                                objtable.Section4 = data[i].Section4;
                                objtable.Section5 = data[i].Section5;
                                objtable.Section6 = data[i].Section6;
                                objtable.FooterSection1 = data[i].FooterSection1;
                                objtable.FooterSection2 = data[i].FooterSection2;
                                return PartialView("_Blog1", objtable);
                            }
                        }
                    }
                    return PartialView("_Blog1");
                }
                else if (selectedItem == "9")
                {
                    UrbaEntityModel objentity = new UrbaEntityModel();
                    var data = (from customer in objentity.HomePageContents.Where(y => y.PageName == "Blog2").Take(10).OrderByDescending(x => x.ID)
                                select customer).ToList();
                    if (data != null && data.Count > 0)
                    {
                        for (int i = 0; i < data.Count; i++)
                        {
                            if (data[i].Section1 != null && data[i].Section1 != "undefined")
                            {
                                HomePageContent objtable = new HomePageContent();
                                objtable.Section1 = data[i].Section1;
                                objtable.Section2 = data[i].Section2;
                                objtable.Section3 = data[i].Section3;
                                objtable.Section4 = data[i].Section4;
                                objtable.Section5 = data[i].Section5;
                                objtable.Section6 = data[i].Section6;
                                objtable.FooterSection1 = data[i].FooterSection1;
                                objtable.FooterSection2 = data[i].FooterSection2;
                                return PartialView("_Blog2", objtable);
                            }
                        }
                    }
                    return PartialView("_Blog2");
                }
                else if (selectedItem == "10")
                {
                    UrbaEntityModel objentity = new UrbaEntityModel();
                    var data = (from customer in objentity.HomePageContents.Where(y => y.PageName == "Blog3").Take(10).OrderByDescending(x => x.ID)
                                select customer).ToList();
                    if (data != null && data.Count > 0)
                    {
                        for (int i = 0; i < data.Count; i++)
                        {
                            if (data[i].Section1 != null && data[i].Section1 != "undefined")
                            {
                                HomePageContent objtable = new HomePageContent();
                                objtable.Section1 = data[i].Section1;
                                objtable.Section2 = data[i].Section2;
                                objtable.Section3 = data[i].Section3;
                                objtable.Section4 = data[i].Section4;
                                objtable.Section5 = data[i].Section5;
                                objtable.Section6 = data[i].Section6;
                                objtable.FooterSection1 = data[i].FooterSection1;
                                objtable.FooterSection2 = data[i].FooterSection2;
                                return PartialView("_Blog3", objtable);
                            }
                        }
                    }
                    return PartialView("_Blog3");
                }
                else if (selectedItem == "11")
                {
                    UrbaEntityModel objentity = new UrbaEntityModel();
                    var data = (from customer in objentity.HomePageContents.Where(y => y.PageName == "Testimonials").Take(10).OrderByDescending(x => x.ID)
                                select customer).ToList();
                    if (data != null && data.Count > 0)
                    {
                        for (int i = 0; i < data.Count; i++)
                        {
                            if (data[i].Section1 != null && data[i].Section1 != "undefined")
                            {
                                HomePageContent objtable = new HomePageContent();
                                objtable.Section1 = data[i].Section1;
                                objtable.Section2 = data[i].Section2;
                                objtable.Section3 = data[i].Section3;
                                objtable.Section4 = data[i].Section4;
                                objtable.Section5 = data[i].Section5;
                                objtable.Section6 = data[i].Section6;
                                objtable.FooterSection1 = data[i].FooterSection1;
                                objtable.FooterSection2 = data[i].FooterSection2;
                                return PartialView("_Testimonials", objtable);
                            }
                        }
                    }
                    return PartialView("_Testimonials");
                }
            }
            catch (Exception ex) { }
            return RedirectToAction("TestPage", "HTML");
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateInput(false)]
        public ActionResult SaveRecord(string text, string text4, string text5)
        {
            string output = string.Empty;
            try
            {
                HomePageContent objtable = new HomePageContent();
                objtable.Section1 = text;
                objtable.Section5 = text4;
                objtable.Section6 = text5;
                objtable.PageName = "Home Page";
                UrbaEntityModel objentity = new UrbaEntityModel();
                objentity.HomePageContents.Add(objtable);
                objentity.SaveChanges();
                int id = objtable.ID;
                output = id.ToString();
            }
            catch (Exception ex) { }

            return Json(output, JsonRequestBehavior.AllowGet);
            //   string output = "Success";

            //  return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateInput(false)]
        public ActionResult UpdateRecord(string text, string val, string text1)
        {
            string output = string.Empty;
            try
            {
                using (var db = new UrbaEntityModel())
                {
                    HomePageContent objtable = new HomePageContent();
                    objtable.ID = Convert.ToInt32(text1);
                    if (val == "1")
                    {
                        objtable.Section2 = text;
                        db.HomePageContents.Attach(objtable);
                        db.Entry(objtable).Property(x => x.Section2).IsModified = true;
                    }
                    if (val == "2")
                    {
                        objtable.Section3 = text;
                        db.HomePageContents.Attach(objtable);
                        db.Entry(objtable).Property(x => x.Section3).IsModified = true;
                    }
                    if (val == "3")
                    {
                        objtable.Section4 = text;
                        db.HomePageContents.Attach(objtable);
                        db.Entry(objtable).Property(x => x.Section4).IsModified = true;
                    }
                    if (val == "4")
                    {
                        objtable.FooterSection1 = text;
                        db.HomePageContents.Attach(objtable);
                        db.Entry(objtable).Property(x => x.FooterSection1).IsModified = true;
                    }
                    if (val == "5")
                    {
                        objtable.FooterSection2 = text;
                        db.HomePageContents.Attach(objtable);
                        db.Entry(objtable).Property(x => x.FooterSection2).IsModified = true;
                    }
                    db.SaveChanges();
                }
                output = "Success";
            }
            catch (Exception ex) { }
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateInput(false)]
        public ActionResult SaveAboutUs(string text, string text4, string text5)
        {
            string output = string.Empty;
            try
            {
                HomePageContent objtable = new HomePageContent();
                objtable.Section1 = text;
                objtable.Section3 = text4;
                objtable.Section4 = text5;
                objtable.PageName = "About Us";
                UrbaEntityModel objentity = new UrbaEntityModel();
                objentity.HomePageContents.Add(objtable);
                objentity.SaveChanges();
                int id = objtable.ID;

                output = id.ToString();
            }
            catch (Exception ex) { }
            return Json(output, JsonRequestBehavior.AllowGet);
            //   string output = "Success";

            //  return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateInput(false)]
        public ActionResult UpdateAboutUsRecord(string text, string val, string text1)
        {
            string output = string.Empty;
            try
            {
                using (var db = new UrbaEntityModel())
                {
                    HomePageContent objtable = new HomePageContent();
                    objtable.ID = Convert.ToInt32(text1);
                    if (val == "1")
                    {
                        objtable.Section2 = text;
                        db.HomePageContents.Attach(objtable);
                        db.Entry(objtable).Property(x => x.Section2).IsModified = true;
                    }
                    if (val == "2")
                    {
                        objtable.Section5 = text;
                        db.HomePageContents.Attach(objtable);
                        db.Entry(objtable).Property(x => x.Section5).IsModified = true;
                    }
                    if (val == "3")
                    {
                        objtable.FooterSection1 = text;
                        db.HomePageContents.Attach(objtable);
                        db.Entry(objtable).Property(x => x.FooterSection1).IsModified = true;
                    }
                    if (val == "4")
                    {
                        objtable.FooterSection2 = text;
                        db.HomePageContents.Attach(objtable);
                        db.Entry(objtable).Property(x => x.FooterSection2).IsModified = true;
                    }

                    db.SaveChanges();
                }
                output = "Success";
            }
            catch (Exception ex) { }
            // ViewBag.HTMLData = HttpUtility.HtmlEncode(htmlString);
            // return View();
            return Json(output, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateInput(false)]
        public ActionResult SaveService(string text, string text4, string text5)
        {
            string output = string.Empty;
            try
            {
                HomePageContent objtable = new HomePageContent();
                objtable.Section1 = text;
                objtable.Section3 = text4;
                objtable.Section4 = text5;
                objtable.PageName = "Service";
                UrbaEntityModel objentity = new UrbaEntityModel();
                objentity.HomePageContents.Add(objtable);
                objentity.SaveChanges();
                int id = objtable.ID;

                output = id.ToString();
            }
            catch (Exception ex) { }
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateInput(false)]
        public ActionResult UpdateServiceRecord(string text, string val, string text1)
        {
            string output = string.Empty;
            try
            {
                using (var db = new UrbaEntityModel())
                {
                    HomePageContent objtable = new HomePageContent();
                    objtable.ID = Convert.ToInt32(text1);
                    if (val == "1")
                    {
                        objtable.Section2 = text;
                        db.HomePageContents.Attach(objtable);
                        db.Entry(objtable).Property(x => x.Section2).IsModified = true;
                    }
                    if (val == "2")
                    {
                        objtable.Section5 = text;
                        db.HomePageContents.Attach(objtable);
                        db.Entry(objtable).Property(x => x.Section5).IsModified = true;
                    }
                    if (val == "3")
                    {
                        objtable.FooterSection1 = text;
                        db.HomePageContents.Attach(objtable);
                        db.Entry(objtable).Property(x => x.FooterSection1).IsModified = true;
                    }
                    if (val == "4")
                    {
                        objtable.FooterSection2 = text;
                        db.HomePageContents.Attach(objtable);
                        db.Entry(objtable).Property(x => x.FooterSection2).IsModified = true;
                    }

                    db.SaveChanges();
                }
                output = "Success";
            }
            catch (Exception ex) { }
            // ViewBag.HTMLData = HttpUtility.HtmlEncode(htmlString);
            // return View();
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateInput(false)]
        public ActionResult SaveHowToWork(string text, string text4, string text5)
        {
            string output = string.Empty;
            try
            {
                HomePageContent objtable = new HomePageContent();
                objtable.Section1 = text;
                objtable.Section3 = text4;
                objtable.Section4 = text5;
                objtable.PageName = "How it Work";
                UrbaEntityModel objentity = new UrbaEntityModel();
                objentity.HomePageContents.Add(objtable);
                objentity.SaveChanges();
                int id = objtable.ID;
                output = id.ToString();
            }
            catch (Exception ex) { }
            return Json(output, JsonRequestBehavior.AllowGet);
            //   string output = "Success";

            //  return Json(output, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateInput(false)]
        public ActionResult UpdateHowToWorkRecord(string text, string val, string text1)
        {
            string output = string.Empty;
            try
            {
                using (var db = new UrbaEntityModel())
                {
                    HomePageContent objtable = new HomePageContent();
                    objtable.ID = Convert.ToInt32(text1);
                    if (val == "1")
                    {
                        objtable.Section2 = text;
                        db.HomePageContents.Attach(objtable);
                        db.Entry(objtable).Property(x => x.Section2).IsModified = true;
                    }
                    if (val == "2")
                    {
                        objtable.Section5 = text;
                        db.HomePageContents.Attach(objtable);
                        db.Entry(objtable).Property(x => x.Section5).IsModified = true;
                    }
                    if (val == "3")
                    {
                        objtable.FooterSection1 = text;
                        db.HomePageContents.Attach(objtable);
                        db.Entry(objtable).Property(x => x.FooterSection1).IsModified = true;
                    }
                    if (val == "4")
                    {
                        objtable.FooterSection2 = text;
                        db.HomePageContents.Attach(objtable);
                        db.Entry(objtable).Property(x => x.FooterSection2).IsModified = true;
                    }

                    db.SaveChanges();
                }
                output = "Success";
            }
            catch (Exception ex) { }
            // ViewBag.HTMLData = HttpUtility.HtmlEncode(htmlString);
            // return View();
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateInput(false)]
        public ActionResult SaveFAQ(string text, string text4)
        {
            string output = string.Empty;
            try
            {
                HomePageContent objtable = new HomePageContent();
                objtable.Section1 = text;
                objtable.Section2 = text4;
                //  objtable.Section4 = text5;
                objtable.PageName = "FAQ";
                UrbaEntityModel objentity = new UrbaEntityModel();
                objentity.HomePageContents.Add(objtable);
                objentity.SaveChanges();
                int id = objtable.ID;
                output = id.ToString();
            }
            catch (Exception ex) { }
            return Json(output, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateInput(false)]
        public ActionResult UpdateFAQRecord(string text, string val, string text1)
        {
            string output = string.Empty;
            try
            {
                using (var db = new UrbaEntityModel())
                {
                    HomePageContent objtable = new HomePageContent();
                    objtable.ID = Convert.ToInt32(text1);
                    if (val == "1")
                    {
                        objtable.Section3 = text;
                        db.HomePageContents.Attach(objtable);
                        db.Entry(objtable).Property(x => x.Section3).IsModified = true;
                    }
                    if (val == "2")
                    {
                        objtable.FooterSection1 = text;
                        db.HomePageContents.Attach(objtable);
                        db.Entry(objtable).Property(x => x.FooterSection1).IsModified = true;
                    }
                    if (val == "3")
                    {
                        objtable.FooterSection2 = text;
                        db.HomePageContents.Attach(objtable);
                        db.Entry(objtable).Property(x => x.FooterSection2).IsModified = true;
                    }
                    db.SaveChanges();
                }
                // ViewBag.HTMLData = HttpUtility.HtmlEncode(htmlString);
                // return View();
                output = "Success";
            }
            catch (Exception ex) { }
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateInput(false)]
        public ActionResult SaveContactUs(string text, string text4)
        {
            string output = string.Empty;
            try
            {
                HomePageContent objtable = new HomePageContent();
                objtable.Section1 = text;
                objtable.Section2 = text4;
                //  objtable.Section4 = text5;
                objtable.PageName = "Contact Us";
                UrbaEntityModel objentity = new UrbaEntityModel();
                objentity.HomePageContents.Add(objtable);
                objentity.SaveChanges();
                int id = objtable.ID;
                output = id.ToString();
            }
            catch (Exception ex) { }
            return Json(output, JsonRequestBehavior.AllowGet);
            //   string output = "Success";

            //  return Json(output, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateInput(false)]
        public ActionResult UpdateContactUsRecord(string text, string val, string text1)
        {
            string output = string.Empty;
            try
            {
                using (var db = new UrbaEntityModel())
                {
                    HomePageContent objtable = new HomePageContent();
                    objtable.ID = Convert.ToInt32(text1);
                    if (val == "1")
                    {
                        objtable.Section3 = text;
                        db.HomePageContents.Attach(objtable);
                        db.Entry(objtable).Property(x => x.Section3).IsModified = true;
                    }
                    if (val == "2")
                    {
                        objtable.FooterSection1 = text;
                        db.HomePageContents.Attach(objtable);
                        db.Entry(objtable).Property(x => x.FooterSection1).IsModified = true;
                    }
                    if (val == "3")
                    {
                        objtable.FooterSection2 = text;
                        db.HomePageContents.Attach(objtable);
                        db.Entry(objtable).Property(x => x.FooterSection2).IsModified = true;
                    }
                    db.SaveChanges();
                }
                output = "Success";
            }
            catch (Exception ex) { }
            return Json(output, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        [AllowAnonymous]
        [ValidateInput(false)]
        public ActionResult SaveBlog(string text, string text1, string text2)
        {
            string output = string.Empty;
            try
            {
                HomePageContent objtable = new HomePageContent();
                objtable.Section1 = text;
                objtable.Section2 = text1;
                objtable.Section3 = text2;
                objtable.PageName = "Blog";
                UrbaEntityModel objentity = new UrbaEntityModel();
                objentity.HomePageContents.Add(objtable);
                objentity.SaveChanges();
                int id = objtable.ID;

                output = id.ToString();
            }
            catch (Exception ex) { }
            return Json(output, JsonRequestBehavior.AllowGet);
            //   string output = "Success";

            //  return Json(output, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateInput(false)]
        public ActionResult UpdateBlogRecord(string text, string val, string text1)
        {
            string output = string.Empty;
            try
            {
                using (var db = new UrbaEntityModel())
                {
                    HomePageContent objtable = new HomePageContent();
                    objtable.ID = Convert.ToInt32(text1);
                    if (val == "1")
                    {
                        objtable.Section4 = text;
                        db.HomePageContents.Attach(objtable);
                        db.Entry(objtable).Property(x => x.Section4).IsModified = true;
                    }
                    if (val == "2")
                    {
                        objtable.Section5 = text;
                        db.HomePageContents.Attach(objtable);
                        db.Entry(objtable).Property(x => x.Section5).IsModified = true;
                    }
                    if (val == "3")
                    {
                        objtable.Section6 = text;
                        db.HomePageContents.Attach(objtable);
                        db.Entry(objtable).Property(x => x.Section6).IsModified = true;
                    }
                    if (val == "4")
                    {
                        objtable.FooterSection1 = text;
                        db.HomePageContents.Attach(objtable);
                        db.Entry(objtable).Property(x => x.FooterSection1).IsModified = true;
                    }
                    if (val == "5")
                    {
                        objtable.FooterSection2 = text;
                        db.HomePageContents.Attach(objtable);
                        db.Entry(objtable).Property(x => x.FooterSection2).IsModified = true;
                    }
                    db.SaveChanges();
                }

                // ViewBag.HTMLData = HttpUtility.HtmlEncode(htmlString);
                // return View();
                output = "Success";
            }
            catch (Exception ex) { }
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateInput(false)]
        public ActionResult SaveBlog1(string text, string text1, string text2)
        {
            string output = string.Empty;
            try
            {
                HomePageContent objtable = new HomePageContent();
                objtable.Section1 = text;
                objtable.Section2 = text1;
                objtable.Section3 = text2;
                objtable.PageName = "Blog1";
                UrbaEntityModel objentity = new UrbaEntityModel();
                objentity.HomePageContents.Add(objtable);
                objentity.SaveChanges();
                int id = objtable.ID;

                output = id.ToString();
            }
            catch (Exception ex) { }
            return Json(output, JsonRequestBehavior.AllowGet);
            //   string output = "Success";

            //  return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateInput(false)]
        public ActionResult UpdateBlog1Record(string text, string val, string text1)
        {
            string output = string.Empty;
            try
            {
                using (var db = new UrbaEntityModel())
                {
                    HomePageContent objtable = new HomePageContent();
                    objtable.ID = Convert.ToInt32(text1);
                    if (val == "1")
                    {
                        objtable.Section4 = text;
                        db.HomePageContents.Attach(objtable);
                        db.Entry(objtable).Property(x => x.Section4).IsModified = true;
                    }
                    if (val == "2")
                    {
                        objtable.FooterSection1 = text;
                        db.HomePageContents.Attach(objtable);
                        db.Entry(objtable).Property(x => x.FooterSection1).IsModified = true;
                    }
                    if (val == "3")
                    {
                        objtable.FooterSection2 = text;
                        db.HomePageContents.Attach(objtable);
                        db.Entry(objtable).Property(x => x.FooterSection2).IsModified = true;
                    }

                    db.SaveChanges();
                }
                // ViewBag.HTMLData = HttpUtility.HtmlEncode(htmlString);
                // return View();
                output = "Success";
            }
            catch (Exception ex) { }
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateInput(false)]
        public ActionResult SaveBlog2(string text, string text1, string text2)
        {
            string output = string.Empty;
            try
            {
                HomePageContent objtable = new HomePageContent();
                objtable.Section1 = text;
                objtable.Section2 = text1;
                objtable.Section3 = text2;
                objtable.PageName = "Blog2";
                UrbaEntityModel objentity = new UrbaEntityModel();
                objentity.HomePageContents.Add(objtable);
                objentity.SaveChanges();
                int id = objtable.ID;
                output = id.ToString();
            }
            catch (Exception ex) { }
            return Json(output, JsonRequestBehavior.AllowGet);
            //   string output = "Success";

            //  return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateInput(false)]
        public ActionResult UpdateBlog2Record(string text, string val, string text1)
        {
            string output = string.Empty;
            try
            {
                using (var db = new UrbaEntityModel())
                {
                    HomePageContent objtable = new HomePageContent();
                    objtable.ID = Convert.ToInt32(text1);
                    if (val == "1")
                    {
                        objtable.Section4 = text;
                        db.HomePageContents.Attach(objtable);
                        db.Entry(objtable).Property(x => x.Section4).IsModified = true;
                    }
                    if (val == "2")
                    {
                        objtable.FooterSection1 = text;
                        db.HomePageContents.Attach(objtable);
                        db.Entry(objtable).Property(x => x.FooterSection1).IsModified = true;
                    }
                    if (val == "3")
                    {
                        objtable.FooterSection2 = text;
                        db.HomePageContents.Attach(objtable);
                        db.Entry(objtable).Property(x => x.FooterSection2).IsModified = true;
                    }

                    db.SaveChanges();
                }
                output = "Success";
            }
            catch (Exception ex) { }
            return Json(output, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateInput(false)]
        public ActionResult SaveBlog3(string text, string text1, string text2)
        {
            string output = string.Empty;
            try
            {
                HomePageContent objtable = new HomePageContent();
                objtable.Section1 = text;
                objtable.Section2 = text1;
                objtable.Section3 = text2;
                objtable.PageName = "Blog3";
                UrbaEntityModel objentity = new UrbaEntityModel();
                objentity.HomePageContents.Add(objtable);
                objentity.SaveChanges();
                int id = objtable.ID;

                output = id.ToString();
            }
            catch (Exception ex) { }
            return Json(output, JsonRequestBehavior.AllowGet);
            //   string output = "Success";

            //  return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateInput(false)]
        public ActionResult UpdateBlog3Record(string text, string val, string text1)
        {
            string output = string.Empty;
            try
            {
                using (var db = new UrbaEntityModel())
                {
                    HomePageContent objtable = new HomePageContent();
                    objtable.ID = Convert.ToInt32(text1);
                    if (val == "1")
                    {
                        objtable.Section4 = text;
                        db.HomePageContents.Attach(objtable);
                        db.Entry(objtable).Property(x => x.Section4).IsModified = true;
                    }
                    if (val == "2")
                    {
                        objtable.FooterSection1 = text;
                        db.HomePageContents.Attach(objtable);
                        db.Entry(objtable).Property(x => x.FooterSection1).IsModified = true;
                    }
                    if (val == "3")
                    {
                        objtable.FooterSection2 = text;
                        db.HomePageContents.Attach(objtable);
                        db.Entry(objtable).Property(x => x.FooterSection2).IsModified = true;
                    }

                    db.SaveChanges();
                }
                // ViewBag.HTMLData = HttpUtility.HtmlEncode(htmlString);
                // return View();
                output = "Success";
            }
            catch (Exception ex) { }
            return Json(output, JsonRequestBehavior.AllowGet);
        }




        [HttpPost]
        [AllowAnonymous]
        [ValidateInput(false)]
        public ActionResult SaveTestimonials(string text, string text1, string text2)
        {
            string output = string.Empty;
            try
            {
                HomePageContent objtable = new HomePageContent();
                objtable.Section1 = text;
                objtable.Section2 = text1;
                objtable.Section3 = text2;
                objtable.PageName = "Testimonials";
                UrbaEntityModel objentity = new UrbaEntityModel();
                objentity.HomePageContents.Add(objtable);
                objentity.SaveChanges();
                int id = objtable.ID;

                output = id.ToString();
            }
            catch (Exception ex) { }
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateInput(false)]
        public ActionResult UpdateTestimonialsRecord(string text, string val, string text1)
        {
            string output = string.Empty;
            try
            {
                using (var db = new UrbaEntityModel())
                {
                    HomePageContent objtable = new HomePageContent();
                    objtable.ID = Convert.ToInt32(text1);
                    if (val == "1")
                    {
                        objtable.Section4 = text;
                        db.HomePageContents.Attach(objtable);
                        db.Entry(objtable).Property(x => x.Section4).IsModified = true;
                    }
                    if (val == "2")
                    {
                        objtable.FooterSection1 = text;
                        db.HomePageContents.Attach(objtable);
                        db.Entry(objtable).Property(x => x.FooterSection1).IsModified = true;
                    }
                    if (val == "3")
                    {
                        objtable.FooterSection2 = text;
                        db.HomePageContents.Attach(objtable);
                        db.Entry(objtable).Property(x => x.FooterSection2).IsModified = true;
                    }
                    if (val == "4")
                    {
                        objtable.Section5 = text;
                        db.HomePageContents.Attach(objtable);
                        db.Entry(objtable).Property(x => x.Section5).IsModified = true;
                    }
                    db.SaveChanges();
                }
                output = "Success";
            }
            catch (Exception ex) { }
            return Json(output, JsonRequestBehavior.AllowGet);
        }
    }
}