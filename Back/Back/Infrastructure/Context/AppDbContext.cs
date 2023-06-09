﻿using System;
using System.Collections.Generic;
using Back.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Back.Infrastructure.Context;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<City> City { get; set; }

    public virtual DbSet<IncidentType> IncidentType { get; set; }

    public virtual DbSet<Incidents> Incidents { get; set; }

    public virtual DbSet<Notification> Notification { get; set; }

    public virtual DbSet<Power> Power { get; set; }

    public virtual DbSet<PowerIncidentType> PowerIncidentType { get; set; }

    public virtual DbSet<Role> Role { get; set; }

    public virtual DbSet<Users> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("city_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('power_id_seq'::regclass)");
        });

        modelBuilder.Entity<IncidentType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("type_incident_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('type_incident_id_seq'::regclass)");
        });

        modelBuilder.Entity<Incidents>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("incidents_pkey");

            entity.HasOne(d => d.City).WithMany(p => p.Incidents).HasConstraintName("incidents_city_id_fkey");

            entity.HasOne(d => d.IncidentType).WithMany(p => p.Incidents)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("incidents_incident_type_id_fkey");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("notification_pkey");

            entity.HasOne(d => d.Incidents).WithMany(p => p.Notification).HasConstraintName("notification_incidents_id_fkey");
        });

        modelBuilder.Entity<Power>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("power_pkey");
        });

        modelBuilder.Entity<PowerIncidentType>(entity =>
        {
            entity.HasOne(d => d.IncidentType).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("power_type_incident_type_incident_id_fkey");

            entity.HasOne(d => d.Power).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("power_type_incident_power_id_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("role_pkey");
        });

        modelBuilder.Entity<Users>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.Property(e => e.RoleId).HasDefaultValueSql("1");

            entity.HasOne(d => d.Power).WithMany(p => p.Users).HasConstraintName("users_power_id_fkey");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_role_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
