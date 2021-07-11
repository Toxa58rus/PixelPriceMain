using Common.Extensions;
using Common.Models.Mail;
using Common.Rcp;
using System;
using System.Threading.Tasks;

namespace Mail.Command
{
    public class SendMailCommand : ServiceCommand
    {
        public override string Name => "SendMail";

        public override async Task<string> Execute(object jsonValue)
        {
            var value = jsonValue.ToString().DeserializeToObject<SendMailModel>();

            return "123123123".ToJson();
        }
    }
}
