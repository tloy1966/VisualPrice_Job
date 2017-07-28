using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using NLog;
namespace VisualPrice_Job
{
    class Program
    {
        static Logger logger = NLog.LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            DoWork();
        }

        static void DoWork()
        {
            try
            {
                var task = Task<List<string>>.Factory.StartNew(
                    () => Helpers.FileHelper.GetFilesPath(Helpers.FileHelper.strXLSFolder, ".xls").OrderBy(o => o).ToList())
                    .ContinueWith<List<DataTable>>(t => Helpers.ExcelHelper.ReadXLS(t.Result, true, Enums.Parameters.InsertDBMode.Bulk))
                    .ContinueWith(t=>Helpers.DBHelper.InsertData(t.Result));
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }
    }
}
