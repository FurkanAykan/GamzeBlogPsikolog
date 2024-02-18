using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamzeBlogPsikolog.Migrations
{
    /// <inheritdoc />
    public partial class sadsf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSeen",
                table: "ReplyComments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSeen",
                table: "ReplyComments");
        }
    }
}
