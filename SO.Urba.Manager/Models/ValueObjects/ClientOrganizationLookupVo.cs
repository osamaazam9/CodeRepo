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
    [Table("ClientOrganizationLookup", Schema = "data")]
    [Serializable]
    public class ClientOrganizationLookupVo
    {


        [DisplayName("client Organization Lookup Id")]
        [Required]
        [Key]
        public Guid clientOrganizationLookupId { get; set; }

        [DisplayName("Client Id")]
        [Required]
        public int clientId { get; set; }

        [DisplayName("Organization Id")]
        [Required]
        public int organizationId { get; set; }

        [DisplayName("created")]
        [Required]
        public DateTime created { get; set; }

        [DisplayName("modified")]
        [Required]
        public DateTime modified { get; set; }

        [DisplayName("created By")]
        public int? createdBy { get; set; }

        [DisplayName("modified By")]
        public int? modifiedBy { get; set; }

        [DisplayName("is Active")]
        [Required]
        public bool isActive { get; set; }


        [ForeignKey("organizationId")]
        public OrganizationVo organization { get; set; }

        [ForeignKey("clientId")]
        public ClientVo client { get; set; }
       

        public ClientOrganizationLookupVo()
        {
            this.clientOrganizationLookupId = Guid.NewGuid();
            this.isActive = true;
        }

    }

}
