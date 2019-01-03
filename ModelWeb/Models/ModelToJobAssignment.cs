using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ModelWeb.Models
{
    public class ModelToJobAssignment
    {
        [Key]
        public long Id { get; set; }
        public long ModelId { get; set; }
        public long JobId { get; set; }
    }
}
