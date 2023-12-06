﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using orgcat.postgresdb;

#nullable disable

namespace orgcat.postgresdb.Migrations
{
    [DbContext(typeof(OrgCatDb))]
    [Migration("20231204053430_MakeResponseIdUnique")]
    partial class MakeResponseIdUnique
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.DataProtection.EntityFrameworkCore.DataProtectionKey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("FriendlyName")
                        .HasColumnType("text");

                    b.Property<string>("Xml")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("DataProtectionKeys");
                });

            modelBuilder.Entity("orgcat.postgresdb.Entities.Survey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Surveys");
                });

            modelBuilder.Entity("orgcat.postgresdb.Entities.SurveyQuestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("QuestionText")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("SurveyId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SurveyId");

                    b.ToTable("SurveyQuestions");
                });

            modelBuilder.Entity("orgcat.postgresdb.Entities.SurveyQuestionResponse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("QuestionId")
                        .HasColumnType("integer");

                    b.Property<string>("ResponseText")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("SurveyResponseId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.HasIndex("SurveyResponseId");

                    b.ToTable("SurveyQuestionResponses");
                });

            modelBuilder.Entity("orgcat.postgresdb.Entities.SurveyResponse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ResponseId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("SurveyId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ResponseId")
                        .IsUnique();

                    b.HasIndex("SurveyId");

                    b.ToTable("SurveyResponses");
                });

            modelBuilder.Entity("orgcat.postgresdb.Entities.SurveyQuestion", b =>
                {
                    b.HasOne("orgcat.postgresdb.Entities.Survey", "Survey")
                        .WithMany("SurveyQuestions")
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Survey");
                });

            modelBuilder.Entity("orgcat.postgresdb.Entities.SurveyQuestionResponse", b =>
                {
                    b.HasOne("orgcat.postgresdb.Entities.SurveyQuestion", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("orgcat.postgresdb.Entities.SurveyResponse", "SurveyResponse")
                        .WithMany("SurveyQuestionResponses")
                        .HasForeignKey("SurveyResponseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");

                    b.Navigation("SurveyResponse");
                });

            modelBuilder.Entity("orgcat.postgresdb.Entities.SurveyResponse", b =>
                {
                    b.HasOne("orgcat.postgresdb.Entities.Survey", "Survey")
                        .WithMany()
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Survey");
                });

            modelBuilder.Entity("orgcat.postgresdb.Entities.Survey", b =>
                {
                    b.Navigation("SurveyQuestions");
                });

            modelBuilder.Entity("orgcat.postgresdb.Entities.SurveyResponse", b =>
                {
                    b.Navigation("SurveyQuestionResponses");
                });
#pragma warning restore 612, 618
        }
    }
}
