﻿// <auto-generated />
using System;
using Microservice.Demo.Service.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Microservice.Demo.Service.Migrations
{
    [DbContext(typeof(ServiceDbContext))]
    [Migration("20200309095000_init_db")]
    partial class init_db
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Microservice.Demo.Service.Domain.Aggregates.Verification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int");

                    b.Property<int>("BizCode")
                        .HasColumnName("biz_code")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnName("code")
                        .HasColumnType("varchar(80) CHARACTER SET utf8mb4")
                        .HasMaxLength(80);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnName("created_on")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("ExpiredOn")
                        .HasColumnName("expired_on")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsSuspend")
                        .HasColumnName("is_suspend")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsUsed")
                        .HasColumnName("is_used")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("LastUpdOn")
                        .HasColumnName("updated_on")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("To")
                        .IsRequired()
                        .HasColumnName("message_to")
                        .HasColumnType("varchar(80) CHARACTER SET utf8mb4")
                        .HasMaxLength(80);

                    b.HasKey("Id");

                    b.ToTable("messages");
                });
#pragma warning restore 612, 618
        }
    }
}
