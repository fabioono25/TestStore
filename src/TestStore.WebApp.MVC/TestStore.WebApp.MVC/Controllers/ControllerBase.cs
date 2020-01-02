using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestStore.WebApp.MVC.Controllers
{
    public abstract class ControllerBase: Controller
    {
        protected Guid ClienteId = Guid.Parse("9a9c261e-ff75-494e-b971-3d4030d56adb");
    }
}
