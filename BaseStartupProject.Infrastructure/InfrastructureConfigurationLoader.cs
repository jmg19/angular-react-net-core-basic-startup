using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BaseStartupProject.Infrastructure
{    
    public struct InfrastructureConfigurations {
        public string sql_Connection;
    }

    public interface IInfrastructureConfigurationLoader
    {
        InfrastructureConfigurations Load(string config_file);
    }

    public class InfrastructureConfigurationLoader: IInfrastructureConfigurationLoader
    {
        public InfrastructureConfigurations Load(string config_file) {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), config_file);
            configurationBuilder.AddJsonFile(path, false);

            try
            {
                var root = configurationBuilder.Build();
                InfrastructureConfigurations configs = new InfrastructureConfigurations
                {
                    sql_Connection = root.GetSection("ConnectionString").GetSection("DataConnection").Value
                };

                return configs;
            }
            catch (FileNotFoundException) {
                return default(InfrastructureConfigurations);
            }
        }
    }
}
