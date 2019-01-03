using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelbureauet
{
    public class ModelDTO
    {
        public ModelDTO(string name, int phoneNumber, string address, int height, int weight, string hairColor, string comments, long id)
        {
            
            Name = name;
            PhoneNumber = phoneNumber;
            Address = address;
            Height = height;
            Weight = weight;
            HairColor = hairColor;
            Comments = comments;
            Id = id;
        }

        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public int PhoneNumber { get; set; }
        public string Address { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string HairColor { get; set; }
        public string Comments { get; set; }
    }
}
