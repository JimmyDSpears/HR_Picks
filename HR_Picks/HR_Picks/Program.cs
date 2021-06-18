using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Util.Store;
using HR_Picks.Bot;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading;

namespace HR_Picks
{
    class Program
    {
        static string[] scopes = { SheetsService.Scope.Spreadsheets };
        static string appName = "hrcalls";
        static void Main(string[] args)
        {
            UserCredential credential;

            using (var stream =
                new FileStream("GoogleCreds.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = appName,
            });

            var requestBody = new Google.Apis.Sheets.v4.Data.Spreadsheet();

            SpreadsheetsResource.CreateRequest request = service.Spreadsheets.Create(requestBody);

            var response = request.Execute();

            Console.WriteLine(JsonConvert.SerializeObject(response));

            var bot = new HRPicksBot();
            bot.RunAsync().GetAwaiter().GetResult();
        }
    }
}
