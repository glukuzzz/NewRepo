namespace ViachappaFactAuth.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CostItem
    {
        public Guid Id { get; set; }

        public int CostGroup_Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int CostType_Id { get; set; }

        public int CostNature_Id { get; set; }

        public int ExpensesType_Id { get; set; }

        public bool isDeleted { get; set; }

        public virtual CostGroup CostGroup { get; set; }

        public virtual CostNature CostNature { get; set; }

        public virtual CostType CostType { get; set; }

        public virtual ExpensesType ExpensesType { get; set; }
    }
}
