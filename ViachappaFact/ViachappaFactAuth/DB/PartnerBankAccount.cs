namespace ViachappaFactAuth.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PartnerBankAccount
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PartnerBankAccount()
        {
            SubcontoArrays = new HashSet<SubcontoArray>();
            SubcontoArrays1 = new HashSet<SubcontoArray>();
        }

        public Guid Id { get; set; }

        public Guid? Partner_Id { get; set; }

        [Required]
        public string BankName { get; set; }

        [Required]
        [StringLength(9)]
        public string BIK { get; set; }

        [Required]
        [StringLength(20)]
        public string CorrespondentAccount { get; set; }

        [Required]
        [StringLength(20)]
        public string PaymentAccount { get; set; }

        public int? Currency_Id { get; set; }

        public virtual Currency Currency { get; set; }

        public virtual Partner Partner { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubcontoArray> SubcontoArrays { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubcontoArray> SubcontoArrays1 { get; set; }
    }
}
