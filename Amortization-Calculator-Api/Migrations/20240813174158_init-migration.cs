﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Amortization_Calculator_Api.Migrations
{
    /// <inheritdoc />
    public partial class initmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.CreateTable(
          name: "AspNetUsers",
          columns: table => new
          {
              Id = table.Column<string>(type: "TEXT", nullable: false),
              gender = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
              userType = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
              isActivated = table.Column<int>(type: "INTEGER", nullable: false),
              usageLease = table.Column<int>(type: "INTEGER", nullable: false),
              UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
              NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
              Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
              NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
              EmailConfirmed = table.Column<int>(type: "INTEGER", nullable: false),
              PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
              SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
              ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
              PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
              PhoneNumberConfirmed = table.Column<int>(type: "INTEGER", nullable: false),
              TwoFactorEnabled = table.Column<int>(type: "INTEGER", nullable: false),
              LockoutEnd = table.Column<string>(type: "TEXT", nullable: true),
              LockoutEnabled = table.Column<int>(type: "INTEGER", nullable: false),
              AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
          },
          constraints: table =>
          {
              table.PrimaryKey("PK_AspNetUsers", x => x.Id);
          });




            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
