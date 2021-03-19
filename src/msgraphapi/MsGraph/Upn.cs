using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using msgraphapi.ExceptionHandling;

namespace msgraphapi.MsGraph
{
    public class Upn
    {
        public readonly string Value;

        public Upn(string value, IEnumerable<Domain> domains)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new InvalidRequestException($"{nameof(Upn)} {nameof(value)} cannot be null or empty");

            var defaultDomain = domains.FirstOrDefault(x => x.IsDefaultDomain());
            if (defaultDomain == null)
                throw new InvalidOperationException("Default domain for Azure AD could not be determined. Please check the Azure AD domains configuration.");

            Value = value.EndsWith(defaultDomain.id, true, CultureInfo.CurrentCulture)
                ? value.ToLower()
                : $"{value.ToLower().Replace("@", "_")}%23EXT%23%40{defaultDomain.id}";
        }
    }
}
