using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace BTG.Application.Services
{
    public class SmsService
    {
        private readonly string _accountSid = "{SidTwilio}"; // Obtén esto de Twilio
        private readonly string _authToken = "{TokenTwilio}"; // Obtén esto de Twilio

        public SmsService()
        {
            TwilioClient.Init(_accountSid, _authToken);
        }

        public void SendSms(string toPhoneNumber, string message)
        {
            var messageOptions = new CreateMessageOptions(new PhoneNumber(toPhoneNumber))
            {
                From = new PhoneNumber("{PhoneSend}"), // Tu número Twilio
                Body = message
            };
            var msg = MessageResource.Create(messageOptions);
        }
    }
}
