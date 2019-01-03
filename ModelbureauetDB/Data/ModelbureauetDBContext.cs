using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModelbureauetDB.Models;

namespace ModelbureauetDB.Models
{
    public class ModelbureauetDBContext : DbContext
    {
        public ModelbureauetDBContext (DbContextOptions<ModelbureauetDBContext> options)
            : base(options)
        {
        }

        public DbSet<ModelbureauetDB.Models.Model> Model { get; set; }

        public DbSet<ModelbureauetDB.Models.Jobs> Jobs { get; set; }
    }
}
