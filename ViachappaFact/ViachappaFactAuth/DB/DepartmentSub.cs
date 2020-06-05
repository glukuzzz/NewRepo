namespace ViachappaFactAuth.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DepartmentSub
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Department { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DepartmentParent { get; set; }

        public bool isDeleted { get; set; }

        public virtual Department Department1 { get; set; }

        public virtual Department Department2 { get; set; }
    }
}
