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
    [Table("Company", Schema = "data" )]
    [Serializable]
    public  class CompanyVo
    {
    
          
    	[DisplayName("company Id")]
    	[Required]
    	[Key]
        public int companyId { get; set; }
    
    	[DisplayName("Company Name")]
    	[Required]
    	[StringLength(50)]
        public string companyName { get; set; }
    
    	[DisplayName("Contact Person")]
    	[Required]
        public int contactInfoId { get; set; }
    
    	[DisplayName("License #")]
    	[StringLength(20)]
        public string license { get; set; }
    
    	[DisplayName("Bonded")]
    	[StringLength(1)]
        public string bonded { get; set; }
    
    	[DisplayName("Agreement Signed")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<DateTime> agreementSigned { get; set; }
    
    	[DisplayName("Created")]
    	[Required]
        public DateTime created { get; set; }
    
    	[DisplayName("Modified")]
    	[Required]
        public DateTime modified { get; set; }
    
    	[DisplayName("Created By")]
        public Nullable<int> createdBy { get; set; }
    
    	[DisplayName("Modified By")]
        public Nullable<int> modifiedBy { get; set; }
    
    	[DisplayName("Is Active")]
    	[Required]
        public bool isActive { get; set; }
    
    
        [Association("Company_CompanyCategoryLookup", "companyId", "companyId")]
        public List<CompanyCategoryLookupVo> companyCategoryLookupses { get; set; }
       
        [ForeignKey("contactInfoId")]
        public ContactInfoVo contactInfo { get; set; }
       
        [Association("Company_Referral", "companyId", "companyId")]
        public List<ReferralVo> referralses { get; set; }
        
        [NotMapped]
        public List<int> companyCategories { get; set; }

        [NotMapped]
        public string name
        {
            get
            {
                return companyName;
            }
        }

        [NotMapped]
        public decimal? score { get; set; }

        [NotMapped]
        public int scoreCount { get; set; }

        public CompanyVo()
        {
    		this.isActive = true;
            companyCategories = new List<int>();
        }
    
    }
    
}
