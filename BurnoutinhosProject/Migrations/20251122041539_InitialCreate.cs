using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BurnoutinhosProject.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_BURNOUTINHOS_USER",
                columns: table => new
                {
                    ID_USER = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NAME_USER = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    EMAIL_USER = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PASSWORD = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    LANGUAGE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PROFILE_IMAGE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_BURNOUTINHOS_USER", x => x.ID_USER);
                });

            migrationBuilder.CreateTable(
                name: "T_BURNOUTINHOS_NOTIFICATION",
                columns: table => new
                {
                    ID_NOTIF = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ID_USER = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    MESSAGE_NOTIF = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UPDATED_AT = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_BURNOUTINHOS_NOTIFICATION", x => x.ID_NOTIF);
                    table.ForeignKey(
                        name: "FK_T_BURNOUTINHOS_NOTIFICATION_T_BURNOUTINHOS_USER_ID_USER",
                        column: x => x.ID_USER,
                        principalTable: "T_BURNOUTINHOS_USER",
                        principalColumn: "ID_USER",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_BURNOUTINHOS_SUGGESTION",
                columns: table => new
                {
                    ID_SUGGESTION = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    SUGGESTION_DESC = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ID_TODO = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_BURNOUTINHOS_SUGGESTION", x => x.ID_SUGGESTION);
                    table.ForeignKey(
                        name: "FK_T_BURNOUTINHOS_SUGGESTION_T_BURNOUTINHOS_USER_ID_TODO",
                        column: x => x.ID_TODO,
                        principalTable: "T_BURNOUTINHOS_USER",
                        principalColumn: "ID_USER",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_BURNOUTINHOS_TODO",
                columns: table => new
                {
                    ID_TODO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NAME_TODO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    START_TODO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    END_TODO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    DESCRIPTION = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    TYPE = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    USER_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    IS_COMPLETED = table.Column<int>(type: "NUMBER(10)", nullable: false, defaultValue: 0),
                    CREATED_AT = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UPDATED_AT = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_BURNOUTINHOS_TODO", x => x.ID_TODO);
                    table.ForeignKey(
                        name: "USUARIO_TODO",
                        column: x => x.USER_ID,
                        principalTable: "T_BURNOUTINHOS_USER",
                        principalColumn: "ID_USER",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_BURNOUTINHOS_TIMEBLOCK",
                columns: table => new
                {
                    ID_TIMEBK = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NAME_TIMEBK = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    TIME_COUNT = table.Column<float>(type: "BINARY_FLOAT", nullable: false),
                    START_TIMEBK = table.Column<float>(type: "BINARY_FLOAT", nullable: false),
                    MAX_TIMEBK = table.Column<float>(type: "BINARY_FLOAT", nullable: false),
                    TYPE_TIMEBK = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ID_USER = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    ID_TODO = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    CREATED_AT = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UPDATED_AT = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_BURNOUTINHOS_TIMEBLOCK", x => x.ID_TIMEBK);
                    table.ForeignKey(
                        name: "FK_T_BURNOUTINHOS_TIMEBLOCK_T_BURNOUTINHOS_TODO_ID_TODO",
                        column: x => x.ID_TODO,
                        principalTable: "T_BURNOUTINHOS_TODO",
                        principalColumn: "ID_TODO",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_T_BURNOUTINHOS_TIMEBLOCK_T_BURNOUTINHOS_USER_ID_USER",
                        column: x => x.ID_USER,
                        principalTable: "T_BURNOUTINHOS_USER",
                        principalColumn: "ID_USER",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_BURNOUTINHOS_NOTIFICATION_ID_USER",
                table: "T_BURNOUTINHOS_NOTIFICATION",
                column: "ID_USER");

            migrationBuilder.CreateIndex(
                name: "IX_T_BURNOUTINHOS_SUGGESTION_ID_TODO",
                table: "T_BURNOUTINHOS_SUGGESTION",
                column: "ID_TODO");

            migrationBuilder.CreateIndex(
                name: "IX_T_BURNOUTINHOS_TIMEBLOCK_ID_TODO",
                table: "T_BURNOUTINHOS_TIMEBLOCK",
                column: "ID_TODO");

            migrationBuilder.CreateIndex(
                name: "IX_T_BURNOUTINHOS_TIMEBLOCK_ID_USER",
                table: "T_BURNOUTINHOS_TIMEBLOCK",
                column: "ID_USER");

            migrationBuilder.CreateIndex(
                name: "IX_T_BURNOUTINHOS_TODO_USER_ID",
                table: "T_BURNOUTINHOS_TODO",
                column: "USER_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_BURNOUTINHOS_NOTIFICATION");

            migrationBuilder.DropTable(
                name: "T_BURNOUTINHOS_SUGGESTION");

            migrationBuilder.DropTable(
                name: "T_BURNOUTINHOS_TIMEBLOCK");

            migrationBuilder.DropTable(
                name: "T_BURNOUTINHOS_TODO");

            migrationBuilder.DropTable(
                name: "T_BURNOUTINHOS_USER");
        }
    }
}
