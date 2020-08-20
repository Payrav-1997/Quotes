using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Quotes.Models
{
    public class QuoteModels
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Quote { get; set; }
        [Required]
        public string Category { get; set; }
        public DateTime Date { get; set; }
    }
}
