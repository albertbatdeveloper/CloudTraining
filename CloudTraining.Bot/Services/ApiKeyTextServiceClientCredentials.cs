using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Bot.Sample.LuisBot.Services
{
    public class ApiKeyTextServiceClientCredentials : ServiceClientCredentials
    {

        public string _textAnalyticsKey;

        public ApiKeyTextServiceClientCredentials(string textAnalyticsKey)
        {
            _textAnalyticsKey = textAnalyticsKey;
        }

        public override Task ProcessHttpRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("Ocp-Apim-Subscription-Key", _textAnalyticsKey);
            return base.ProcessHttpRequestAsync(request, cancellationToken);
        }
    }
}
