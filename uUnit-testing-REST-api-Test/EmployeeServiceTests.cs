using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using xUnit_testing_REST_api.Datas;
using xUnit_testing_REST_api.Models;
using xUnit_testing_REST_api.Services;

namespace uUnit_testing_REST_api_Test
{
    public class EmployeeServiceTests
    {
        private ApplicationDbContext GetDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task AddEmployee_ShouldAddEmployee()
        {
            using var context = GetDbContext("AddEmployeeDb");
            var service = new EmployeeService(context);

            var result = await service.AddEmployee(new Employee { FirstName = "John" });

            Assert.Equal(1, await context.Employees.CountAsync());
            Assert.Equal("John", (await context.Employees.FirstAsync()).FirstName);
        }

        [Fact]
        public async Task GetEmployeeById_ShouldReturnEmployee()
        {
            using var context = GetDbContext("GetByIdDb");
            context.Employees.Add(new Employee { EmployeeId = 1, FirstName = "John" });
            context.SaveChanges();

            var service = new EmployeeService(context);

            var employee = await service.GetEmployeeById(1);

            Assert.NotNull(employee);
            Assert.Equal("John", employee.FirstName);
        }

        [Fact]
        public async Task DeleteEmployee_ShouldRemoveEmployee()
        {
            using var context = GetDbContext("DeleteDb");
            context.Employees.Add(new Employee { EmployeeId = 1, FirstName = "John" });
            context.SaveChanges();

            var service = new EmployeeService(context);

            var result = await service.DeleteEmployee(1);

            Assert.True(result);
            Assert.Empty(context.Employees);
        }

        [Fact]
        public async Task GetEmployees_ShouldReturnAll()
        {
            using var context = GetDbContext("GetAllDb");
            context.Employees.AddRange(
                new Employee { EmployeeId = 1, FirstName = "John" },
                new Employee { EmployeeId = 2, FirstName = "Jane" });
            context.SaveChanges();

            var service = new EmployeeService(context);

            var employees = await service.GetEmployees();

            Assert.Equal(2, employees.Count);
        }
    }
}
