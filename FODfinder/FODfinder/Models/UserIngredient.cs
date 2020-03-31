namespace FODfinder.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserIngredient
    {
        [Key]
        [Column(Order = 0)]
        public string userID { get; set; }

        [Required]
        [StringLength(10)]
        public string Label { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LabelledIngredientID { get; set; }

        public int? FODMAPIngredientID { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }

        public virtual FODMAPIngredient FODMAPIngredient { get; set; }

        public virtual LabelledIngredient LabelledIngredient { get; set; }
    }
}
