using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelbureauet
{
    public class ModelToJobAssignmentDTO
    {
        public ModelToJobAssignmentDTO(long id, long modelId, long jobId)
        {
            Id = id;
            ModelId = modelId;
            JobId = jobId;
        }
        
        [Key]
        public long Id { get; set; }
        public long ModelId { get; set; }
        public long JobId { get; set; }
    }
}
