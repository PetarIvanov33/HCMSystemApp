# HCMSystemApp – Human Capital Management System

## Overview

**HCMSystemApp** is a full-featured Human Capital Management (HCM) web application built with ASP.NET Core MVC.

The system is designed to manage employees, departments, salaries, payrolls, and vacations within an organization, providing role-based access for HR administrators, managers, and employees.

It focuses on clean architecture, real-world business logic, and scalable backend design.

---

## ✨ Key Features

### 👥 User Management
- New user registration (requires HR Admin approval)
- HR Admin approves users and assigns roles, departments, and initial salary
- Authentication and authorization via ASP.NET Core Identity
- User roles: `Employee`, `Manager`, `HR Admin`

### 🏢 Departments
- Each department has:
  - A name
  - One manager
  - A collection of employees
- Managers can manage employees in their own department

### 💰 Salaries & Payroll
- `Salary` table stores the current salary of each user
- Managers can change the salaries of employees in their department
- Only HR Admin can change the salaries of managers
- `Payroll` table stores monthly payslips with:
  - Bonus
  - Tax amount
  - Net amount
  - Period and issue date

### 🏖️ Vacations
- Employees and managers can request vacations
- Vacation requests are approved or declined by the responsible party (Manager or HR Admin)
- Vacation records are managed within the system as part of the overall employee workflow

---

## 🧱 Technologies

- **Backend:** ASP.NET Core 8, C#
- **Frontend:** ASP.NET Core MVC, Razor Pages, Bootstrap
- **Database:** MS SQL Server + Entity Framework Core
- **Authentication:** ASP.NET Core Identity
- **Architecture:** Layered architecture + Repository Pattern
- **Seeding:** Configuration files for roles, users, departments, and related entities

---

## 🧠 Architecture Highlights

- Layered architecture with clear separation between presentation, business logic, and data access
- Clear separation of concerns between services, repositories, and UI
- Repository pattern for data abstraction
- Service layer for business logic
- ASP.NET Core Identity integration for authentication and authorization
- Entity Framework Core for ORM and database management
- Seed data configuration for fast local setup and testing

---

## 📁 Project Structure

```text
HCMSystemApp/
├── HCMSystemApp.Web            # ASP.NET Core MVC (UI layer)
├── HCMSystemApp.Core           # Interfaces, DTOs, Services, Models, Identity models
├── HCMSystemApp.Infrastructure # Repositories, DbContext, Entity models, Seed data
```

---

## ⚙️ Setup Instructions

1. **Clone the repository:**

```bash
git clone https://github.com/PetarIvanov33/HCMSystemApp.git
```

2. **Apply migrations and initialize the database:**

```bash
cd HCMSystemApp.Web
dotnet ef database update
```

3. **Run the application:**

```bash
dotnet run
```

4. **Seed Data:**
- `HR Admin` user is seeded automatically
- Test roles: `Employee`, `Manager`, `HR Admin`
- Sample departments, employees, payrolls, salaries, and managers are included

---

## 🧪 Test Seed Users

The application comes with pre-seeded test users for each role.  
You can use the following credentials to log in and test the system:

| Role | Username | Password |
|------|----------|----------|
| 👑 **HR Admin** | `admin@example.com` | `123456!` |
| 📂 **Manager** | `manager@example.com` | `123456!` |
| 👤 **Employee** | `employee1@example.com` | `123456!` |
| 👤 **Employee** | `employee2@example.com` | `123456!` |

---

## 🔐 Roles & Permissions

| Role | Permissions |
|------|-------------|
| **HR Admin** | Approves users, assigns roles, departments, salaries, and edits managers |
| **Manager** | Manages employees in their own department, issues payrolls, and handles vacation-related actions |
| **Employee** | Views profile, accesses payslips, and interacts with personal system features |

---

## 🗃️ Core Tables

- `User` (Identity)
- `Role` (Identity)
- `UserRole` (Identity)
- `Department`
- `Employee`
- `Manager`
- `Salary`
- `Payroll`
- `Vacation`

---

## 🛠️ Seed Configuration

```csharp
modelBuilder.ApplyConfiguration(new RoleConfiguration());
modelBuilder.ApplyConfiguration(new UserConfiguration());
modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
modelBuilder.ApplyConfiguration(new SalaryConfiguration());
modelBuilder.ApplyConfiguration(new PayrollConfiguration());
modelBuilder.ApplyConfiguration(new VacationConfiguration());
```

---

## 🔄 User Approval Workflow

1. User registers in the system
2. HR Admin reviews the pending account
3. HR Admin assigns:
   - Role (`Employee` or `Manager`)
   - Department
   - Starting salary
4. The user receives access based on the assigned role and permissions

---

## 🚀 Purpose of the Project

The purpose of this project is to demonstrate the implementation of a complete Human Capital Management system using modern .NET technologies and structured application architecture.

It is focused on solving real-world HR and employee management scenarios through role-based access, business logic separation, and scalable backend design.

---

## 🤝 Contributors

- **Petar Ivanov** – Full system design, backend development, database architecture, authentication, business logic, and UI implementation

---

## 📄 License

This is a personal project, fully designed and developed by me as part of my initiative to build a complete Human Capital Management (HCM) system.

The goal of the project is to demonstrate practical skills in designing scalable web applications, working with real-world business logic, and implementing clean architecture using ASP.NET Core, Entity Framework Core, and SQL Server.

It includes features such as employee management, department organization, payroll processing, vacation management with request and approval workflows, and role-based access control.

This project reflects my own ideas, implementation decisions, and understanding of backend development concepts.
