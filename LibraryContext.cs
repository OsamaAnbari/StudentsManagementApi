using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MySql.EntityFrameworkCore.Extensions;
using System.Collections.Generic;
using System.Reflection.Emit;
using Students_Management_Api.Models;

namespace Students_Management_Api
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options)
        : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Teacher> Teacher { get; set; }
        public DbSet<Supervisor> Supervisor { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<StudentMessages> StudentMessages { get; set; }
        public DbSet<TeacherMessage> TeacherMessage { get; set; }
        public DbSet<Lecture> Lecture { get; set; }
        public DbSet<Class> Class { get; set; }
        public DbSet<LectureStudent> LectureStudent { get; set; }
        public DbSet<ClassStudent> ClassStudent { get; set; }
        public DbSet<StudentTeacherMessage> StudentTeacherMessage { get; set; }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=library;user=root;password=asdrasdr1");
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(entity => entity.Id);
                entity.Property(entity => entity.Username);
                entity.Property(entity => entity.Password);
                entity.Property(entity => entity.Role);
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.HasKey(entity => entity.Id);
                entity.Property(entity => entity.Firstname);
                entity.Property(entity => entity.Surname);
                entity.Property(entity => entity.Phone);
                entity.Property(entity => entity.Tc);
                entity.Property(entity => entity.Study);
                entity.HasOne(e => e.User).WithOne().HasForeignKey<Teacher>(e => e.UserId);
                entity.HasMany(e => e.Classes).WithOne(e => e.Teacher);
                entity.HasMany(e => e.Lectures).WithOne(e => e.Teacher);
                entity.HasMany(e => e.Received).WithOne(e => e.Receiver);
                entity.HasMany(e => e.Sents).WithOne(e => e.Sender);
                //entity.Navigation(e => e.Lectures);
            });

            modelBuilder.Entity<Supervisor>(entity =>
            {
                entity.HasKey(entity => entity.Id);
                entity.Property(entity => entity.Firstname);
                entity.Property(entity => entity.Surname);
                entity.Property(entity => entity.Phone);
                entity.Property(entity => entity.Tc);
                entity.HasOne(e => e.User).WithOne().HasForeignKey<Supervisor>(e => e.UserId);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(entity => entity.Id);
                entity.Property(entity => entity.Firstname).IsRequired(false);
                entity.Property(entity => entity.Surname).IsRequired(false);
                entity.Property(entity => entity.Phone).IsRequired(false);
                entity.Property(entity => entity.Tc).IsRequired(false);
                entity.Property(entity => entity.Faculty).IsRequired(false);
                entity.Property(entity => entity.Department).IsRequired(false);
                entity.Property(entity => entity.Year).IsRequired(false);
                entity.HasOne(e => e.User).WithOne().HasForeignKey<Student>(e => e.UserId);

                entity.HasMany(e => e.Classes).WithMany(e => e.Students)
                      .UsingEntity<ClassStudent>(
                      l => l.HasOne(e => e.Class).WithMany().HasForeignKey(e => e.ClassesId),
                      r => r.HasOne(e => e.Student).WithMany().HasForeignKey(e => e.StudentsId));

                entity.HasMany(e => e.Lectures).WithMany(e => e.Students)
                      .UsingEntity<LectureStudent>(
                      l => l.HasOne(e => e.Lecture).WithMany().HasForeignKey(e => e.LecturesId),
                      r => r.HasOne(e => e.Student).WithMany().HasForeignKey(e => e.StudentsId));

                entity.HasMany(e => e.Received).WithMany(e => e.Receivers);
                entity.HasMany(e => e.Sents).WithOne(e => e.Sender);
            });

            modelBuilder.Entity<Lecture>(entity =>
            {
                entity.HasKey(entity => entity.Id);
                entity.Property(entity => entity.Title);
                entity.Property(entity => entity.Date);
                entity.HasOne(e => e.Teacher).WithMany(e => e.Lectures).HasForeignKey(e => e.TeacherID);
                entity.HasMany(e => e.Students).WithMany(e => e.Lectures);
                //entity.Navigation(e => e.Students);
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.HasKey(entity => entity.Id);
                entity.Property(entity => entity.Time);
                entity.HasOne(e => e.Teacher).WithMany(e => e.Classes);
                entity.HasMany(e => e.Students).WithMany(e => e.Classes);
            });

            modelBuilder.Entity<StudentMessages>(entity =>
            {
                entity.HasKey(entity => entity.Id);
                entity.Property(entity => entity.Subject);
                entity.Property(entity => entity.Body);
                entity.Property(entity => entity.Status);
                entity.Property(entity => entity.Date);
                entity.HasOne(e => e.Sender).WithMany(e => e.Sents).HasForeignKey(e => e.SenderId);
                entity.HasOne(e => e.Receiver).WithMany(e => e.Received).HasForeignKey(e => e.ReceiverId);
            });

            modelBuilder.Entity<TeacherMessage>(entity =>
            {
                entity.HasKey(entity => entity.Id);
                entity.Property(entity => entity.Subject);
                entity.Property(entity => entity.Body);
                entity.Property(entity => entity.Status);
                entity.Property(entity => entity.Date);
                entity.HasOne(e => e.Sender).WithMany(e => e.Sents);
                entity.HasMany(e => e.Receivers).WithMany(e => e.Received)
                      .UsingEntity<StudentTeacherMessage>(
                      l => l.HasOne(e => e.Receivers).WithMany().HasForeignKey(e => e.ReceiversId),
                      r => r.HasOne(e => e.Received).WithMany().HasForeignKey(e => e.ReceivedId));
            });

            /*modelBuilder.Entity<LectureStudent>()
            .HasIndex(e => e.StudentsId)
            .IsUnique();*/
        }
    }
}
