using SO.Urba.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SO.Urba.Manager.Models.ViewModels
{
    public class AllReferralsForCategoryVM
    {
        private ReportManager rm = new ReportManager();
        public AllReferralsForCategoryVM()
        {

        }
        [Required]
        public DateTime fDate { get; set; }
        [Required]
        public DateTime tDate { get; set; }

    }
}
