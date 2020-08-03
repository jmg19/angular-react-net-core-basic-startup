using BaseStartupProject.Application.AppModels.ResultModels;
using BaseStartupProject.Application.Services;
using BaseStartupProject.Application.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseStartupProject.API.Authorization
{
    public class UserAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        protected IApplicationServicesFactory servicesFactory;

        public UserAuthorizeAttribute()
        {
            servicesFactory = new ApplicationServicesFactory();
        }

        public UserAuthorizeAttribute(IApplicationServicesFactory servicesFactory)
        {
            this.servicesFactory = servicesFactory;
        }        
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!string.IsNullOrEmpty(context.HttpContext.Request.Headers["UUID"]) && !string.IsNullOrEmpty(context.HttpContext.Request.Headers["Token"]))
            {
                string uuid = context.HttpContext.Request.Headers["UUID"];
                string tokenString = context.HttpContext.Request.Headers["Token"];
                ISessionTokenService sessionTokenService = servicesFactory.CreateSessionTokenService();
                UserAppModel user = sessionTokenService.DecryptToken(uuid, tokenString);

                if (user != null && user.id > 0)
                {
                    return;
                }
            }

            context.Result = new UnauthorizedResult();
            return;
        }
    }
}
