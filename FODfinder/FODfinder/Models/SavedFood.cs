namespace FODfinder.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SavedFood
    {
        public SavedFood() { }
        public SavedFood(int usdaId, int listId, string brandOwner, string barcode, string description)
        {
            this.usdaFoodID = usdaId;
            this.listID = listId;
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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int listID { get; set; }

        [Required]
        [StringLength(200)]
        public string brand { get; set; }

        [Required]
        [StringLength(32)]
        public string upc { get; set; }

        [Required]
        [StringLength(200)]
        public string desc { get; set; }

        public virtual UserList UserList { get; set; }
    }
}
