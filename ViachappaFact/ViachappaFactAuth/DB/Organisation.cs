namespace ViachappaFactAuth.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Organisation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Organisation()
        {
            MovingAssetDocuments = new HashSet<MovingAssetDocument>();
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string ShortName { get; set; }

        public long INN { get; set; }

        public long KPP { get; set; }

        public long OGRN { get; set; }

        public long? OKPO { get; set; }

        public long? OKATO { get; set; }

        public int? IFNS { get; set; }

        public string IFNS_Name { get; set; }

        [Column(TypeName = "date")]
        public DateTime? IFNS_RegDate { get; set; }

        public string IFNS_RegName { get; set; }

        public bool IsDeleted { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MovingAssetDocument> MovingAssetDocuments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
