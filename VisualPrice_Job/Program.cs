using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace VisualPrice_Job
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        static void DoWork()
        {
            try
            {
                var lstPath = Helpers.FileHelper.GetFilesPath(Helpers.FileHelper.strXLSFolder, ".xls").OrderBy(o => o).ToList();//.ToArray();
                var task = Task<List<string>>.Factory.StartNew(
                    ()=> Helpers.FileHelper.GetFilesPath(Helpers.FileHelper.strXLSFolder, ".xls").OrderBy(o => o).ToList())
                    .ContinueWith<List<DataTable>(t=>Helpers.ExcelHelper.ReadXLS(t.re)
                    
            }
            catch (Exception ex)
            {

            }
        }
    }
}
