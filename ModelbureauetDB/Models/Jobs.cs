using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ModelbureauetDB.Models
{
    public class Jobs
    {
        [Key]
        public long Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime StartDate { get; set; }
        public int NumberOfDays { get; set; }
        public string Location { get; set; }
        public int NumberOfModels { get; set; }
        public string Comments { get; set; }
        public bool IsPlanned { get; set; } = false;
    }
}
