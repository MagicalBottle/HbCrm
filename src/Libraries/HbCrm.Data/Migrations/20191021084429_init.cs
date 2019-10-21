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
                    CreatebyName = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    LastUpdateBy = table.Column<int>(nullable: false),
                    LastUpdateByName = table.Column<string>(nullable: true),
                    LastUpdateDate = table.Column<DateTime>(nullable: false),
                    Guid = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    NickName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    MobilePhone = table.Column<string>(nullable: true),
                    QQ = table.Column<string>(nullable: true),
                    WeChar = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_admin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "sys_function",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateBy = table.Column<int>(nullable: false),
                    CreatebyName = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    LastUpdateBy = table.Column<int>(nullable: false),
                    LastUpdateByName = table.Column<string>(nullable: true),
                    LastUpdateDate = table.Column<DateTime>(nullable: false),
                    FunctionName = table.Column<string>(nullable: true),
                    FunctionSystermName = table.Column<string>(nullable: true),
                    FunctionSort = table.Column<int>(nullable: false),
                    MenuId = table.Column<int>(nullable: false),
                    FunctionRemark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_function", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "sys_menu",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateBy = table.Column<int>(nullable: false),
                    CreatebyName = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    LastUpdateBy = table.Column<int>(nullable: false),
                    LastUpdateByName = table.Column<string>(nullable: true),
                    LastUpdateDate = table.Column<DateTime>(nullable: false),
                    MenuName = table.Column<string>(nullable: true),
                    MenuSystermName = table.Column<string>(nullable: true),
                    MenuUrl = table.Column<string>(nullable: true),
                    ParentMenuId = table.Column<int>(nullable: false),
                    MenuIcon = table.Column<string>(nullable: true),
                    MenuSort = table.Column<int>(nullable: false),
                    MenuRemark = table.Column<string>(nullable: true)
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
                    CreatebyName = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    LastUpdateBy = table.Column<int>(nullable: false),
                    LastUpdateByName = table.Column<string>(nullable: true),
                    LastUpdateDate = table.Column<DateTime>(nullable: false),
                    RoleName = table.Column<string>(nullable: true),
                    RoleStatus = table.Column<int>(nullable: false),
                    RoleRemark = table.Column<string>(nullable: true)
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
                    CreatebyName = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    LastUpdateBy = table.Column<int>(nullable: false),
                    LastUpdateByName = table.Column<string>(nullable: true),
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
                name: "sys_functionrole",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateBy = table.Column<int>(nullable: false),
                    CreatebyName = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    LastUpdateBy = table.Column<int>(nullable: false),
                    LastUpdateByName = table.Column<string>(nullable: true),
                    LastUpdateDate = table.Column<DateTime>(nullable: false),
                    FunctionId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_functionrole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sys_functionrole_sys_function_FunctionId",
                        column: x => x.FunctionId,
                        principalTable: "sys_function",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_sys_functionrole_sys_role_RoleId",
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
                    CreatebyName = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    LastUpdateBy = table.Column<int>(nullable: false),
                    LastUpdateByName = table.Column<string>(nullable: true),
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
                name: "IX_sys_functionrole_FunctionId",
                table: "sys_functionrole",
                column: "FunctionId");

            migrationBuilder.CreateIndex(
                name: "IX_sys_functionrole_RoleId",
                table: "sys_functionrole",
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
                name: "sys_functionrole");

            migrationBuilder.DropTable(
                name: "sys_menurole");

            migrationBuilder.DropTable(
                name: "sys_admin");

            migrationBuilder.DropTable(
                name: "sys_function");

            migrationBuilder.DropTable(
                name: "sys_menu");

            migrationBuilder.DropTable(
                name: "sys_role");
        }
    }
}
