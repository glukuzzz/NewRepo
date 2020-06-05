namespace ViachappaFactAuth.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FixedAsset
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [StringLength(15)]
        public string Subaccount { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateInsert { get; set; }

        public short UsefulLifeMonthes { get; set; }

        public int Department_Id { get; set; }

        public int? Employee_Id { get; set; }

        public bool isDeleted { get; set; }

        public virtual Department Department { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
