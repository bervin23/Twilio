using System;
using System.Web.Mvc;
using Twilio.AspNet.Common;
using Twilio.AspNet.Mvc;
using Twilio.TwiML;

namespace Twilio_Ivr.Controllers
{
    public class VoiceController : TwilioController
    {
        [HttpPost]
        public TwiMLResult Index(VoiceRequest request)
        {
            var response = new VoiceResponse();

            if (!string.IsNullOrEmpty(request.Digits))
            {
                if (request.Digits == "1")
                    response.Say("Your new appointment is being scheduled.");
                else if (request.Digits == "2")
                    response.Say("Your appointment has been changed.");
                else if (request.Digits == "3")
                    response.Say("Your appointment has been cancelled.");
                else
                {
                    response.Say("Sorry, I don't understand that choice.").Pause();
                    RenderMainMenu(response);
                }
            }
            else
            {
                // If no input was sent, use the <Gather> verb to collect user input
                RenderMainMenu(response);
            }

            return TwiML(response);
        }

        private static void RenderMainMenu(VoiceResponse response)
        {
            response.Gather(numDigits: 1)
                .Say("Thanks for calling this test IVR. To schedule a new appointment, press 1. To change an existing appointment, press 2. To cancel an existing appointment, press 3.");

            // If the user doesn't enter input, loop
            response.Redirect(new Uri("/voice", UriKind.Relative));
        }
    }
}
