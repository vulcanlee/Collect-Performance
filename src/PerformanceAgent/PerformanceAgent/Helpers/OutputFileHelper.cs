using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceAgent.Helpers
{
    public class OutputFileHelper
    {
        public string PrepareOutputFile(string folderName, string filename)
        {
            var dataFolder = Path.Combine(Directory.GetCurrentDirectory(),
                folderName);
            if (Directory.Exists(dataFolder) == false) 
                Directory.CreateDirectory(dataFolder);
            var result = Path.Combine(dataFolder, filename);
            return result;
        }
    }
}
