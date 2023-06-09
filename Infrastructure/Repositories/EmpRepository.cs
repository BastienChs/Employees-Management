﻿using Domain.Models;
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
    public class EmpRepository : IEmpRepository
    {
        private readonly EntrepriseDatabaseSettings _dbSettings;
        private readonly MySqlConnection _db;
        public EmpRepository(IOptions<EntrepriseDatabaseSettings> dbSettings)
        {
            _dbSettings = dbSettings.Value;
            _db = new MySqlConnection(_dbSettings.ConnectionString);
        }

        public Task<Employee> AddEmployeeAsync(Employee employee)
        {
            
            _db.Open();
            var cmd = _db.CreateCommand();
            cmd.CommandText = "INSERT INTO emp (ENAME, JOB, MGR, HIREDATE, SAL, COMM, DEPTNO) VALUES (@name, @job, @managerId, @hireDate, @salary, @commission, @departmentId)";
            cmd.Parameters.AddWithValue("@name", employee.Name);
            cmd.Parameters.AddWithValue("@job", employee.JobTitle);
            cmd.Parameters.AddWithValue("@managerId", employee.ManagerId);
            cmd.Parameters.AddWithValue("@hireDate", employee.HireDate);
            cmd.Parameters.AddWithValue("@salary", employee.Salary);
            cmd.Parameters.AddWithValue("@commission", employee.Commission);
            cmd.Parameters.AddWithValue("@departmentId", employee.DepartmentId);
            cmd.ExecuteNonQuery();
            _db.Close();
            return Task.FromResult(employee);
        }

        public Task<Employee> DeleteEmployeeAsync(int id)
        {
            return null;
            //var employee = _context.Employees.Find(id);
            //if (employee == null)
            //{
            //    return null;
            //}
            //else
            //{
            //    _context.Employees.Remove(employee);
            //    _context.SaveChanges();
            //    return Task.FromResult(employee);
            //}
        }

        public Task<Employee> GetEmployeeAsync(int id)
        {
            //Retrieve an Employee by its id
            _db.Open();
            var cmd = _db.CreateCommand();
            cmd.CommandText = "SELECT * FROM emp WHERE EMPNO = @id";
            cmd.Parameters.AddWithValue("@id", id);
            var reader = cmd.ExecuteReader();
            if(!reader.HasRows)
            {
                _db.Close();
                throw new Exception("Employee not found");
            }
            var employee = new Employee();
            while (reader.Read())
            {
                employee.Id = reader.GetInt32("EMPNO");
                employee.Name = reader.GetString("ENAME");
                employee.JobTitle = reader.GetString("JOB");
                employee.ManagerId = reader.IsDBNull(reader.GetOrdinal("MGR")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("MGR"));
                employee.HireDate = reader.GetDateTime("HIREDATE");
                employee.Salary = reader.GetInt32("SAL");
                employee.Commission = reader.IsDBNull(reader.GetOrdinal("COMM")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("COMM"));
                employee.DepartmentId = reader.GetInt32("DEPTNO");
            }
            _db.Close();
            return Task.FromResult(employee);
        }

        public Task<List<Employee>> GetEmployeesAsync()
        {
            //First we want to retrieve all of the employees from the database which are stored in the 'emp' table.
            _db.Open();
            var cmd = _db.CreateCommand();
            cmd.CommandText = "SELECT * FROM emp";
            var reader = cmd.ExecuteReader();
            var employees = new List<Employee>();

            while (reader.Read())
            {
                var employee = new Employee
                {
                    Id = reader.GetInt32("EMPNO"),
                    Name = reader.GetString("ENAME"),
                    JobTitle = reader.GetString("JOB"),
                    ManagerId = reader.IsDBNull(reader.GetOrdinal("MGR")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("MGR")),
                    HireDate = reader.GetDateTime("HIREDATE"),
                    Salary = reader.GetInt32("SAL"),
                    Commission = reader.IsDBNull(reader.GetOrdinal("COMM")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("COMM")),
                    DepartmentId = reader.GetInt32("DEPTNO"),
                };
                employees.Add(employee);
            }
            _db.Close();
            return Task.FromResult(employees);
        }

        public Task<List<Employee>> GetEmployeesByManagerIdAsync(int managerId)
        {
            _db.Open();
            var cmd = _db.CreateCommand();
            cmd.CommandText = "SELECT * FROM emp WHERE MGR = @managerId";
            cmd.Parameters.AddWithValue("@managerId", managerId);
            var reader = cmd.ExecuteReader();
            var employees = new List<Employee>();
            while (reader.Read())
            {
                var employee = new Employee
                {
                    Id = reader.GetInt32("EMPNO"),
                    Name = reader.GetString("ENAME"),
                    JobTitle = reader.GetString("JOB"),
                    ManagerId = reader.IsDBNull(reader.GetOrdinal("MGR")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("MGR")),
                    HireDate = reader.GetDateTime("HIREDATE"),
                    Salary = reader.GetInt32("SAL"),
                    Commission = reader.IsDBNull(reader.GetOrdinal("COMM")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("COMM")),
                    DepartmentId = reader.GetInt32("DEPTNO"),
                };
                employees.Add(employee);
            }
            _db.Close();
            return Task.FromResult(employees);
        }

        public Task<Employee> UpdateEmployeeAsync(Employee employee)
        {
            _db.Open();
            var cmd = _db.CreateCommand();
            cmd.CommandText = "UPDATE emp SET ENAME = @name, JOB = @job, MGR = @managerId, HIREDATE = @hireDate, SAL = @salary, COMM = @commission, DEPTNO = @departmentId WHERE EMPNO = @id";
            cmd.Parameters.AddWithValue("@id", employee.Id);
            cmd.Parameters.AddWithValue("@name", employee.Name);
            cmd.Parameters.AddWithValue("@job", employee.JobTitle);
            cmd.Parameters.AddWithValue("@managerId", employee.ManagerId);
            cmd.Parameters.AddWithValue("@hireDate", employee.HireDate);
            cmd.Parameters.AddWithValue("@salary", employee.Salary);
            cmd.Parameters.AddWithValue("@commission", employee.Commission);
            cmd.Parameters.AddWithValue("@departmentId", employee.DepartmentId);
            cmd.ExecuteNonQuery();
            _db.Close();
            return Task.FromResult(employee);
        }
    }
}
