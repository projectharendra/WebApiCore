using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApiCore.Models
{
    public partial class DemoDbContext : DbContext
    {
        public DemoDbContext()
        {
        }

        public DemoDbContext(DbContextOptions<DemoDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<TblCategory> TblCategory { get; set; }
        public virtual DbSet<TblCrudNetCore> TblCrudNetCore { get; set; }
        public virtual DbSet<TblCustomer> TblCustomer { get; set; }
        public virtual DbSet<TblDesignation> TblDesignation { get; set; }
        public virtual DbSet<TblEmployee> TblEmployee { get; set; }
        public virtual DbSet<TblMenu> TblMenu { get; set; }
        public virtual DbSet<TblPermission> TblPermission { get; set; }
        public virtual DbSet<TblProduct> TblProduct { get; set; }
        public virtual DbSet<TblRefreshtoken> TblRefreshtoken { get; set; }
        public virtual DbSet<TblRole> TblRole { get; set; }
        public virtual DbSet<TblSalesHeader> TblSalesHeader { get; set; }
        public virtual DbSet<TblSalesProductInfo> TblSalesProductInfo { get; set; }
        public virtual DbSet<TblUser> TblUser { get; set; }
        public virtual DbSet<TblUserMaster> TblUserMaster { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=LAPTOP-34RBJQ60;Database=DemoDb;User ID=sa;Password=Passw0rd;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.DepartmentId).ValueGeneratedOnAdd();

                entity.Property(e => e.DepartmentName)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.DateOfJoining).HasColumnType("date");

                entity.Property(e => e.Department)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeId).ValueGeneratedOnAdd();

                entity.Property(e => e.EmployeeName)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.PhotoFileName)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblCategory>(entity =>
            {
                entity.ToTable("tbl_Category");
            });

            modelBuilder.Entity<TblCrudNetCore>(entity =>
            {
                entity.ToTable("tblCrudNetCore");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblCustomer>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK_tbl_customer");

                entity.ToTable("tbl_Customer");

                entity.Property(e => e.Code).HasMaxLength(20);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreateUser).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyUser).HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Phoneno).HasMaxLength(50);
            });

            modelBuilder.Entity<TblDesignation>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK__TblDesig__A25C5AA69562B8A1");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblEmployee>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK__TblEmplo__A25C5AA67C655622");

                entity.Property(e => e.Designation)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblMenu>(entity =>
            {
                entity.ToTable("tbl_menu");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LinkName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblPermission>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.MenuId });

                entity.ToTable("tbl_permission");

                entity.Property(e => e.RoleId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MenuId)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblProduct>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.ToTable("tbl_product");

                entity.Property(e => e.Code)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 3)");
            });

            modelBuilder.Entity<TblRefreshtoken>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tblRefreshtoken");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.RefreshToken).HasMaxLength(500);

                entity.Property(e => e.TokenId)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblRole>(entity =>
            {
                entity.HasKey(e => e.Roleid);

                entity.ToTable("tbl_role");

                entity.Property(e => e.Roleid)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblSalesHeader>(entity =>
            {
                entity.HasKey(e => e.InvoiceNo)
                    .HasName("PK_tbl_SaleHeader");

                entity.ToTable("tbl_SalesHeader");

                entity.Property(e => e.InvoiceNo).HasMaxLength(20);

                entity.Property(e => e.CreateDate).HasColumnType("smalldatetime");

                entity.Property(e => e.CreateUser).HasMaxLength(50);

                entity.Property(e => e.CustomerId)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.CustomerName)
                    .HasColumnName("Customer Name")
                    .HasMaxLength(100);

                entity.Property(e => e.DeliveryAddress).HasColumnType("ntext");

                entity.Property(e => e.InvoiceDate).HasColumnType("smalldatetime");

                entity.Property(e => e.ModifyDate).HasColumnType("smalldatetime");

                entity.Property(e => e.ModifyUser).HasMaxLength(50);

                entity.Property(e => e.NetTotal).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Remarks).HasColumnType("ntext");

                entity.Property(e => e.Tax).HasColumnType("numeric(18, 4)");

                entity.Property(e => e.Total).HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<TblSalesProductInfo>(entity =>
            {
                entity.HasKey(e => new { e.InvoiceNo, e.ProductCode })
                    .HasName("PK_tbl_SalesInvoiceDetail");

                entity.ToTable("tbl_SalesProductInfo");

                entity.Property(e => e.InvoiceNo).HasMaxLength(20);

                entity.Property(e => e.ProductCode).HasMaxLength(20);

                entity.Property(e => e.CreateDate).HasColumnType("smalldatetime");

                entity.Property(e => e.CreateUser).HasMaxLength(50);

                entity.Property(e => e.ModifyDate).HasColumnType("smalldatetime");

                entity.Property(e => e.ModifyUser).HasMaxLength(50);

                entity.Property(e => e.ProductName).HasMaxLength(100);

                entity.Property(e => e.SalesPrice).HasColumnType("numeric(18, 3)");

                entity.Property(e => e.Total).HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tblUser");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblUserMaster>(entity =>
            {
                entity.HasKey(e => e.Userid);

                entity.ToTable("tbl_user_master");

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Isactive)
                    .HasColumnName("isactive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .HasColumnName("role")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
