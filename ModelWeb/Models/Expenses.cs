using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ModelWeb.Models
{
    public class Expenses
    {
        [Key]
        public long Id { get; set; }
        public long JobId { get; set; }
        public long ModelId { get; set; }
        public DateTime StartDate { get; set; }
        public string Comments { get; set; }
        public double Amount { get; set; }
    }
}
