using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TgBot_FW.Model;

namespace TgBot_FW.ViewModel
{
    public class SettingsVM : INotifyBase
    {
        Settings settings = new Settings();

        string tgToken;
        public string TgToken
        {
            get => tgToken;
            set
            {
                if (value != tgToken)
                {
                    tgToken = value;
                    settings.TelegrammToken = value;
                    OnPropertyChanged();
                }
            }
        }

        string dialogFlowProject;
        public string DialogFlowProject
        {
            get => dialogFlowProject;
            set
            {
                if (value != dialogFlowProject)
                {
                    dialogFlowProject = value;
                    settings.DialogFlowProject = value;
                    OnPropertyChanged();
                }
            }
        }

        string dialogFlowJson;
        public string DialogFlowJson
        {
            get => dialogFlowJson;
            set
            {
                if (value != dialogFlowJson)
                {
                    dialogFlowJson = value;
                    settings.JsoneFilePath = value;
                    OnPropertyChanged();
                }
            }
        }

        string openWeatherToken;
        public string OpenWeatherToken
        {
            get => openWeatherToken;
            set
            {
                if (value != openWeatherToken)
                {
                    openWeatherToken = value;
                    settings.OpenWeatherToken = value;
                    OnPropertyChanged();
                }
            }
        }

        string iDontKnowMessage;
        public string IDontKnowMessage
        {
            get => iDontKnowMessage;
            set
            {
                if (value != iDontKnowMessage)
                {
                    iDontKnowMessage = value;
                    settings.ErrorMessage = value;
                    OnPropertyChanged();
                }
            }
        }

        public SettingsVM()
        {}

        public SettingsVM(Settings sets)
        {
            TgToken = sets.TelegrammToken;
            DialogFlowJson = sets.JsoneFilePath;
            DialogFlowProject = sets.DialogFlowProject;
            OpenWeatherToken = sets.OpenWeatherToken;
            IDontKnowMessage = sets.ErrorMessage;
        }


        RelayCommand okBtn;
        public RelayCommand OkBtn => okBtn ?? (okBtn = new RelayCommand(
        o =>
        {
            string sets = JsonConvert.SerializeObject(settings);
            File.WriteAllText("Settings.json", sets);
        },
        o => settings.CheckAllFields()));

        RelayCommand addDialogFlowJson;
        public RelayCommand AddDialogFlowJson => addDialogFlowJson ?? (addDialogFlowJson = new RelayCommand(
        o =>
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON документ|*.json";
            if (openFileDialog.ShowDialog() == true)
            {
                DialogFlowJson = openFileDialog.FileName;
            }
        },
        o => true));


        
    }
}
