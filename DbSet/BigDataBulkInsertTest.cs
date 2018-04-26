using NBench;

namespace Kros.KORM.PerformanceTests.DbSetTests
{
    public class BigDataBulkInsertTest : InsertUpdateTestBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public BigDataBulkInsertTest() : base(Helper.TemplateInsertMDBName)
        {
        }


        [PerfSetup]
        public void Setup()
        {
            CreateTempFolder();
            CopyTemplateDBToTempFolder();
            ConnectToTemplateDb();
            AddItemsIntoDbSetAsAdded(LoadDataFromRepository(10000));
        }


        [PerfBenchmark(TestMode = TestMode.Test, NumberOfIterations = 5)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 2100)]
        public void BenchMark()
        {
            DbSet.BulkInsert();
        }


        [PerfCleanup]
        public void Cleanup()
        {
            Clear();
        }
    }
}
