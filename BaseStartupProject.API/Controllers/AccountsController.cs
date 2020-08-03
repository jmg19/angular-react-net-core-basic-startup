using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseStartupProject.API.Authorization;
using BaseStartupProject.Application.AppModels.ResultModels;
using BaseStartupProject.Application.Commands.Users;
using BaseStartupProject.Application.Exceptions;
using BaseStartupProject.Application.Queries.Users;
using BaseStartupProject.Application.Services;
using BaseStartupProject.Application.Services.User;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BaseStartupProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : AbstractController
    {        
        private readonly ILogger<AccountsController> _logger;

        public AccountsController(ILogger<AccountsController> logger, IApplicationServicesFactory servicesFactory) : base(servicesFactory)
        {
            _logger = logger;
        }

        [UserAuthorize]
        [HttpGet()]
        public IEnumerable<UserAppModel> Get()
        {
            IUserQueriesService userQueriesService = servicesFactory.CreateUserQueriesService();
            return userQueriesService.GetAllUsers();
        }

        [UserAuthorize]
        [HttpGet("{id:int}")]
        public UserAppModel Get(int id)
        {
            IUserQueriesService userQueriesService = servicesFactory.CreateUserQueriesService();
            return userQueriesService.GetUser(id);
        }

        [UserAuthorize]
        [HttpGet("{userName}")]
        public UserAppModel Get(string userName)
        {
            IUserQueriesService userQueriesService = servicesFactory.CreateUserQueriesService();
            UserQuery query = new UserByUsernameQuery(userName);
            IList<UserAppModel> result = userQueriesService.GetUsers(query);

            if(result.Count > 0)
            {
                return result[0];
            }
            
            return null;
        }

        [BaseAuthorize]
        [HttpGet("check/{userName}")]
        public bool CheckExists(string userName)
        {
            IUserQueriesService userQueriesService = servicesFactory.CreateUserQueriesService();
            UserQuery query = new UserByUsernameQuery(userName);
            IList<UserAppModel> result = userQueriesService.GetUsers(query);

            if (result.Count > 0)
            {
                return true;
            }

            return false;
        }

        [UserAuthorize]
        [HttpPost("by")]
        public IEnumerable<UserAppModel> Get(UserQuery query)
        {
            IUserQueriesService userQueriesService = servicesFactory.CreateUserQueriesService();
            return userQueriesService.GetUsers(query);
        }

        [BaseAuthorize]
        [HttpPost()]
        public bool Post(SignUpUser command)
        {
            try
            {
                IUserCommandsService userCommandsService = servicesFactory.CreateUserCommandsService();
                userCommandsService.SignUpNewUser(command);
                return true;
            }
            catch (UserAlreadyInUseException)
            {
                return false;
            }
        }

        [BaseAuthorize]
        [HttpPost("login")]
        public string Login(UserLogin command)
        {
            IUserCommandsService userCommandsService = servicesFactory.CreateUserCommandsService();
            int userId = userCommandsService.Login(command);
            if (userId > 0)
            {
                string UUID = Request.Headers["UUID"];
                ISessionTokenService sessionTokenService = servicesFactory.CreateSessionTokenService();
                return sessionTokenService.GenerateToken(UUID, new UserAppModel { id = userId, username = command.UserName,  active = true }, true);
            }

            return "";
        }

        [UserAuthorize]
        [HttpPatch("{id}/activate")]
        public void Activate(int id)
        {
            IUserCommandsService userCommandsService = servicesFactory.CreateUserCommandsService();
            userCommandsService.Activate(new ActivateUser { userId = id });
        }

        [UserAuthorize]
        [HttpPatch("{id}/inactivate")]
        public void Inactivate(int id)
        {
            IUserCommandsService userCommandsService = servicesFactory.CreateUserCommandsService();
            userCommandsService.Inactivate(new InactivateUser { userId = id });
        }
    }
}
