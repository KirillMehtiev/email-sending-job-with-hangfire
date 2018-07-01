using System;
using Emailer.Configuration;
using Emailer.Repositories;
using Emailer.Services;
using Emailer.Services.Extensions;
using Emailer.Worker;
using Emailer.Worker.Jobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Hangfire;

namespace Emailer
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
            services.AddSingleton<IEmailerConfiguration>(
                new EmailerConfiguration(Configuration.GetConnectionString("DefaultConnection"), Configuration["EmailProvider:Key"]));

            services.AddTransient<IEmailMessageRepository, EmailMessageRepository>();
            services.AddTransient<IEmailMessageService, EmailMessageService>();
            services.AddTransient<IEmailSenderService, EmailSenderService>();
            services.AddTransient<IEmailSendigJob, EmailSendingEmailSendigJob>();

            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseHangfireServer();
            app.UseHangfireDashboard();
            app.ScheduleJobs(serviceProvider);
            app.RegisterSafeTypeForEmails();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
