namespace ViachappaFactAuth.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderPosition
    {
        public Guid Id { get; set; }

        public Guid? Order_Id { get; set; }

        public double Count { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public Guid Material_Id { get; set; }

        public int? VATRate_Id { get; set; }

        public int? PlanAccountType_Id { get; set; }

        public int? PlanAccountType_NDS_Id { get; set; }

        public Guid? OrderAct_Id { get; set; }

        public virtual Material Material { get; set; }

        public virtual OrderAct OrderAct { get; set; }

        public virtual Order Order { get; set; }

        public virtual PlanAccountType PlanAccountType { get; set; }

        public virtual PlanAccountType PlanAccountType1 { get; set; }

        public virtual VATRate VATRate { get; set; }
    }
}
