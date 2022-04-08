using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MoVik.Models
{
    public class FoodItem
    {
        [Key]
        public int FoodId { get; set; }
        
        [Required]
        [ForeignKey("MenuItems")]
        public int MenuId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(MAX)")]
        [DisplayName("Image Path")]
        public String ImagePath { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Name")]
        public String Name { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(MAX)")]
        [DisplayName("Description")]
        public String Description { get; set; }

        [Required]
        [DisplayName("Price")]
        public int Price { get; set; }

        [DisplayName("Items Sold")]
        public int Sold { get; set; }


        public virtual MenuItem MenuItems { get; set; }

    }
}
