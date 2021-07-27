using Common.Models.Mail;
using Contracts.Mail.MailRespounse;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiGateways.Service.CommandService.Mail
{
    public interface IMailServiceCommand
    {
        Task<SendMailRespounse> SendMessage(string UserId);
    }
}
