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
        static public Logger logger = NLog.LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            DoWork();
        }

        static void DoWorkTask()
        {
            try
            {
                logger.Info("Start Task:");
                var task = Task<List<string>>.Factory.StartNew(
                    () => Helpers.FileHelper.GetFilesPath(Enums.Parameters.GetXlsFolder(), Enums.Parameters.strXLSFilter))
                    .ContinueWith<List<DataTable>>(t => Helpers.ExcelHelper.ReadXLS(t.Result))
                    .ContinueWith(t=>Helpers.DBHelper.InsertData(t.Result));
                logger.Info("End Task");
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                logger.Error(ex.InnerException);
            }
        }
        static void DoWork()
        {
            var lstFiles = Helpers.FileHelper.GetFilesPath(Enums.Parameters.GetXlsFolder(), Enums.Parameters.strXLSFilter);
            var lstTables = Helpers.ExcelHelper.ReadXLS(lstFiles);
            Helpers.DBHelper.InsertData(lstTables);
        }
    }
}
