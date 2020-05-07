namespace FODfinder.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Quote
    {
        public int ID { get; set; }

        [Column("Body")]
        [Required]
        [StringLength(250)]
        public string Body { get; set; }

        [Required]
        [StringLength(50)]
        public string Author { get; set; }
    }
}
