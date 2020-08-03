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
    public class BaseAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        protected IApplicationServicesFactory servicesFactory;

        public BaseAuthorizeAttribute()
        {
            servicesFactory = new ApplicationServicesFactory();
        }

        public BaseAuthorizeAttribute(IApplicationServicesFactory servicesFactory)
        {
            this.servicesFactory = servicesFactory;
        }

        public bool dontNeedToken { get; set; }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (dontNeedToken && !string.IsNullOrEmpty(context.HttpContext.Request.Headers["UUID"]))
            {
                return;
            }

            if (!string.IsNullOrEmpty(context.HttpContext.Request.Headers["UUID"]) && !string.IsNullOrEmpty(context.HttpContext.Request.Headers["Token"]))
            {
                string uuid = context.HttpContext.Request.Headers["UUID"];
                string tokenString = context.HttpContext.Request.Headers["Token"];
                ISessionTokenService sessionTokenService = servicesFactory.CreateSessionTokenService();
                UserAppModel user = sessionTokenService.DecryptToken(uuid, tokenString);

                if (user != null)
                {
                    return;
                }
            }

            context.Result = new UnauthorizedResult();
            return;
        }
    }
}
