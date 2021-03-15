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
    [Table("Member", Schema = "data" )]
    [Serializable]
    public  class MemberVo
    {
    
          
    	[DisplayName("member Id")]
    	[Required]
    	[Key]
        public int memberId { get; set; }
    
    	[DisplayName("contact Info Id")]
        public Nullable<int> contactInfoId { get; set; }

        [Required]
    	[DisplayName("username")]
    	[StringLength(50, MinimumLength = 3, ErrorMessage = "Please enter minimum 3 characters!") ]
        [RegularExpression(@"^[a-zA-Z\s?]+$", ErrorMessage = "[username] Please enter valid characters!")]
        public string username { get; set; }

        [Required]
    	[DisplayName("password")]
    	[StringLength(64)]
        public string password { get; set; }

        [NotMapped]
        public string passwordReset { get; set; }
    
    
    	[DisplayName("force Password Reset")]
        public Nullable<bool> forcePasswordReset { get; set; }
   

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
       
        [Association("Member_MemberRoleLookup", "memberId", "memberId")]
        public List<MemberRoleLookupVo> memberRoleLookupses { get; set; }

        [NotMapped]
        public List<int> memberRoleTypes { get; set; }
       
      public MemberVo()
            {
    				this.isActive = true;
                    memberRoleTypes = new List<int>();
            }
    
    }
    
}
