using Hangfire;
using System.Linq.Expressions;
using WebApp.Interfaces;

namespace WebApp.Services
{
    public class BackgroundJobService : IBackgroundJobService
    {
        public void Schedule<T>(Expression<Func<T, Task>> expression)
        {
            BackgroundJob.Schedule(expression, TimeSpan.Zero);
        }
    }
}
