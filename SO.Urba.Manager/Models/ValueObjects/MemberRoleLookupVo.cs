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
    [Table("MemberRoleLookup", Schema = "data" )]
    [Serializable]
    public  class MemberRoleLookupVo
    {
    
          
    	[DisplayName("member Role Id")]
    	[Required]
    	[Key]
        public Guid memberRoleId { get; set; }
    
    	[DisplayName("member Role Type Id")]
        public Nullable<int> memberRoleTypeId { get; set; }
    
    	[DisplayName("member Id")]
        public Nullable<int> memberId { get; set; }
    
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
    
    
        [ForeignKey("memberRoleTypeId")]
        public MemberRoleTypeVo memberRoleType { get; set; }
       
        [ForeignKey("memberId")]
        public MemberVo member { get; set; }

      public MemberRoleLookupVo()
            {
    				this.memberRoleId = Guid.NewGuid();
    				this.isActive = true;
            }
    
    }
    
}
