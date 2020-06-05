namespace ViachappaFactAuth.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            AspNetUsers = new HashSet<AspNetUser>();
            EmployeeStaffingUnits = new HashSet<EmployeeStaffingUnit>();
            FixedAssets = new HashSet<FixedAsset>();
            StaffingUnits = new HashSet<StaffingUnit>();
            SubcontoArrays = new HashSet<SubcontoArray>();
            Warehouses = new HashSet<Warehouse>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string SecondName { get; set; }

        [Required]
        public string LastName { get; set; }

        public DateTime Birthday { get; set; }

        public string BirthPlace { get; set; }

        public string Nationality { get; set; }

        public int Education { get; set; }

        public Guid? Passport { get; set; }

        public string PassportResidencePlace { get; set; }

        public string ActualResidencePlace { get; set; }

        public string PhoneNumber { get; set; }

        public Guid? MilitaryAccounting { get; set; }

        public bool isDeleted { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AspNetUser> AspNetUsers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeStaffingUnit> EmployeeStaffingUnits { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FixedAsset> FixedAssets { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StaffingUnit> StaffingUnits { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubcontoArray> SubcontoArrays { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Warehouse> Warehouses { get; set; }
    }
}
