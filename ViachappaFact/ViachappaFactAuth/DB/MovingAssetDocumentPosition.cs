namespace ViachappaFactAuth.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MovingAssetDocumentPosition
    {
        public Guid Id { get; set; }

        [Column(TypeName = "money")]
        public decimal Cost { get; set; }

        public string Content { get; set; }

        public Guid Document_Id { get; set; }

        public Guid SubcontoArray_Id { get; set; }

        public Guid SubcontoArray_Id_To { get; set; }

        public virtual MovingAssetDocument MovingAssetDocument { get; set; }

        public virtual SubcontoArray SubcontoArray { get; set; }

        public virtual SubcontoArray SubcontoArray1 { get; set; }
    }
}
