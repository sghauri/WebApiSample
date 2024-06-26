using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiSample.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedDataForTodoItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TodoItems",
                columns: new[] { "Id", "CreatedDate", "Description", "LastUpdatedDate", "Status", "Title" },
                values: new object[] { 1, new DateTime(2024, 6, 14, 11, 9, 20, 637, DateTimeKind.Local).AddTicks(4305), "First to do item added via seed method of EF core.", null, "Not Started", "First Item" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TodoItems",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
