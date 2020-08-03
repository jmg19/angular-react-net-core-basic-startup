using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace BaseStartupProject.Application.Exceptions
{
    public class SystemConfigurationMissingException: Exception
    {
        public enum ConfigurationName
        {
            TokenEncriptKey
        }
        public SystemConfigurationMissingException(ConfigurationName configurationName):base(ProcessConfigurationName(configurationName))
        {            
        }

        private static string ProcessConfigurationName(ConfigurationName configurationName)
        {
            switch (configurationName)
            {
                case ConfigurationName.TokenEncriptKey:
                    return "Token Encript Key is not setted in the System!";
                default:
                    return "Configuration Missing in the System!";
            }
        }
    }
}
