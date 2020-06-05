namespace ViachappaFactAuth.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EmployeeStaffingUnit
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Employee_Id { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid StaffingUnit { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateStart { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateEnd { get; set; }

        public bool isDeleted { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual StaffingUnit StaffingUnit1 { get; set; }
    }
}
