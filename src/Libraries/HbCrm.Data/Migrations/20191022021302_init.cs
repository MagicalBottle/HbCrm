using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HbCrm.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "sys_admin",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateBy = table.Column<int>(nullable: false),
                    CreatebyName = table.Column<string>(maxLength: 50, nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    LastUpdateBy = table.Column<int>(nullable: false),
                    LastUpdateByName = table.Column<string>(maxLength: 50, nullable: false),
                    LastUpdateDate = table.Column<DateTime>(nullable: false),
                    Guid = table.Column<string>(maxLength: 50, nullable: false),
                    UserName = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<string>(maxLength: 50, nullable: false),
                    NickName = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: true),
                    MobilePhone = table.Column<string>(maxLength: 50, nullable: true),
                    QQ = table.Column<string>(maxLength: 50, nullable: true),
                    WeChar = table.Column<string>(maxLength: 50, nullable: true),
                    Status = table.Column<int>(nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_admin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "sys_menu",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateBy = table.Column<int>(nullable: false),
                    CreatebyName = table.Column<string>(maxLength: 50, nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    LastUpdateBy = table.Column<int>(nullable: false),
                    LastUpdateByName = table.Column<string>(maxLength: 50, nullable: false),
                    LastUpdateDate = table.Column<DateTime>(nullable: false),
                    MenuName = table.Column<string>(maxLength: 50, nullable: false),
                    MenuSystermName = table.Column<string>(maxLength: 255, nullable: false),
                    MenuUrl = table.Column<string>(nullable: true),
                    ParentMenuId = table.Column<int>(nullable: false, defaultValue: 0),
                    Type = table.Column<int>(nullable: false, defaultValue: 1),
                    MenuType = table.Column<int>(nullable: false),
                    MenuIcon = table.Column<string>(maxLength: 50, nullable: true),
                    MenuSort = table.Column<int>(nullable: false, defaultValue: 0),
                    MenuRemark = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_menu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "sys_role",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateBy = table.Column<int>(nullable: false),
                    CreatebyName = table.Column<string>(maxLength: 50, nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    LastUpdateBy = table.Column<int>(nullable: false),
                    LastUpdateByName = table.Column<string>(maxLength: 50, nullable: false),
                    LastUpdateDate = table.Column<DateTime>(nullable: false),
                    RoleName = table.Column<string>(maxLength: 50, nullable: false),
                    Status = table.Column<int>(nullable: false, defaultValue: 1),
                    RoleRemark = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "sys_adminrole",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateBy = table.Column<int>(nullable: false),
                    CreatebyName = table.Column<string>(maxLength: 50, nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    LastUpdateBy = table.Column<int>(nullable: false),
                    LastUpdateByName = table.Column<string>(maxLength: 50, nullable: false),
                    LastUpdateDate = table.Column<DateTime>(nullable: false),
                    AdminId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_adminrole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sys_adminrole_sys_admin_AdminId",
                        column: x => x.AdminId,
                        principalTable: "sys_admin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sys_adminrole_sys_role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "sys_role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sys_menurole",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateBy = table.Column<int>(nullable: false),
                    CreatebyName = table.Column<string>(maxLength: 50, nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    LastUpdateBy = table.Column<int>(nullable: false),
                    LastUpdateByName = table.Column<string>(maxLength: 50, nullable: false),
                    LastUpdateDate = table.Column<DateTime>(nullable: false),
                    MenuId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_menurole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sys_menurole_sys_menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "sys_menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sys_menurole_sys_role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "sys_role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_sys_adminrole_AdminId",
                table: "sys_adminrole",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_sys_adminrole_RoleId",
                table: "sys_adminrole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_sys_menurole_MenuId",
                table: "sys_menurole",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_sys_menurole_RoleId",
                table: "sys_menurole",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sys_adminrole");

            migrationBuilder.DropTable(
                name: "sys_menurole");

            migrationBuilder.DropTable(
                name: "sys_admin");

            migrationBuilder.DropTable(
                name: "sys_menu");

            migrationBuilder.DropTable(
                name: "sys_role");
        }
    }
}
