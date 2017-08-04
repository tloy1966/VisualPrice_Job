using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using NLog;
using System.Diagnostics;
namespace VisualPrice_Job
{
    class Program
    {
        static public Logger logger = NLog.LogManager.GetCurrentClassLogger();
        static Stopwatch sw = new Stopwatch();
        static void Main(string[] args)
        {
            DoWorkTask();
        }

        static void DoWorkTask()
        {
            try
            {
                sw.Reset();
                sw.Start();
                var task = Task<List<string>>.Factory.StartNew(
                    () => Helpers.FileHelper.GetFilesPath(Enums.Parameters.GetXlsFolder(), Enums.Parameters.strXLSFilter))
                    .ContinueWith<List<DataTable>>(t => Helpers.ExcelHelper.ReadXLS(t.Result))
                    .ContinueWith(t=>Helpers.DBHelper.InsertData(t.Result));
                task.Wait();
                sw.Stop();
                Console.WriteLine();
                Console.WriteLine(sw.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                logger.Error(ex.InnerException);
            }
        }
        static void DoWork()
        {
            sw.Reset();
            sw.Start();
            var lstFiles = Helpers.FileHelper.GetFilesPath(Enums.Parameters.GetXlsFolder(), Enums.Parameters.strXLSFilter);
            var lstTables = Helpers.ExcelHelper.Read(lstFiles);
            Helpers.DBHelper.InsertData(lstTables);
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
            lstTables.ForEach(t => Console.WriteLine(t.TableName));
        }
    }
}
