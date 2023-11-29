﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using orgcat.postgresdb;

#nullable disable

namespace orgcat.postgresdb.Migrations
{
    [DbContext(typeof(OrgCatDb))]
    partial class OrgCatDbModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("orgcat.postgresdb.Entities.Dummy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Dummies");
                });

            modelBuilder.Entity("orgcat.postgresdb.Entities.SurveyQuestionResponse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ResponseText")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SurveyId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("SurveyId");

                    b.ToTable("SurveyQuestionResponses");
                });

            modelBuilder.Entity("orgcat.postgresdb.Entities.SurveyResponse", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("SurveyResponses");
                });

            modelBuilder.Entity("orgcat.postgresdb.Entities.SurveyQuestionResponse", b =>
                {
                    b.HasOne("orgcat.postgresdb.Entities.SurveyResponse", "Survey")
                        .WithMany("SurveyQuestionResponses")
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Survey");
                });

            modelBuilder.Entity("orgcat.postgresdb.Entities.SurveyResponse", b =>
                {
                    b.Navigation("SurveyQuestionResponses");
                });
#pragma warning restore 612, 618
        }
    }
}
