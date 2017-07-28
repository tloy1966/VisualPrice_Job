using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace VisualPrice_Job.Helpers
{
    public class ExcelHelper
    {
        static public List<DataTable> ReadXLS(List<string> lstXlsPath)
        {
            List<DataTable> lstTables = new List<DataTable>();
            foreach (var path in lstXlsPath)
            {
                Task.Factory.StartNew(
                    ()=> {

                    });
            }
            return lstTables;
        }
    }
}
