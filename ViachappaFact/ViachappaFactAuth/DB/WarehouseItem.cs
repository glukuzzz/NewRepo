namespace ViachappaFactAuth.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class WarehouseItem
    {
        public Guid Id { get; set; }

        public Guid Material_Id { get; set; }

        public double Count { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public int VATRate_Id { get; set; }

        public int? Warehouse_Id { get; set; }

        public DateTime OrderInvoiceDate { get; set; }

        public DateTime? WH_BindingDate { get; set; }

        [StringLength(128)]
        public string WH_BindingUser { get; set; }

        public Guid? OrderTransferDoc_Id { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }

        public virtual Material Material { get; set; }

        public virtual OrderTransferDoc OrderTransferDoc { get; set; }

        public virtual VATRate VATRate { get; set; }

        public virtual Warehouse Warehouse { get; set; }
    }
}
