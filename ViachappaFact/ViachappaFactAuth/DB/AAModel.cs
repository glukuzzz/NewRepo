namespace ViachappaFactAuth.DB
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AAModel : DbContext
    {
        public AAModel()
            : base("name=AAModel10")
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AssetsLiabilitiesType> AssetsLiabilitiesTypes { get; set; }
        public virtual DbSet<BudgetPaymentType> BudgetPaymentTypes { get; set; }
        public virtual DbSet<ContractAdd> ContractAdds { get; set; }
        public virtual DbSet<Contract> Contracts { get; set; }
        public virtual DbSet<ContractSpec> ContractSpecs { get; set; }
        public virtual DbSet<CostGroup> CostGroups { get; set; }
        public virtual DbSet<CostItem> CostItems { get; set; }
        public virtual DbSet<CostNature> CostNatures { get; set; }
        public virtual DbSet<CostType> CostTypes { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<DepartmentSub> DepartmentSubs { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeStaffingUnit> EmployeeStaffingUnits { get; set; }
        public virtual DbSet<ExpensesType> ExpensesTypes { get; set; }
        public virtual DbSet<FixedAsset> FixedAssets { get; set; }
        public virtual DbSet<IntangibleAsset> IntangibleAssets { get; set; }
        public virtual DbSet<MaterialGroup> MaterialGroups { get; set; }
        public virtual DbSet<Material> Materials { get; set; }
        public virtual DbSet<MaterialType> MaterialTypes { get; set; }
        public virtual DbSet<MovingAssetDocumentPosition> MovingAssetDocumentPositions { get; set; }
        public virtual DbSet<MovingAssetDocument> MovingAssetDocuments { get; set; }
        public virtual DbSet<OrderAct> OrderActs { get; set; }
        public virtual DbSet<OrderInvoice> OrderInvoices { get; set; }
        public virtual DbSet<OrderInvoiceType> OrderInvoiceTypes { get; set; }
        public virtual DbSet<OrderPosition> OrderPositions { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderTransferDoc> OrderTransferDocs { get; set; }
        public virtual DbSet<OrderType> OrderTypes { get; set; }
        public virtual DbSet<Organisation> Organisations { get; set; }
        public virtual DbSet<PartnerBankAccount> PartnerBankAccounts { get; set; }
        public virtual DbSet<Partner> Partners { get; set; }
        public virtual DbSet<PlanAccountType> PlanAccountTypes { get; set; }
        public virtual DbSet<ProductProcessingOperation> ProductProcessingOperations { get; set; }
        public virtual DbSet<ResearchDevelopmentType> ResearchDevelopmentTypes { get; set; }
        public virtual DbSet<StaffingUnit> StaffingUnits { get; set; }
        public virtual DbSet<SubcontoArray> SubcontoArrays { get; set; }
        public virtual DbSet<SubcontoType> SubcontoTypes { get; set; }
        public virtual DbSet<VATRate> VATRates { get; set; }
        public virtual DbSet<WarehouseItem> WarehouseItems { get; set; }
        public virtual DbSet<Warehouse> Warehouses { get; set; }
        public virtual DbSet<Waybill> Waybills { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.OrderInvoices)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.User_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.OrderTransferDocs)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.User_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.WarehouseItems)
                .WithOptional(e => e.AspNetUser)
                .HasForeignKey(e => e.WH_BindingUser);

            modelBuilder.Entity<Contract>()
                .HasMany(e => e.ContractAdds)
                .WithRequired(e => e.Contract)
                .HasForeignKey(e => e.Contract_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Contract>()
                .HasMany(e => e.ContractSpecs)
                .WithRequired(e => e.Contract)
                .HasForeignKey(e => e.Contract_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Contract>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Contract)
                .HasForeignKey(e => e.Contract_Id);

            modelBuilder.Entity<ContractSpec>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<CostGroup>()
                .HasMany(e => e.CostItems)
                .WithRequired(e => e.CostGroup)
                .HasForeignKey(e => e.CostGroup_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CostNature>()
                .HasMany(e => e.CostItems)
                .WithRequired(e => e.CostNature)
                .HasForeignKey(e => e.CostNature_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CostType>()
                .HasMany(e => e.CostItems)
                .WithRequired(e => e.CostType)
                .HasForeignKey(e => e.CostType_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Currency>()
                .Property(e => e.ShortName)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Currency>()
                .HasMany(e => e.PartnerBankAccounts)
                .WithOptional(e => e.Currency)
                .HasForeignKey(e => e.Currency_Id);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.DepartmentSubs)
                .WithRequired(e => e.Department1)
                .HasForeignKey(e => e.Department)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.DepartmentSubs1)
                .WithRequired(e => e.Department2)
                .HasForeignKey(e => e.DepartmentParent)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.FixedAssets)
                .WithRequired(e => e.Department)
                .HasForeignKey(e => e.Department_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.IntangibleAssets)
                .WithRequired(e => e.Department)
                .HasForeignKey(e => e.Department_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.ProductProcessingOperations)
                .WithRequired(e => e.Department)
                .HasForeignKey(e => e.Department_Id);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.StaffingUnits)
                .WithRequired(e => e.Department)
                .HasForeignKey(e => e.Department_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.SubcontoArrays)
                .WithOptional(e => e.Department)
                .HasForeignKey(e => e.Department_Id);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.Warehouses)
                .WithRequired(e => e.Department)
                .HasForeignKey(e => e.Department_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.AspNetUsers)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.Employee_Id);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EmployeeStaffingUnits)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.Employee_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.FixedAssets)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.Employee_Id);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.StaffingUnits)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.Employee_Id);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.SubcontoArrays)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.Employee_Id);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Warehouses)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.Responsible);

            modelBuilder.Entity<ExpensesType>()
                .HasMany(e => e.CostItems)
                .WithRequired(e => e.ExpensesType)
                .HasForeignKey(e => e.ExpensesType_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FixedAsset>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<IntangibleAsset>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MaterialGroup>()
                .HasMany(e => e.Materials)
                .WithOptional(e => e.MaterialGroup)
                .HasForeignKey(e => e.MaterialGroup_Id);

            modelBuilder.Entity<MaterialGroup>()
                .HasMany(e => e.SubcontoArrays)
                .WithOptional(e => e.MaterialGroup)
                .HasForeignKey(e => e.MaterialGroup_Id);

            modelBuilder.Entity<Material>()
                .HasMany(e => e.Materials1)
                .WithOptional(e => e.Material1)
                .HasForeignKey(e => e.Analog);

            modelBuilder.Entity<Material>()
                .HasMany(e => e.OrderPositions)
                .WithRequired(e => e.Material)
                .HasForeignKey(e => e.Material_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Material>()
                .HasMany(e => e.SubcontoArrays)
                .WithOptional(e => e.Material)
                .HasForeignKey(e => e.Material_Id);

            modelBuilder.Entity<Material>()
                .HasMany(e => e.WarehouseItems)
                .WithRequired(e => e.Material)
                .HasForeignKey(e => e.Material_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MaterialType>()
                .HasMany(e => e.Materials)
                .WithRequired(e => e.MaterialType)
                .HasForeignKey(e => e.MaterialType_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MovingAssetDocumentPosition>()
                .Property(e => e.Cost)
                .HasPrecision(19, 4);

            modelBuilder.Entity<MovingAssetDocument>()
                .HasMany(e => e.MovingAssetDocumentPositions)
                .WithRequired(e => e.MovingAssetDocument)
                .HasForeignKey(e => e.Document_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderAct>()
                .HasMany(e => e.OrderPositions)
                .WithOptional(e => e.OrderAct)
                .HasForeignKey(e => e.OrderAct_Id);

            modelBuilder.Entity<OrderInvoice>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.OrderInvoice)
                .HasForeignKey(e => e.OrderInvoice_Id);

            modelBuilder.Entity<OrderInvoice>()
                .HasMany(e => e.Orders1)
                .WithOptional(e => e.OrderInvoice1)
                .HasForeignKey(e => e.OrderInvoice_Correction_Id);

            modelBuilder.Entity<OrderInvoice>()
                .HasMany(e => e.OrderTransferDocs)
                .WithOptional(e => e.OrderInvoice)
                .HasForeignKey(e => e.OrderInvoice_Id);

            modelBuilder.Entity<OrderPosition>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderPositions)
                .WithOptional(e => e.Order)
                .HasForeignKey(e => e.Order_Id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<OrderTransferDoc>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.OrderTransferDoc)
                .HasForeignKey(e => e.OrderTransferDoc_Id);

            modelBuilder.Entity<OrderTransferDoc>()
                .HasMany(e => e.WarehouseItems)
                .WithOptional(e => e.OrderTransferDoc)
                .HasForeignKey(e => e.OrderTransferDoc_Id);

            modelBuilder.Entity<OrderType>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.OrderType)
                .HasForeignKey(e => e.OrderType_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Organisation>()
                .HasMany(e => e.MovingAssetDocuments)
                .WithRequired(e => e.Organisation)
                .HasForeignKey(e => e.Organisation_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Organisation>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Organisation)
                .HasForeignKey(e => e.Organisation_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PartnerBankAccount>()
                .Property(e => e.CorrespondentAccount)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<PartnerBankAccount>()
                .Property(e => e.PaymentAccount)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<PartnerBankAccount>()
                .HasMany(e => e.SubcontoArrays)
                .WithOptional(e => e.PartnerBankAccount)
                .HasForeignKey(e => e.Contract_Id);

            modelBuilder.Entity<PartnerBankAccount>()
                .HasMany(e => e.SubcontoArrays1)
                .WithOptional(e => e.PartnerBankAccount1)
                .HasForeignKey(e => e.PartnerBankAccount_Id);

            modelBuilder.Entity<Partner>()
                .HasMany(e => e.Contracts)
                .WithOptional(e => e.Partner)
                .HasForeignKey(e => e.Partner_Id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Partner>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.Partner)
                .HasForeignKey(e => e.Partner_Id);

            modelBuilder.Entity<Partner>()
                .HasMany(e => e.PartnerBankAccounts)
                .WithOptional(e => e.Partner)
                .HasForeignKey(e => e.Partner_Id);

            modelBuilder.Entity<Partner>()
                .HasMany(e => e.SubcontoArrays)
                .WithOptional(e => e.Partner)
                .HasForeignKey(e => e.Partner_Id);

            modelBuilder.Entity<Partner>()
                .HasMany(e => e.Waybills)
                .WithRequired(e => e.Partner)
                .HasForeignKey(e => e.Partner_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PlanAccountType>()
                .HasMany(e => e.OrderPositions)
                .WithOptional(e => e.PlanAccountType)
                .HasForeignKey(e => e.PlanAccountType_Id);

            modelBuilder.Entity<PlanAccountType>()
                .HasMany(e => e.OrderPositions1)
                .WithOptional(e => e.PlanAccountType1)
                .HasForeignKey(e => e.PlanAccountType_NDS_Id);

            modelBuilder.Entity<PlanAccountType>()
                .HasMany(e => e.SubcontoArrays)
                .WithRequired(e => e.PlanAccountType)
                .HasForeignKey(e => e.PlanAccountType_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PlanAccountType>()
                .HasMany(e => e.SubcontoTypes)
                .WithMany(e => e.PlanAccountTypes)
                .Map(m => m.ToTable("PlanAccountsSubcontosTypes"));

            modelBuilder.Entity<StaffingUnit>()
                .Property(e => e.Salary)
                .HasPrecision(19, 4);

            modelBuilder.Entity<StaffingUnit>()
                .HasMany(e => e.EmployeeStaffingUnits)
                .WithRequired(e => e.StaffingUnit1)
                .HasForeignKey(e => e.StaffingUnit)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SubcontoArray>()
                .HasMany(e => e.MovingAssetDocumentPositions)
                .WithRequired(e => e.SubcontoArray)
                .HasForeignKey(e => e.SubcontoArray_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SubcontoArray>()
                .HasMany(e => e.MovingAssetDocumentPositions1)
                .WithRequired(e => e.SubcontoArray1)
                .HasForeignKey(e => e.SubcontoArray_Id_To)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VATRate>()
                .HasMany(e => e.OrderPositions)
                .WithOptional(e => e.VATRate)
                .HasForeignKey(e => e.VATRate_Id);

            modelBuilder.Entity<VATRate>()
                .HasMany(e => e.VATRates1)
                .WithOptional(e => e.VATRate1)
                .HasForeignKey(e => e.Parent);

            modelBuilder.Entity<VATRate>()
                .HasMany(e => e.WarehouseItems)
                .WithRequired(e => e.VATRate)
                .HasForeignKey(e => e.VATRate_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WarehouseItem>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Warehouse>()
                .HasMany(e => e.OrderTransferDocs)
                .WithRequired(e => e.Warehouse)
                .HasForeignKey(e => e.Warehouse_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Warehouse>()
                .HasMany(e => e.SubcontoArrays)
                .WithOptional(e => e.Warehouse)
                .HasForeignKey(e => e.Warehouse_Id);

            modelBuilder.Entity<Warehouse>()
                .HasMany(e => e.WarehouseItems)
                .WithOptional(e => e.Warehouse)
                .HasForeignKey(e => e.Warehouse_Id);
        }
    }
}
