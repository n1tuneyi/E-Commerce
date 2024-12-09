using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class test_upanddown : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4961fef4-4095-499c-8162-d8c7e5d90024");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8b52de39-3b71-468d-822d-ca05628c0503");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("1359d32e-78a7-4299-a015-0dd12fba5013"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("531a64d5-45ed-472b-beb1-8820b63c5232"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "87a76db5-2988-4b5f-aa1c-1c099c267d93", null, "Admin", "ADMIN" },
                    { "bcb4eb4a-5d0d-4957-8e41-a1f544425179", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Description", "IsDeleted", "Name", "Price", "StockQuantity", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("099070d7-bb59-415e-9b2f-d9778f1ede71"), 0L, new DateTime(2024, 12, 2, 8, 46, 53, 671, DateTimeKind.Local).AddTicks(9480), "American Apples with rich Vitamins and good taste", false, "Apples", 8m, 60, 0L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("4adfec5e-00e2-4f51-80b9-ab03e5e8065f"), 0L, new DateTime(2024, 12, 2, 8, 46, 53, 671, DateTimeKind.Local).AddTicks(9590), "Egyptian Oranges", false, "Oranges", 4m, 100, 0L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "87a76db5-2988-4b5f-aa1c-1c099c267d93");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bcb4eb4a-5d0d-4957-8e41-a1f544425179");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("099070d7-bb59-415e-9b2f-d9778f1ede71"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("4adfec5e-00e2-4f51-80b9-ab03e5e8065f"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4961fef4-4095-499c-8162-d8c7e5d90024", null, "Admin", "ADMIN" },
                    { "8b52de39-3b71-468d-822d-ca05628c0503", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Description", "IsDeleted", "Name", "Price", "StockQuantity", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("1359d32e-78a7-4299-a015-0dd12fba5013"), 0L, new DateTime(2024, 11, 30, 18, 42, 20, 414, DateTimeKind.Local).AddTicks(1219), "American Apples with rich Vitamins and good taste", false, "Apples", 8m, 60, 0L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("531a64d5-45ed-472b-beb1-8820b63c5232"), 0L, new DateTime(2024, 11, 30, 18, 42, 20, 414, DateTimeKind.Local).AddTicks(1298), "Egyptian Oranges", false, "Oranges", 4m, 100, 0L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }
    }
}
