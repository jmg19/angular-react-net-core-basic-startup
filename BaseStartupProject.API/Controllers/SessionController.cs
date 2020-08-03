using BaseStartupProject.API.Authorization;
using BaseStartupProject.Application.AppModels.ResultModels;
using BaseStartupProject.Application.Services;
using BaseStartupProject.Application.Services.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseStartupProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SessionController : AbstractController
    {
        private readonly ILogger<SessionController> _logger;
        public SessionController(ILogger<SessionController> logger, IApplicationServicesFactory servicesFactory) : base(servicesFactory)
        {
            _logger = logger;
        }

        [BaseAuthorize(dontNeedToken = true)]
        [HttpGet("new")]
        public string NewToken()
        {
            string UUID = Request.Headers["UUID"];
            ISessionTokenService sessionTokenService = servicesFactory.CreateSessionTokenService();
            return sessionTokenService.GenerateToken(UUID, new UserAppModel { active = true }, true);
        }

        [BaseAuthorize(dontNeedToken = true)]
        [HttpPost("validate-token")]
        public UserAppModel GetUserByToken(Dictionary<string, string> form)
        {
            string UUID = Request.Headers["UUID"];
            ISessionTokenService sessionTokenService = servicesFactory.CreateSessionTokenService();
            string uuid = HttpContext.Request.Headers["UUID"];

            if (form != null && form["token"] != null)
            {
                return sessionTokenService.DecryptToken(uuid, form["token"]);
            }

            throw new Exception("Token Field is Required!");
        }
    }
}
