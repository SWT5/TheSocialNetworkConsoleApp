using System;
using System.Collections.Generic;
using System.Text;
using TheSocialNetworkConsoleApp.Queries;

namespace TheSocialNetworkConsoleApp
{
    public class DummyData
    {
        public Services.Services _services { get; set; }
        public SetUp setUp;

        public DummyData(Services.Services services)
        {
            setUp = new SetUp(services);
        }


        public void seedData()
        {

        }

    }
}
