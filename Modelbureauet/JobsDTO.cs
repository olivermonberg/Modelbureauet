using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelbureauet
{
    public class JobsDTO
    {
        public JobsDTO(string customerName, DateTime startDate, int numberOfDays, string location, int numberOfModels, string comments, bool isPlanned, long id)
        {
            CustomerName = customerName;
            StartDate = startDate;
            NumberOfDays = numberOfDays;
            Location = location;
            NumberOfModels = numberOfModels;
            Comments = comments;
            IsPlanned = isPlanned;
            Id = id;
        }
        
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
