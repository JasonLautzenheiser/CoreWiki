﻿using System;
using CoreWiki.Areas.Identity.Data;
using CoreWiki.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(CoreWiki.Areas.Identity.IdentityHostingStartup))]
namespace CoreWiki.Areas.Identity
{
	public class IdentityHostingStartup : IHostingStartup
	{
		public void Configure(IWebHostBuilder builder)
		{
			builder.ConfigureServices((context, services) =>
			{
				services.AddDbContext<CoreWikiIdentityContext>(options =>
					options.UseSqlite(
						context.Configuration.GetConnectionString("CoreWikiIdentityContextConnection")));

				services.AddDefaultIdentity<CoreWikiUser>()
					.AddEntityFrameworkStores<CoreWikiIdentityContext>(); 

				if (!string.IsNullOrEmpty(context.Configuration["Authentication:Microsoft:ApplicationId"]))
				{

					services.AddAuthentication().AddMicrosoftAccount(microsoftOptions =>
				 {
					 microsoftOptions.ClientId = context.Configuration["Authentication:Microsoft:ApplicationId"];
					 microsoftOptions.ClientSecret = context.Configuration["Authentication:Microsoft:Password"];
				 });

				}

			});
		}
	}
}
