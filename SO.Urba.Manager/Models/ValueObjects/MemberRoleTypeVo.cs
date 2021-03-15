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
    [Table("MemberRoleType", Schema = "app" )]
    [Serializable]
    public  class MemberRoleTypeVo
    {
    
          
    	[DisplayName("member Role Type Id")]
    	[Required]
    	[Key]
        public int memberRoleTypeId { get; set; }
    
    	[DisplayName("name")]
    	[Required]
    	[StringLength(200)]
        public string name { get; set; }
    
    	[DisplayName("description")]
        public string description { get; set; }

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
    
    
        [Association("MemberRoleType_MemberRoleLookup", "memberRoleTypeId", "memberRoleTypeId")]
        public List<MemberRoleLookupVo> memberRoleLookupses { get; set; }
       
      public MemberRoleTypeVo()
            {
    				this.isActive = true;
            }
    
    }
    
}
