using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgBot_FW.Model
{
    class TgCommandsHandler
    {
        string command;
        public TgCommandsHandler(string command)
        {
            this.command = command;
        }

        /// <summary>
        /// Возвращает ответ на комманду ввиде строки
        /// </summary>
        public string GetResponse()
        {
            switch (this.command)
            {
                case "/start":
                    return "Привет!!!";
                default:
                    return "Комманда не найдена :(";
            }
        }

    }
}
