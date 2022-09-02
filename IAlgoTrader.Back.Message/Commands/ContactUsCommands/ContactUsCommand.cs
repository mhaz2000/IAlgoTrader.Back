using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAlgoTrader.Back.Message.Commands.ContactUsCommands
{
    public class ContactUsCommand
    {
        public string Addresses { get; set; }
        public string PhoneNumbers { get; set; }
        public string Emails { get; set; }
    }
}
