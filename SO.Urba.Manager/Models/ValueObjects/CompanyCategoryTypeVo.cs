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
    [Table("CompanyCategoryType", Schema = "app" )]
    [Serializable]
    public  class CompanyCategoryTypeVo
    {
    
          
    	[DisplayName("company Category Type Id")]
    	[Required]
    	[Key]
        public int companyCategoryTypeId { get; set; }
    
    	[DisplayName("name")]
    	[StringLength(50)]
        public string name { get; set; }
    
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
    
   
       
      //  [Association("CompanyCategoryType_CompanyCategoryLookup", "companyCategoryTypeId", "companyCategoryTypeId")]
      //  public List<CompanyCategoryLookupVo> companyCategoryLookupses { get; set; }
       
        [Association("CompanyCategoryType_Referral", "companyCategoryTypeId", "companyCategoryTypeId")]
        public List<ReferralVo> referralses { get; set; }

        [NotMapped]
        public string label { get; set; }
       
      public CompanyCategoryTypeVo()
            {
    				this.isActive = true;
            }
    
    }
    
}
