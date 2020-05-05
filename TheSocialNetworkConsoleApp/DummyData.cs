using System;
using System.Collections.Generic;
using System.Text;
using TheSocialNetworkConsoleApp.Models;
using TheSocialNetworkConsoleApp.Queries;
using TheSocialNetworkConsoleApp.Models;
using TheSocialNetworkConsoleApp.Services;

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
            
            
            var henrik = _services.CreateUser(new User { UserName = "Henrik", Age = 22, Gender = "Male" });
            henrik.Circles.Add(studieven.CircleId);
            
            var odin = _services.CreateUser(new User { UserName = "Odin", Age = 20, Gender = "Male" });
            odin.Circles.Add(studieven.CircleId);
            odin.Circles.Add(coronagruppen.CircleId);
            odin.Circles.Add(WildWest.CircleId);


            var ninna = _services.CreateUser(new User { UserName = "Ninna", Age = 25, Gender = "Female" });
            ninna.Circles.Add(folketinget.CircleId);
            ninna.Circles.Add(LoyaltoFamilia.CircleId);

            var ida = _services.CreateUser(new User { UserName = "Ida", Age = 21, Gender = "Female" });
            ida.Circles.Add(folketinget.CircleId);
            ida.Circles.Add(LoyaltoFamilia.CircleId);
            ida.Circles.Add(coronagruppen.CircleId);

            var svend = _services.CreateUser(new User { UserName = "Svend", Age = 27, Gender = "Male" });
            svend.Circles.Add(folketinget.CircleId);
            svend.Circles.Add(LoyaltoFamilia.CircleId);
            svend.Circles.Add(coronagruppen.CircleId);

            var vilde = _services.CreateUser(new User { UserName = "Vilde", Age = 24, Gender = "Female" });
            vilde.Circles.Add(folketinget.CircleId);
            vilde.Circles.Add(LoyaltoFamilia.CircleId);
            vilde.Circles.Add(coronagruppen.CircleId);
            vilde.Circles.Add(WildWest.CircleId);
            vilde.Circles.Add(studieven.CircleId);


            _services.UpdateUser(henrik.UserId, henrik);
            _services.UpdateUser(odin.UserId, odin);
            _services.UpdateUser(ninna.UserId, ninna);
            _services.UpdateUser(ida.UserId, ida);
        }
           

        
    }
}
