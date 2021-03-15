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
    public class ContactInfoVm
    {
        public ContactInfoVo contactInfo { get; set; }
        public int? companyId { get; set; }

        public ContactInfoVm()
        {
            contactInfo = new ContactInfoVo();
        }

    }
}