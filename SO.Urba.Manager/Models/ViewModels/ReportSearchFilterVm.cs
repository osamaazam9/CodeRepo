using SO.Urba.Models.ValueObjects;
using SO.Utility.Classes;
using SO.Utility.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SO.Urba.Models.ViewModels
{
    public class ReportSearchFilterVm : SearchFilterVm 
    {

        //public List<CompanyVo> data { get; set; }
        //public List<CompanyCategoryTypeVo> categoryData { get; set; }
        public int? companyCategoryTypeId { get; set; }
        public int? companyId { get; set; }
        public int? clientId { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }

        //public string period = "Period: ";
        //public string periodDates = (String)fromDate + " thru " + (String)toDate;


        public ReportSearchFilterVm()
            : base()
        {
           
        }

        public class ContactMembershipSearchFilterVm : SearchFilterVm
        {
            //public List<ClientVo> data { get; set; }

            public ContactMembershipSearchFilterVm()
                : base()
            {

            }

            public string keywordMin
            {
                get { return keyword; }
                set { keyword = value; }
            }
            public string keywordMax { get; set; }
        }
    }

}