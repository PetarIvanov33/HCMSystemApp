using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HCMSystemApp.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Managers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Managers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Salary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Salary_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vacations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    IsReviewed = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vacations_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ManagerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_Managers_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Managers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payrolls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Period = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IssuedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Bonus = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SalaryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payrolls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payrolls_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payrolls_Salary_SalaryId",
                        column: x => x.SalaryId,
                        principalTable: "Salary",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9b325984-c63f-4dec-a00b-eeaab3d34035", null, "", "Employee", "EMPLOYEE" },
                    { "a2196e3c-e72a-4778-994f-36c85380e060", null, "", "Manager", "MANAGER" },
                    { "b4656095-c561-4bfa-a5ad-08f7678af1bf", null, "", "HRAdmin", "HRADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Age", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "79e1d63d-bbd0-4724-91f6-2ab694ebf4a9", 0, 28, "7236241f-9c4c-4d8d-8b26-a2a7ec6778d8", "employee1@example.com", true, "Ivan", "Ivanov", false, null, "EMPLOYEE1@EXAMPLE.COM", "EMPLOYEE1@EXAMPLE.COM", "AQAAAAIAAYagAAAAEHCFmqg5jtBbVIat6+mHKU77rcJnKRbtSWirulVshiqgZmS2GMeZT/hbThRhBNQWmw==", "0881111111", false, "41fc5ac2-5742-447f-85cf-5d2c8740fa42", false, "employee1@example.com" },
                    { "8d04dce2-969a-435d-bba4-df3f325983dc", 0, 40, "14a986db-7aaa-4bea-8281-8ed983a91716", "admin@example.com", true, "Admin", "User", false, null, "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEE+RpsQ9ytKSs0WQ8ICGRFoNk9nDoPU4uq9b7C6EQ/mPQXuY72DvFsXOdN9hDX74nQ==", "0123456789", false, "8f1dff50-04db-40c2-b453-046d42fc9d8d", false, "admin@example.com" },
                    { "d0bd2a23-6c8d-40b5-a476-b7992e7b50e1", 0, 30, "e2bd504a-d1ac-43b9-8696-bef608b0727b", "employee2@example.com", true, "Georgi", "Georgiev", false, null, "EMPLOYEE2@EXAMPLE.COM", "EMPLOYEE2@EXAMPLE.COM", "AQAAAAIAAYagAAAAEGhp7EWgWIw/r2RAXLNya0cHwbB7EyL5AiZ4v303Nr8ewVR+RmAgW4ueM6GC7bhVxQ==", "0882222222", false, "72fa0f83-ae62-4d13-9e9f-9fc0aa65af2f", false, "employee2@example.com" },
                    { "f36fc003-dbd1-47b4-9dfd-45ec0f16f5d6", 0, 35, "b10763cd-d4b5-4332-ba0a-9f6523a976c1", "manager@example.com", true, "Manager", "User", false, null, "MANAGER@EXAMPLE.COM", "MANAGER@EXAMPLE.COM", "AQAAAAIAAYagAAAAEMKlnpO0Q+xmBLolERzrghKk23JvSYU4Cab6gXhnvA2XrQB2hRDyrSApN2xuER669A==", "0881234567", false, "eb5172c2-15c0-4def-98ee-62e4bce6c962", false, "manager@example.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "9b325984-c63f-4dec-a00b-eeaab3d34035", "79e1d63d-bbd0-4724-91f6-2ab694ebf4a9" },
                    { "b4656095-c561-4bfa-a5ad-08f7678af1bf", "8d04dce2-969a-435d-bba4-df3f325983dc" },
                    { "9b325984-c63f-4dec-a00b-eeaab3d34035", "d0bd2a23-6c8d-40b5-a476-b7992e7b50e1" },
                    { "a2196e3c-e72a-4778-994f-36c85380e060", "f36fc003-dbd1-47b4-9dfd-45ec0f16f5d6" }
                });

            migrationBuilder.InsertData(
                table: "Managers",
                columns: new[] { "Id", "UserId" },
                values: new object[] { 1, "f36fc003-dbd1-47b4-9dfd-45ec0f16f5d6" });

            migrationBuilder.InsertData(
                table: "Salary",
                columns: new[] { "Id", "Amount", "UserId" },
                values: new object[,]
                {
                    { 1, 5500.00m, "f36fc003-dbd1-47b4-9dfd-45ec0f16f5d6" },
                    { 2, 3200.00m, "79e1d63d-bbd0-4724-91f6-2ab694ebf4a9" },
                    { 3, 3400.00m, "d0bd2a23-6c8d-40b5-a476-b7992e7b50e1" }
                });

            migrationBuilder.InsertData(
                table: "Vacations",
                columns: new[] { "Id", "EndDate", "IsApproved", "IsReviewed", "Reason", "StartDate", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 5, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, "Почивка в Гърция", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "79e1d63d-bbd0-4724-91f6-2ab694ebf4a9" },
                    { 2, new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, "Семейна почивка", new DateTime(2025, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "d0bd2a23-6c8d-40b5-a476-b7992e7b50e1" },
                    { 3, new DateTime(2025, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, "Лични ангажименти", new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "f36fc003-dbd1-47b4-9dfd-45ec0f16f5d6" }
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "ManagerId", "Name" },
                values: new object[] { 1, 1, "Software Development" });

            migrationBuilder.InsertData(
                table: "Payrolls",
                columns: new[] { "Id", "Bonus", "IssuedOn", "NetAmount", "Period", "SalaryId", "TaxAmount", "UserId" },
                values: new object[,]
                {
                    { 1, 500.00m, new DateTime(2025, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 4750.00m, "01-2025", 1, 750.00m, "f36fc003-dbd1-47b4-9dfd-45ec0f16f5d6" },
                    { 2, 200.00m, new DateTime(2025, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 2920.00m, "01-2025", 2, 480.00m, "79e1d63d-bbd0-4724-91f6-2ab694ebf4a9" },
                    { 3, 250.00m, new DateTime(2025, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 3140.00m, "01-2025", 3, 510.00m, "d0bd2a23-6c8d-40b5-a476-b7992e7b50e1" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "DepartmentId", "Position", "UserId" },
                values: new object[,]
                {
                    { 1, 1, "Junior .Net Developer", "79e1d63d-bbd0-4724-91f6-2ab694ebf4a9" },
                    { 2, 1, "Sinior full stack Developer", "d0bd2a23-6c8d-40b5-a476-b7992e7b50e1" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_ManagerId",
                table: "Departments",
                column: "ManagerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserId",
                table: "Employees",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_UserId",
                table: "Managers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Payrolls_SalaryId",
                table: "Payrolls",
                column: "SalaryId");

            migrationBuilder.CreateIndex(
                name: "IX_Payrolls_UserId",
                table: "Payrolls",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Salary_UserId",
                table: "Salary",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vacations_UserId",
                table: "Vacations",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Payrolls");

            migrationBuilder.DropTable(
                name: "Vacations");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Salary");

            migrationBuilder.DropTable(
                name: "Managers");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
