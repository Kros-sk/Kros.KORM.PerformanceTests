using Kros.KORM.PerformanceTests.TestData;
using System.Diagnostics;
using System.IO;

namespace Kros.KORM.PerformanceTests
{
    class Program
    {
        /// <summary>
        /// Mains the specified arguments.
        /// </summary>
        static void Main(string[] args)
        {
            // Start run.bat to run performance tests
            // If you want see example how can use KORM, you can see this performance tests.
            //      See classes BigDataPerformanceTest and NormalDataSizePerformanceTest

            Process process = Process.Start("run.bat");

            process.WaitForExit();

            DeleteInsertUpdateTestTempFolder();
            OpenFolder();
        }

        private static void DeleteInsertUpdateTestTempFolder()
        {
            string tempFolder = Path.Combine(LocationFolder(), Helper.InsertUpdateTestTempFolder);

            if (Directory.Exists(tempFolder))
            {
                try
                {
                    Directory.Delete(tempFolder, true);
                }
                catch { }
            }
        }

        private static void OpenFolder()
        {
            Process.Start(Path.Combine(LocationFolder(), "Report"));
        }

        private static string LocationFolder()
        {
            var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            return Path.GetDirectoryName(location);
        }
    }
}
