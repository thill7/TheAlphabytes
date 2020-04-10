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
        public SavedFood(int usdaId, string userId, string brandOwner, string barcode, string description)
        {
            this.usdaFoodID = usdaId;
            this.userID = userId;
            this.brand = brandOwner;
            this.upc = barcode;
            this.desc = description;
        }
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int usdaFoodID { get; set; }

        [Key]
        [Column(Order = 1)]
        public string userID { get; set; }

        public string brand { get; set; }

        public string upc { get; set; }

        public string desc { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }
    }
}
