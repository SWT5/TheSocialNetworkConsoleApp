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
            henrik.CircleId.Add(Studieven.CircleId);
            
            var odin = _services.CreateUser(new User { UserName = "Odin", Age = 20, Gender = "Male" });
            odin.CircleId.Add(Studieven.CircleId);
            
            var ninna = _services.CreateUser(new User { UserName = "Ninna", Age = 25, Gender = "Female" });

            var ida = _services.CreateUser(new User { UserName = "Ida", Age = 21, Gender = "Female" });
            ida.CircleId.Add(Studieven.CircleId);


            _services.UpdateUser(henrik.UserId, henrik);
            _services.UpdateUser(odin.UserId, odin);
            _services.UpdateUser(ninna.UserId, ninna);
            _services.UpdateUser(ida.UserId, ida);
        }
            var studieven = _services.CreateCircle(new Circle{CircleName = "Studieven"});
            var coronagruppen = _services.CreateCircle(new Circle {CircleName = "coronagruppen" });
            var WildWest = _services.CreateCircle(new Circle { CircleName = "WildWest" });
            var folketinget = _services.CreateCircle(new Circle { CircleName = "folketinget" });
            var LoyaltoFamilia = _services.CreateCircle(new Circle { CircleName = "LoyaltoFamilia" });

        }
    }
}
