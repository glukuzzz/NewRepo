namespace ViachappaFactAuth.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderInvoiceType
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
