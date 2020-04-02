using Google.Cloud.Dialogflow.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgBot_FW.Model.DialogFlow
{
    class ResponseHandler
    {
        string[] messages;
        string[] context;
        Dictionary<string, string> parameters;

        public string[] Messages
        {
            get
            {
                return messages;
            }
            set
            {
                if (value.Length == 1)
                {
                    if (value[0] == "[\"\"]")
                    {
                        messages = null;
                    }
                }
                else messages = value;
            }
        }
        public string[] Context
        {
            get
            {
                if (context.Count() == 0) return null;
                else return context;
            }
            set
            {
                context = value;
            }
        }
        public Dictionary<string, string> Parameters
        {
            get
            {
                return parameters;
            }
            set
            {
                parameters = value;
            }

        }


        public ResponseHandler(QueryResult input)
        {
            Messages = GetMessages(input);
            Context = GetContext(input);
            Parameters = GetParmeters(input);
        }

        /// <summary>
        /// Получаем массив отформатированных сообщений
        /// </summary>
        string[] GetMessages(QueryResult input)
        {
            // Для buf может быть правильнее стрингБилдер использовать?
            var messages = input.FulfillmentMessages;
            string[] result = new string[messages.Count];
            var buf = messages[0].Text.Text_.ToString();
            result[0] = buf.Substring(0, buf.Length - 3).Substring(3);
            if (messages.Count > 1)
            {
                for (int i = 1; i < messages.Count; i++)
                {
                    buf = messages[i].Text.Text_.ToString();
                    result[i] = buf.Substring(0, buf.Length - 3).Substring(3);
                }
            }
            return result;
        }
        /// <summary>
        /// Получаем массив отформатированных контекстов
        /// </summary>
        string[] GetContext(QueryResult input)
        {
            var contexts = input.OutputContexts;
            string[] result = new string[contexts.Count];
            for (int i = 0; i < contexts.Count; i++)
            {
                result[i] = contexts[i].ContextName.ContextId;
            }
            return result;
        }
        /// <summary>
        /// Получаем словарь отформатированных параметров
        /// </summary>
        Dictionary<string, string> GetParmeters(QueryResult input)
        {
            var parameters = input.Parameters.Fields;
            if (parameters.Count != 0)
            {
                Dictionary<string, string> result = new Dictionary<string, string>();
                foreach (var v in parameters)
                {
                    result.Add(v.Key, v.Value.StringValue);
                }
                return result;
            }
            else return null;
        }

    }
}
