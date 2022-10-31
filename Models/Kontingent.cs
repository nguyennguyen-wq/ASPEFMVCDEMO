using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace WebEFMVCDemo.Models
{
    public class Kontingent
    {
        [Key]
        public int KontintId { get; set; }
        public string? Name { get; set; }
        public ICollection<Medlem> Medlems { get; set; }
    }
}
