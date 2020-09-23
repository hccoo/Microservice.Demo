using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Microservice.Demo.Service.Migrations
{
    public partial class init_db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "messages",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(maxLength: 80, nullable: false),
                    message_to = table.Column<string>(maxLength: 80, nullable: false),
                    expired_on = table.Column<DateTime>(nullable: false),
                    biz_code = table.Column<int>(nullable: false),
                    is_used = table.Column<bool>(nullable: false),
                    is_suspend = table.Column<bool>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false),
                    updated_on = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_messages", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "messages");
        }
    }
}
