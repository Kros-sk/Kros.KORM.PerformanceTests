using Kros.KORM.PerformanceTests.Model;
using NBench;
using System.Linq;

namespace Kros.KORM.PerformanceTests.DbSetTests
{
    public class NormalDataUpdateTest : InsertUpdateTestBase
    {

        /// <summary>
        /// Constructor
        /// </summary>
        public NormalDataUpdateTest() : base(Helper.TemplateUpdateMDBName)
        {
        }


        [PerfSetup]
        public void Setup()
        {
            CreateTempFolder();
            CopyTemplateDBToTempFolder();
            ConnectToTemplateDb();
            AddIntoDbSetAsEdited(Database.Query<Employee>().Take(1000));
        }


        [PerfBenchmark(TestMode = TestMode.Test, NumberOfIterations = 5)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 3100)]
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
