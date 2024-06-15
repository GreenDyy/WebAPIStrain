using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebAPIStrain.Entities;

public partial class IrtContext : DbContext
{
    public IrtContext()
    {
    }

    public IrtContext(DbContextOptions<IrtContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AccountForCustomer> AccountForCustomers { get; set; }

    public virtual DbSet<AccountForEmployee> AccountForEmployees { get; set; }

    public virtual DbSet<Bill> Bills { get; set; }

    public virtual DbSet<BillDetail> BillDetails { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartDetail> CartDetails { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<ConditionalStrain> ConditionalStrains { get; set; }

    public virtual DbSet<ContentWork> ContentWorks { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Genu> Genus { get; set; }

    public virtual DbSet<IdentifyStrain> IdentifyStrains { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<IsolatorStrain> IsolatorStrains { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Partner> Partners { get; set; }

    public virtual DbSet<Phylum> Phylums { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectContent> ProjectContents { get; set; }

    public virtual DbSet<Province> Provinces { get; set; }

    public virtual DbSet<RoleForEmployee> RoleForEmployees { get; set; }

    public virtual DbSet<ScienceNewspaper> ScienceNewspapers { get; set; }

    public virtual DbSet<Species> Species { get; set; }

    public virtual DbSet<Strain> Strains { get; set; }

    public virtual DbSet<StrainApprovalHistory> StrainApprovalHistories { get; set; }

    public virtual DbSet<Ward> Wards { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=IRT;Persist Security Info=True;User ID=sa;Password=123;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccountForCustomer>(entity =>
        {
            entity.HasKey(e => e.IdCustomer).HasName("PK__AccountF__2D8FDE5FA6945EA5");

            entity.ToTable("AccountForCustomer");

            entity.HasIndex(e => e.Username, "UQ__AccountF__536C85E43300510A").IsUnique();

            entity.Property(e => e.IdCustomer)
                .HasMaxLength(50)
                .HasColumnName("ID_Customer");
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(100);
            entity.Property(e => e.Username).HasMaxLength(255);

            entity.HasOne(d => d.IdCustomerNavigation).WithOne(p => p.AccountForCustomer)
                .HasForeignKey<AccountForCustomer>(d => d.IdCustomer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccountForCustomer_Customer");
        });

        modelBuilder.Entity<AccountForEmployee>(entity =>
        {
            entity.HasKey(e => e.IdEmployee).HasName("PK__AccountF__D9EE4F36EA5EDE74");

            entity.ToTable("AccountForEmployee");

            entity.HasIndex(e => e.Username, "UQ__AccountF__536C85E4228C1588").IsUnique();

            entity.Property(e => e.IdEmployee)
                .HasMaxLength(50)
                .HasColumnName("ID_Employee");
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(100);
            entity.Property(e => e.Username).HasMaxLength(255);

            entity.HasOne(d => d.IdEmployeeNavigation).WithOne(p => p.AccountForEmployee)
                .HasForeignKey<AccountForEmployee>(d => d.IdEmployee)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccountForEmployee_Employee");
        });

        modelBuilder.Entity<Bill>(entity =>
        {
            entity.HasKey(e => e.IdBill).HasName("PK__Bill__F098680AD51345C5");

            entity.ToTable("Bill");

            entity.Property(e => e.IdBill)
                .HasMaxLength(50)
                .HasColumnName("ID_Bill");
            entity.Property(e => e.IdCustomer)
                .HasMaxLength(50)
                .HasColumnName("ID_Customer");
            entity.Property(e => e.IdEmployee)
                .HasMaxLength(50)
                .HasColumnName("ID_Employee");
            entity.Property(e => e.IdOrder).HasColumnName("ID_Order");
            entity.Property(e => e.StatusOfBill)
                .HasMaxLength(255)
                .HasColumnName("Status_Of_Bill");
            entity.Property(e => e.TypeOfBill)
                .HasMaxLength(255)
                .HasColumnName("Type_Of_Bill");

            entity.HasOne(d => d.IdCustomerNavigation).WithMany(p => p.Bills)
                .HasForeignKey(d => d.IdCustomer)
                .HasConstraintName("FK_BillOffline_Customer");

            entity.HasOne(d => d.IdEmployeeNavigation).WithMany(p => p.Bills)
                .HasForeignKey(d => d.IdEmployee)
                .HasConstraintName("FK_BillOffline_Employee");

            entity.HasOne(d => d.IdOrderNavigation).WithMany(p => p.Bills)
                .HasForeignKey(d => d.IdOrder)
                .HasConstraintName("FK__Bill__ID_Order__7A672E12");
        });

        modelBuilder.Entity<BillDetail>(entity =>
        {
            entity.HasKey(e => e.IdBillDetail).HasName("PK__BillDeta__3421CE5D695499EA");

            entity.ToTable("BillDetail");

            entity.Property(e => e.IdBillDetail).HasColumnName("ID_BillDetail");
            entity.Property(e => e.IdBill)
                .HasMaxLength(50)
                .HasColumnName("ID_Bill");
            entity.Property(e => e.IdStrain).HasColumnName("ID_Strain");

            entity.HasOne(d => d.IdBillNavigation).WithMany(p => p.BillDetails)
                .HasForeignKey(d => d.IdBill)
                .HasConstraintName("FK_BillOfflineDetail_Bill");

            entity.HasOne(d => d.IdStrainNavigation).WithMany(p => p.BillDetails)
                .HasForeignKey(d => d.IdStrain)
                .HasConstraintName("FK_BillOfflineDetail_Strain");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.IdCart).HasName("PK__Cart__72140ECFC2662ED7");

            entity.ToTable("Cart");

            entity.Property(e => e.IdCart).HasColumnName("ID_Cart");
            entity.Property(e => e.IdCustomer)
                .HasMaxLength(50)
                .HasColumnName("ID_Customer");
            entity.Property(e => e.TotalProduct).HasColumnName("Total_Product");

            entity.HasOne(d => d.IdCustomerNavigation).WithMany(p => p.Carts)
                .HasForeignKey(d => d.IdCustomer)
                .HasConstraintName("FK_Cart_Customer");
        });

        modelBuilder.Entity<CartDetail>(entity =>
        {
            entity.HasKey(e => e.IdCartDetail).HasName("PK__CartDeta__19B4E08205AEB8F4");

            entity.ToTable("CartDetail");

            entity.Property(e => e.IdCartDetail).HasColumnName("ID_CartDetail");
            entity.Property(e => e.IdCart).HasColumnName("ID_Cart");
            entity.Property(e => e.IdStrain).HasColumnName("ID_Strain");
            entity.Property(e => e.QuantityOfStrain).HasColumnName("Quantity_Of_Strain");

            entity.HasOne(d => d.IdCartNavigation).WithMany(p => p.CartDetails)
                .HasForeignKey(d => d.IdCart)
                .HasConstraintName("FK_CartDetail_Cart");

            entity.HasOne(d => d.IdStrainNavigation).WithMany(p => p.CartDetails)
                .HasForeignKey(d => d.IdStrain)
                .HasConstraintName("FK_CartDetail_Strain");
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.IdClass).HasName("PK__Class__D7CF744C601A1195");

            entity.ToTable("Class");

            entity.Property(e => e.IdClass).HasColumnName("ID_Class");
            entity.Property(e => e.IdPhylum).HasColumnName("ID_Phylum");
            entity.Property(e => e.NameClass)
                .HasMaxLength(255)
                .HasColumnName("Name_Class");

            entity.HasOne(d => d.IdPhylumNavigation).WithMany(p => p.Classes)
                .HasForeignKey(d => d.IdPhylum)
                .HasConstraintName("FK_Class_Phylum");
        });

        modelBuilder.Entity<ConditionalStrain>(entity =>
        {
            entity.HasKey(e => e.IdCondition).HasName("PK__Conditio__BA54C9AE40B717AF");

            entity.ToTable("ConditionalStrain");

            entity.Property(e => e.IdCondition).HasColumnName("ID_Condition");
            entity.Property(e => e.Duration)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)");
            entity.Property(e => e.LightIntensity)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("Light_Intensity");
            entity.Property(e => e.Medium)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)");
            entity.Property(e => e.Temperature)
                .HasMaxLength(255)
                .HasDefaultValueSql("(NULL)");
        });

        modelBuilder.Entity<ContentWork>(entity =>
        {
            entity.HasKey(e => e.IdContentWork).HasName("PK__ContentW__951336A76DBCBB1D");

            entity.ToTable("ContentWork");

            entity.Property(e => e.IdContentWork).HasColumnName("ID_ContentWork");
            entity.Property(e => e.IdEmployee)
                .HasMaxLength(50)
                .HasColumnName("ID_Employee");
            entity.Property(e => e.IdProjectContent).HasColumnName("ID_ProjectContent");
            entity.Property(e => e.NameContent).HasColumnName("Name_Content");
            entity.Property(e => e.Priority).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(255);

            entity.HasOne(d => d.IdEmployeeNavigation).WithMany(p => p.ContentWorks)
                .HasForeignKey(d => d.IdEmployee)
                .HasConstraintName("FK_ContentWork_Employee");

            entity.HasOne(d => d.IdProjectContentNavigation).WithMany(p => p.ContentWorks)
                .HasForeignKey(d => d.IdProjectContent)
                .HasConstraintName("FK_ContentWork_ProjectContent");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.IdCustomer).HasName("PK__Customer__2D8FDE5F417F43E5");

            entity.ToTable("Customer");

            entity.HasIndex(e => e.Email, "UC_Customer").IsUnique();

            entity.Property(e => e.IdCustomer)
                .HasMaxLength(50)
                .HasColumnName("ID_Customer");
            entity.Property(e => e.DateOfBirth).HasColumnName("Date_of_Birth");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.NameDistrict).HasMaxLength(255);
            entity.Property(e => e.NameProvince).HasMaxLength(255);
            entity.Property(e => e.NameWard).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("Phone_Number");
        });

        modelBuilder.Entity<District>(entity =>
        {
            entity.HasKey(e => e.IdDistricts).HasName("PK__District__A89A7C70F0F514BA");

            entity.Property(e => e.Name).HasMaxLength(255);

            entity.HasOne(d => d.IdProvincesNavigation).WithMany(p => p.Districts)
                .HasForeignKey(d => d.IdProvinces)
                .HasConstraintName("FK__Districts__IdPro__04E4BC85");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.IdEmployee).HasName("PK__Employee__D9EE4F36C14AF4D0");

            entity.ToTable("Employee");

            entity.Property(e => e.IdEmployee)
                .HasMaxLength(50)
                .HasColumnName("ID_Employee");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.DateOfBirth).HasColumnName("Date_of_Birth");
            entity.Property(e => e.Degree).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.IdCard)
                .HasMaxLength(12)
                .HasColumnName("ID_Card");
            entity.Property(e => e.IdRole).HasColumnName("ID_Role");
            entity.Property(e => e.ImageEmployee).HasColumnName("Image_Employee");
            entity.Property(e => e.JoinDate).HasColumnName("Join_Date");
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.NameDistrict).HasMaxLength(255);
            entity.Property(e => e.NameProvince).HasMaxLength(255);
            entity.Property(e => e.NameWard).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("Phone_Number");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.IdRole)
                .HasConstraintName("FK_Employee_RoleForEmployee");
        });

        modelBuilder.Entity<Genu>(entity =>
        {
            entity.HasKey(e => e.IdGenus).HasName("PK__Genus__7B3106852DE6DA63");

            entity.Property(e => e.IdGenus).HasColumnName("ID_Genus");
            entity.Property(e => e.IdClass).HasColumnName("ID_Class");
            entity.Property(e => e.NameGenus)
                .HasMaxLength(255)
                .HasColumnName("Name_Genus");

            entity.HasOne(d => d.IdClassNavigation).WithMany(p => p.Genus)
                .HasForeignKey(d => d.IdClass)
                .HasConstraintName("FK_Genus_Class");
        });

        modelBuilder.Entity<IdentifyStrain>(entity =>
        {
            entity.HasKey(e => new { e.IdEmployee, e.IdStrain }).HasName("PK__Identify__23CDA4B436042C76");

            entity.ToTable("IdentifyStrain");

            entity.Property(e => e.IdEmployee)
                .HasMaxLength(50)
                .HasColumnName("ID_Employee");
            entity.Property(e => e.IdStrain).HasColumnName("ID_Strain");
            entity.Property(e => e.YearOfIdentify).HasColumnName("Year_of_Identify");

            entity.HasOne(d => d.IdEmployeeNavigation).WithMany(p => p.IdentifyStrains)
                .HasForeignKey(d => d.IdEmployee)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IdentifyStrain_Employee");

            entity.HasOne(d => d.IdStrainNavigation).WithMany(p => p.IdentifyStrains)
                .HasForeignKey(d => d.IdStrain)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IdentifyStrain_Strain");
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.InventoryId).HasName("PK__Inventor__F5FDE6D3D1FAB8F2");

            entity.ToTable("Inventory");

            entity.Property(e => e.InventoryId).HasColumnName("InventoryID");
            entity.Property(e => e.IdStrain).HasColumnName("ID_Strain");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdStrainNavigation).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.IdStrain)
                .HasConstraintName("FK__Inventory__ID_St__09A971A2");
        });

        modelBuilder.Entity<IsolatorStrain>(entity =>
        {
            entity.HasKey(e => new { e.IdEmployee, e.IdStrain }).HasName("PK__Isolator__23CDA4B44889CF7B");

            entity.ToTable("IsolatorStrain");

            entity.Property(e => e.IdEmployee)
                .HasMaxLength(50)
                .HasColumnName("ID_Employee");
            entity.Property(e => e.IdStrain).HasColumnName("ID_Strain");
            entity.Property(e => e.YearOfIsolator).HasColumnName("Year_of_Isolator");

            entity.HasOne(d => d.IdEmployeeNavigation).WithMany(p => p.IsolatorStrains)
                .HasForeignKey(d => d.IdEmployee)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeeStrain_Employee");

            entity.HasOne(d => d.IdStrainNavigation).WithMany(p => p.IsolatorStrains)
                .HasForeignKey(d => d.IdStrain)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeeStrain_Strain");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.IdOrder).HasName("PK__Orders__EC9FA9558135FBF4");

            entity.Property(e => e.IdOrder).HasColumnName("ID_Order");
            entity.Property(e => e.DeliveryAddress).HasMaxLength(255);
            entity.Property(e => e.IdCustomer)
                .HasMaxLength(50)
                .HasColumnName("ID_Customer");
            entity.Property(e => e.IdEmployee)
                .HasMaxLength(50)
                .HasColumnName("ID_Employee");
            entity.Property(e => e.PaymentMethod).IsUnicode(false);
            entity.Property(e => e.Status).HasMaxLength(255);
            entity.Property(e => e.StatusOrder).HasMaxLength(255);

            entity.HasOne(d => d.IdCustomerNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdCustomer)
                .HasConstraintName("FK__Orders__ID_Custo__0E6E26BF");

            entity.HasOne(d => d.IdEmployeeNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdEmployee)
                .HasConstraintName("FK__Orders__ID_Emplo__0F624AF8");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.IdOrderDetail).HasName("PK__OrderDet__855D4EF583614C91");

            entity.ToTable("OrderDetail");

            entity.Property(e => e.IdOrderDetail).HasColumnName("ID_OrderDetail");
            entity.Property(e => e.IdOrder).HasColumnName("ID_Order");
            entity.Property(e => e.IdStrain).HasColumnName("ID_Strain");

            entity.HasOne(d => d.IdOrderNavigation).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.IdOrder)
                .HasConstraintName("FK__OrderDeta__ID_Or__0C85DE4D");

            entity.HasOne(d => d.IdStrainNavigation).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.IdStrain)
                .HasConstraintName("FK__OrderDeta__ID_St__0D7A0286");
        });

        modelBuilder.Entity<Partner>(entity =>
        {
            entity.HasKey(e => e.IdPartner).HasName("PK__Partner__B982253D1888B268");

            entity.ToTable("Partner");

            entity.Property(e => e.IdPartner).HasColumnName("ID_Partner");
            entity.Property(e => e.AddressCompany)
                .HasMaxLength(255)
                .HasColumnName("Address_Company");
            entity.Property(e => e.BankName)
                .HasMaxLength(255)
                .HasColumnName("Bank_Name");
            entity.Property(e => e.BankNumber)
                .HasMaxLength(255)
                .HasColumnName("Bank_Number");
            entity.Property(e => e.NameCompany)
                .HasMaxLength(255)
                .HasColumnName("Name_Company");
            entity.Property(e => e.NameDistrict).HasMaxLength(255);
            entity.Property(e => e.NamePartner)
                .HasMaxLength(255)
                .HasColumnName("Name_Partner");
            entity.Property(e => e.NameProvince).HasMaxLength(255);
            entity.Property(e => e.NameWard).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("Phone_Number");
            entity.Property(e => e.Position).HasMaxLength(255);
            entity.Property(e => e.QhnsNumber)
                .HasMaxLength(255)
                .HasColumnName("QHNS_Number");
        });

        modelBuilder.Entity<Phylum>(entity =>
        {
            entity.HasKey(e => e.IdPhylum).HasName("PK__Phylum__DE75F901107237FD");

            entity.ToTable("Phylum");

            entity.Property(e => e.IdPhylum).HasColumnName("ID_Phylum");
            entity.Property(e => e.NamePhylum)
                .HasMaxLength(255)
                .HasColumnName("Name_Phylum");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.IdProject).HasName("PK__Project__D310AEBF8BC4D9E5");

            entity.ToTable("Project");

            entity.Property(e => e.IdProject)
                .HasMaxLength(50)
                .HasColumnName("ID_Project");
            entity.Property(e => e.IdEmployee)
                .HasMaxLength(50)
                .HasColumnName("ID_Employee");
            entity.Property(e => e.IdPartner).HasColumnName("ID_Partner");
            entity.Property(e => e.Status).HasMaxLength(255);

            entity.HasOne(d => d.IdEmployeeNavigation).WithMany(p => p.Projects)
                .HasForeignKey(d => d.IdEmployee)
                .HasConstraintName("FK_Project_Employee");

            entity.HasOne(d => d.IdPartnerNavigation).WithMany(p => p.Projects)
                .HasForeignKey(d => d.IdPartner)
                .HasConstraintName("FK_Project_Partner");
        });

        modelBuilder.Entity<ProjectContent>(entity =>
        {
            entity.HasKey(e => e.IdProjectContent).HasName("PK__ProjectC__61A0E4E7EC135451");

            entity.ToTable("ProjectContent");

            entity.Property(e => e.IdProjectContent).HasColumnName("ID_ProjectContent");
            entity.Property(e => e.IdProject)
                .HasMaxLength(50)
                .HasColumnName("ID_Project");
            entity.Property(e => e.NameContent).HasColumnName("Name_Content");
            entity.Property(e => e.Priority).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(255);

            entity.HasOne(d => d.IdProjectNavigation).WithMany(p => p.ProjectContents)
                .HasForeignKey(d => d.IdProject)
                .HasConstraintName("FK_ProjectContent_Project");
        });

        modelBuilder.Entity<Province>(entity =>
        {
            entity.HasKey(e => e.IdProvinces).HasName("PK__Province__EED764E0AAE3F75B");

            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<RoleForEmployee>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("PK__RoleForE__43DCD32D77CAE87B");

            entity.ToTable("RoleForEmployee");

            entity.Property(e => e.IdRole).HasColumnName("ID_Role");
            entity.Property(e => e.RoleDescription).HasMaxLength(255);
            entity.Property(e => e.RoleName).HasMaxLength(100);
        });

        modelBuilder.Entity<ScienceNewspaper>(entity =>
        {
            entity.HasKey(e => e.IdNewspaper).HasName("PK__ScienceN__DD461981BBF9CA03");

            entity.ToTable("ScienceNewspaper");

            entity.Property(e => e.IdNewspaper).HasColumnName("ID_Newspaper");
            entity.Property(e => e.IdEmployee).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.IdEmployeeNavigation).WithMany(p => p.ScienceNewspapers)
                .HasForeignKey(d => d.IdEmployee)
                .HasConstraintName("FK__ScienceNe__IdEmp__19DFD96B");
        });

        modelBuilder.Entity<Species>(entity =>
        {
            entity.HasKey(e => e.IdSpecies).HasName("PK__Species__33D1C117FCE9791E");

            entity.Property(e => e.IdSpecies).HasColumnName("ID_Species");
            entity.Property(e => e.IdGenus).HasColumnName("ID_Genus");
            entity.Property(e => e.NameSpecies)
                .HasMaxLength(255)
                .HasColumnName("Name_Species");

            entity.HasOne(d => d.IdGenusNavigation).WithMany(p => p.Species)
                .HasForeignKey(d => d.IdGenus)
                .HasConstraintName("FK_Species_Genus");
        });

        modelBuilder.Entity<Strain>(entity =>
        {
            entity.HasKey(e => e.IdStrain).HasName("PK__Strain__A23EB82EEDD23D86");

            entity.ToTable("Strain");

            entity.Property(e => e.IdStrain).HasColumnName("ID_Strain");
            entity.Property(e => e.AgitationResistance)
                .HasMaxLength(255)
                .HasColumnName("Agitation_Resistance");
            entity.Property(e => e.CellSize)
                .HasMaxLength(255)
                .HasColumnName("Cell_Size");
            entity.Property(e => e.Characteristics).HasMaxLength(255);
            entity.Property(e => e.CollectionSite).HasColumnName("Collection_Site");
            entity.Property(e => e.CommonName)
                .HasMaxLength(255)
                .HasColumnName("Common_Name");
            entity.Property(e => e.Continent).HasMaxLength(255);
            entity.Property(e => e.Country).HasMaxLength(255);
            entity.Property(e => e.FormerName)
                .HasMaxLength(255)
                .HasColumnName("Former_Name");
            entity.Property(e => e.GeneInformation).HasColumnName("Gene_Information");
            entity.Property(e => e.IdCondition).HasColumnName("ID_Condition");
            entity.Property(e => e.IdSpecies).HasColumnName("ID_Species");
            entity.Property(e => e.ImageStrain)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("Image_Strain");
            entity.Property(e => e.IsolationSource)
                .HasMaxLength(255)
                .HasColumnName("Isolation_Source");
            entity.Property(e => e.Organization).HasMaxLength(255);
            entity.Property(e => e.Publications).HasMaxLength(255);
            entity.Property(e => e.RecommendedForTeaching)
                .HasMaxLength(20)
                .HasColumnName("Recommended_For_Teaching");
            entity.Property(e => e.Remarks).HasMaxLength(255);
            entity.Property(e => e.ScientificName)
                .HasMaxLength(255)
                .HasColumnName("Scientific_Name");
            entity.Property(e => e.StateOfStrain)
                .HasMaxLength(255)
                .HasColumnName("State_of_Strain");
            entity.Property(e => e.StrainNumber)
                .HasMaxLength(100)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("Strain_Number");
            entity.Property(e => e.SynonymStrain)
                .HasMaxLength(255)
                .HasColumnName("Synonym_Strain");
            entity.Property(e => e.ToxinProducer)
                .HasMaxLength(255)
                .HasColumnName("Toxin_Producer");

            entity.HasOne(d => d.IdConditionNavigation).WithMany(p => p.Strains)
                .HasForeignKey(d => d.IdCondition)
                .HasConstraintName("FK_Strain_Condition");

            entity.HasOne(d => d.IdSpeciesNavigation).WithMany(p => p.Strains)
                .HasForeignKey(d => d.IdSpecies)
                .HasConstraintName("FK_Strain_Species");
        });

        modelBuilder.Entity<StrainApprovalHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StrainAp__3214EC2757124F50");

            entity.ToTable("StrainApprovalHistory");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdStrain).HasColumnName("ID_Strain");
            entity.Property(e => e.Status).HasMaxLength(255);

            entity.HasOne(d => d.IdStrainNavigation).WithMany(p => p.StrainApprovalHistories)
                .HasForeignKey(d => d.IdStrain)
                .HasConstraintName("FK_StrainApprovalHistory_Strain");
        });

        modelBuilder.Entity<Ward>(entity =>
        {
            entity.HasKey(e => e.IdWards).HasName("PK__Wards__6E35F738437F4A37");

            entity.Property(e => e.Name).HasMaxLength(255);

            entity.HasOne(d => d.IdDistrictsNavigation).WithMany(p => p.Wards)
                .HasForeignKey(d => d.IdDistricts)
                .HasConstraintName("FK__Wards__IdDistric__17036CC0");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
