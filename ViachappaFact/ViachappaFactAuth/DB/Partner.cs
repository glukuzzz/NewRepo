namespace ViachappaFactAuth.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Partner
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Partner()
        {
            Contracts = new HashSet<Contract>();
            Orders = new HashSet<Order>();
            PartnerBankAccounts = new HashSet<PartnerBankAccount>();
            SubcontoArrays = new HashSet<SubcontoArray>();
            Waybills = new HashSet<Waybill>();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public long INN { get; set; }

        public long KPP { get; set; }

        public long OGRN { get; set; }

        public long? OKPO { get; set; }

        public long? OKATO { get; set; }

        [Required]
        public string LegalAddress { get; set; }

        [Required]
        public string ActualAddress { get; set; }

        [Required]
        public string Director { get; set; }

        [Required]
        public string AccountantGeneral { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Fax { get; set; }

        [Required]
        public string Email { get; set; }

        public string ShortName { get; set; }

        public bool isDeleted { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Contract> Contracts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PartnerBankAccount> PartnerBankAccounts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubcontoArray> SubcontoArrays { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Waybill> Waybills { get; set; }
    }
}
