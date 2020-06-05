namespace ViachappaFactAuth.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ContractAdd
    {
        public Guid Id { get; set; }

        public Guid Contract_Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [Required]
        public string Abstract { get; set; }

        public string FileName { get; set; }

        public byte[] FileData { get; set; }

        public string DocNumber { get; set; }

        public bool isDeleted { get; set; }

        public virtual Contract Contract { get; set; }
    }
}
