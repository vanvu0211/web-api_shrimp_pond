﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Infrastructure.Communication
{
    public class MqttOptions
    {
        public int CommunicationTimeout { get; set; }
        public string Host { get; set; } = "";
        public int Port { get; set; }
        public int KeepAliveInterval { get; set; }
        public string ClientId { get; set; } = "";
        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
