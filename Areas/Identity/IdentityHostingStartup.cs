﻿using System;
using crowsoftmvc.Areas.Identity.Data;
using crowsoftmvc.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(crowsoftmvc.Areas.Identity.IdentityHostingStartup))]
namespace crowsoftmvc.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            //builder.ConfigureServices((context, services) => {
            //    services.AddDbContext<crowsoftmvcContext>(options =>
            //        options.UseMySQL(
            //            context.Configuration.GetConnectionString("DefaultConnection")));

            //    services.AddDefaultIdentity<crowsoftmvcUser>()
            //        .AddEntityFrameworkStores<crowsoftmvcContext>();
            //});
        }
    }
}