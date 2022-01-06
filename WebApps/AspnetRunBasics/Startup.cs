using AspnetRunBasics.Services;
using AspnetRunBasics.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace AspnetRunBasics
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //services.AddTransient<LoggingDelegatingHandler>();

            services.AddHttpClient<ICatalogService, CatalogService>(c =>
                c.BaseAddress = new Uri(Configuration["ApiSettings:GatewayAddress"]));
            //.AddHttpMessageHandler<LoggingDelegatingHandler>()
            //.AddPolicyHandler(GetRetryPolicy())
            //.AddPolicyHandler(GetCircuitBreakerPolicy());

            services.AddHttpClient<IBasketService, BasketService>(c =>
                c.BaseAddress = new Uri(Configuration["ApiSettings:GatewayAddress"]));
            //    .AddHttpMessageHandler<LoggingDelegatingHandler>()
            //    .AddPolicyHandler(GetRetryPolicy())
            //    .AddPolicyHandler(GetCircuitBreakerPolicy());

            services.AddHttpClient<IOrderService, OrderService>(c =>
                c.BaseAddress = new Uri(Configuration["ApiSettings:GatewayAddress"]));
            //    .AddHttpMessageHandler<LoggingDelegatingHandler>()
            //    .AddPolicyHandler(GetRetryPolicy())
            //    .AddPolicyHandler(GetCircuitBreakerPolicy());

            services.AddHttpClient<IUserService, UserService>(c => 
                c.BaseAddress = new Uri(Configuration["ApiSettings:GatewayAddress"]));
            //    .AddHttpMessageHandler<LoggingDelegatingHandler>()
            //    .AddPolicyHandler(GetRetryPolicy())
            //    .AddPolicyHandler(GetCircuitBreakerPolicy());


            services.AddRazorPages();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            });
            services.AddAuthentication().AddFacebook(options =>
            {
                options.AppId = "479144716347128";
                options.AppSecret = "8888cefba55e9cfa06a2b28f0495e533";
            });
            services.AddAuthentication().AddGoogle(options =>
            {
                options.ClientId = "501895478080-bg9l9dmbio9k0n4vrglnb0nttbku18a1.apps.googleusercontent.com";
                options.ClientSecret = "QXNcManVHU1ZJzR5duBbuZbA";

            });
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
