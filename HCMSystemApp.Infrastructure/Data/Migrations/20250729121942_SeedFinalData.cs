using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HCMSystemApp.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedFinalData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "79e1d63d-bbd0-4724-91f6-2ab694ebf4a9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6a7a8a5e-1d48-4d4c-bafc-2de2a6cfb616", "AQAAAAIAAYagAAAAEHG4FReGRiUWYK5rqD32mi4GE6TCFDvH9Zjyyh2wv/4VPYLk18tAHYvk4GYwhy4raQ==", "b7839534-e496-4b51-9042-508ca7f1a80d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8d04dce2-969a-435d-bba4-df3f325983dc",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3dd4d1fd-4782-48dd-a702-9b8a5fab91ba", "AQAAAAIAAYagAAAAEH7a1BuhkpyMGvwZNMCkX4HK+0lzQmVevj243Gz46XI8ar5XD2r6KxXweIVXSm3+pg==", "3ceb8bee-079b-4766-bacd-e69aa6148ca6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d0bd2a23-6c8d-40b5-a476-b7992e7b50e1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c6f4cb3e-c122-4348-a660-08edf67c0aff", "AQAAAAIAAYagAAAAEABwwsdELH95kweURbBYaZHwIsRwyb4qpsrpFLMuHJxByjnAfflTXGOeRqDp9GqLRg==", "a5fdc2e8-07a4-4f9b-ba95-bf4a464a2e08" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f36fc003-dbd1-47b4-9dfd-45ec0f16f5d6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ab1d0437-3698-4110-b40d-ffd68d87f1fa", "AQAAAAIAAYagAAAAENSVXS8EzrNPNAseJfIbgwSLQav95b3RTRCMyqqIIk6s6eSKLCdczRFPVtObyB8FZw==", "d7e3ecc5-90ad-4cf7-8c57-20c0bb442e83" });

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
                columns: new[] { "Id", "EndDate", "IsApproved", "Reason", "StartDate", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 5, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Почивка в Гърция", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "79e1d63d-bbd0-4724-91f6-2ab694ebf4a9" },
                    { 2, new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Семейна почивка", new DateTime(2025, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "d0bd2a23-6c8d-40b5-a476-b7992e7b50e1" },
                    { 3, new DateTime(2025, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Лични ангажименти", new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "f36fc003-dbd1-47b4-9dfd-45ec0f16f5d6" }
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Payrolls",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Payrolls",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Payrolls",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Vacations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Vacations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Vacations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Managers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Salary",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Salary",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Salary",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "79e1d63d-bbd0-4724-91f6-2ab694ebf4a9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dee4cb20-2708-4157-81d4-3b16e91f6037", "AQAAAAIAAYagAAAAEDka60i8EkzCwptsksnq3pNjakru8gImgQ/dFb8XueckDkDyfKmrNBENLPyU94HhWw==", "0a8efbfe-862d-4b00-b5f7-aa976dcca757" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8d04dce2-969a-435d-bba4-df3f325983dc",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7a4fbda7-9962-48f6-89e6-ec3dab94a5e2", "AQAAAAIAAYagAAAAEDV5eH5e7XbQuhdVgA1GgJmIY6dNZfpYGFph6ZcwahhnhdB2cDSS7suPDCZs7xvkrQ==", "ad042688-4a54-4990-90d4-efa58c450e1a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d0bd2a23-6c8d-40b5-a476-b7992e7b50e1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "70dce1b8-0ccf-441e-a42d-ee0027ca9c47", "AQAAAAIAAYagAAAAENmSGl9igIBrAPyM7x26XpSmh60mW1IxBiRslm68Z7tM6TsTbYA2IJNIiEmfYGVdVQ==", "0302156a-9954-4bea-ac2e-b63287507a15" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f36fc003-dbd1-47b4-9dfd-45ec0f16f5d6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bcf6f879-f36e-45f6-aa0a-729ffcec28f6", "AQAAAAIAAYagAAAAEDP50x+Hf9JPJsiBD6NIxlPJeppzoAvnu3zfIZHeLbSVDkZPCIuCkgH/k7DrSW8Zbw==", "9152701b-4977-4829-95fd-113c7481fb3c" });
        }
    }
}
