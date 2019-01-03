using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModelWeb.Areas.Identity.Data;
using ModelWeb.Models;

[assembly: HostingStartup(typeof(ModelWeb.Areas.Identity.IdentityHostingStartup))]
namespace ModelWeb.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            //builder.ConfigureServices((context, services) => {
            //    services.AddDbContext<ModelWebContext>(options =>
            //        options.UseSqlServer(
            //            context.Configuration.GetConnectionString("ModelWebContextConnection")));
            //
            //    services.AddDefaultIdentity<ModelWebUser>()
            //        .AddEntityFrameworkStores<ModelWebContext>();
            //});
        }
    }
}