﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Models;

public partial class DbRenalCarContext : DbContext
{
    public DbRenalCarContext()
    {
    }

    public DbRenalCarContext(DbContextOptions<DbRenalCarContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<BlogComment> BlogComments { get; set; }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<CarRentalOrder> CarRentalOrders { get; set; }

    public virtual DbSet<CarType> CarTypes { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Contract> Contracts { get; set; }

    public virtual DbSet<ContractDetail> ContractDetails { get; set; }

    public virtual DbSet<ContractSettlement> ContractSettlements { get; set; }

    public virtual DbSet<ContractSettlementDetail> ContractSettlementDetails { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.ToTable("Account");

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FullName).HasMaxLength(50);
            entity.Property(e => e.LastLogin)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.Customer).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_Account_Customers");

            entity.HasOne(d => d.Role).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_Account_Role");
        });

        modelBuilder.Entity<Blog>(entity =>
        {
            entity.ToTable("Blog");

            entity.Property(e => e.Alias).HasMaxLength(250);
            entity.Property(e => e.CreatedBy).HasMaxLength(150);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(4000);
            entity.Property(e => e.Image).HasMaxLength(500);
            entity.Property(e => e.ModifiedBy).HasMaxLength(150);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.SeoDescription).HasMaxLength(500);
            entity.Property(e => e.SeoKeywords).HasMaxLength(250);
            entity.Property(e => e.SeoTitle).HasMaxLength(250);
            entity.Property(e => e.Title).HasMaxLength(250);

            entity.HasOne(d => d.Account).WithMany(p => p.Blogs)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK_Blog_Account");
        });

        modelBuilder.Entity<BlogComment>(entity =>
        {
            entity.HasKey(e => e.CommentId);

            entity.ToTable("BlogComment");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Detail).HasMaxLength(200);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(50);

            entity.HasOne(d => d.Blog).WithMany(p => p.BlogComments)
                .HasForeignKey(d => d.BlogId)
                .HasConstraintName("FK_BlogComment_Blog");
        });

        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.CarId).HasName("PK__Cars__68A0340E10AA00BE");

            entity.Property(e => e.CarId).HasColumnName("CarID");
            entity.Property(e => e.Alias)
                .HasMaxLength(50)
                .HasColumnName("alias");
            entity.Property(e => e.CarBrand).HasMaxLength(50);
            entity.Property(e => e.CarName).HasMaxLength(100);
            entity.Property(e => e.CarTypeId).HasColumnName("CarTypeID");
            entity.Property(e => e.Color).HasMaxLength(50);
            entity.Property(e => e.Condition).HasMaxLength(100);
            entity.Property(e => e.Deposit).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.LicensePlate).HasMaxLength(20);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Rate).HasColumnName("rate");
            entity.Property(e => e.SalePrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.CarType).WithMany(p => p.Cars)
                .HasForeignKey(d => d.CarTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cars__CarTypeID__286302EC");
        });

        modelBuilder.Entity<CarRentalOrder>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__CarRenta__C3905BAF612B80AC");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Deposit).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Payment).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.StatusId).HasColumnName("StatusID");

            entity.HasOne(d => d.Customer).WithMany(p => p.CarRentalOrders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CarRental__Custo__2D27B809");

            entity.HasOne(d => d.Status).WithMany(p => p.CarRentalOrders)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK__CarRental__Statu__2E1BDC42");
        });

        modelBuilder.Entity<CarType>(entity =>
        {
            entity.HasKey(e => e.CarTypeId).HasName("PK__CarTypes__2B2E84BDA5A9E5DA");

            entity.Property(e => e.CarTypeId).HasColumnName("CarTypeID");
            entity.Property(e => e.CarTypeName).HasMaxLength(100);
            entity.Property(e => e.Manufacturer).HasMaxLength(100);
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Contact");

            entity.Property(e => e.ContactId).ValueGeneratedOnAdd();
            entity.Property(e => e.CreatedBy).HasMaxLength(150);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.ModifiedBy).HasMaxLength(150);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.Phone).HasMaxLength(50);
        });

        modelBuilder.Entity<Contract>(entity =>
        {
            entity.HasKey(e => e.ContractId).HasName("PK__Contract__C90D3409E955298D");

            entity.ToTable("Contract");

            entity.Property(e => e.ContractId).HasColumnName("ContractID");
            entity.Property(e => e.AdvancePayment).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

            entity.HasOne(d => d.Customer).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Contracts__Custo__30F848ED");
        });

        modelBuilder.Entity<ContractDetail>(entity =>
        {
            entity.HasKey(e => e.ContractDetailId).HasName("PK__Contract__CCA7AF02CDE85DA8");

            entity.Property(e => e.ContractDetailId).HasColumnName("ContractDetailID");
            entity.Property(e => e.CarTypeId).HasColumnName("CarTypeID");
            entity.Property(e => e.ContractId).HasColumnName("ContractID");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.CarType).WithMany(p => p.ContractDetails)
                .HasForeignKey(d => d.CarTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ContractD__CarTy__38996AB5");

            entity.HasOne(d => d.Contract).WithMany(p => p.ContractDetails)
                .HasForeignKey(d => d.ContractId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ContractD__Contr__37A5467C");
        });

        modelBuilder.Entity<ContractSettlement>(entity =>
        {
            entity.HasKey(e => e.SettlementId).HasName("PK__Contract__771254BA0D921783");

            entity.Property(e => e.SettlementId).HasColumnName("SettlementID");
            entity.Property(e => e.AdvancePayment).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ContractId).HasColumnName("ContractID");
            entity.Property(e => e.PaidAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PaymentMethod).HasMaxLength(50);
            entity.Property(e => e.TotalPayment).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Contract).WithMany(p => p.ContractSettlements)
                .HasForeignKey(d => d.ContractId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ContractS__Contr__3B75D760");
        });

        modelBuilder.Entity<ContractSettlementDetail>(entity =>
        {
            entity.HasKey(e => e.SettlementDetailId).HasName("PK__Contract__6BEA900069DBAA63");

            entity.Property(e => e.SettlementDetailId).HasColumnName("SettlementDetailID");
            entity.Property(e => e.CarId).HasColumnName("CarID");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SettlementId).HasColumnName("SettlementID");
            entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Car).WithMany(p => p.ContractSettlementDetails)
                .HasForeignKey(d => d.CarId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ContractS__CarID__3F466844");

            entity.HasOne(d => d.Settlement).WithMany(p => p.ContractSettlementDetails)
                .HasForeignKey(d => d.SettlementId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ContractS__Settl__3E52440B");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64B8D5A7BBA1");

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.AccountNumber).HasMaxLength(50);
            entity.Property(e => e.Bank).HasMaxLength(50);
            entity.Property(e => e.CompanyName).HasMaxLength(100);
            entity.Property(e => e.Idcard)
                .HasMaxLength(20)
                .HasColumnName("IDCard");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.ToTable("Menu");

            entity.Property(e => e.MenuId).HasColumnName("MenuID");
            entity.Property(e => e.Alias).HasMaxLength(150);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Title).HasMaxLength(150);
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Alias).HasMaxLength(250);
            entity.Property(e => e.CreatedBy).HasMaxLength(150);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(4000);
            entity.Property(e => e.Image).HasMaxLength(500);
            entity.Property(e => e.ModifiedBy).HasMaxLength(150);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.NewsId).ValueGeneratedOnAdd();
            entity.Property(e => e.SeoDescription).HasMaxLength(500);
            entity.Property(e => e.SeoKeywords).HasMaxLength(250);
            entity.Property(e => e.SeoTitle).HasMaxLength(250);
            entity.Property(e => e.Title).HasMaxLength(250);
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK__OrderDet__D3B9D30C2C8C4FE9");

            entity.Property(e => e.OrderDetailId).HasColumnName("OrderDetailID");
            entity.Property(e => e.CarId).HasColumnName("CarID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Car).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.CarId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderDeta__CarID__34C8D9D1");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderDeta__Order__33D4B598");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__OrderSta__C8EE20433338C4B8");

            entity.ToTable("OrderStatus");

            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.StatusDescription).HasMaxLength(100);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.RoleId).ValueGeneratedNever();
            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}