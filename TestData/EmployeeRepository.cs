using System;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace Kros.KORM.PerformanceTests.TestData
{
    public class EmployeeRepository
    {
        private const string DataSourceFilePath = @".\Resources\Data.xml";
        private const string DataSourceZipFilePath = @".\Resources\Data.zip";
        private DataTable _bigData;

        public static readonly EmployeeRepository Instance = new EmployeeRepository();

        private EmployeeRepository()
        {
        }

        public DataTable GetBigData()
        {
            LoadData();

            return _bigData;
        }

        public DataTable NormalSizeData(int count)
        {
            LoadData();

            return _bigData.AsEnumerable().Take(count).CopyToDataTable();
        }

        private void LoadData()
        {
            lock (this)
            {
                if (_bigData == null)
                {
                    UnZipData();
                    _bigData = new DataTable();
                    _bigData.ReadXml(DataSourceFilePath);
                }
            }
        }

        private void UnZipData()
        {
            if (!File.Exists(DataSourceFilePath))
            {
                ZipFile.ExtractToDirectory(DataSourceZipFilePath, Path.GetDirectoryName(DataSourceFilePath));
            }
        }
    }
}
