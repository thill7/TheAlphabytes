namespace FODfinder.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserInformation")]
    public partial class UserInformation
    {
        [Key]
        public string userID { get; set; }

        [Required]
        [StringLength(30)]
        [DisplayName("First Name")]
        public string firstName { get; set; }

        [Required]
        [StringLength(30)]
        [DisplayName("Last Name")]
        public string lastName { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Ethnicity")]
        public string ethnicity { get; set; }

        [Required]
        [DisplayName("Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime birthdate { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Location")]
        public string country { get; set; }

        [Required]
        [StringLength(25)]
        [DisplayName("Gender")]
        public string gender { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }
    }
}
