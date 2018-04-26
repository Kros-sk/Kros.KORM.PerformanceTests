using NBench;

namespace Kros.KORM.PerformanceTests.DbSetTests
{
    public class BigDataInsertTest : InsertUpdateTestBase
    {

        /// <summary>
        /// Constructor
        /// </summary>
        public BigDataInsertTest() : base(Helper.TemplateInsertMDBName)
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
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 17000)]
        public void BenchMark()
        {
            CommitChanges();
        }


        [PerfCleanup]
        public void Cleanup()
        {
            Clear();
        }

    }
}
