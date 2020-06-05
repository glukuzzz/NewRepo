namespace ViachappaFactAuth.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IntangibleAsset
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateInsert { get; set; }

        public short UsefulLifeMonthes { get; set; }

        public int Department_Id { get; set; }

        public virtual Department Department { get; set; }
    }
}
