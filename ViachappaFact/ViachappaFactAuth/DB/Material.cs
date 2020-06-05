namespace ViachappaFactAuth.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Material
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Material()
        {
            Materials1 = new HashSet<Material>();
            OrderPositions = new HashSet<OrderPosition>();
            SubcontoArrays = new HashSet<SubcontoArray>();
            WarehouseItems = new HashSet<WarehouseItem>();
        }

        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Articul { get; set; }

        public double NDS { get; set; }

        [Required]
        public string UnitName { get; set; }

        public string Comment { get; set; }

        public Guid? Analog { get; set; }

        public int? MaterialGroup_Id { get; set; }

        public bool isDeleted { get; set; }

        public bool isUsluga { get; set; }

        public int MaterialType_Id { get; set; }

        public virtual MaterialGroup MaterialGroup { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Material> Materials1 { get; set; }

        public virtual Material Material1 { get; set; }

        public virtual MaterialType MaterialType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderPosition> OrderPositions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubcontoArray> SubcontoArrays { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WarehouseItem> WarehouseItems { get; set; }
    }
}
