using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using VisualPrice_Job.Interface;
using VisualPrice_Job.Util;
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
                    if (file.Extension.Equals(strFilter, StringComparison.OrdinalIgnoreCase))
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
