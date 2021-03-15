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
    public class CompanySearchFilterVm : SearchFilterVm 
    {

       

        public List<CompanyVo> data { get; set; }
        public int? companyCategoryTypeId { get; set; }

        public CompanySearchFilterVm()
            : base()
        {
           
        }
    }


    public class ClientSearchFilterVm : SearchFilterVm
    {
        //public List<ClientVo> data { get; set; }

        public ClientSearchFilterVm()
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