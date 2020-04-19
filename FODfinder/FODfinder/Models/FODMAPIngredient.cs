namespace FODfinder.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FODMAPIngredient
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FODMAPIngredient()
        {
            UserIngredients = new HashSet<UserIngredient>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Ingredient Name")]
        public string Name { get; set; }

        [StringLength(50)]
        [DisplayName("Also Listed As")]
        public string Aliases { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserIngredient> UserIngredients { get; set; }
    }
}
