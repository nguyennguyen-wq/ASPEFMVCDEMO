
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMVCDemo.Models
{
    public class Medlem
    {
        [Key]
        public Guid Medlem_Id { get; set; }
        public string Fornavn { get; set; }
        public string Etternavn { get; set; }
        public string? Bosted { get; set; }
        public int MobilTlf { get; set; }
        public string Email { get; set; }
        [Column(TypeName = "date")]
        public DateTime Fodselsdato { get; set; }
        public int CurrentKontintId { get; set; }
        public Kontingent Kontingent { get; set; }
    }
}

    

