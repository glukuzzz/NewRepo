namespace ViachappaFactAuth.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderInvoice
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrderInvoice()
        {
            Orders = new HashSet<Order>();
            Orders1 = new HashSet<Order>();
            OrderTransferDocs = new HashSet<OrderTransferDoc>();
        }

        public Guid Id { get; set; }

        [Required]
        public string InvNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime InvDate { get; set; }

        public string FileName { get; set; }

        public byte[] FileData { get; set; }

        [Required]
        [StringLength(128)]
        public string User_Id { get; set; }

        public DateTime OperDate { get; set; }

        public bool isPost { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderTransferDoc> OrderTransferDocs { get; set; }
    }
}
