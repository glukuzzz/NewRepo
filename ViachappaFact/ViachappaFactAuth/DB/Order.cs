namespace ViachappaFactAuth.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrderPositions = new HashSet<OrderPosition>();
        }

        public Guid Id { get; set; }
        [Required]
        public string DocNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime DocDate { get; set; }

        public DateTime OperDate { get; set; }

        public Guid? Partner_Id { get; set; }

        public int Organisation_Id { get; set; }

        public Guid Contract_Id { get; set; }

        public int OrderType_Id { get; set; }

        public bool isPaid { get; set; }

        public Guid? OrderInvoice_Id { get; set; }

        public Guid? OrderInvoice_Correction_Id { get; set; }

        public Guid? OrderTransferDoc_Id { get; set; }

        public bool isDeleted { get; set; }

        public virtual Contract Contract { get; set; }

        public virtual OrderInvoice OrderInvoice { get; set; }

        public virtual OrderInvoice OrderInvoice1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderPosition> OrderPositions { get; set; }

        public virtual OrderTransferDoc OrderTransferDoc { get; set; }

        public virtual OrderType OrderType { get; set; }

        public virtual Organisation Organisation { get; set; }

        public virtual Partner Partner { get; set; }
    }
}
