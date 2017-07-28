using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualPrice_Job.Enums
{
    static public class Parameters
    {
        public enum InsertDBMode
        {
            OneByOne = 0,
            Bulk = 1
        }

        public enum DBCol
        {
            ID = -2,
            City = -1,
            DISTRICT = 0,
            CASE_T = 1,
            LOCATION = 2,
            LANDA = 3,
            CASE_F = 4,

            LANDA_X = 5,
            LANDA_Z = 6,
            SDATE = 7,
            SCNT = 8,
            SBUILD = 9,
            TBUILD = 10,

            BUITYPE = 11,
            PBUILD = 12,
            MBUILD = 13,
            FDATE = 14,
            FAREA = 15,

            BUILD_R = 16,
            BUILD_L = 17,
            BUILD_B = 18,
            BUILD_P = 19,
            RULE = 20,

            FURNITURE = 21,

            TPRICE = 22,
            UPRICE = 23,
            PARKTYPE = 24,

            PAREA = 25,
            PPRICE = 26,
            RMNOTE = 27,
            ID2 = 28,
            isActive = 29

            #region Others
            /*Furniture_C = 21,
            TPRICE_C = 22,
            UPRICE_C = 23,
            PARKTYPE_C = 24,
            PAREA_C = 25,
            PPRICE_C = 26,
            RMNOTE_C = 27,
            ID2_C = 28,
            isActive_C = 29*/
            #endregion

        }
    }
}
