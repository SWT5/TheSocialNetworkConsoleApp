using System;
using System.Collections.Generic;
using System.Text;
using TheSocialNetworkConsoleApp.Models;
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
            var studieven = _services.CreateCircle(new Circle {CircleName = "Studieven"});
            var coronagruppen = _services.CreateCircle(new Circle {CircleName = "coronagruppen"});
            var WildWest = _services.CreateCircle(new Circle {CircleName = "WildWest"});
            var folketinget = _services.CreateCircle(new Circle {CircleName = "folketinget"});
            var LoyaltoFamilia = _services.CreateCircle(new Circle {CircleName = "LoyaltoFamilia"});

        }
    }
}
