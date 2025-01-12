using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class AllTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "adoption_statuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_adoption_statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    address = table.Column<string>(type: "TEXT", nullable: false),
                    created_at = table.Column<DateTime>(type: "TIMESTAMPTZ", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "health_statuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_health_statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "owner_surrender_reasons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_owner_surrender_reasons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "pet_genders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pet_genders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "pet_sizes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pet_sizes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "pet_types",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pet_types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "veterinarians",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ClinicName = table.Column<string>(type: "text", nullable: false),
                    ContactNumber = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_veterinarians", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "shelters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    website = table.Column<string>(type: "TEXT", nullable: true),
                    capacity = table.Column<int>(type: "integer", nullable: false),
                    is_no_kill = table.Column<bool>(type: "BOOLEAN", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shelters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_shelters_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "TEXT", nullable: false),
                    last_name = table.Column<string>(type: "TEXT", nullable: false),
                    has_children = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    has_other_pets = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    home_type = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_users_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    breed = table.Column<string>(type: "TEXT", nullable: true),
                    avatar_img = table.Column<string>(type: "TEXT", nullable: false),
                    image_showcase = table.Column<string[]>(type: "TEXT[]", nullable: false),
                    age_years = table.Column<int>(type: "integer", nullable: false),
                    type_id = table.Column<Guid>(type: "UUID", nullable: false),
                    gender_id = table.Column<Guid>(type: "UUID", nullable: false),
                    size_id = table.Column<Guid>(type: "UUID", nullable: false),
                    adoption_status_id = table.Column<Guid>(type: "UUID", nullable: false),
                    health_status_id = table.Column<Guid>(type: "UUID", nullable: false),
                    good_with_children = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    good_with_cats = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    good_with_dogs = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    energy_level = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: true),
                    special_requirements = table.Column<string>(type: "TEXT", nullable: true),
                    behaviorial_notes = table.Column<string>(type: "TEXT", nullable: true),
                    intake_date = table.Column<DateTime>(type: "DATE", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_pets_adoption_statuses_adoption_status_id",
                        column: x => x.adoption_status_id,
                        principalTable: "adoption_statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pets_health_statuses_health_status_id",
                        column: x => x.health_status_id,
                        principalTable: "health_statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pets_pet_genders_gender_id",
                        column: x => x.gender_id,
                        principalTable: "pet_genders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pets_pet_sizes_size_id",
                        column: x => x.size_id,
                        principalTable: "pet_sizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pets_pet_types_type_id",
                        column: x => x.type_id,
                        principalTable: "pet_types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "medical_records",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    veterinarian_id = table.Column<Guid>(type: "UUID", nullable: false),
                    spay_neuter_status = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    last_medical_checkup = table.Column<DateOnly>(type: "DATE", nullable: true),
                    microchip_number = table.Column<string>(type: "TEXT", nullable: true),
                    created_at = table.Column<DateTime>(type: "TIMESTAMPTZ", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medical_records", x => x.Id);
                    table.ForeignKey(
                        name: "FK_medical_records_veterinarians_veterinarian_id",
                        column: x => x.veterinarian_id,
                        principalTable: "veterinarians",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "vet_specializations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    vet_id = table.Column<Guid>(type: "UUID", nullable: false),
                    specialization = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vet_specializations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_vet_specializations_veterinarians_vet_id",
                        column: x => x.vet_id,
                        principalTable: "veterinarians",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdopterPetGenderPreferences",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    adopter_id = table.Column<Guid>(type: "UUID", nullable: false),
                    gender_id = table.Column<Guid>(type: "UUID", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdopterPetGenderPreferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdopterPetGenderPreferences_pet_genders_gender_id",
                        column: x => x.gender_id,
                        principalTable: "pet_genders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdopterPetGenderPreferences_users_adopter_id",
                        column: x => x.adopter_id,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdopterPetSizePreferences",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    adopter_id = table.Column<Guid>(type: "UUID", nullable: false),
                    size_id = table.Column<Guid>(type: "UUID", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdopterPetSizePreferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdopterPetSizePreferences_pet_sizes_size_id",
                        column: x => x.size_id,
                        principalTable: "pet_sizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdopterPetSizePreferences_users_adopter_id",
                        column: x => x.adopter_id,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdopterPetTypePreferences",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    adopter_id = table.Column<Guid>(type: "UUID", nullable: false),
                    type_id = table.Column<Guid>(type: "UUID", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdopterPetTypePreferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdopterPetTypePreferences_pet_types_type_id",
                        column: x => x.type_id,
                        principalTable: "pet_types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdopterPetTypePreferences_users_adopter_id",
                        column: x => x.adopter_id,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "adoptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    pet_id = table.Column<Guid>(type: "UUID", nullable: false),
                    adopter_id = table.Column<Guid>(type: "UUID", nullable: false),
                    adoption_date = table.Column<DateTime>(type: "DATE", nullable: false),
                    adoption_fee = table.Column<decimal>(type: "DECIMAL", nullable: false),
                    follow_up_date = table.Column<DateTime>(type: "DATE", nullable: true),
                    counselor_notes = table.Column<string>(type: "TEXT", nullable: false),
                    is_successful = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    created_at = table.Column<DateTime>(type: "TIMESTAMPTZ", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_adoptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_adoptions_pets_pet_id",
                        column: x => x.pet_id,
                        principalTable: "pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_adoptions_users_adopter_id",
                        column: x => x.adopter_id,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OwnerPetListings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    adopter_id = table.Column<Guid>(type: "UUID", nullable: false),
                    pet_id = table.Column<Guid>(type: "UUID", nullable: false),
                    surrender_reason_id = table.Column<Guid>(type: "UUID", nullable: false),
                    review_date = table.Column<DateTime>(type: "TIMESTAMPTZ", nullable: false),
                    submission_date = table.Column<DateTime>(type: "TIMESTAMPTZ", nullable: false),
                    approved = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnerPetListings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OwnerPetListings_owner_surrender_reasons_surrender_reason_id",
                        column: x => x.surrender_reason_id,
                        principalTable: "owner_surrender_reasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OwnerPetListings_pets_pet_id",
                        column: x => x.pet_id,
                        principalTable: "pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OwnerPetListings_users_adopter_id",
                        column: x => x.adopter_id,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "medical_conditions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    medical_record_id = table.Column<Guid>(type: "UUID", nullable: false),
                    condition_name = table.Column<string>(type: "TEXT", nullable: false),
                    notes = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medical_conditions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_medical_conditions_medical_records_medical_record_id",
                        column: x => x.medical_record_id,
                        principalTable: "medical_records",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShelterPetListings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    pet_id = table.Column<Guid>(type: "UUID", nullable: false),
                    medical_record_id = table.Column<Guid>(type: "UUID", nullable: false),
                    shelter_id = table.Column<Guid>(type: "UUID", nullable: false),
                    intake_date = table.Column<DateOnly>(type: "DATE", nullable: true),
                    approved = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShelterPetListings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShelterPetListings_medical_records_medical_record_id",
                        column: x => x.medical_record_id,
                        principalTable: "medical_records",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShelterPetListings_pets_pet_id",
                        column: x => x.pet_id,
                        principalTable: "pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShelterPetListings_shelters_shelter_id",
                        column: x => x.shelter_id,
                        principalTable: "shelters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "vaccinations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    medical_record_id = table.Column<Guid>(type: "UUID", nullable: false),
                    vaccine_name = table.Column<string>(type: "TEXT", nullable: false),
                    vaccine_date = table.Column<DateOnly>(type: "Date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vaccinations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_vaccinations_medical_records_medical_record_id",
                        column: x => x.medical_record_id,
                        principalTable: "medical_records",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OwnerPetListingDocuments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    listing_id = table.Column<Guid>(type: "UUID", nullable: false),
                    document_url = table.Column<string>(type: "TEXT", nullable: false),
                    document_type = table.Column<string>(type: "TEXT", nullable: false),
                    uploaded_at = table.Column<DateTime>(type: "TIMESTAMPTZ", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnerPetListingDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OwnerPetListingDocuments_OwnerPetListings_listing_id",
                        column: x => x.listing_id,
                        principalTable: "OwnerPetListings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdopterPetGenderPreferences_adopter_id",
                table: "AdopterPetGenderPreferences",
                column: "adopter_id");

            migrationBuilder.CreateIndex(
                name: "IX_AdopterPetGenderPreferences_gender_id",
                table: "AdopterPetGenderPreferences",
                column: "gender_id");

            migrationBuilder.CreateIndex(
                name: "IX_AdopterPetSizePreferences_adopter_id",
                table: "AdopterPetSizePreferences",
                column: "adopter_id");

            migrationBuilder.CreateIndex(
                name: "IX_AdopterPetSizePreferences_size_id",
                table: "AdopterPetSizePreferences",
                column: "size_id");

            migrationBuilder.CreateIndex(
                name: "IX_AdopterPetTypePreferences_adopter_id",
                table: "AdopterPetTypePreferences",
                column: "adopter_id");

            migrationBuilder.CreateIndex(
                name: "IX_AdopterPetTypePreferences_type_id",
                table: "AdopterPetTypePreferences",
                column: "type_id");

            migrationBuilder.CreateIndex(
                name: "IX_adoptions_adopter_id",
                table: "adoptions",
                column: "adopter_id");

            migrationBuilder.CreateIndex(
                name: "IX_adoptions_pet_id",
                table: "adoptions",
                column: "pet_id");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_medical_conditions_medical_record_id",
                table: "medical_conditions",
                column: "medical_record_id");

            migrationBuilder.CreateIndex(
                name: "IX_medical_records_veterinarian_id",
                table: "medical_records",
                column: "veterinarian_id");

            migrationBuilder.CreateIndex(
                name: "IX_OwnerPetListingDocuments_listing_id",
                table: "OwnerPetListingDocuments",
                column: "listing_id");

            migrationBuilder.CreateIndex(
                name: "IX_OwnerPetListings_adopter_id",
                table: "OwnerPetListings",
                column: "adopter_id");

            migrationBuilder.CreateIndex(
                name: "IX_OwnerPetListings_pet_id",
                table: "OwnerPetListings",
                column: "pet_id");

            migrationBuilder.CreateIndex(
                name: "IX_OwnerPetListings_surrender_reason_id",
                table: "OwnerPetListings",
                column: "surrender_reason_id");

            migrationBuilder.CreateIndex(
                name: "IX_pets_adoption_status_id",
                table: "pets",
                column: "adoption_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_pets_gender_id",
                table: "pets",
                column: "gender_id");

            migrationBuilder.CreateIndex(
                name: "IX_pets_health_status_id",
                table: "pets",
                column: "health_status_id");

            migrationBuilder.CreateIndex(
                name: "IX_pets_size_id",
                table: "pets",
                column: "size_id");

            migrationBuilder.CreateIndex(
                name: "IX_pets_type_id",
                table: "pets",
                column: "type_id");

            migrationBuilder.CreateIndex(
                name: "IX_ShelterPetListings_medical_record_id",
                table: "ShelterPetListings",
                column: "medical_record_id");

            migrationBuilder.CreateIndex(
                name: "IX_ShelterPetListings_pet_id",
                table: "ShelterPetListings",
                column: "pet_id");

            migrationBuilder.CreateIndex(
                name: "IX_ShelterPetListings_shelter_id",
                table: "ShelterPetListings",
                column: "shelter_id");

            migrationBuilder.CreateIndex(
                name: "IX_vaccinations_medical_record_id",
                table: "vaccinations",
                column: "medical_record_id");

            migrationBuilder.CreateIndex(
                name: "IX_vet_specializations_vet_id",
                table: "vet_specializations",
                column: "vet_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdopterPetGenderPreferences");

            migrationBuilder.DropTable(
                name: "AdopterPetSizePreferences");

            migrationBuilder.DropTable(
                name: "AdopterPetTypePreferences");

            migrationBuilder.DropTable(
                name: "adoptions");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "medical_conditions");

            migrationBuilder.DropTable(
                name: "OwnerPetListingDocuments");

            migrationBuilder.DropTable(
                name: "ShelterPetListings");

            migrationBuilder.DropTable(
                name: "vaccinations");

            migrationBuilder.DropTable(
                name: "vet_specializations");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "OwnerPetListings");

            migrationBuilder.DropTable(
                name: "shelters");

            migrationBuilder.DropTable(
                name: "medical_records");

            migrationBuilder.DropTable(
                name: "owner_surrender_reasons");

            migrationBuilder.DropTable(
                name: "pets");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "veterinarians");

            migrationBuilder.DropTable(
                name: "adoption_statuses");

            migrationBuilder.DropTable(
                name: "health_statuses");

            migrationBuilder.DropTable(
                name: "pet_genders");

            migrationBuilder.DropTable(
                name: "pet_sizes");

            migrationBuilder.DropTable(
                name: "pet_types");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
