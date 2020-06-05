namespace ViachappaFactAuth.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MovingAssetDocument
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MovingAssetDocument()
        {
            MovingAssetDocumentPositions = new HashSet<MovingAssetDocumentPosition>();
        }

        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Column(TypeName = "date")]
        public DateTime DocDate { get; set; }

        public DateTime OperDate { get; set; }

        public int DocNumber { get; set; }

        public int Organisation_Id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MovingAssetDocumentPosition> MovingAssetDocumentPositions { get; set; }

        public virtual Organisation Organisation { get; set; }
    }
}
