namespace FODfinder.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SavedFood
    {
        public SavedFood() { }
        public SavedFood(int usdaId, string userId)
        {
            this.usdaFoodID = usdaId;
            this.userID = userId;
        }
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int usdaFoodID { get; set; }

        [Key]
        [Column(Order = 1)]
        public string userID { get; set; }
    }
}
