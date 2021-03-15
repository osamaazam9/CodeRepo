using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FileHelpers;
using System.IO;
using System.Collections.Specialized;


using SO.Utility.Extensions;
using System.Collections;

namespace SO.Utility.Helpers
{
    public class ImportExportHelper
    {

        public static IEnumerable<RecordIndexer> importCsv(string fileName, char splitter = ',')
        {
            var result = CommonEngine.ReadCsv(fileName, splitter, 1).ToList();
            return result;
        }


        public static FileInfo exportToCsv<T>(List<T> data,  bool showHeader=true, List<string> columns = null,string fileName=null)
        {
            var folder = Path.GetTempPath() + "/export/";
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            if (fileName == null) fileName = folder + "/file_" + Guid.NewGuid().ToString() + ".csv";

            if (data == null || data.Count() == 0)
                return null;

            data.ToCSV(fileName, "", "", columns, showHeader);

            var file = new FileInfo(fileName);

            return file;
        }



        public static string getContentType(string fileExtension)
        {
            var nv = new NameValueCollection();
            nv[".ppt"] = "Application/vnd.ms-powerpoint";
            nv[".pps"] = "Application/vnd.ms-powerpoint";
            nv[".xls"] = "Application/vnd.ms-excel";
            nv[".dwf"] = "Application/x-dwf";
            nv[".pdf"] = "Application/pdf";
            nv[".doc"] = "Application/vnd.ms-word";
            nv[".docx"] = "Application/vnd.ms-word";
            string value = nv[fileExtension.ToLower()];
            if (string.IsNullOrEmpty(value)) value = "Application/octet-stream";
            return value;
        }


    }
}
