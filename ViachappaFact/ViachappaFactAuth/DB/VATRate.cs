namespace ViachappaFactAuth.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class VATRate
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VATRate()
        {
            OrderPositions = new HashSet<OrderPosition>();
            VATRates1 = new HashSet<VATRate>();
            WarehouseItems = new HashSet<WarehouseItem>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public double Price { get; set; }

        public bool isActive { get; set; }

        public int? Parent { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderPosition> OrderPositions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VATRate> VATRates1 { get; set; }

        public virtual VATRate VATRate1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WarehouseItem> WarehouseItems { get; set; }
    }
}
