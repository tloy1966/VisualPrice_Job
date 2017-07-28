using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Text.RegularExpressions;
namespace VisualPrice_Job.Helpers
{
    public class ExcelHelper
    {
        static Regex rgxFileName = new Regex(@"\b[A-Z]{1}_lvr_land_[A]{1}", RegexOptions.IgnoreCase);
        static Regex rgxCheckLetters = new Regex(@"^[a-zA-Z0-9]+$");

        static public List<DataTable> ReadXLS(List<string> lstXlsPath)
        {
            List<DataTable> lstTables = new List<DataTable>();
            foreach (var path in lstXlsPath)
            {
                string _path = path;
                Task.Factory.StartNew(
                    ()=> {
                        lstTables.Add(Read(_path));
                    },TaskCreationOptions.AttachedToParent | TaskCreationOptions.LongRunning);
            }
            return lstTables;
        }

        static private DataTable Read(string _path)
        {
            DataTable dt;
            HSSFWorkbook wk;
            ISheet sheet = null;
            using (FileStream fs = new FileStream(_path, FileMode.Open, FileAccess.ReadWrite))
            {
                wk = new HSSFWorkbook(fs);
            }
            try
            {
                if (wk.GetSheet("不動產買賣") != null)
                {
                    sheet = wk.GetSheet("不動產買賣");//預售屋買賣 不動產租賃
                }
                else if (wk.GetSheet("預售屋買賣") != null)
                {
                    sheet = wk.GetSheet("預售屋買賣");
                }
                else if (wk.GetSheet("不動產租賃") != null)
                {
                    sheet = wk.GetSheet("不動產租賃");
                }
                else
                {
                    return null;
                }
                Console.WriteLine("Processing");

                dt =  ReadExcelFile(sheet, _path);
            }
            catch (Exception ex)
            {
                dt = null;
                //then copy this file to another folder, and check
            }
            return dt;
        }

        static private DataTable ReadExcelFile(ISheet sheet, string strXlsPath)
        {
            DataTable dt = Models.MainData.CreateMainData();

            var CityCodeAndSellType = GetCityandTypeCode(strXlsPath);
            for (int i = 2; i <= sheet.LastRowNum; i++)
            {
                var nowRow = sheet.GetRow(i);
                var t = nowRow.Cells[(int)(Enums.Parameters.DBCol.CASE_T)];
                if (string.IsNullOrEmpty(nowRow.Cells[(int)(Enums.Parameters.DBCol.DISTRICT)].ToString()))
                {
                    continue;
                }

                var dr = dt.NewRow();
                if (CityCodeAndSellType == null)
                {
                    continue;
                }
                dr[1] = CityCodeAndSellType.Item1;
                dr[2] = CityCodeAndSellType.Item2;
                dr[Enums.Parameters.DBCol.CASE_T.ToString()] =Helpers.UtilHelper.TypeCheck(nowRow.Cells[(int)(Enums.Parameters.DBCol.CASE_T)].ToString(), "string", 50);
                dr[Enums.Parameters.DBCol.DISTRICT.ToString()] = Helpers.UtilHelper.TypeCheck(nowRow.Cells[(int)(Enums.Parameters.DBCol.DISTRICT)].ToString(), "string", 50);
                dr[Enums.Parameters.DBCol.LOCATION.ToString()] = ((string)Helpers.UtilHelper.TypeCheck(nowRow.Cells[(int)(Enums.Parameters.DBCol.LOCATION)].ToString(), "string", 400)).Replace("~", "-");
                dr[Enums.Parameters.DBCol.LANDA.ToString()] = Helpers.UtilHelper.TypeCheck(nowRow.Cells[(int)(Enums.Parameters.DBCol.LANDA)].ToString(), "decimal", 0);
                dr[Enums.Parameters.DBCol.CASE_F.ToString()] = Helpers.UtilHelper.TypeCheck(nowRow.Cells[(int)(Enums.Parameters.DBCol.CASE_F)].ToString(), "string", 50);

                dr[Enums.Parameters.DBCol.LANDA_X.ToString()] = Helpers.UtilHelper.TypeCheck(nowRow.Cells[(int)(Enums.Parameters.DBCol.LANDA_X)].ToString(), "string", 50);
                dr[Enums.Parameters.DBCol.LANDA_Z.ToString()] = Helpers.UtilHelper.TypeCheck(nowRow.Cells[(int)(Enums.Parameters.DBCol.LANDA_Z)].ToString(), "string", 50);
                dr[Enums.Parameters.DBCol.SDATE.ToString()] = Helpers.UtilHelper.TypeCheck(nowRow.Cells[(int)(Enums.Parameters.DBCol.SDATE)].ToString(), "datetime", 0);// nowRow.Cells[(int)(Enums.Parameters.DBCol.SDATE)].ToString().Insert(3, "-").Insert(6, "-");//[Enums.Parameters.DBCol.SDATE.ToString().Insert(3, "-").Insert(6, "-")]).Date;
                dr[Enums.Parameters.DBCol.SCNT.ToString()] = Helpers.UtilHelper.TypeCheck(nowRow.Cells[(int)(Enums.Parameters.DBCol.SCNT)].ToString(), "string", 200);
                dr[Enums.Parameters.DBCol.SBUILD.ToString()] = Helpers.UtilHelper.TypeCheck(nowRow.Cells[(int)(Enums.Parameters.DBCol.SBUILD)].ToString(), "string", 200);
                dr[Enums.Parameters.DBCol.TBUILD.ToString()] = Helpers.UtilHelper.TypeCheck(nowRow.Cells[(int)(Enums.Parameters.DBCol.TBUILD)].ToString(), "string", 50);

                dr[Enums.Parameters.DBCol.BUITYPE.ToString()] = Helpers.UtilHelper.TypeCheck(nowRow.Cells[(int)(Enums.Parameters.DBCol.BUITYPE)].ToString(), "string", 200);
                dr[Enums.Parameters.DBCol.PBUILD.ToString()] = Helpers.UtilHelper.TypeCheck(nowRow.Cells[(int)(Enums.Parameters.DBCol.PBUILD)].ToString(), "string", 50);
                dr[Enums.Parameters.DBCol.MBUILD.ToString()] = Helpers.UtilHelper.TypeCheck(nowRow.Cells[(int)(Enums.Parameters.DBCol.MBUILD)].ToString(), "string", 300);
                dr[Enums.Parameters.DBCol.FDATE.ToString()] = Helpers.UtilHelper.TypeCheck(nowRow.Cells[(int)(Enums.Parameters.DBCol.FDATE)].ToString(), "datetime", 0);
                dr[Enums.Parameters.DBCol.FAREA.ToString()] = Helpers.UtilHelper.TypeCheck(nowRow.Cells[(int)(Enums.Parameters.DBCol.FAREA)].ToString(), "decimal", 0);

                dr[Enums.Parameters.DBCol.BUILD_R.ToString()] = Helpers.UtilHelper.TypeCheck(nowRow.Cells[(int)(Enums.Parameters.DBCol.BUILD_R)].ToString(), "int", 0);
                dr[Enums.Parameters.DBCol.BUILD_L.ToString()] = Helpers.UtilHelper.TypeCheck(nowRow.Cells[(int)(Enums.Parameters.DBCol.BUILD_L)].ToString(), "int", 0);
                dr[Enums.Parameters.DBCol.BUILD_B.ToString()] = Helpers.UtilHelper.TypeCheck(nowRow.Cells[(int)(Enums.Parameters.DBCol.BUILD_B)].ToString(), "int", 0);
                dr[Enums.Parameters.DBCol.BUILD_P.ToString()] = Helpers.UtilHelper.TypeCheck(nowRow.Cells[(int)(Enums.Parameters.DBCol.BUILD_P)].ToString(), "string", 50);
                dr[Enums.Parameters.DBCol.RULE.ToString()] = Helpers.UtilHelper.TypeCheck(nowRow.Cells[(int)(Enums.Parameters.DBCol.RULE)].ToString(), "string", 50);


                string tempID2 = "";

                if (CityCodeAndSellType.Item2 == "A")
                {
                    /*DB col the same but excel not the same*/
                    dr[Enums.Parameters.DBCol.FURNITURE.ToString()] = 0;
                    dr[Enums.Parameters.DBCol.TPRICE.ToString()] = Helpers.UtilHelper.TypeCheck(nowRow.Cells[(int)(Enums.Parameters.DBCol.TPRICE) - 1].ToString(), "int", 0);
                    dr[Enums.Parameters.DBCol.UPRICE.ToString()] = Helpers.UtilHelper.TypeCheck(nowRow.Cells[(int)(Enums.Parameters.DBCol.UPRICE) - 1].ToString(), "decimal", 0);
                    dr[Enums.Parameters.DBCol.PARKTYPE.ToString()] = Helpers.UtilHelper.TypeCheck(nowRow.Cells[(int)(Enums.Parameters.DBCol.PARKTYPE) - 1].ToString(), "string", 50);

                    dr[Enums.Parameters.DBCol.PAREA.ToString()] = Helpers.UtilHelper.TypeCheck(nowRow.Cells[(int)(Enums.Parameters.DBCol.PAREA) - 1].ToString(), "decimal", 0);
                    dr[Enums.Parameters.DBCol.PPRICE.ToString()] = Helpers.UtilHelper.TypeCheck(nowRow.Cells[(int)(Enums.Parameters.DBCol.PPRICE) - 1].ToString(), "int", 0);
                    dr[Enums.Parameters.DBCol.RMNOTE.ToString()] = Helpers.UtilHelper.TypeCheck(nowRow.Cells[(int)(Enums.Parameters.DBCol.RMNOTE) - 1].ToString(), "string", 400);

                    if (string.IsNullOrEmpty(Helpers.UtilHelper.TypeCheck(nowRow.Cells[(int)(Enums.Parameters.DBCol.ID2) - 1].ToString(), "string", 400)))
                    {
                        continue; ;
                    }

                    dr[Enums.Parameters.DBCol.ID2.ToString()] = Helpers.UtilHelper.TypeCheck(nowRow.Cells[(int)(Enums.Parameters.DBCol.ID2) - 1].ToString(), "string", 400);
                    var tempID = Helpers.UtilHelper.TypeCheck(nowRow.Cells[(int)(Enums.Parameters.DBCol.ID2) - 1].ToString(), "letters", 0);
                    if (tempID is DBNull)
                    {
                        dr[Enums.Parameters.DBCol.isActive.ToString()] = 0;
                    }
                    else
                    {
                        dr[Enums.Parameters.DBCol.isActive.ToString()] = 1;
                        tempID2 = tempID;
                    }

                }
                else if (CityCodeAndSellType.Item2 == "C")
                {
                    dr[Enums.Parameters.DBCol.FURNITURE.ToString()] = Helpers.UtilHelper.TypeCheck(nowRow.Cells[(int)(Enums.Parameters.DBCol.FURNITURE)].ToString(), "bool", 0);
                    dr[Enums.Parameters.DBCol.TPRICE.ToString()] = Helpers.UtilHelper.TypeCheck(nowRow.Cells[(int)(Enums.Parameters.DBCol.TPRICE)].ToString(), "int", 0);
                    dr[Enums.Parameters.DBCol.UPRICE.ToString()] = Helpers.UtilHelper.TypeCheck(nowRow.Cells[(int)(Enums.Parameters.DBCol.UPRICE)].ToString(), "decimal", 0);
                    dr[Enums.Parameters.DBCol.PARKTYPE.ToString()] = Helpers.UtilHelper.TypeCheck(nowRow.Cells[(int)(Enums.Parameters.DBCol.PARKTYPE)].ToString(), "string", 50);

                    dr[Enums.Parameters.DBCol.PAREA.ToString()] = Helpers.UtilHelper.TypeCheck(nowRow.Cells[(int)(Enums.Parameters.DBCol.PAREA)].ToString(), "decimal", 0);
                    dr[Enums.Parameters.DBCol.PPRICE.ToString()] = Helpers.UtilHelper.TypeCheck(nowRow.Cells[(int)(Enums.Parameters.DBCol.PPRICE)].ToString(), "int", 0);
                    dr[Enums.Parameters.DBCol.RMNOTE.ToString()] = Helpers.UtilHelper.TypeCheck(nowRow.Cells[(int)(Enums.Parameters.DBCol.RMNOTE)].ToString(), "string", 400);

                    dr[Enums.Parameters.DBCol.ID2.ToString()] = Helpers.UtilHelper.TypeCheck(nowRow.Cells[(int)(Enums.Parameters.DBCol.ID2)].ToString(), "string", 400);
                    if (string.IsNullOrEmpty(Helpers.UtilHelper.TypeCheck(nowRow.Cells[(int)(Enums.Parameters.DBCol.ID2)].ToString(), "string", 400)))
                    {
                        continue;
                    }
                    var tempID = Helpers.UtilHelper.TypeCheck(nowRow.Cells[(int)(Enums.Parameters.DBCol.ID2)].ToString(), "letters", 0);
                    if (tempID is DBNull)
                    {
                        dr[Enums.Parameters.DBCol.isActive.ToString()] = 0;
                    }
                    else
                    {
                        dr[Enums.Parameters.DBCol.isActive.ToString()] = 1;
                        tempID2 = tempID;
                    }
                }

                if (string.IsNullOrEmpty(tempID2))
                {
                    Console.WriteLine("ID is null");
                    continue;
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        static private Tuple<string, string> GetCityandTypeCode(string strPath)
        {
            //example : A_lvr_land_A.xls
            if (File.Exists(strPath))
            {
                var strName = Path.GetFileName(strPath);
                if (rgxFileName.IsMatch(strName))
                {
                    return new Tuple<string, string>(strName.Split('_').FirstOrDefault(), strName.Split('.').FirstOrDefault().Split('_').LastOrDefault());
                }
            }
            return null;
        }
    }
}
