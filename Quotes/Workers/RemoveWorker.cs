using Microsoft.Extensions.Hosting;
using Quotes.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Quotes.Workers
{
    public class RemoveWorker : IHostedService
    {
        private Timer timer;
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(Canecellation, null, 0, 300000);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Canecellation(object state)
        {
            QuoteController.quotesList.RemoveAll(t => DateTime.Now.Subtract(t.Date).TotalMinutes > 1440); //24 часа
        }
    }
}
