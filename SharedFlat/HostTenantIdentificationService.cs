﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace SharedFlat
{
    public sealed class HostTenantIdentificationService : ITenantIdentificationService
    {
        private readonly HostTenantSettings _tenants;

        public HostTenantIdentificationService(IConfiguration configuration)
        {
            this._tenants = configuration.GetHostTenantSettings();
        }

        public HostTenantIdentificationService(HostTenantSettings tenants)
        {
            this._tenants = tenants;
        }

        public IEnumerable<string> GetTenants()
        {
            return this._tenants.Tenants.Values;
        }

        public string GetTenant(HttpContext context)
        {
            if (!this._tenants.Tenants.TryGetValue(context.Request.Host.Host, out var tenant))
            {
                tenant = this._tenants.Default;
            }

            return tenant;
        }
    }

}
