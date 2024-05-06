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

    public virtual DbSet<AuthorNewspaper> AuthorNewspapers { get; set; }

    public virtual DbSet<Bill> Bills { get; set; }

    public virtual DbSet<BillDetail> BillDetails { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartDetail> CartDetails { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<ConditionalStrain> ConditionalStrains { get; set; }

    public virtual DbSet<ContentWork> ContentWorks { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Genu> Genus { get; set; }

    public virtual DbSet<IdentifyStrain> IdentifyStrains { get; set; }

    public virtual DbSet<IsolatorStrain> IsolatorStrains { get; set; }

    public virtual DbSet<Partner> Partners { get; set; }

    public virtual DbSet<Phylum> Phylums { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectContent> ProjectContents { get; set; }

    public virtual DbSet<RoleForEmployee> RoleForEmployees { get; set; }

    public virtual DbSet<ScienceNewspaper> ScienceNewspapers { get; set; }

    public virtual DbSet<Species> Species { get; set; }

    public virtual DbSet<Strain> Strains { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=IRT;Persist Security Info=True;User ID=sa;Password=123;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccountForCustomer>(entity =>
        {
            entity.HasKey(e => e.IdCustomer).HasName("PK__AccountF__2D8FDE5F1531858C");

            entity.ToTable("AccountForCustomer");

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
            entity.HasKey(e => e.IdEmployee).HasName("PK__AccountF__D9EE4F36DA5D6483");

            entity.ToTable("AccountForEmployee");

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

        modelBuilder.Entity<AuthorNewspaper>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("AuthorNewspaper");

            entity.Property(e => e.IdEmployee)
                .HasMaxLength(50)
                .HasColumnName("ID_Employee");
            entity.Property(e => e.IdNewspaper).HasColumnName("ID_Newspaper");
            entity.Property(e => e.PostDate).HasColumnName("Post_Date");
            entity.Property(e => e.RoleOfAuthor)
                .HasMaxLength(255)
                .HasColumnName("Role_Of_Author");

            entity.HasOne(d => d.IdEmployeeNavigation).WithMany()
                .HasForeignKey(d => d.IdEmployee)
                .HasConstraintName("FK_AuthorNewspaper_Employee");

            entity.HasOne(d => d.IdNewspaperNavigation).WithMany()
                .HasForeignKey(d => d.IdNewspaper)
                .HasConstraintName("FK_AuthorNewspaper_ScienceNewspaper");
        });

        modelBuilder.Entity<Bill>(entity =>
        {
            entity.HasKey(e => e.IdBill).HasName("PK__Bill__F098680ADC8D488B");

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
        });

        modelBuilder.Entity<BillDetail>(entity =>
        {
            entity.HasKey(e => e.IdBillDetail).HasName("PK__BillDeta__3421CE5D6FB982BA");

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
            entity.HasKey(e => e.IdCart).HasName("PK__Cart__72140ECF07CF2BDD");

            entity.ToTable("Cart");

            entity.Property(e => e.IdCart).HasColumnName("ID_Cart");
            entity.Property(e => e.IdCustomer)
                .HasMaxLength(50)
                .HasColumnName("ID_Customer");
            entity.Property(e => e.ToatalProduct).HasColumnName("Toatal_Product");

            entity.HasOne(d => d.IdCustomerNavigation).WithMany(p => p.Carts)
                .HasForeignKey(d => d.IdCustomer)
                .HasConstraintName("FK_Cart_Customer");
        });

        modelBuilder.Entity<CartDetail>(entity =>
        {
            entity.HasKey(e => e.IdCartDetail).HasName("PK__CartDeta__19B4E08299F90818");

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
            entity.HasKey(e => e.IdClass).HasName("PK__Class__D7CF744C1AE93673");

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
            entity.HasKey(e => e.IdCondition).HasName("PK__Conditio__BA54C9AE66FB72ED");

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
            entity.HasKey(e => e.IdContentWork).HasName("PK__ContentW__951336A759BE98DD");

            entity.ToTable("ContentWork");

            entity.Property(e => e.IdContentWork).HasColumnName("ID_ContentWork");
            entity.Property(e => e.IdEmployee)
                .HasMaxLength(50)
                .HasColumnName("ID_Employee");
            entity.Property(e => e.IdProjectContent).HasColumnName("ID_ProjectContent");
            entity.Property(e => e.NameContent).HasColumnName("Name_Content");
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
            entity.HasKey(e => e.IdCustomer).HasName("PK__Customer__2D8FDE5F9C51D375");

            entity.ToTable("Customer");

            entity.Property(e => e.IdCustomer)
                .HasMaxLength(50)
                .HasColumnName("ID_Customer");
            entity.Property(e => e.DateOfBirth).HasColumnName("Date_of_Birth");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("Phone_Number");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.IdEmployee).HasName("PK__Employee__D9EE4F36C7A56CF7");

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
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("Phone_Number");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.IdRole)
                .HasConstraintName("FK_Employee_RoleForEmployee");
        });

        modelBuilder.Entity<Genu>(entity =>
        {
            entity.HasKey(e => e.IdGenus).HasName("PK__Genus__7B3106851A3E0856");

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
            entity.HasKey(e => new { e.IdEmployee, e.IdStrain }).HasName("PK__Identify__23CDA4B42B1FD926");

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

        modelBuilder.Entity<IsolatorStrain>(entity =>
        {
            entity.HasKey(e => new { e.IdEmployee, e.IdStrain }).HasName("PK__Isolator__23CDA4B4A6AFECC0");

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

        modelBuilder.Entity<Partner>(entity =>
        {
            entity.HasKey(e => e.IdPartner).HasName("PK__Partner__B982253D6B0D2CC8");

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
            entity.Property(e => e.NamePartner)
                .HasMaxLength(255)
                .HasColumnName("Name_Partner");
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
            entity.HasKey(e => e.IdPhylum).HasName("PK__Phylum__DE75F90114A077FC");

            entity.ToTable("Phylum");

            entity.Property(e => e.IdPhylum).HasColumnName("ID_Phylum");
            entity.Property(e => e.NamePhylum)
                .HasMaxLength(255)
                .HasColumnName("Name_Phylum");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.IdProject).HasName("PK__Project__D310AEBF4538E957");

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
            entity.HasKey(e => e.IdProjectContent).HasName("PK__ProjectC__61A0E4E7F92040E3");

            entity.ToTable("ProjectContent");

            entity.Property(e => e.IdProjectContent).HasColumnName("ID_ProjectContent");
            entity.Property(e => e.IdProject)
                .HasMaxLength(50)
                .HasColumnName("ID_Project");
            entity.Property(e => e.NameContent).HasColumnName("Name_Content");
            entity.Property(e => e.Status).HasMaxLength(255);

            entity.HasOne(d => d.IdProjectNavigation).WithMany(p => p.ProjectContents)
                .HasForeignKey(d => d.IdProject)
                .HasConstraintName("FK_ProjectContent_Project");
        });

        modelBuilder.Entity<RoleForEmployee>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("PK__RoleForE__43DCD32DECFEB708");

            entity.ToTable("RoleForEmployee");

            entity.Property(e => e.IdRole).HasColumnName("ID_Role");
            entity.Property(e => e.RoleDescription).HasMaxLength(255);
            entity.Property(e => e.RoleName).HasMaxLength(100);
        });

        modelBuilder.Entity<ScienceNewspaper>(entity =>
        {
            entity.HasKey(e => e.IdNewspaper).HasName("PK__ScienceN__DD461981A129FFD7");

            entity.ToTable("ScienceNewspaper");

            entity.Property(e => e.IdNewspaper).HasColumnName("ID_Newspaper");
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.Url)
                .HasMaxLength(255)
                .HasColumnName("URL");
        });

        modelBuilder.Entity<Species>(entity =>
        {
            entity.HasKey(e => e.IdSpecies).HasName("PK__Species__33D1C117C4279610");

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
            entity.HasKey(e => e.IdStrain).HasName("PK__Strain__A23EB82EA53DD845");

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
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
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
            entity.Property(e => e.Status).HasMaxLength(255);
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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
