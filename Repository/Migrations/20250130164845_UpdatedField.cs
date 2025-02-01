using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "approved",
                table: "shelter_pet_listings");

            migrationBuilder.DropColumn(
                name: "approved",
                table: "owner_pet_listings");

            migrationBuilder.DropColumn(
                name: "adoption_fee",
                table: "adoptions");

            migrationBuilder.DropColumn(
                name: "is_successful",
                table: "adoptions");

            migrationBuilder.AddColumn<float>(
                name: "adoption_fee",
                table: "shelter_pet_listings",
                type: "DECIMAL",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "adoption_fee",
                table: "owner_pet_listings",
                type: "DECIMAL",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "approved",
                table: "adoptions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "adoption_fee",
                table: "shelter_pet_listings");

            migrationBuilder.DropColumn(
                name: "adoption_fee",
                table: "owner_pet_listings");

            migrationBuilder.DropColumn(
                name: "approved",
                table: "adoptions");

            migrationBuilder.AddColumn<int>(
                name: "approved",
                table: "shelter_pet_listings",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "approved",
                table: "owner_pet_listings",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "adoption_fee",
                table: "adoptions",
                type: "DECIMAL",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "is_successful",
                table: "adoptions",
                type: "BOOLEAN",
                nullable: false,
                defaultValue: false);
        }
    }
}
