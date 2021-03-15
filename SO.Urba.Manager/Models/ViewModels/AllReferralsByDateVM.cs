using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SO.Urba.Manager.Models.ViewModels
{
    public class AllReferralsByDateVM
    {
        public List<object> allReferrals { get; set; }
        [Required]
        [DisplayName("From Date")]
        public DateTime fDate2 { get; set; }
        [Required]
        [DisplayName("To Date")]
        public DateTime tDate2 { get; set; }
    }
}
