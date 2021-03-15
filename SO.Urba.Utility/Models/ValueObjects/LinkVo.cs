using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SO.Utility.Models.ValueObjects
{
    public class LinkVo
    {
        public string text { get; set; }
        public string url { get; set; }

        public LinkVo(string text=null, string url=null)
        {
            this.text = text;
            this.url = url;
            if(this.url!=null && (this.url.EndsWith("/") || this.url.EndsWith("\\")) ){
                this.url = this.url.Substring(0,this.url.Length-1);
            }
        }
    }
}
