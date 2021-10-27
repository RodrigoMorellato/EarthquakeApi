using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EarthquakeApiTests.Resources
{
    public static class HelperFileTest
    {
        private static string _filePath;

        public static void CreateFileTest()
        {
            var usgsTempFolder = Path.Combine(Path.GetTempPath(), "UsgsInfoEarthquakeTests");
            const string filename = "all_month.csv";

            if (!Directory.Exists(usgsTempFolder))
                Directory.CreateDirectory(usgsTempFolder);
            else
                foreach (var f in Directory.GetFiles(usgsTempFolder))
                    File.Delete(f);

            _filePath = Path.Combine(usgsTempFolder, filename);
            File.WriteAllText(_filePath, Properties.Resources.all_month);
        }

        public static string GetFilePathTest()
        {
            return _filePath;
        }
    }
}
