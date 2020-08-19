using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quotes.Models
{
    public class QuoteModels
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Quote { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
    }
}
