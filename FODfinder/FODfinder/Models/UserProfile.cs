namespace FODfinder.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserProfile")]
    public partial class UserProfile
    {
        [Key]
        public string userID { get; set; }

        [Required]
        [DisplayName("Profile Privacy")]
        public bool is_public { get; set; }

        [Required]
        [DisplayName("Ethnicity Privacy")]
        public bool showEthnicity { get; set; }

        [Required]
        [DisplayName("Date of Birth Privacy")]
        public bool showAge { get; set; }

        [Required]
        [DisplayName("Location Privacy")]
        public bool showCountry { get; set; }

        [Required]
        [DisplayName("Gender Privacy")]
        public bool showGender { get; set; }

        [Required]
        [DisplayName("Make Date of Birth Public")]
        public bool showContact { get; set; }

        [StringLength(2000)]
        [DisplayName("Description")]
        public string description { get; set; }

        [StringLength(500)]
        [DisplayName("Profile Picture URL")]
        public string profileImgUrl { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }
    }
}
