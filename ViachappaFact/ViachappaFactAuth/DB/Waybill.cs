namespace ViachappaFactAuth.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Waybill
    {
        public Guid Id { get; set; }

        [Required]
        public string DocNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime DocDate { get; set; }

        public Guid Partner_Id { get; set; }

        public virtual Partner Partner { get; set; }
    }
}
