using Kros.KORM.PerformanceTests.Model;
using Kros.KORM.PerformanceTests.TestData;
using Kros.KORM.Query.MsAccess;
using NBench;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace Kros.KORM.PerformanceTests.ModelBuilder
{
    public class NormalDataPerformanceTest
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

        [PerfBenchmark(TestMode = TestMode.Test, NumberOfIterations = 20)]
        [CounterMeasurement("ItemsCounter")]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 17)]
        public void DataToListTest()
        {
            var bigDataTable = EmployeeRepository.Instance.NormalSizeData(1500);

            var employees = _database.ModelBuilder.Materialize<Employee>(bigDataTable).ToList();

            foreach (var employee in employees)
            {
                _counter.Increment();
            }
        }

        [PerfBenchmark(TestMode = TestMode.Test, NumberOfIterations = 20)]
        [CounterMeasurement("ItemsCounter")]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 4)]
        public void Data200ToListTest()
        {
            var bigDataTable = EmployeeRepository.Instance.NormalSizeData(200);

            var employees = _database.ModelBuilder.Materialize<Employee>(bigDataTable).ToList();

            foreach (var employee in employees)
            {
                _counter.Increment();
            }
        }
    }
}
