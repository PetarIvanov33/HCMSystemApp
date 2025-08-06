# HCMSystemApp

## Overview

**HCMSystemApp** (Human Capital Management System App) is an ASP.NET Core MVC web application designed to manage human resources in an organization. It provides a centralized platform for administrating employees, managers, departments, salaries, vacations, and payrolls. The system supports role-based access and features for HR admins, managers, and employees.

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
- Vacation requests are approved/declined by the responsible party (Manager or HR Admin)

---

## 🧱 Technologies

- **Backend:** ASP.NET Core 8, C#
- **Frontend:** ASP.NET Core MVC, Razor Pages, Bootstrap
- **Database:** MS SQL Server + Entity Framework Core
- **Authentication:** ASP.NET Identity
- **Architecture:** Layered architecture + Repository Pattern
- **Seeding:** Configuration files for roles, users, departments, etc.

---

## 📁 Project Structure

```
HCMSystemApp/
├── HCMSystemApp.Web           # ASP.NET Core MVC (UI layer)
├── HCMSystemApp.Core          # Interfaces, DTOs, Services, Models, Identity models
├── HCMSystemApp.Infrastructure# Repositories, DbContext, Entity models, Seed data
```

---

## ⚙️ Setup Instructions

1. **Clone the repository:**

```bash
git clone [https://github.com/your-username/HCMSystemApp.git](https://github.com/PetarIvanov33/HCMSystemApp)
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
   - Test roles: Employee, Manager, HR Admin
   - Sample departments, employees, and managers included

---

## 🧪 Test Seed Users

The application comes with pre-seeded test users for each role.  
You can use the following credentials to log in and test the system:

| Role         | Username                | Password  |
|--------------|-------------------------|-----------|
| 👑 **HR Admin** | `admin@example.com`     | `123456!` |
| 📂 **Manager**  | `manager@example.com`   | `123456!` |
| 👤 **Employee** | `employee1@example.com` | `123456!` |
| 👤 **Employee** | `employee2@example.com` | `123456!` |

---

## 🔐 Roles & Permissions

| Role       | Permissions                                                                 |
|------------|------------------------------------------------------------------------------|
| HR Admin   | Approves users, assigns roles, departments, salaries, edits managers         |
| Manager    | Manages employees in own department, issues payrolls, approves vacations     |
| Employee   | Views profile, requests vacations, views payslips                            |

---

## 🗃️ Core Tables

- `User` (Identity)
- `Role` (Identity)
- `UserRole` (Identity)
- `Department`
- `Employee`, `Manager` (extensions of User)
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

1. User registers
2. HR Admin reviews pending account
3. HR Admin assigns:
   - Role (Employee or Manager)
   - Department (if Employee → assigned to existing Manager)
   - Starting salary
4. User receives access based on assigned role

---

## 🤝 Contributors

- Petar Ivanov – Architecture, Backend, Identity, UI

---

## 📄 License

This project is part of an internal assignment under UKG Human Capital Management and is not intended for public distribution.
