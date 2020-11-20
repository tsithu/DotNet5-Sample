﻿// <auto-generated />
using System;
using DotNet5WebApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DotNet5_Sample.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20201120171155_CreateDatabase")]
    partial class CreateDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("DotNet5WebApi.Models.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<decimal>("Amount")
                        .HasColumnType("TEXT")
                        .HasColumnName("amount");

                    b.Property<string>("Code")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasColumnName("code");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT")
                        .HasColumnName("created_date");

                    b.Property<string>("CurrencyCode")
                        .HasMaxLength(20)
                        .HasColumnType("TEXT")
                        .HasColumnName("currency_code");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Status")
                        .HasColumnType("TEXT")
                        .HasColumnName("status");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("TEXT")
                        .HasColumnName("transaction_date");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("TEXT")
                        .HasColumnName("updated_date");

                    b.HasKey("Id");

                    b.ToTable("transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
