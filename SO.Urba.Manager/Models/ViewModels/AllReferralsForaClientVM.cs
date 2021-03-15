using SO.Urba.Managers;
using SO.Urba.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SO.Urba.Manager.Models.ViewModels
{
    public class AllReferralsForaClientVM
    {
        private ReportManager reportManager = new ReportManager();
        public AllReferralsForaClientVM()
        {
            allClientNamesList = reportManager.getAllClients();
            allClientNames = reportManager.getSelectListForClientsItems(allClientNamesList);
        }
        public List<object> allReferralsForaClient { get; set; }
        public IEnumerable<SelectListItem> allClientNames { get; set; }
        public IEnumerable<ClientVo> allClientNamesList { get; set; }
        public int clientId1 { get; set; }
        [Required]
        [DisplayName("Start Date")]
        public DateTime fDate3 { get; set; }
        [Required]
        [DisplayName("Finish Date")]
        public DateTime tDate3 { get; set; }
    }
}
