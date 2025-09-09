using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xUnit_testing_REST_api.Datas;
using xUnit_testing_REST_api.Models;
using xUnit_testing_REST_api.Services.Interfaces;

namespace xUnit_testing_REST_api.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public EmployeeService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            var newEmployee = new Employee
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Title = employee.Title,
                BirthDate = employee.BirthDate,
                HireDate = employee.HireDate,
                Address = employee.Address,
                City = employee.City,
                Region = employee.Region,
                PostalCode = employee.PostalCode,
                Country = employee.Country,
                HomePhone = employee.HomePhone,
                Extension = employee.Extension
            };
            await _applicationDbContext.Employees.AddAsync(newEmployee);
            await _applicationDbContext.SaveChangesAsync();
            return newEmployee;
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            var employee = await _applicationDbContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return false;
            }
            _applicationDbContext.Employees.Remove(employee);
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            return await _applicationDbContext.Employees.FindAsync(id);
        }

        public async Task<List<Employee>> GetEmployees()
        {
            return await _applicationDbContext.Employees.ToListAsync();
        }

        public async Task<Employee> UpdateEmployee(int id, Employee employee)
        {
            var existingEmployee = await _applicationDbContext.Employees.FindAsync(id);
            if (existingEmployee == null)
            {
                return null;
            }
            existingEmployee.FirstName = employee.FirstName;
            existingEmployee.LastName = employee.LastName;
            existingEmployee.Title = employee.Title;
            existingEmployee.BirthDate = employee.BirthDate;
            existingEmployee.HireDate = employee.HireDate;
            existingEmployee.Address = employee.Address;
            existingEmployee.City = employee.City;
            existingEmployee.Region = employee.Region;
            existingEmployee.PostalCode = employee.PostalCode;
            existingEmployee.Country = employee.Country;
            existingEmployee.HomePhone = employee.HomePhone;
            existingEmployee.Extension = employee.Extension;
            _applicationDbContext.Employees.Update(existingEmployee);
            await _applicationDbContext.SaveChangesAsync();
            return existingEmployee;
        }
    }
}
