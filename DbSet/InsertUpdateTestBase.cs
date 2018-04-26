using Kros.Data.MsAccess;
using Kros.KORM.PerformanceTests.Model;
using Kros.KORM.Query.MsAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Kros.KORM.PerformanceTests.DbSetTests
{
    /// <summary>
    /// Helper for insert, update performance tests.
    /// </summary>
    public class InsertUpdateTestBase
    {

        #region Private fields

        private System.Data.Common.DbConnection _connection;
        private string _templateMdbName;

        #endregion


        #region Public properties


        private string _currentFolder;

        /// <summary>
        /// Currently used folder.
        /// </summary>
        private string CurrentFolder
        {
            get
            {
                if (string.IsNullOrEmpty(_currentFolder))
                {
                    _currentFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                }

                return _currentFolder;
            }
        }


        private string _tempFolder;

        /// <summary>
        /// Temporary folder.
        /// </summary>
        private string TempFolder
        {
            get
            {
                if (string.IsNullOrEmpty(_tempFolder))
                {
                    _tempFolder = Path.Combine(CurrentFolder, Helper.InsertUpdateTestTempFolder);
                }

                return _tempFolder;
            }
        }


        private string _databaseFile;

        /// <summary>
        /// Database full path with file name in temporary folder.
        /// </summary>
        private string DatabaseFile
        {
            get
            {
                if (string.IsNullOrEmpty(_databaseFile))
                {
                    _databaseFile = Path.Combine(TempFolder, $"{_templateMdbName}_{DateTime.Now.ToString("HHmmssfff")}.mdb");
                }

                return _databaseFile;
            }
        }


        private Query.IDbSet<Employee> _dbSet;

        /// <summary>
        /// DbSet.
        /// </summary>
        public Query.IDbSet<Employee> DbSet
        {
            get
            {
                if (_dbSet == null)
                {
                    _dbSet = Database.Query<Employee>().AsDbSet();
                }

                return _dbSet;
            }
            set
            {
                _dbSet = value;
            }
        }


        private IDatabase _database;

        /// <summary>
        /// Database.
        /// </summary>
        public IDatabase Database
        {
            get
            {
                return _database;
            }
        }

        #endregion


        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="templateMdbName">Template Mdb name.</param>
        public InsertUpdateTestBase(string templateMdbName)
        {
            Helper.CheckMsAccessProvider();
            MsAccessQueryProviderFactory.Register();
            _templateMdbName = templateMdbName;
        }

        #endregion


        /// <summary>
        /// Load data from repository.
        /// </summary>
        /// <param name="itemsCount">The number of loaed items.</param>
        /// <returns></returns>
        public IEnumerable<Employee> LoadDataFromRepository(int itemsCount)
        {
            var bigDataTable = TestData.EmployeeRepository.Instance.GetBigData();
            return _database.ModelBuilder.Materialize<Employee>(bigDataTable).Take(itemsCount);
        }


        /// <summary>
        /// Add items from collection into the DbSet as Added items.
        /// </summary>
        /// <param name="itemsCollection">Collection of items.</param>
        public void AddItemsIntoDbSetAsAdded(IEnumerable<Employee> itemsCollection)
        {
            foreach (Employee item in itemsCollection)
            {
                DbSet.Add(item);
            }
        }


        /// <summary>
        /// Add items from collection into the DbSet as Edited items.
        /// </summary>
        /// <param name="itemsCollection">Collection of items.</param>
        public void AddIntoDbSetAsEdited(IEnumerable<Employee> itemsCollection)
        {
            foreach (Employee item in itemsCollection)
            {
                DbSet.Edit(EditEmployee(item));
            }
        }


        private Employee EditEmployee(Employee employee)
        {
            employee.Address = employee.Address.ToUpper();
            employee.Age += 10;
            employee.Double1 *= 1.1;
            employee.Double2 *= 1.2;
            employee.Double3 *= 1.3;
            employee.Double4 *= 1.4;
            employee.EmploymentYearsCount += 2;
            employee.LastName += "ová";
            employee.Salary *= 2.2;

            return employee;
        }


        /// <summary>
        /// Create temporary folder.
        /// </summary>
        public void CreateTempFolder()
        {
            if (!Directory.Exists(TempFolder))
            {
                Directory.CreateDirectory(TempFolder);
            }
        }


        /// <summary>
        /// Copy template database to temporary folder.
        /// </summary>
        public void CopyTemplateDBToTempFolder()
        {
            string templateDatabaseFile = Path.Combine(CurrentFolder, $"Resources\\{_templateMdbName}.mdb");
            string templateSystemMdwFile = Path.Combine(CurrentFolder, $"Resources\\System.mdw");

            File.Copy(templateDatabaseFile, DatabaseFile, true);
            File.SetAttributes(DatabaseFile, FileAttributes.Temporary);
        }


        /// <summary>
        /// Connect to template database.
        /// </summary>
        public void ConnectToTemplateDb()
        {
            _connection = GetNewConnection();
            _connection.Open();
            _database = new Database(_connection);
        }


        private System.Data.Common.DbConnection GetNewConnection()
        {
            return new System.Data.OleDb.OleDbConnection(GetConnectionString());
        }


        private string GetConnectionString()
        {
            return string.Format(Helper.TemplateMDBConnectionString, MsAccessDataHelper.MsAccessAceProvider, DatabaseFile);
        }


        /// <summary>
        /// Commit changes.
        /// </summary>
        public void CommitChanges()
        {
            DbSet.CommitChanges();
        }


        /// <summary>
        /// Cleanup.
        /// </summary>
        public void Clear()
        {
            _connection.Close();
            _connection.Dispose();
        }
    }
}
