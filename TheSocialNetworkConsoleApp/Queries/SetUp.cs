using System;
using System.Collections.Generic;
using System.Text;
using TheSocialNetworkConsoleApp.Services;

namespace TheSocialNetworkConsoleApp.Queries
{
    public class SetUp
    {
        public SetUp(Services.Services services)
        {
            _services = services;
        }

        public Services.Services _services { get; set; }
    }
}
