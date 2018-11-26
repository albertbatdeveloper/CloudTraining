
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
using Microsoft.Bot.Sample.LuisBot.Services;
using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Ilitia.TA.Services.Services
{
    public class TextAnalyticsApiService 
    {

        private string _textAnalyticsKey;
        private string _textAnalyticsEndpoint;

        public TextAnalyticsApiService()
        {
            _textAnalyticsKey = ConfigurationManager.AppSettings["TextAnalyticsKey"];
            _textAnalyticsEndpoint = ConfigurationManager.AppSettings["TextAnalyticsEndpoint"];
        }

        public async Task<double?> GetSentimentTextAnalysisAsync(string text)
        {
            try
            {
                ITextAnalyticsClient client = new TextAnalyticsClient(new ApiKeyTextServiceClientCredentials(_textAnalyticsKey))
                {
                    Endpoint = _textAnalyticsEndpoint
                };

                SentimentBatchResult sentimentResult = await client.SentimentAsync(
                    new MultiLanguageBatchInput(
                        new List<MultiLanguageInput>()
                        {
                        new MultiLanguageInput("es", "1", text)
                        }));

                var sentiment = sentimentResult.Documents.First();
                return sentiment.Score.Value;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
