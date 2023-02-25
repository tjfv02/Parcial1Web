using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Parcial1_1201619.Models;

public partial class ParcialWeb1Context : DbContext
{
    public ParcialWeb1Context()
    {
    }

    public ParcialWeb1Context(DbContextOptions<ParcialWeb1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<PersonalInformation> PersonalInformations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__appointm__3213E83F08788F94");

            entity.ToTable("appointment");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AppointmentDate)
                .HasColumnType("date")
                .HasColumnName("appointment_date");
            entity.Property(e => e.AppointmentTime).HasColumnName("appointment_time");
            entity.Property(e => e.PersonalInformationDoctorId).HasColumnName("personal_information_doctor_id");
            entity.Property(e => e.PersonalInformationPatientId).HasColumnName("personal_information_patient_id");

            entity.HasOne(d => d.PersonalInformationDoctor).WithMany(p => p.AppointmentPersonalInformationDoctors)
                .HasForeignKey(d => d.PersonalInformationDoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_doctor_id");

            entity.HasOne(d => d.PersonalInformationPatient).WithMany(p => p.AppointmentPersonalInformationPatients)
                .HasForeignKey(d => d.PersonalInformationPatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_patient_id");
        });

        modelBuilder.Entity<PersonalInformation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__personal__3213E83F40840CED");

            entity.ToTable("personal_information");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.DateOfBirth)
                .HasColumnType("date")
                .HasColumnName("date_of_birth");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone_number");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
