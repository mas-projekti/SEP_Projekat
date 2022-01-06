using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Identity
{
    public static class IdentityConfigExtensions
    {
        public static IdentityConfig GetIdentityConfig(this IConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            var serviceConfig = new IdentityConfig
            {
                IdentityURL = configuration.GetValue<Uri>("Identity:URL")
            };

            return serviceConfig;
        }
    }
}
