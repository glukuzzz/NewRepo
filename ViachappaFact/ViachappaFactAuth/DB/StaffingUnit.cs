namespace ViachappaFactAuth.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class StaffingUnit
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StaffingUnit()
        {
            EmployeeStaffingUnits = new HashSet<EmployeeStaffingUnit>();
        }

        public Guid Id { get; set; }

        public int? PositionCode { get; set; }

        [Required]
        public string UnitName { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateStart { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateEnd { get; set; }

        [Column(TypeName = "money")]
        public decimal Salary { get; set; }

        public int? Employee_Id { get; set; }

        public int Department_Id { get; set; }

        public virtual Department Department { get; set; }

        public virtual Employee Employee { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeStaffingUnit> EmployeeStaffingUnits { get; set; }
    }
}
