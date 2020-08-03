using BaseStartupProject.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BaseStartupProject.API.Controllers
{
    public abstract class AbstractController : ControllerBase
    {
        protected IApplicationServicesFactory servicesFactory;

        public AbstractController()
        {
            servicesFactory = new ApplicationServicesFactory();
        }

        public AbstractController(IApplicationServicesFactory servicesFactory)
        {
            this.servicesFactory = servicesFactory;
        }
    }
}
