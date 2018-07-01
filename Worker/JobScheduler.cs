using System;
using System.Threading;
using Emailer.Worker.Jobs;
using Hangfire;
using Hangfire.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Emailer.Worker
{
    public static class JobScheduler
    {
        public static IApplicationBuilder ScheduleJobs(this IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            var emailSendingJob = serviceProvider.GetService<IEmailSendingJob>();

            RecurringJob.AddOrUpdate(() => emailSendingJob.Execute(), Cron.Minutely);

            return app;
        }

    }
}