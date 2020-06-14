﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace InvestCarControl.Configuration
{
    public static class MvcAndRazorConfig
    {
        public static IServiceCollection AddMvcAndRazor(this IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddRazorPages();
            services.AddControllers(config =>
            {
                // using Microsoft.AspNetCore.Mvc.Authorization;
                // using Microsoft.AspNetCore.Authorization;
                var policy = new AuthorizationPolicyBuilder()
                                 .RequireAuthenticatedUser()
                                 .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });
            return services;
        }
    }
}