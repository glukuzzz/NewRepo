namespace ViachappaFactAuth.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Contract
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Contract()
        {
            ContractAdds = new HashSet<ContractAdd>();
            ContractSpecs = new HashSet<ContractSpec>();
            Orders = new HashSet<Order>();
        }

        public Guid Id { get; set; }

        [Required]
        public string ContractNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime ContractDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateStart { get; set; }

        public string Abstract { get; set; }

        public Guid? Partner_Id { get; set; }

        public string FileName { get; set; }

        public byte[] FileData { get; set; }

        public bool isDeleted { get; set; }

        public bool UseUpd { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ContractAdd> ContractAdds { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ContractSpec> ContractSpecs { get; set; }

        public virtual Partner Partner { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
