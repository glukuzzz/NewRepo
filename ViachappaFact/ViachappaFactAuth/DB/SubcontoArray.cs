namespace ViachappaFactAuth.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SubcontoArray
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SubcontoArray()
        {
            MovingAssetDocumentPositions = new HashSet<MovingAssetDocumentPosition>();
            MovingAssetDocumentPositions1 = new HashSet<MovingAssetDocumentPosition>();
        }

        public Guid Id { get; set; }

        public int PlanAccountType_Id { get; set; }

        public int? Employee_Id { get; set; }

        public int? Department_Id { get; set; }

        public Guid? Partner_Id { get; set; }

        public Guid? PartnerBankAccount_Id { get; set; }

        public int? Warehouse_Id { get; set; }

        public Guid? Contract_Id { get; set; }

        public int? MaterialGroup_Id { get; set; }

        public Guid? Material_Id { get; set; }

        public virtual Department Department { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual MaterialGroup MaterialGroup { get; set; }

        public virtual Material Material { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MovingAssetDocumentPosition> MovingAssetDocumentPositions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MovingAssetDocumentPosition> MovingAssetDocumentPositions1 { get; set; }

        public virtual PartnerBankAccount PartnerBankAccount { get; set; }

        public virtual PartnerBankAccount PartnerBankAccount1 { get; set; }

        public virtual Partner Partner { get; set; }

        public virtual PlanAccountType PlanAccountType { get; set; }

        public virtual Warehouse Warehouse { get; set; }
    }
}
