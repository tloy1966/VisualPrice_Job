using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
namespace VisualPrice_Job.Helpers
{
    public class FileHelper
    {
        //Map before multiple task
        static public List<string> GetFilesPath(string strFolder, string strFilter)
        {
            if (!Directory.Exists(strFolder))
            {
                throw new Exception("No such folder...");
            }
            List<string> lstFiles = new List<string>();
            DirectoryInfo Dir = new DirectoryInfo(strFolder);
            foreach (var subDir in Dir.GetDirectories())
            {
                foreach (var file in subDir.GetFiles())
                {
                    var t = file.Extension;
                    if (t.Equals(strFilter, StringComparison.OrdinalIgnoreCase))
                    {
                        lstFiles.Add(file.FullName);
                    }
                }
            }
            lstFiles = lstFiles.OrderBy(o => o).ToList();
            return lstFiles;
        }

        
    }
}
