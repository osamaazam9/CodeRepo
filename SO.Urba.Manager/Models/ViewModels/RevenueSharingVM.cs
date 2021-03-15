using SO.Urba.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel;

namespace SO.Urba.Manager.Models.ViewModels
{
    public class RevenueSharingVM
    {
        private OrganizationManager organizationManager = new OrganizationManager();
        public RevenueSharingVM()
        {
            allorganizationNamesList = organizationManager.getAllOrganizationNames();
            allorganizationNames = organizationManager.GetSelectListItems(allorganizationNamesList);
        }
        [Required]
        [Range(typeof(decimal), "0.0", "9999999999")]
        [DisplayName("Revenue Sharing Rate")]
        public decimal rate { get; set; }
        public decimal totalGetBackAmount { get; set; }
        [Required]
        [DisplayName("Organization Name")]
        public string orgName { get; set; }
        public IEnumerable<SelectListItem> allorganizationNames { get; set; }
        public IEnumerable<string> allorganizationNamesList { get; set; }
    }
}
