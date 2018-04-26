using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kros.KORM.PerformanceTests.Model
{
    public class Employee
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Mail { get; set; }

        public int Age { get; set; }

        public double Salary { get; set; }

        public string Address { get; set; }

        public int EmploymentYearsCount { get; set; }

        public int SupervisorId { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime Date { get; set; }

        public Guid IdMaster { get; set; }

        public string Note { get; set; }
        public string Note1 { get; set; }
        public string Note2 { get; set; }

        public Guid Guid1 { get; set; }
        public Guid Guid2 { get; set; }
        public Guid Guid3 { get; set; }
        public Guid Guid4 { get; set; }

        public double Double1 { get; set; }
        public double Double2 { get; set; }
        public double Double3 { get; set; }
        public double Double4 { get; set; }
    }
}
