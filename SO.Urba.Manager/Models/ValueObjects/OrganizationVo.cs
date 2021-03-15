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
using SO.Urba.Managers;

namespace SO.Urba.Models.ValueObjects
{
    [Table("Organization", Schema = "data")]
    [Serializable]
    public class OrganizationVo
    {


        [Required]
        [Key]
        public int organizationId { get; set; }

        [DisplayName("name")]
        [Required]
        [StringLength(50)]
        public string name { get; set; }

        [DisplayName("contact Info Id")]
        public Nullable<int> contactInfoId { get; set; }

        [DisplayName("fee Amount")]
        [Range(typeof(decimal), "0.0", "9999999999")]
        public Nullable<decimal> feeAmount { get; set; }

        [DisplayName("has Paid Fee")]
        public Nullable<bool> hasPaidFee { get; set; }


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

        [Association("Organization_ClientOrganizationLookup", "organizationId", "organizationId")]
        public List<ClientOrganizationLookupVo> clientOrganizationLookups { get; set; }
        [NotMapped]
        public int totalAmountofMembers { get; set; }
        public OrganizationVo()
        { 
            this.isActive = true;
        }

    }

}
