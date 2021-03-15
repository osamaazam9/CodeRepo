using SO.Utility.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SO.Utility.Models.ViewModels
{
    public class DropDownVm
    {

        
        public string propertyName { get; set; }
        public string dataValueField { get; set; }
        public string dataTextField { get; set; }
        public object selectedValue { get; set; }

        public string optionLabel { get; set; }
       
        public IList items { get; set; }

        public bool isMultiple { get; set; }

        public DropDownVm()
        {
            this.optionLabel = "None";
            this.isMultiple = false;
            
        }

        public object htmlAttributes {
            get
            {
                var ret = new
                {
                    multiple = isMultiple,


                };

                if (!isMultiple)
                    return null;

                return ret;
            }
        }



    }
}