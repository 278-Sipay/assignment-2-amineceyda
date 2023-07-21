﻿// <auto-generated />

using Assignment2Api.Data.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;



namespace Assignment2Api.Data.Migrations
{
    [DbContext(typeof(SimDbContext))]
    partial class SimDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SipayApi.Data.Domain.Account", b =>
                {
                    b.Property<int>("AccountNumber")
                        .HasColumnType("integer");

                    b.Property<decimal>("Balance")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(15, 4)
                        .HasColumnType("numeric(15,4)")
                        .HasDefaultValue(0m);

                    b.Property<string>("CurrencyCode")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("character varying(4)");

                    b.Property<int>("CustomerNumber")
                        .HasColumnType("integer");

                    b.Property<DateTime>("InsertDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("InsertUser")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime>("OpenDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("AccountNumber");

                    b.HasIndex("AccountNumber")
                        .IsUnique();

                    b.HasIndex("CustomerNumber");

                    b.ToTable("Account", "dbo");
                });

            modelBuilder.Entity("SipayApi.Data.Domain.Customer", b =>
                {
                    b.Property<int>("CustomerNumber")
                        .HasColumnType("integer");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(350)
                        .HasColumnType("character varying(350)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTime>("InsertDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("InsertUser")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("CustomerNumber");

                    b.HasIndex("CustomerNumber")
                        .IsUnique();

                    b.ToTable("Customer", "dbo");
                });

            modelBuilder.Entity("SipayApi.Data.Domain.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountNumber")
                        .HasColumnType("integer");

                    b.Property<decimal>("CreditAmount")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(15, 4)
                        .HasColumnType("numeric(15,4)")
                        .HasDefaultValue(0m);

                    b.Property<decimal>("DebitAmount")
                        .HasMaxLength(0)
                        .HasPrecision(15, 4)
                        .HasColumnType("numeric(15,4)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<DateTime>("InsertDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("InsertUser")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("ReferenceNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("AccountNumber");

                    b.HasIndex("ReferenceNumber");

                    b.ToTable("Transaction", "dbo");
                });

            modelBuilder.Entity("SipayApi.Data.Domain.Account", b =>
                {
                    b.HasOne("SipayApi.Data.Domain.Customer", "Customer")
                        .WithMany("Accounts")
                        .HasForeignKey("CustomerNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("SipayApi.Data.Domain.Transaction", b =>
                {
                    b.HasOne("SipayApi.Data.Domain.Account", "Account")
                        .WithMany("Transactions")
                        .HasForeignKey("AccountNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("SipayApi.Data.Domain.Account", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("SipayApi.Data.Domain.Customer", b =>
                {
                    b.Navigation("Accounts");
                });
#pragma warning restore 612, 618
        }
    }
}
