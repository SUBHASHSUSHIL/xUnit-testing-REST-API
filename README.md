# xUnit-testing-REST-api

This project demonstrates how to write **unit and integration tests** for a REST API built with **ASP.NET Core**, **Entity Framework Core**, and **xUnit**.

## 📌 Project Overview
- REST API with basic CRUD operations for `Employee`.
- Service layer implemented with **Entity Framework Core**.
- Tests written using **xUnit** framework.
- Two different testing strategies explored:
  - **Moq** (strict unit testing, mocking `DbContext` and `DbSet`)
  - **EF Core InMemory Database** (integration style testing with a real DbContext in memory)

---

## 🧪 Testing Approach

### 🔹 Using InMemoryDatabase
For most service tests, I used **Entity Framework Core’s InMemory provider**:

```csharp
var options = new DbContextOptionsBuilder<ApplicationDbContext>()
    .UseInMemoryDatabase("TestDb")
    .Options;

using var context = new ApplicationDbContext(options);
var service = new EmployeeService(context);

// Act
await service.AddEmployee(new Employee { FirstName = "John" });

// Assert
Assert.Equal(1, await context.Employees.CountAsync());
