using NBench;

namespace Kros.KORM.PerformanceTests.DbSetTests
{
    public class BigDataUpdateTest: InsertUpdateTestBase
    {

        /// <summary>
        /// Constructor
        /// </summary>
        public BigDataUpdateTest() : base(Helper.TemplateUpdateMDBName)
        {
        }


        [PerfSetup]
        public void Setup()
        {
            CreateTempFolder();
            CopyTemplateDBToTempFolder();
            ConnectToTemplateDb();
            AddIntoDbSetAsEdited(DbSet);
        }


        [PerfBenchmark(TestMode = TestMode.Test, NumberOfIterations = 5)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 31000)]
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
