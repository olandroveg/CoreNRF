using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoreNRF.RequestAPI
{
    public class NotifySuscription
    {
        private string _NFurl;
        public NotifySuscription(string NFaddress)
        {
            _NFurl = NFaddress;
        }

    }
}
