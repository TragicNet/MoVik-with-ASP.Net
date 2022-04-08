using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MoVik.Models
{
    public class MenuItem
    {
        [Key]
        public int MenuId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Name")]
        public String Name { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public String ImagePath { get; set; }

        [Required]
        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile ImageFile { get; set; }
    }
}
