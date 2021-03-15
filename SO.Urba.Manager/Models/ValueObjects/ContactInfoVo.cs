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
    public enum ContactMethod
    {
        Email,
        SMS,
    }

    [Table("ContactInfo", Schema = "data")]
    [Serializable]
    public class ContactInfoVo
    {


        [DisplayName("contact Info Id")]
        [Required]
        [Key]
        public int contactInfoId { get; set; }

        [DisplayName("First Name")]
        [Required]
        [StringLength(50)]
        public string firstName { get; set; }

        [DisplayName("Last Name")]
        [StringLength(50)]
        public string lastName { get; set; }

        [DisplayName("Address")]
        [StringLength(50)]
        public string address { get; set; }

        [DisplayName("City")]
        [StringLength(50)]
        public string city { get; set; }

        [DisplayName("State")]
        [StringLength(2, MinimumLength = 2)]
        [RegularExpression(@"^(?:(A[KLRZklrz]|a[KLRZklrz]|C[AOTaot]|c[AOTaot]|D[CEce]|d[ceCE]|FL|fl|Fl|fL|GA|ga|Ga|gA|HI|hi|Hi|hI|I[ADLNadln]|i[adlnADLN]|K[SYsy]|k[sySY]|LA|la|La|lA|M[ADEINOSTadeinost]|m[ADEINOSTadeinost]|N[CDEHJMVYcdehjmvy]|n[CDEHJMVYcdehjmvy]|O[HKRhkr]|o[HKRhkr]|P[ARar]|p[ARar]|RI|ri|Ri|rI|S[CDcd]|s[CDcd]|T[NXnx]|t[NXnx]|UT|ut|Ut|uT|V[AITait]|v[AITait]|W[AIVYaivy]|w[AIVYaivy]))$", ErrorMessage = "Invalid State abbreviation format. State must contain 2 characters.")]
        public string state { get; set; }

        [DisplayName("Zip")]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid Zip format. Zip Code must be 5 or 9 digits '99999' or '99999-1234'.")]
        [StringLength(10)]
        public string zip { get; set; }

        [DisplayName("Home Phone")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered home phone format is not valid.")]
        [StringLength(20)]
        public string homePhone { get; set; }

        [DisplayName("Work Phone")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered work phone format is not valid.")]
        [StringLength(20)]
        public string workPhone { get; set; }

        [DisplayName("Fax")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered faxnumber format is not valid.")]
        [StringLength(20)]
        public string fax { get; set; }

        [DisplayName("Cell")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered cell phone format is not valid.")]
        [StringLength(20)]
        public string cell { get; set; }

        [DisplayName("Email")]
        [RegularExpression(@"^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[_a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$", ErrorMessage = "Email entry error.")]
        [StringLength(50)]
        public string email { get; set; }

        [DisplayName("Preferred Contact Method")]
        [Required]
        public ContactMethod preferredContactMethod { get; set; }

        [DisplayName("Created")]
        [Required]
        public DateTime created { get; set; }

        [DisplayName("Modified")]
        [Required]
        public DateTime modified { get; set; }

        [DisplayName("Created By")]
        public Nullable<int> createdBy { get; set; }

        [DisplayName("Codified By")]
        public Nullable<int> modifiedBy { get; set; }

        [DisplayName("Is Active")]
        [Required]
        public bool isActive { get; set; }

        [Association("ContactInfo_Company", "contactInfoId", "contactInfoId")]
        public List<CompanyVo> companieses { get; set; }

        [Association("ContactInfo_Client", "contactInfoId", "contactInfoId")]
        public List<ClientVo> clientses { get; set; }

        [Association("ContactInfo_Member", "contactInfoId", "contactInfoId")]
        public List<MemberVo> memberses { get; set; }
        [NotMapped]
        public Nullable<decimal> feeAmount { get; set; }

        [NotMapped]
        public string fullname { get { return this.firstName + ((firstName != null ? " " : "") + this.lastName); } }

        [NotMapped]
        public string fullAddress { get { return address + (city != null ? " " + city + ", " : " ") + (state != null ? state : "") + (zip != null ? " " + zip : ""); } }

        [NotMapped]
        public string phoneNumbers { get { return (cell != null ? "C:" + cell : "") + (homePhone != null ? "\nH:" + homePhone : "") + (workPhone != null ? "\nW:" + workPhone : ""); } }

        [NotMapped]
        public string name
        {
            get
            {
                if (firstName == null)
                    return lastName;
                if (lastName == null)
                    return firstName;

                return firstName + " " + lastName;
            }
        }

        public ContactInfoVo()
        {
            this.preferredContactMethod = ContactMethod.Email;
            this.isActive = true;
        }
    }
}
