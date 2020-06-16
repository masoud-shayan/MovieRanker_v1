﻿using System.Threading.Tasks;

 namespace IdentityServer.EmailService
{
    public interface IEmailSender
    {
        void SendEmail(Message message);
        Task SendEmailAsync(Message message);
    }
}
