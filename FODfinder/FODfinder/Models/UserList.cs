namespace FODfinder.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserList
    {
        [Key]
        public int listID { get; set; }

        [Required]
        [StringLength(128)]
        public string userID { get; set; }

        [Required]
        [StringLength(150)]
        public string listName { get; set; }
    }
}
