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
    public class SpecificCategoryReferralsByDateVM
    {
        private ReportManager reportManager = new ReportManager();
        public SpecificCategoryReferralsByDateVM()
        {
            allCategoryNamesList = reportManager.getAllCategoryNames();
            allCategoryNames = reportManager.getSelectListForCategoryItems(allCategoryNamesList);
        }
        public string categoryName { get; set; }
        public int id { get; set; }
        public IEnumerable<SelectListItem> allCategoryNames { get; set; }
        public IEnumerable<CompanyCategoryTypeVo> allCategoryNamesList { get; set; }
        public List<object> result { get; set; }
        [Required]
        [DisplayName("From Date")]
        public DateTime fDate { get; set; }
        [Required]
        [DisplayName("To Date")]
        public DateTime tDate { get; set; }
 
    }
}
