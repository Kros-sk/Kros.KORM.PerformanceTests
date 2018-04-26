using NBench;

namespace Kros.KORM.PerformanceTests.DbSetTests
{
    public class NormalDataInsertTest: InsertUpdateTestBase
    {

        /// <summary>
        /// Constructor
        /// </summary>
        public NormalDataInsertTest() : base(Helper.TemplateInsertMDBName)
        {
        }


        [PerfSetup]
        public void Setup()
        {
            CreateTempFolder();
            CopyTemplateDBToTempFolder();
            ConnectToTemplateDb();
            AddItemsIntoDbSetAsAdded(LoadDataFromRepository(1000));
        }


        [PerfBenchmark(TestMode = TestMode.Test, NumberOfIterations = 5)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 1750)]
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
