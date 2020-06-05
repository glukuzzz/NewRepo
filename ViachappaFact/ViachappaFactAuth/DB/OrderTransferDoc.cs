namespace ViachappaFactAuth.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderTransferDoc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrderTransferDoc()
        {
            Orders = new HashSet<Order>();
            WarehouseItems = new HashSet<WarehouseItem>();
        }

        public Guid Id { get; set; }

        [Required]
        public string DocNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime DocDate { get; set; }

        public string FileName { get; set; }

        public byte[] FileData { get; set; }

        [Required]
        [StringLength(128)]
        public string User_Id { get; set; }

        public DateTime OperDate { get; set; }

        public Guid? OrderInvoice_Id { get; set; }


        public bool isPost { get; set; }

        public int Warehouse_Id { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }

        public virtual OrderInvoice OrderInvoice { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }

        public virtual Warehouse Warehouse { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WarehouseItem> WarehouseItems { get; set; }
    }
}
