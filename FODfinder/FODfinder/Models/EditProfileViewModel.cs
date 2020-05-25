using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FODfinder.Models
{
    public class EditProfileViewModel
    {
        public EditProfileViewModel(UserInformation userInfo, UserProfile userProf)
        {
            this.userID = userInfo.userID;
            this.is_public = userProf.is_public;
            this.optIn = userProf.optIn;
            this.showAge = userProf.showAge;
            this.showContact = userProf.showContact;
            this.showCountry = userProf.showCountry;
            this.showEthnicity = userProf.showEthnicity;
            this.showGender = userProf.showGender;
            this.profileImgUrl = userProf.profileImgUrl;
            this.description = userProf.description;
            this.firstName = userInfo.firstName;
            this.lastName = userInfo.lastName;
            this.ethnicity = userInfo.ethnicity;
            this.country = userInfo.country;
            this.gender = userInfo.gender;
            this.birthdate = userInfo.birthdate;
        }

        [Key]
        public string userID { get; set; }

        [Required]
        [DisplayName("Make Profile Public")]
        public bool is_public { get; set; }

        [Required]
        [DisplayName("Use my anonymized demographic info")]
        public bool optIn { get; set; }

        [Required]
        [DisplayName("Make Ethnicity Public")]
        public bool showEthnicity { get; set; }

        [Required]
        [DisplayName("Make Age Public")]
        public bool showAge { get; set; }

        [Required]
        [DisplayName("Make Location Public")]
        public bool showCountry { get; set; }

        [Required]
        [DisplayName("Make Gender Public")]
        public bool showGender { get; set; }

        [Required]
        [DisplayName("Make Email Public")]
        public bool showContact { get; set; }

        [StringLength(2000)]
        [DisplayName("Description")]
        public string description { get; set; }

        [StringLength(15)]
        [DisplayName("Profile Picture")]
        public string profileImgUrl { get; set; }

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
        [StringLength(50)]
        [DisplayName("Location")]
        public string country { get; set; }

        [Required]
        [StringLength(25)]
        [DisplayName("Gender")]
        public string gender { get; set; }

        [Required]
        [DisplayName("Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime birthdate { get; set; }
    }
}