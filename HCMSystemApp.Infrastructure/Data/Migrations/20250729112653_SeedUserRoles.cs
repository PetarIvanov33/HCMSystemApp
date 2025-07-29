using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HCMSystemApp.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedUserRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    { "79e1d63d-bbd0-4724-91f6-2ab694ebf4a9", 0, 28, "dee4cb20-2708-4157-81d4-3b16e91f6037", "employee1@example.com", true, "Ivan", "Ivanov", false, null, "EMPLOYEE1@EXAMPLE.COM", "EMPLOYEE1@EXAMPLE.COM", "AQAAAAIAAYagAAAAEDka60i8EkzCwptsksnq3pNjakru8gImgQ/dFb8XueckDkDyfKmrNBENLPyU94HhWw==", null, false, "0a8efbfe-862d-4b00-b5f7-aa976dcca757", false, "employee1@example.com" },
                    { "8d04dce2-969a-435d-bba4-df3f325983dc", 0, 40, "7a4fbda7-9962-48f6-89e6-ec3dab94a5e2", "admin@example.com", true, "Admin", "User", false, null, "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEDV5eH5e7XbQuhdVgA1GgJmIY6dNZfpYGFph6ZcwahhnhdB2cDSS7suPDCZs7xvkrQ==", null, false, "ad042688-4a54-4990-90d4-efa58c450e1a", false, "admin@example.com" },
                    { "d0bd2a23-6c8d-40b5-a476-b7992e7b50e1", 0, 30, "70dce1b8-0ccf-441e-a42d-ee0027ca9c47", "employee2@example.com", true, "Georgi", "Georgiev", false, null, "EMPLOYEE2@EXAMPLE.COM", "EMPLOYEE2@EXAMPLE.COM", "AQAAAAIAAYagAAAAENmSGl9igIBrAPyM7x26XpSmh60mW1IxBiRslm68Z7tM6TsTbYA2IJNIiEmfYGVdVQ==", null, false, "0302156a-9954-4bea-ac2e-b63287507a15", false, "employee2@example.com" },
                    { "f36fc003-dbd1-47b4-9dfd-45ec0f16f5d6", 0, 35, "bcf6f879-f36e-45f6-aa0a-729ffcec28f6", "manager@example.com", true, "Manager", "User", false, null, "MANAGER@EXAMPLE.COM", "MANAGER@EXAMPLE.COM", "AQAAAAIAAYagAAAAEDP50x+Hf9JPJsiBD6NIxlPJeppzoAvnu3zfIZHeLbSVDkZPCIuCkgH/k7DrSW8Zbw==", null, false, "9152701b-4977-4829-95fd-113c7481fb3c", false, "manager@example.com" }
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "9b325984-c63f-4dec-a00b-eeaab3d34035", "79e1d63d-bbd0-4724-91f6-2ab694ebf4a9" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "b4656095-c561-4bfa-a5ad-08f7678af1bf", "8d04dce2-969a-435d-bba4-df3f325983dc" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "9b325984-c63f-4dec-a00b-eeaab3d34035", "d0bd2a23-6c8d-40b5-a476-b7992e7b50e1" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a2196e3c-e72a-4778-994f-36c85380e060", "f36fc003-dbd1-47b4-9dfd-45ec0f16f5d6" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9b325984-c63f-4dec-a00b-eeaab3d34035");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a2196e3c-e72a-4778-994f-36c85380e060");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b4656095-c561-4bfa-a5ad-08f7678af1bf");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "79e1d63d-bbd0-4724-91f6-2ab694ebf4a9");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8d04dce2-969a-435d-bba4-df3f325983dc");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d0bd2a23-6c8d-40b5-a476-b7992e7b50e1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f36fc003-dbd1-47b4-9dfd-45ec0f16f5d6");
        }
    }
}
