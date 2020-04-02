using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Dialogflow.V2;

namespace TgBot_FW.Model.DialogFlow
{
    public class DialogflowManager
    {
        Settings settings;
        private string userID;
        //private string webRootPath;
        //private string contentRootPath;
        //private string projectId;
        private SessionsClient sessionsClient;
        private SessionName sessionName;

        public DialogflowManager(string userID, Settings settings)
        {
            this.userID = userID;
            this.settings = settings;
            //this.projectId = settings.DialogFlowProject;
            SetEnvironmentVariable();
        }
        private void SetEnvironmentVariable()
        {
            try
            {
                Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", settings.JsoneFilePath);
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (SecurityException)
            {
                throw;
            }
        }
        private async Task CreateSession()
        {
            // Create client
            sessionsClient = await SessionsClient.CreateAsync();
            // Initialize request argument(s)
            sessionName = new SessionName(settings.DialogFlowProject, userID);

        }
        public async Task<QueryResult> CheckIntent(string userInput, string LanguageCode = "ru")
        {
            await CreateSession();
            DetectIntentResponse response;
            QueryInput queryInput = new QueryInput();
            var queryText = new TextInput();
            queryText.Text = userInput;
            queryText.LanguageCode = LanguageCode;
            queryInput.Text = queryText;
            try
            {
                response = await sessionsClient.DetectIntentAsync(sessionName, queryInput);
            }
            catch
            {
                response = null;
            }
            return response.QueryResult;
        }
    }
}
