using System;
using System.Configuration;
using System.Threading.Tasks;
using CoudTraining.Data.Repositories;
//
using Ilitia.TA.Services.Services;
using LuisBot.Resources;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;

namespace Microsoft.Bot.Sample.LuisBot
{

    // For more information about this template visit http://aka.ms/azurebots-csharp-luis
    [Serializable]
    public class MainDialog : IDialog<object>
    {
        public MainDialog()
        {
        }

        public async Task StartAsync(IDialogContext context)
        {

            context.Wait(AskFirstQuestionAsync);
        }

        private async Task AskFirstQuestionAsync(IDialogContext context, IAwaitable<object> result)
        {
            try
            {
                var message = await result as Activity;
                var opinionProcessed = false;
                if (message != null)
                {
                    var textAnalyticsService = new TextAnalyticsApiService();
                    var resultOpinion = await textAnalyticsService.GetSentimentTextAnalysisAsync(message.Text);
                    if (resultOpinion != null)
                    {
                        var repo = new CloudTrainingRepository();
                        repo.InsertCognitiveMeasure(" opinion ", resultOpinion.Value, message.Text);
                        if (resultOpinion.Value > 0.5)
                        {
                            await context.PostAsync(Messages.Response1Good);
                        }
                        else
                        {
                            await context.PostAsync(Messages.Response1Bad);
                        }
                        opinionProcessed = true;
                    }
                }
                if (opinionProcessed)
                {
                    //go to question 2
                    await context.PostAsync(Messages.Question2);
                    context.Wait(AskSecondQuestionAsync);
                }
                else
                {
                    await context.PostAsync(Messages.Sorry);
                    context.Done(context);
                }
            }
            catch (Exception ex)
            {
                await context.PostAsync(Messages.Sorry);
                context.Done(context);
            }
        }

        private async Task AskSecondQuestionAsync(IDialogContext context, IAwaitable<object> result)
        {
            try
            {
                var message = await result as Activity;
                if (message != null)
                {
                    var textAnalyticsService = new TextAnalyticsApiService();
                    var resultOpinion = await textAnalyticsService.GetSentimentTextAnalysisAsync(message.Text);
                    if (resultOpinion != null)
                    {
                        var repo = new CloudTrainingRepository();
                        repo.InsertCognitiveMeasure(" entrenamiento ", resultOpinion.Value, message.Text);
                        if (resultOpinion.Value > 0.6)
                        {
                            await context.PostAsync(Messages.Response2Good);
                        }
                        else
                        {
                            await context.PostAsync(Messages.Response2Bad);
                        }
                    }
                }

                await context.PostAsync(Messages.End);
                context.Done(context);

            }
            catch (Exception ex)
            {
                await context.PostAsync(Messages.Sorry);
                context.Done(context);
            }
        }

    }
}