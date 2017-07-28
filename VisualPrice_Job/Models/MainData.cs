using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace VisualPrice_Job.Models
{
    public class MainData
    {
        static public DataTable CreateMainData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("City", typeof(string));
            dt.Columns.Add("SellType", typeof(string));
            dt.Columns.Add("DISTRICT", typeof(string));//0
            dt.Columns.Add("CASE_T", typeof(string));

            dt.Columns.Add("LOCATION", typeof(string));
            dt.Columns.Add("LANDA", typeof(decimal));
            dt.Columns.Add("CASE_F", typeof(string));
            dt.Columns.Add("LANDA_X", typeof(string));
            dt.Columns.Add("LANDA_Z", typeof(string));//6

            dt.Columns.Add("SDATE", typeof(string));
            dt.Columns.Add("SCNT", typeof(string));
            dt.Columns.Add("SBUILD", typeof(string));
            dt.Columns.Add("TBUILD", typeof(string));//10
            dt.Columns.Add("BUITYPE", typeof(string));

            dt.Columns.Add("PBUILD", typeof(string));
            dt.Columns.Add("MBUILD", typeof(string));
            dt.Columns.Add("FDATE", typeof(string));
            dt.Columns.Add("FAREA", typeof(decimal));//15
            dt.Columns.Add("BUILD_R", typeof(int));

            dt.Columns.Add("BUILD_L", typeof(int));
            dt.Columns.Add("BUILD_B", typeof(int));
            dt.Columns.Add("BUILD_P", typeof(string));
            dt.Columns.Add("RULE", typeof(string));//20
                                                   //dt.Columns.Add("BUILD_C", typeof(string));


            dt.Columns.Add("FURNITURE", typeof(bool));
            dt.Columns.Add("TPRICE", typeof(int));
            dt.Columns.Add("UPRICE", typeof(decimal));

            dt.Columns.Add("PARKTYPE", typeof(string));
            dt.Columns.Add("PAREA", typeof(decimal));
            dt.Columns.Add("PPRICE", typeof(int));//25

            dt.Columns.Add("RMNOTE", typeof(string));
            dt.Columns.Add("ID2", typeof(string));
            dt.Columns.Add("isActive", typeof(bool));
            return dt;
        }
    }
}
