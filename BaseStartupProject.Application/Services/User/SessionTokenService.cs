using BaseStartupProject.Application.AppModels.ResultModels;
using BaseStartupProject.Application.Exceptions;
using BaseStartupProject.Application.Mappers;
using BaseStartupProject.Infrastructure.Repositories;
using BaseStartupProject.Infrastructure.Repositories.DataTableObjects;
using Global.SessionTokenGeneratorPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseStartupProject.Application.Services.User
{
    public class SessionTokenService : BaseQueryService, ISessionTokenService
    {

        ISessionTokenGenerator tokenGenerator;
        public SessionTokenService(): base()
        {
            string key = GetEncriptKey();
            this.tokenGenerator = new SessionTokenGenerator(key);
        }

        public SessionTokenService(IRepositoryFactory repositoryFactory) : base(repositoryFactory)
        {
            string key = GetEncriptKey();
            this.tokenGenerator = new SessionTokenGenerator(key);
        }

        public SessionTokenService(IRepositoryFactory repositoryFactory, ISessionTokenGenerator sessionTokenGenerator) : base(repositoryFactory)
        {            
            this.tokenGenerator = sessionTokenGenerator;
        }
        private string GetEncriptKey()
        {
            IReadOnlyRepository<DtoConfiguration> repository = repositoryFactory.CreateReadOnlyConfigurationsRepository();
            var line = repository.GetAll().Where(x => x.Name == "tokenEncriptKey").FirstOrDefault();
            if (line != null) {
                return line.Value;
            }

            throw new SystemConfigurationMissingException(SystemConfigurationMissingException.ConfigurationName.TokenEncriptKey);
        }

        public UserAppModel DecryptToken(string UUID, string tokenString)
        {
            SessionToken sessionToken = tokenGenerator.Decrypt(tokenString);
            
            if (sessionToken.IsValid(UUID)) { 
                AbstractMapper<SessionToken, UserAppModel> mapper = mapperFactory.Create<SessionToken, UserAppModel>();
                return mapper.Map(sessionToken);            
            };

            return null;
        }

        public string GenerateToken(string UUID, UserAppModel userModel, bool isExpirable)
        {
            int daysToExpire = isExpirable ? 1 : 0;
            SessionToken token = new SessionToken
            {
                UUID = UUID,
                daysToExpire = daysToExpire,
                userId = userModel.id,
                userName = userModel.username,
                dateTime = DateTime.Now
            };

            return tokenGenerator.Encript(token);
        }
    }
}
