using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Web;

namespace IoTControllers.Controllers
{
    public class TempIoTController : IIoTController
    {
        public TempIoTController()
        {

        }

        public void SendEmail(string deviceId, string messageData)
        {
            string Formattedmessage = GetFormatedMessage(messageData);
            MailMessage mailMessage = new MailMessage("azurefundemo@gmail.com", "azurefundemo@gmail.com");
            mailMessage.Body = Formattedmessage;
            mailMessage.Subject = "Data received from "+ deviceId;
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.Credentials = new System.Net.NetworkCredential()
            {
                UserName = "azurefundemo@gmail.com",
                Password = "azurefunctiondemo1!"
            };
            smtpClient.EnableSsl = true;
            try
            {
                smtpClient.Send(mailMessage);

            }
            catch (Exception ex)
            {

            }
        }

        public string GetFormatedMessage(string message)
        {
            TemperatureModel temperatureModel = Newtonsoft.Json.JsonConvert.DeserializeObject<TemperatureModel>(message);
            string messageTobeSent = "Received Weather details as below :";
            messageTobeSent = messageTobeSent + "\n\n";
            messageTobeSent = messageTobeSent + "Wind : "+temperatureModel.Wind+"\n";
            messageTobeSent = messageTobeSent + "Humidity : " + temperatureModel.Humidity + "\n";
            messageTobeSent = messageTobeSent + "Precipitation : " + temperatureModel.Precipitation + "\n";

           // string body = HttpUtility.HtmlDecode(messageTobeSent);
            return messageTobeSent;
        }

    }

    public class TemperatureModel
    {
        public decimal Wind { get; set; }
        public decimal Humidity { get; set; }
        public decimal  Precipitation { get; set; }
    }
}
