using Microsoft.Extensions.Hosting;
using MimeKit;
using Quotes.Controllers;
using Quotes.Models;
using SocialApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Quotes.Workers
{
    public class MailWorker : IHostedService
    {
        private Timer timer;

        public async Task Send(List<Subscribers> subscribers, List<QuoteModels> quotes)
        {
            Email emailService = new Email();
            var mails = subscribers.Select(p => new MailboxAddress(p.Email)).ToList();
            await emailService.SendEmailAsync(mails, "Цитаты дня", string.Join('\n', quotes.Select(p=> p.Quote)));
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(Canecellation, null, 0, 60000); //Для ежедневной отправки измените на 86400000
            return Task.CompletedTask;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public async void Canecellation(object state)
        {
            var subscribers = QuoteController.subscribersList;
            var quotes = QuoteController.quotesList;
            if(subscribers.Count > 0&& quotes.Count > 0)
            await Send(subscribers, quotes);
        }
    }
}
