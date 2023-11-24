using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace bound.UpTimeTimer
{
    public class BoundHubUpTimerFunction
    {
        [FunctionName("BoundHubUpTimerFunction")]
        public async Task RunAsync([TimerTrigger("0 */5 * * * *", RunOnStartup = true)] TimerInfo myTimer, ILogger log)
        {
            var HttpClient = new HttpClient();
            var result = await HttpClient.GetStringAsync("https://boundhub.azurewebsites.net/send?name=Daniel&machinename=Chestmachine&status=ofsdf&reps=10&weight=10&DebugText=debug");
            var result2 = await HttpClient.GetStringAsync("https://boundhub.azurewebsites.net/");

            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            log.LogInformation(result);
            log.LogInformation(result2);
        }
    }
}
