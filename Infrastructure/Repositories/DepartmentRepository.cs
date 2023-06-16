using Domain.Models;
using Domain.Repositories;
using Infrastructure.Common;
using Microsoft.Extensions.Options;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class DeptRepository : IDeptRepository
    {
        private readonly EntrepriseDatabaseSettings _dbSettings;
        private readonly MySqlConnection _db;
        public DeptRepository(IOptions<EntrepriseDatabaseSettings> dbSettings)
        {
            _dbSettings = dbSettings.Value;
            _db = new MySqlConnection(_dbSettings.ConnectionString);
        }

        public async Task<List<Department>> GetDepartmentsAsync()
        {
            //Retrieve all Departments
            _db.Open();
            var cmd = _db.CreateCommand();
            cmd.CommandText = "SELECT * FROM dept";
            var reader = await cmd.ExecuteReaderAsync();
            var departments = new List<Department>();
            while (await reader.ReadAsync())
            {
                var department = new Department();
                department.Id = reader.GetInt32("DEPTNO");
                department.Name = reader.GetString("DNAME");
                department.LocationCity = reader.GetString("LOC");
                departments.Add(department);
            }
            _db.Close();
            return departments;
        }
    }
}
