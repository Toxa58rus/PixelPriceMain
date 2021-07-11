using Common.Models.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiGateways.Service.CommandService.Mail
{
    public interface IMailServiceCommand
    {
        Task<string> SendMessage(string UserId);
    }
}
