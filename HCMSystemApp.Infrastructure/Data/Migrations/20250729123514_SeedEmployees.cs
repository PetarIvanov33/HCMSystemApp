using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HCMSystemApp.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedEmployees : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "79e1d63d-bbd0-4724-91f6-2ab694ebf4a9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c6a794d4-2e3c-4534-bd10-ee3d532e3087", "AQAAAAIAAYagAAAAENfPgQ1VEqklhtpupp6d7Sgto8ZkvFN31Z58zP3cESzJqeh6guddtrlOmTMXEERIbQ==", "a8f6b209-fe9f-411b-8e86-606738c802c9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8d04dce2-969a-435d-bba4-df3f325983dc",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9e4c69c6-c744-4822-99b3-4ecbbff39117", "AQAAAAIAAYagAAAAEBWu15qCgxRBl3Ml+PFPCwSp96q/+Jt4aOQyu/mAu9CHfXDIxRGOa49ZSKRyBUJ2Yw==", "2a063be2-d9f2-4fce-ba49-a2a17a94b4d4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d0bd2a23-6c8d-40b5-a476-b7992e7b50e1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "88f9c0e2-4a3e-4cea-a32e-ec33164a0f26", "AQAAAAIAAYagAAAAEKAUi9rMHcJsJ9bRRQxvKHqnyyZRX1wUbcM5/fZxwike/qTGZ9ltBgK9GiWyST26ww==", "2a35bad3-c747-419e-9270-e5bd0dfb8ba3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f36fc003-dbd1-47b4-9dfd-45ec0f16f5d6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d720a968-e21d-4277-9e65-66118932de02", "AQAAAAIAAYagAAAAEElEt+M+WFU0jpQQdCDp7u9xSz7WJsrCLAWirRqOjg4WCf9Injiqz3i8fwELkDP6Qg==", "318352cd-fb0b-42ca-9a5e-5d4cac94914b" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "DepartmentId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, "79e1d63d-bbd0-4724-91f6-2ab694ebf4a9" },
                    { 2, 1, "d0bd2a23-6c8d-40b5-a476-b7992e7b50e1" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2);

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
        }
    }
}
