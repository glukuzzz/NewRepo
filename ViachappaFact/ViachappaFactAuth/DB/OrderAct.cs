namespace ViachappaFactAuth.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderAct
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrderAct()
        {
            OrderPositions = new HashSet<OrderPosition>();
        }

        public Guid Id { get; set; }

        [Required]
        public string DocNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime DocDate { get; set; }

        public bool isPost { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderPosition> OrderPositions { get; set; }
    }
}
