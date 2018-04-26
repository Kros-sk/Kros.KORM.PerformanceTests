using NBench;

namespace Kros.KORM.PerformanceTests.DbSetTests
{
    public class BigDataBulkUpdateTest : InsertUpdateTestBase
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public BigDataBulkUpdateTest() : base(Helper.TemplateUpdateMDBName)
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
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 3000)]
        public void BenchMark()
        {
            DbSet.BulkUpdate();
        }


        [PerfCleanup]
        public void Cleanup()
        {
            Clear();
        }

    }
}
