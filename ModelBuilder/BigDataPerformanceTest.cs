using Kros.KORM.PerformanceTests.Model;
using Kros.KORM.PerformanceTests.TestData;
using Kros.KORM.Query.MsAccess;
using NBench;
using System.Data.SqlClient;
using System.Linq;

namespace Kros.KORM.PerformanceTests.ModelBuilderTests
{
    public class BigDataPerformanceTest
    {
        private Counter _counter;

        private IDatabase _database;

        [PerfSetup]
        public void Setup(BenchmarkContext context)
        {
            MsAccessQueryProviderFactory.Register();
            _counter = context.GetCounter("ItemsCounter");
            _database = new Database(new SqlConnection());
        }

        [PerfBenchmark(TestMode = TestMode.Test, NumberOfIterations = 5)]
        [CounterMeasurement("ItemsCounter")]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 1000)]
        public void DataToListTest()
        {
            var bigDataTable = EmployeeRepository.Instance.GetBigData();

            var employees = _database.ModelBuilder.Materialize<Employee>(bigDataTable).ToList();

            foreach (var employee in employees)
            {
                _counter.Increment();
            }
        }

        [PerfBenchmark(TestMode = TestMode.Test, NumberOfIterations = 10)]
        [CounterMeasurement("ItemsCounter")]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 25)]
        public void WhereTakeOnly5000Items()
        {
            var bigDataTable = EmployeeRepository.Instance.GetBigData();

            var employees = _database.ModelBuilder.Materialize<Employee>(bigDataTable).Take(5000);

            foreach (var employee in employees)
            {
                _counter.Increment();
            }
        }

        [PerfBenchmark(TestMode = TestMode.Test, NumberOfIterations = 10)]
        [CounterMeasurement("ItemsCounter")]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 50)]
        public void ExistAnyWithKey()
        {
            var bigDataTable = EmployeeRepository.Instance.GetBigData();

            var exist = _database.ModelBuilder.Materialize<Employee>(bigDataTable).Any(p => p.Id == 10000);
            if (exist)
            {
                _counter.Increment();
            }
        }
    }
}
