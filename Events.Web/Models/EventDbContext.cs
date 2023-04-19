using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Events.Web.Models;

public partial class EventDbContext : DbContext
{
    public EventDbContext()
    {
    }

    public EventDbContext(DbContextOptions<EventDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Eventattendee> Eventattendees { get; set; }

    public virtual DbSet<Eventcouponassignment> Eventcouponassignments { get; set; }

    public virtual DbSet<Eventcouponassignmentmapping> Eventcouponassignmentmappings { get; set; }

    public virtual DbSet<Eventcoupontype> Eventcoupontypes { get; set; }

    public virtual DbSet<Eventexpense> Eventexpenses { get; set; }

    public virtual DbSet<Eventsponsor> Eventsponsors { get; set; }

    public virtual DbSet<Eventsponsorsimage> Eventsponsorsimages { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost;user id=root;Password=;database=event_db;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("events");

            entity.HasIndex(e => e.CreatedBy, "CreatedBy");

            entity.HasIndex(e => e.ModifiedBy, "ModifiedBy");

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20)")
                .HasColumnName("ID");
            entity.Property(e => e.CreatedBy)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("bigint(20)");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.EventDate).HasColumnType("datetime");
            entity.Property(e => e.EventEndTime).HasColumnType("datetime");
            entity.Property(e => e.EventName).HasMaxLength(100);
            entity.Property(e => e.EventStartTime).HasColumnType("datetime");
            entity.Property(e => e.EventStatus).HasMaxLength(100);
            entity.Property(e => e.EventVenue).HasMaxLength(500);
            entity.Property(e => e.EventYear).HasColumnType("date");
            entity.Property(e => e.ModifiedBy)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("bigint(20)");
            entity.Property(e => e.ModifiedOn)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.EventCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("events_ibfk_1");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.EventModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("events_ibfk_2");
        });

        modelBuilder.Entity<Eventattendee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("eventattendees");

            entity.HasIndex(e => e.User, "User");

            entity.HasIndex(e => e.EventId, "eventattendees_ibfk_1");

            entity.HasIndex(e => e.CouponTypeId, "eventattendees_ibfk_4");

            entity.HasIndex(e => e.ModifiedBy, "eventattendees_ibfk_7");

            entity.HasIndex(e => e.CreatedBy, "eventattendees_ibfk_8");

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20)")
                .HasColumnName("ID");
            entity.Property(e => e.AttendeeName).HasMaxLength(200);
            entity.Property(e => e.ContactNo).HasMaxLength(20);
            entity.Property(e => e.CouponTypeId)
                .HasColumnType("bigint(20)")
                .HasColumnName("CouponTypeID");
            entity.Property(e => e.CouponsPurchased).HasMaxLength(300);
            entity.Property(e => e.CreatedBy).HasColumnType("bigint(20)");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.EventId)
                .HasColumnType("bigint(20)")
                .HasColumnName("EventID");
            entity.Property(e => e.User)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("bigint(20)");
            entity.Property(e => e.ModeOfPayment)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("enum('Cash','UPI','Bank_Transfer','Others')");
            entity.Property(e => e.ModifiedBy)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("bigint(20)");
            entity.Property(e => e.ModifiedOn)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.PaymentReference).HasMaxLength(255);
            entity.Property(e => e.PurchasedOn).HasColumnType("datetime");
            entity.Property(e => e.TotalAmount).HasPrecision(10);

            entity.HasOne(d => d.CouponType).WithMany(p => p.Eventattendees)
                .HasForeignKey(d => d.CouponTypeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("eventattendees_ibfk_4");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.EventattendeeCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("eventattendees_ibfk_8");

            entity.HasOne(d => d.Event).WithMany(p => p.Eventattendees)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("eventattendees_ibfk_1");

            entity.HasOne(d => d.UserNavigation).WithMany(p => p.EventattendeeUserNavigations)
                .HasForeignKey(d => d.User)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("eventattendees_ibfk_9");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.EventattendeeModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("eventattendees_ibfk_7");
        });

        modelBuilder.Entity<Eventcouponassignment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("eventcouponassignments");

            entity.HasIndex(e => e.CreatedBy, "CreatedBy");

            entity.HasIndex(e => e.User, "User");

            entity.HasIndex(e => e.ModifiedBy, "ModifiedBy");

            entity.HasIndex(e => e.EventId, "eventcouponassignments_ibfk_1");

            entity.HasIndex(e => e.CouponTypeId, "eventcouponassignments_ibfk_5");

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20)")
                .HasColumnName("ID");
            entity.Property(e => e.CouponTypeId)
                .HasColumnType("bigint(20)")
                .HasColumnName("CouponTypeID");
            entity.Property(e => e.CouponsFrom).HasColumnType("int(11)");
            entity.Property(e => e.CouponsTo).HasColumnType("int(11)");
            entity.Property(e => e.CreatedBy).HasColumnType("bigint(20)");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.EventId)
                .HasColumnType("bigint(20)")
                .HasColumnName("EventID");
            entity.Property(e => e.User)
                .HasColumnType("bigint(20)")
                .HasColumnName("User");
            entity.Property(e => e.ModifiedBy)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("bigint(20)");
            entity.Property(e => e.ModifiedOn)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.TotalCoupons).HasColumnType("bigint(20)");

            entity.HasOne(d => d.CouponType).WithMany(p => p.Eventcouponassignments)
                .HasForeignKey(d => d.CouponTypeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("eventcouponassignments_ibfk_5");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.EventcouponassignmentCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("eventcouponassignments_ibfk_3");

            entity.HasOne(d => d.Event).WithMany(p => p.Eventcouponassignments)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("eventcouponassignments_ibfk_1");

            entity.HasOne(d => d.Users).WithMany(p => p.EventcouponassignmentUsers)
                .HasForeignKey(d => d.User)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("eventcouponassignments_ibfk_2");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.EventcouponassignmentModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("eventcouponassignments_ibfk_4");
        });

        modelBuilder.Entity<Eventcouponassignmentmapping>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("eventcouponassignmentmapping");

            entity.HasIndex(e => e.Attendee, "Attendee");

            entity.HasIndex(e => e.CouponTypeId, "CouponTypeID");

            entity.HasIndex(e => e.EventId, "EventID");

            entity.HasIndex(e => e.User, "User");

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20)")
                .HasColumnName("ID");
            entity.Property(e => e.Attendee)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("bigint(20)");
            entity.Property(e => e.Booked)
                .HasDefaultValueSql("'''false'''")
                .HasColumnType("enum('true','false')")
                .HasColumnName("Booked?");
            entity.Property(e => e.CouponNumber).HasPrecision(10);
            entity.Property(e => e.CouponTypeId)
                .HasColumnType("bigint(20)")
                .HasColumnName("CouponTypeID");
            entity.Property(e => e.EventId)
                .HasColumnType("bigint(20)")
                .HasColumnName("EventID");
            entity.Property(e => e.User)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("bigint(20)");

            entity.HasOne(d => d.AttendeeNavigation).WithMany(p => p.Eventcouponassignmentmappings)
                .HasForeignKey(d => d.Attendee)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("eventcouponassignmentmapping_ibfk_4");

            entity.HasOne(d => d.CouponType).WithMany(p => p.Eventcouponassignmentmappings)
                .HasForeignKey(d => d.CouponTypeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("eventcouponassignmentmapping_ibfk_2");

            entity.HasOne(d => d.Event).WithMany(p => p.Eventcouponassignmentmappings)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("eventcouponassignmentmapping_ibfk_3");

            entity.HasOne(d => d.UserNavigation).WithMany(p => p.Eventcouponassignmentmappings)
                .HasForeignKey(d => d.User)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("eventcouponassignmentmapping_ibfk_1");
        });

        modelBuilder.Entity<Eventcoupontype>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("eventcoupontype");

            entity.HasIndex(e => e.EventId, "eventcoupontype_ibfk_3");

            entity.HasIndex(e => e.ModifiedBy, "eventcoupontype_ibfk_4");

            entity.HasIndex(e => e.CreatedBy, "eventcoupontype_ibfk_5");

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20)")
                .HasColumnName("ID");
            entity.Property(e => e.CouponName).HasMaxLength(100);
            entity.Property(e => e.CouponPrice).HasPrecision(10);
            entity.Property(e => e.CreatedBy).HasColumnType("bigint(20)");
            entity.Property(e => e.CreatedOn).HasMaxLength(6);
            entity.Property(e => e.EventId)
                .HasColumnType("bigint(20)")
                .HasColumnName("EventID");
            entity.Property(e => e.ModifiedBy)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("bigint(20)");
            entity.Property(e => e.ModifiedOn)
                .HasMaxLength(6)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.TotalCoupon).HasColumnType("double(10,0)");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.EventcoupontypeCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("eventcoupontype_ibfk_5");

            entity.HasOne(d => d.Event).WithMany(p => p.Eventcoupontypes)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("eventcoupontype_ibfk_3");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.EventcoupontypeModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("eventcoupontype_ibfk_4");
        });

        modelBuilder.Entity<Eventexpense>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("eventexpenses");

            entity.HasIndex(e => e.CreatedBy, "CreatedBy");

            entity.HasIndex(e => e.ModifiedBy, "ModifiedBy");

            entity.HasIndex(e => e.EventId, "eventexpenses_ibfk_1");

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20)")
                .HasColumnName("ID");
            entity.Property(e => e.AmountSpent).HasPrecision(10);
            entity.Property(e => e.CreatedBy).HasColumnType("bigint(11)");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.EventId)
                .HasColumnType("bigint(20)")
                .HasColumnName("EventID");
            entity.Property(e => e.ExpenseName).HasMaxLength(100);
            entity.Property(e => e.ExpenseSubject).HasMaxLength(300);
            entity.Property(e => e.ModifiedBy)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("bigint(20)");
            entity.Property(e => e.ModifiedOn)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.EventexpenseCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("eventexpenses_ibfk_2");

            entity.HasOne(d => d.Event).WithMany(p => p.Eventexpenses)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("eventexpenses_ibfk_1");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.EventexpenseModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("eventexpenses_ibfk_3");
        });

        modelBuilder.Entity<Eventsponsor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("eventsponsors");

            entity.HasIndex(e => e.CreatedBy, "CreatedBy");

            entity.HasIndex(e => e.ModifiedBy, "ModifiedBy");

            entity.HasIndex(e => e.EventId, "eventsponsors_ibfk_1");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("ID");
            entity.Property(e => e.AmountSponsored).HasPrecision(10);
            entity.Property(e => e.CreatedBy).HasColumnType("bigint(20)");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.EventId)
                .HasColumnType("bigint(20)")
                .HasColumnName("EventID");
            entity.Property(e => e.ModifiedBy)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("bigint(20)");
            entity.Property(e => e.ModifiedOn)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.SponsorName).HasMaxLength(200);
            entity.Property(e => e.SponsorOrganization).HasMaxLength(200);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.EventsponsorCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("eventsponsors_ibfk_2");

            entity.HasOne(d => d.Event).WithMany(p => p.Eventsponsors)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("eventsponsors_ibfk_1");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.EventsponsorModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("eventsponsors_ibfk_3");
        });

        modelBuilder.Entity<Eventsponsorsimage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("eventsponsorsimage");

            entity.HasIndex(e => e.EventId, "eventsponsorsimage_ibfk_1");

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20)")
                .HasColumnName("ID");
            entity.Property(e => e.EventId)
                .HasColumnType("bigint(20)")
                .HasColumnName("EventID");
            entity.Property(e => e.SponsorImage).HasMaxLength(255);

            entity.HasOne(d => d.Event).WithMany(p => p.Eventsponsorsimages)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("eventsponsorsimage_ibfk_1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.CreatedBy, "CreatedBy");

            entity.HasIndex(e => e.ModifiedBy, "ModifiedBy");

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20)")
                .HasColumnName("ID");
            entity.Property(e => e.AppointedOn)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedBy)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("bigint(20)");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Designation).HasMaxLength(200);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.ModifiedBy)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("bigint(20)");
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.InverseCreatedByNavigation)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("users_ibfk_2");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.InverseModifiedByNavigation)
                .HasForeignKey(d => d.ModifiedBy)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("users_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
