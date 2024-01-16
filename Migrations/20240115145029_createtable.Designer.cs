﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Students_Management_Api;

#nullable disable

namespace Students_Management_Api.Migrations
{
    [DbContext(typeof(LibraryContext))]
    [Migration("20240115145029_createtable")]
    partial class createtable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ClassStudent", b =>
                {
                    b.Property<int>("ClassesId")
                        .HasColumnType("int");

                    b.Property<int>("StudentsId")
                        .HasColumnType("int");

                    b.HasKey("ClassesId", "StudentsId");

                    b.HasIndex("StudentsId");

                    b.ToTable("ClassStudent");
                });

            modelBuilder.Entity("Students_Management_Api.Models.Class", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("TeacherId");

                    b.ToTable("Class");
                });

            modelBuilder.Entity("Students_Management_Api.Models.Lecture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("TeacherID")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("TeacherID");

                    b.ToTable("Lecture");
                });

            modelBuilder.Entity("Students_Management_Api.Models.LectureStudent", b =>
                {
                    b.Property<int>("LecturesId")
                        .HasColumnType("int");

                    b.Property<int>("StudentsId")
                        .HasColumnType("int");

                    b.HasKey("LecturesId", "StudentsId");

                    b.HasIndex("StudentsId");

                    b.ToTable("LectureStudent");
                });

            modelBuilder.Entity("Students_Management_Api.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Department")
                        .HasColumnType("longtext");

                    b.Property<string>("Faculty")
                        .HasColumnType("longtext");

                    b.Property<string>("Firstname")
                        .HasColumnType("longtext");

                    b.Property<string>("Phone")
                        .HasColumnType("longtext");

                    b.Property<string>("Surname")
                        .HasColumnType("longtext");

                    b.Property<string>("Tc")
                        .HasColumnType("longtext");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("Year")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("birth")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Student");
                });

            modelBuilder.Entity("Students_Management_Api.Models.StudentMessages", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Body")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("ReceiverId")
                        .HasColumnType("int");

                    b.Property<int>("SenderId")
                        .HasColumnType("int");

                    b.Property<bool?>("Status")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Subject")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.ToTable("StudentMessages");
                });

            modelBuilder.Entity("Students_Management_Api.Models.StudentTeacherMessage", b =>
                {
                    b.Property<int>("ReceivedId")
                        .HasColumnType("int");

                    b.Property<int>("ReceiversId")
                        .HasColumnType("int");

                    b.HasKey("ReceivedId", "ReceiversId");

                    b.HasIndex("ReceiversId");

                    b.ToTable("StudentTeacherMessage");
                });

            modelBuilder.Entity("Students_Management_Api.Models.Supervisor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Phone")
                        .HasColumnType("longtext");

                    b.Property<string>("Surname")
                        .HasColumnType("longtext");

                    b.Property<string>("Tc")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Supervisor");
                });

            modelBuilder.Entity("Students_Management_Api.Models.Teacher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Phone")
                        .HasColumnType("longtext");

                    b.Property<string>("Study")
                        .HasColumnType("longtext");

                    b.Property<string>("Surname")
                        .HasColumnType("longtext");

                    b.Property<string>("Tc")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Teacher");
                });

            modelBuilder.Entity("Students_Management_Api.Models.TeacherMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Body")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("SenderId")
                        .HasColumnType("int");

                    b.Property<bool?>("Status")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Subject")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("SenderId");

                    b.ToTable("TeacherMessage");
                });

            modelBuilder.Entity("Students_Management_Api.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("ClassStudent", b =>
                {
                    b.HasOne("Students_Management_Api.Models.Class", null)
                        .WithMany()
                        .HasForeignKey("ClassesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Students_Management_Api.Models.Student", null)
                        .WithMany()
                        .HasForeignKey("StudentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Students_Management_Api.Models.Class", b =>
                {
                    b.HasOne("Students_Management_Api.Models.Teacher", "Teacher")
                        .WithMany("Classes")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("Students_Management_Api.Models.Lecture", b =>
                {
                    b.HasOne("Students_Management_Api.Models.Teacher", "Teacher")
                        .WithMany("Lectures")
                        .HasForeignKey("TeacherID");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("Students_Management_Api.Models.LectureStudent", b =>
                {
                    b.HasOne("Students_Management_Api.Models.Lecture", "Lecture")
                        .WithMany()
                        .HasForeignKey("LecturesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Students_Management_Api.Models.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lecture");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Students_Management_Api.Models.Student", b =>
                {
                    b.HasOne("Students_Management_Api.Models.User", "User")
                        .WithOne()
                        .HasForeignKey("Students_Management_Api.Models.Student", "UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Students_Management_Api.Models.StudentMessages", b =>
                {
                    b.HasOne("Students_Management_Api.Models.Teacher", "Receiver")
                        .WithMany("Received")
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Students_Management_Api.Models.Student", "Sender")
                        .WithMany("Sents")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Receiver");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("Students_Management_Api.Models.StudentTeacherMessage", b =>
                {
                    b.HasOne("Students_Management_Api.Models.TeacherMessage", "Received")
                        .WithMany()
                        .HasForeignKey("ReceivedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Students_Management_Api.Models.Student", "Receivers")
                        .WithMany()
                        .HasForeignKey("ReceiversId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Received");

                    b.Navigation("Receivers");
                });

            modelBuilder.Entity("Students_Management_Api.Models.Supervisor", b =>
                {
                    b.HasOne("Students_Management_Api.Models.User", "User")
                        .WithOne()
                        .HasForeignKey("Students_Management_Api.Models.Supervisor", "UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Students_Management_Api.Models.Teacher", b =>
                {
                    b.HasOne("Students_Management_Api.Models.User", "User")
                        .WithOne()
                        .HasForeignKey("Students_Management_Api.Models.Teacher", "UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Students_Management_Api.Models.TeacherMessage", b =>
                {
                    b.HasOne("Students_Management_Api.Models.Teacher", "Sender")
                        .WithMany("Sents")
                        .HasForeignKey("SenderId");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("Students_Management_Api.Models.Student", b =>
                {
                    b.Navigation("Sents");
                });

            modelBuilder.Entity("Students_Management_Api.Models.Teacher", b =>
                {
                    b.Navigation("Classes");

                    b.Navigation("Lectures");

                    b.Navigation("Received");

                    b.Navigation("Sents");
                });
#pragma warning restore 612, 618
        }
    }
}