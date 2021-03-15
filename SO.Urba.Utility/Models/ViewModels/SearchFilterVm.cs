using SO.Utility.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SO.Utility.Models.ViewModels
{
    public enum SearchField
    {
        [Display(Name = "All")]
        All,
        [Display(Name = "Member ID")]
        MemberID,
        [Display(Name = "First Name")]
        FirstName,
        [Display(Name = "Last Name")]
        LastName
    }

    public class SearchFilterVm
    {


        public List<object> result { get; set; }
        public string keyword { get; set; }

        [DisplayName("Search by:")]
        public SearchField searchField { get; set; }

        [DisplayName("Is active:")]
        public bool? isActive { get; set; }
        public string submitButton { get; set; }
        public Paging paging;


        public SearchFilterVm()
        {
            this.result = new List<object>();
            if (paging == null)
                paging = new Paging();
        }
    }
}