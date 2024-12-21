﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Repository;

#nullable disable

namespace Repository.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241221155516_NewMigrationName")]
    partial class NewMigrationName
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.AdopterPetGenderPreference", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AdopterId")
                        .HasColumnType("UUID")
                        .HasColumnName("adopter_id");

                    b.Property<Guid>("PetGenderId")
                        .HasColumnType("UUID")
                        .HasColumnName("gender_id");

                    b.HasKey("Id");

                    b.HasIndex("AdopterId");

                    b.HasIndex("PetGenderId");

                    b.ToTable("AdopterPetGenderPreferences");
                });

            modelBuilder.Entity("Domain.AdopterPetSizePreference", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AdopterId")
                        .HasColumnType("UUID")
                        .HasColumnName("adopter_id");

                    b.Property<Guid>("PetSizeId")
                        .HasColumnType("UUID")
                        .HasColumnName("size_id");

                    b.HasKey("Id");

                    b.HasIndex("AdopterId");

                    b.HasIndex("PetSizeId");

                    b.ToTable("AdopterPetSizePreferences");
                });

            modelBuilder.Entity("Domain.AdopterPetTypePreference", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AdopterId")
                        .HasColumnType("UUID")
                        .HasColumnName("adopter_id");

                    b.Property<Guid>("PetTypeId")
                        .HasColumnType("UUID")
                        .HasColumnName("type_id");

                    b.HasKey("Id");

                    b.HasIndex("AdopterId");

                    b.HasIndex("PetTypeId");

                    b.ToTable("AdopterPetTypePreferences");
                });

            modelBuilder.Entity("Domain.Adoption", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AdopterId")
                        .HasColumnType("UUID")
                        .HasColumnName("adopter_id");

                    b.Property<DateTime>("AdoptionDate")
                        .HasColumnType("DATE")
                        .HasColumnName("adoption_date");

                    b.Property<decimal>("AdoptionFee")
                        .HasColumnType("DECIMAL")
                        .HasColumnName("adoption_fee");

                    b.Property<string>("CounselorNotes")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("counselor_notes");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TIMESTAMPTZ")
                        .HasColumnName("created_at");

                    b.Property<DateTime?>("FollowUpDate")
                        .HasColumnType("DATE")
                        .HasColumnName("follow_up_date");

                    b.Property<bool>("IsSuccessful")
                        .HasColumnType("BOOLEAN")
                        .HasColumnName("is_successful");

                    b.Property<Guid>("PetId")
                        .HasColumnType("UUID")
                        .HasColumnName("pet_id");

                    b.HasKey("Id");

                    b.HasIndex("AdopterId");

                    b.HasIndex("PetId");

                    b.ToTable("adoptions");
                });

            modelBuilder.Entity("Domain.AdoptionStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("adoption_statuses");
                });

            modelBuilder.Entity("Domain.HealthStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(255)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("health_statuses");
                });

            modelBuilder.Entity("Domain.MedicalCondition", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ConditionName")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("condition_name");

                    b.Property<Guid>("MedicalRecordId")
                        .HasColumnType("UUID")
                        .HasColumnName("medical_record_id");

                    b.Property<string>("Notes")
                        .HasColumnType("TEXT")
                        .HasColumnName("notes");

                    b.HasKey("Id");

                    b.HasIndex("MedicalRecordId");

                    b.ToTable("medical_conditions");
                });

            modelBuilder.Entity("Domain.MedicalRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TIMESTAMPTZ")
                        .HasColumnName("created_at");

                    b.Property<DateOnly?>("LastMedicalCheckup")
                        .HasColumnType("DATE")
                        .HasColumnName("last_medical_checkup");

                    b.Property<string>("MicrochipNumber")
                        .HasColumnType("TEXT")
                        .HasColumnName("microchip_number");

                    b.Property<bool>("SpayNeuterStatus")
                        .HasColumnType("BOOLEAN")
                        .HasColumnName("spay_neuter_status");

                    b.Property<Guid>("VetId")
                        .HasColumnType("UUID")
                        .HasColumnName("veterinarian_id");

                    b.HasKey("Id");

                    b.HasIndex("VetId");

                    b.ToTable("medical_records");
                });

            modelBuilder.Entity("Domain.OwnerPetListing", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AdopterId")
                        .HasColumnType("UUID")
                        .HasColumnName("adopter_id");

                    b.Property<int>("Approved")
                        .HasColumnType("INTEGER")
                        .HasColumnName("approved");

                    b.Property<Guid>("PetId")
                        .HasColumnType("UUID")
                        .HasColumnName("pet_id");

                    b.Property<DateTime>("ReviewDate")
                        .HasColumnType("TIMESTAMPTZ")
                        .HasColumnName("review_date");

                    b.Property<DateTime>("SubmissionDate")
                        .HasColumnType("TIMESTAMPTZ")
                        .HasColumnName("submission_date");

                    b.Property<Guid>("SurrenderReasonId")
                        .HasColumnType("UUID")
                        .HasColumnName("surrender_reason_id");

                    b.HasKey("Id");

                    b.HasIndex("AdopterId");

                    b.HasIndex("PetId");

                    b.HasIndex("SurrenderReasonId");

                    b.ToTable("OwnerPetListings");
                });

            modelBuilder.Entity("Domain.OwnerPetListingDocument", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("DocumentType")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("document_type");

                    b.Property<string>("DocumentUrl")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("document_url");

                    b.Property<Guid>("ListingId")
                        .HasColumnType("UUID")
                        .HasColumnName("listing_id");

                    b.Property<DateTime>("UploadedAt")
                        .HasColumnType("TIMESTAMPTZ")
                        .HasColumnName("uploaded_at");

                    b.HasKey("Id");

                    b.HasIndex("ListingId");

                    b.ToTable("OwnerPetListingDocuments");
                });

            modelBuilder.Entity("Domain.OwnerSurrenderReason", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("VARCHAR(255)")
                        .HasColumnName("description");

                    b.HasKey("Id");

                    b.ToTable("owner_surrender_reasons");
                });

            modelBuilder.Entity("Domain.Pet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AdoptionStatusId")
                        .HasColumnType("UUID")
                        .HasColumnName("adoption_status_id");

                    b.Property<int>("AgeYears")
                        .HasColumnType("integer")
                        .HasColumnName("age_years");

                    b.Property<string>("AvatarImg")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("avatar_img");

                    b.Property<string>("BehaviorialNotes")
                        .HasColumnType("TEXT")
                        .HasColumnName("behaviorial_notes");

                    b.Property<string>("Breed")
                        .HasColumnType("VARCHAR(255)")
                        .HasColumnName("breed");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT")
                        .HasColumnName("description");

                    b.Property<int>("EnergyLevel")
                        .HasColumnType("integer")
                        .HasColumnName("energy_level");

                    b.Property<bool>("GoodWithCats")
                        .HasColumnType("BOOLEAN")
                        .HasColumnName("good_with_cats");

                    b.Property<bool>("GoodWithChildren")
                        .HasColumnType("BOOLEAN")
                        .HasColumnName("good_with_children");

                    b.Property<bool>("GoodWithDogs")
                        .HasColumnType("BOOLEAN")
                        .HasColumnName("good_with_dogs");

                    b.Property<Guid>("HealthStatusId")
                        .HasColumnType("UUID")
                        .HasColumnName("health_status_id");

                    b.PrimitiveCollection<string[]>("ImageShowcase")
                        .IsRequired()
                        .HasColumnType("TEXT[]")
                        .HasColumnName("image_showcase");

                    b.Property<DateTime?>("IntakeDate")
                        .HasColumnType("DATE")
                        .HasColumnName("intake_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(255)")
                        .HasColumnName("name");

                    b.Property<Guid>("PetGenderId")
                        .HasColumnType("UUID")
                        .HasColumnName("gender_id");

                    b.Property<Guid>("PetSizeId")
                        .HasColumnType("UUID")
                        .HasColumnName("size_id");

                    b.Property<Guid>("PetTypeId")
                        .HasColumnType("UUID")
                        .HasColumnName("type_id");

                    b.Property<string>("SpecialRequirements")
                        .HasColumnType("TEXT")
                        .HasColumnName("special_requirements");

                    b.HasKey("Id");

                    b.HasIndex("AdoptionStatusId");

                    b.HasIndex("HealthStatusId");

                    b.HasIndex("PetGenderId");

                    b.HasIndex("PetSizeId");

                    b.HasIndex("PetTypeId");

                    b.ToTable("pets");
                });

            modelBuilder.Entity("Domain.PetGender", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("pet_genders");
                });

            modelBuilder.Entity("Domain.PetSize", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(255)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("pet_sizes");
                });

            modelBuilder.Entity("Domain.PetType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("pet_types");
                });

            modelBuilder.Entity("Domain.Shelter", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("address");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TIMESTAMPTZ")
                        .HasColumnName("created_at");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(255)")
                        .HasColumnName("name");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("phone_number");

                    b.Property<string>("Website")
                        .HasColumnType("TEXT")
                        .HasColumnName("website");

                    b.Property<int>("capacity")
                        .HasColumnType("integer")
                        .HasColumnName("capacity");

                    b.Property<bool>("isNoKill")
                        .HasColumnType("BOOLEAN")
                        .HasColumnName("is_no_kill");

                    b.HasKey("Id");

                    b.ToTable("shelters");
                });

            modelBuilder.Entity("Domain.ShelterPetListing", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Approved")
                        .HasColumnType("INTEGER")
                        .HasColumnName("approved");

                    b.Property<DateOnly?>("IntakeDate")
                        .HasColumnType("DATE")
                        .HasColumnName("intake_date");

                    b.Property<Guid>("MedicalRecordId")
                        .HasColumnType("UUID")
                        .HasColumnName("medical_record_id");

                    b.Property<Guid>("PetId")
                        .HasColumnType("UUID")
                        .HasColumnName("pet_id");

                    b.Property<Guid>("ShelterId")
                        .HasColumnType("UUID")
                        .HasColumnName("shelter_id");

                    b.HasKey("Id");

                    b.HasIndex("MedicalRecordId");

                    b.HasIndex("PetId");

                    b.HasIndex("ShelterId");

                    b.ToTable("ShelterPetListings");
                });

            modelBuilder.Entity("Domain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("address");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TIMESTAMPTZ")
                        .HasColumnName("created_at");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("varchar(11)")
                        .HasColumnName("first_name");

                    b.Property<bool>("HasChildren")
                        .HasColumnType("BOOLEAN")
                        .HasColumnName("has_children");

                    b.Property<bool>("HasOtherPets")
                        .HasColumnType("BOOLEAN")
                        .HasColumnName("has_other_pets");

                    b.Property<string>("HomeType")
                        .IsRequired()
                        .HasColumnType("VARCHAR(255)")
                        .HasColumnName("home_type");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("varchar(11)")
                        .HasColumnName("last_name");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("text");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("Domain.Vaccination", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("MedicalRecordId")
                        .HasColumnType("UUID")
                        .HasColumnName("medical_record_id");

                    b.Property<DateOnly>("VaccineDate")
                        .HasColumnType("Date")
                        .HasColumnName("vaccine_date");

                    b.Property<string>("VaccineName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("vaccine_name");

                    b.HasKey("Id");

                    b.HasIndex("MedicalRecordId");

                    b.ToTable("vaccinations");
                });

            modelBuilder.Entity("Domain.Veterinarian", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ClinicName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("veterinarians");
                });

            modelBuilder.Entity("Domain.VeterinarianSpecilization", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Specialization")
                        .IsRequired()
                        .HasColumnType("VARCHAR(255)")
                        .HasColumnName("specialization");

                    b.Property<Guid>("VeterinarianId")
                        .HasColumnType("UUID")
                        .HasColumnName("vet_id");

                    b.HasKey("Id");

                    b.HasIndex("VeterinarianId");

                    b.ToTable("vet_specializations");
                });

            modelBuilder.Entity("Domain.AdopterPetGenderPreference", b =>
                {
                    b.HasOne("Domain.User", "Adopter")
                        .WithMany()
                        .HasForeignKey("AdopterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.PetGender", "PetGender")
                        .WithMany()
                        .HasForeignKey("PetGenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Adopter");

                    b.Navigation("PetGender");
                });

            modelBuilder.Entity("Domain.AdopterPetSizePreference", b =>
                {
                    b.HasOne("Domain.User", "Adopter")
                        .WithMany()
                        .HasForeignKey("AdopterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.PetSize", "PetSize")
                        .WithMany("AdopterPetSizesPreference")
                        .HasForeignKey("PetSizeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Adopter");

                    b.Navigation("PetSize");
                });

            modelBuilder.Entity("Domain.AdopterPetTypePreference", b =>
                {
                    b.HasOne("Domain.User", "Adopter")
                        .WithMany()
                        .HasForeignKey("AdopterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.PetType", "PetType")
                        .WithMany()
                        .HasForeignKey("PetTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Adopter");

                    b.Navigation("PetType");
                });

            modelBuilder.Entity("Domain.Adoption", b =>
                {
                    b.HasOne("Domain.User", "Adopter")
                        .WithMany()
                        .HasForeignKey("AdopterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Pet", "Pet")
                        .WithMany("Adoptions")
                        .HasForeignKey("PetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Adopter");

                    b.Navigation("Pet");
                });

            modelBuilder.Entity("Domain.MedicalCondition", b =>
                {
                    b.HasOne("Domain.MedicalRecord", "MedicalRecord")
                        .WithMany("MedicalConditions")
                        .HasForeignKey("MedicalRecordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MedicalRecord");
                });

            modelBuilder.Entity("Domain.MedicalRecord", b =>
                {
                    b.HasOne("Domain.Veterinarian", "Veterinarian")
                        .WithMany("MedicalRecords")
                        .HasForeignKey("VetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Veterinarian");
                });

            modelBuilder.Entity("Domain.OwnerPetListing", b =>
                {
                    b.HasOne("Domain.User", "Adopter")
                        .WithMany()
                        .HasForeignKey("AdopterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Pet", "Pet")
                        .WithMany("OwnerPetListings")
                        .HasForeignKey("PetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.OwnerSurrenderReason", "SurrenderReason")
                        .WithMany()
                        .HasForeignKey("SurrenderReasonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Adopter");

                    b.Navigation("Pet");

                    b.Navigation("SurrenderReason");
                });

            modelBuilder.Entity("Domain.OwnerPetListingDocument", b =>
                {
                    b.HasOne("Domain.OwnerPetListing", "Listing")
                        .WithMany("OwnerPetListingDocuments")
                        .HasForeignKey("ListingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Listing");
                });

            modelBuilder.Entity("Domain.Pet", b =>
                {
                    b.HasOne("Domain.AdoptionStatus", "AdoptionStatus")
                        .WithMany("Pets")
                        .HasForeignKey("AdoptionStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.HealthStatus", "HealthStatus")
                        .WithMany("Pets")
                        .HasForeignKey("HealthStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.PetGender", "PetGender")
                        .WithMany("Pets")
                        .HasForeignKey("PetGenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.PetSize", "PetSize")
                        .WithMany("Pets")
                        .HasForeignKey("PetSizeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.PetType", "PetType")
                        .WithMany("Pets")
                        .HasForeignKey("PetTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AdoptionStatus");

                    b.Navigation("HealthStatus");

                    b.Navigation("PetGender");

                    b.Navigation("PetSize");

                    b.Navigation("PetType");
                });

            modelBuilder.Entity("Domain.ShelterPetListing", b =>
                {
                    b.HasOne("Domain.MedicalRecord", "MedicalRecord")
                        .WithMany()
                        .HasForeignKey("MedicalRecordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Pet", "Pet")
                        .WithMany("ShelterPetListings")
                        .HasForeignKey("PetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Shelter", "Shelter")
                        .WithMany()
                        .HasForeignKey("ShelterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MedicalRecord");

                    b.Navigation("Pet");

                    b.Navigation("Shelter");
                });

            modelBuilder.Entity("Domain.Vaccination", b =>
                {
                    b.HasOne("Domain.MedicalRecord", "MedicalRecord")
                        .WithMany("Vaccinations")
                        .HasForeignKey("MedicalRecordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MedicalRecord");
                });

            modelBuilder.Entity("Domain.VeterinarianSpecilization", b =>
                {
                    b.HasOne("Domain.Veterinarian", "Veterinarian")
                        .WithMany("VetSpecializations")
                        .HasForeignKey("VeterinarianId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Veterinarian");
                });

            modelBuilder.Entity("Domain.AdoptionStatus", b =>
                {
                    b.Navigation("Pets");
                });

            modelBuilder.Entity("Domain.HealthStatus", b =>
                {
                    b.Navigation("Pets");
                });

            modelBuilder.Entity("Domain.MedicalRecord", b =>
                {
                    b.Navigation("MedicalConditions");

                    b.Navigation("Vaccinations");
                });

            modelBuilder.Entity("Domain.OwnerPetListing", b =>
                {
                    b.Navigation("OwnerPetListingDocuments");
                });

            modelBuilder.Entity("Domain.Pet", b =>
                {
                    b.Navigation("Adoptions");

                    b.Navigation("OwnerPetListings");

                    b.Navigation("ShelterPetListings");
                });

            modelBuilder.Entity("Domain.PetGender", b =>
                {
                    b.Navigation("Pets");
                });

            modelBuilder.Entity("Domain.PetSize", b =>
                {
                    b.Navigation("AdopterPetSizesPreference");

                    b.Navigation("Pets");
                });

            modelBuilder.Entity("Domain.PetType", b =>
                {
                    b.Navigation("Pets");
                });

            modelBuilder.Entity("Domain.Veterinarian", b =>
                {
                    b.Navigation("MedicalRecords");

                    b.Navigation("VetSpecializations");
                });
#pragma warning restore 612, 618
        }
    }
}