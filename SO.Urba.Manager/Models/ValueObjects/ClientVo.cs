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
    [Table("Client", Schema = "data" )]
    [Serializable]
    public  class ClientVo
    {
    
          
    	[DisplayName("client Id")]
    	[Required]
    	[Key]
        public int clientId { get; set; }
    
    	[DisplayName("contact Info Id")]
    	[Required]
        public int contactInfoId { get; set; }
    
    	[DisplayName("has Paid Fee")]
        public Nullable<bool> hasPaidFee { get; set; }
    
    	[DisplayName("fee Amount")]
        [Range(typeof(decimal), "0.0", "9999999999")] 
        public Nullable<decimal> feeAmount { get; set; }
    
    	[DisplayName("starting Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<DateTime> startingDate { get; set; }
    
    	[DisplayName("end Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<DateTime> endDate { get; set; }
    
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
       
        [ForeignKey("contactInfoId")]
        public ContactInfoVo contactInfo { get; set; }

        [Association("Client_Referral", "clientId", "clientId")]
        public List<ReferralVo> referralses { get; set; }

        [Association("Client_ClientOrganizationLookup", "clientId", "clientId")]
        public List<ClientOrganizationLookupVo> clientOrganizationLookupses { get; set; }

        [NotMapped]
        public List<int> organizations { get; set; }

        public ClientVo()
        {
    		this.isActive = true;
                    organizations = new List<int>();
        }
    
    }
    
}
