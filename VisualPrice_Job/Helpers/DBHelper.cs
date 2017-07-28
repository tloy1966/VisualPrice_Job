using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace VisualPrice_Job.Helpers
{
    public class DBHelper
    {
        static public void InsertData(List<DataTable> lstTable)
        {
            foreach (var dt in lstTable)
            {
                try
                {
                    InsertByDataTable(Autho.Azure.GetConnectionString(), dt);
                }
                catch (Exception ex)
                {
                    //try insert one by one
                    Console.WriteLine(ex.Message);
                    InsertRow(Autho.Azure.GetConnectionString(), dt);
                }
            }
            
        }

        static public void InsertByDataTable(string strCn, DataTable dt)
        {
            using (SqlConnection cn = new SqlConnection(strCn))
            using (SqlBulkCopy bc = new SqlBulkCopy(cn))
            {
                cn.Open();
                bc.BulkCopyTimeout = 600;
                bc.BatchSize = 500;
                bc.DestinationTableName = "MainData";
                bc.WriteToServer(dt);
            }
        }

        static public void InsertRow(string strCn, DataTable dt)
        {
            using (SqlConnection cn = new SqlConnection(strCn))
            using (SqlBulkCopy bc = new SqlBulkCopy(cn))
            {
                cn.Open();
                bc.BulkCopyTimeout = 100;
                bc.BatchSize = 1;
                bc.DestinationTableName = "MainData";
                foreach (var dr in dt.Rows)
                {
                    try
                    {
                        //not efficient...
                        var tmpDt = dt.Clone();
                        tmpDt.Rows.Add(dr);
                        bc.WriteToServer(tmpDt);
                    }
                    catch (Exception ex)
                    {
                        Program.logger.Error($"Insert error: {ex}");
                    }
                }
            }
        }
    }
}
