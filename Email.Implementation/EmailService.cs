﻿using Email.Interfaces;

namespace Email.Implementation
{
    public class EmailService : IEmailService
    {
        public Task SendAsync(string  address, string subject, string body)
        {
            Console.WriteLine($"Email to {address} subject '{subject}' body '{body}'");
            Console.Out.Flush();

            return Task.CompletedTask;
        }
    }
}
