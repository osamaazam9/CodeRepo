using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace SO.Urba.Models.ValueObjects
{
    public enum NotificationStatus
    {
        None,
        Sent,
        Delivered,
        Confirmed,
    }


    [Table("Referral", Schema = "data")]
    [Serializable]
    public class ReferralVo
    {
        [DisplayName("referral Id")]
        [Required]
        [Key]
        public int referralId { get; set; }

        [DisplayName("client Id")]
        [Required]
        public int clientId { get; set; }

        [DisplayName("company Id")]
        [Required]
        public int companyId { get; set; }

        [DisplayName("company Category Type Id")]
        [Required]
        public int companyCategoryTypeId { get; set; }

        [DisplayName("Referral Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<DateTime> referralDate { get; set; }

        [DisplayName("Quote ($)")]
        [Range(0.00, 99999999999999, ErrorMessage = "Quote can not  be more then 11 characters or negative")]
        [RegularExpression(@"^\d+(\.\d{0,2}0*)?$", ErrorMessage = "Quote can not have more than 2 decimal places")]
        public Nullable<decimal> quote { get; set; }

        [DisplayName("NotificationStatus")]
        public NotificationStatus notificationStatus { get; set; }

        [DisplayName("Accepted")]
        public bool accepted { get; set; }

        [DisplayName("Final Quote ($)")]
        [Range(0.00, 99999999999999, ErrorMessage = "Final Quote is too big or negative")]
        [RegularExpression(@"^\d+(\.\d{0,2}0*)?$", ErrorMessage = "Final Quote can not have more than 2 decimal places")]
        public Nullable<decimal> finalQuote { get; set; }

        [DisplayName("Finish Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<DateTime> finishDate { get; set; }

        [DisplayName("Commission Amount ($)")]
        [Range(0.00, 99999999999999, ErrorMessage = "Final Quote is too big or negative")]
        [RegularExpression(@"^\d+(\.\d{0,2}0*)?$", ErrorMessage = "Final Quote can not have more than 2 decimal places")]
        public Nullable<decimal> commissionAmount { get; set; }

        [DisplayName("Commission Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<DateTime> commissionDatePaid { get; set; }

        [DisplayName("Description")]
        public string description { get; set; }

        [DisplayName("Survey Comment")]
        public string surveyComment { get; set; }

        [DisplayName("created")]
        [Required]
        public DateTime created { get; set; }

        [DisplayName("modified")]
        [Required]
        public DateTime modified { get; set; }

        [DisplayName("created By")]
        public Nullable<int> createdBy { get; set; }

        [DisplayName("modified By")]
        public Nullable<int> modifiedBy { get; set; }

        [DisplayName("is Active")]
        [Required]
        public bool isActive { get; set; }

        // 1 - Add single referral
        // 2 - Add Triple referral
        // 3 - Edit existing referral
        [NotMapped]
        public int dlgType { get; set; }

        [ForeignKey("companyCategoryTypeId")]
        public CompanyCategoryTypeVo companyCategoryType { get; set; }

        [ForeignKey("companyId")]
        public CompanyVo company { get; set; }

        [ForeignKey("clientId")]
        public ClientVo client { get; set; }

        [Association("Referral_SurveyAnswer", "referralId", "referralId")]
        public List<SurveyAnswerVo> surveyAnswerses { get; set; }

        public ReferralVo()
        {
            this.notificationStatus = NotificationStatus.None;
            this.isActive = true;
        }

    }

}
