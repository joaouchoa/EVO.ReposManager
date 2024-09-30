﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EVO.ReposManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Repositories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 39, nullable: false),
                    Description = table.Column<string>(type: "varchar(100)", nullable: false),
                    Url = table.Column<string>(type: "varchar(100)", nullable: false),
                    Language = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repositories", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Repositories");
        }
    }
}
