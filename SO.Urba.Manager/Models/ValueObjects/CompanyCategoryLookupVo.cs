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
    [Table("CompanyCategoryLookup", Schema = "data" )]
    [Serializable]
    public  class CompanyCategoryLookupVo
    {
    
          
    	[DisplayName("company Category Id")]
    	[Required]
    	[Key]
        public Guid companyCategoryId { get; set; }
    
    	[DisplayName("company Id")]
    	[Required]
        public int companyId { get; set; }
    
    	[DisplayName("company Category Type Id")]
    	[Required]
        public int companyCategoryTypeId { get; set; }
    
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
    
    
        [ForeignKey("companyCategoryTypeId")]
        public CompanyCategoryTypeVo companyCategoryType { get; set; }
       
        [ForeignKey("companyId")]
        public CompanyVo company { get; set; }
       
      public CompanyCategoryLookupVo()
            {
    				this.companyCategoryId = Guid.NewGuid();
    				this.isActive = true;
            }
    
    }
    
}
