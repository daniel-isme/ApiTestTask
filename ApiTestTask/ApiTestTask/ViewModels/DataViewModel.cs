using ApiTestTask.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows.Input;

namespace ApiTestTask.ViewModels
{
    class DataViewModel
    {
        private IList<DataRow> _DatasList;

        public DataViewModel()
        {
            var url = "https://uc51dcd2fde5d5d87d793279c0af.dl.dropboxusercontent.com/cd/0/get/A93zuT7EnXcCC6MvhEy0F1SCUnt84zmse6jkBnm-Ex3UJoh0dVkiiT5NY9wj4DBtyXCxWFoda4ScMtNGqbgGI48hesP2b0u3G-CcRwmRlguNfdIeNczjZNP6UEqx-S_UvkM/file";
            string json;
            using (WebClient wc = new WebClient())
            { 
                json = wc.DownloadString(url); 
            }

            var jsonObject = JsonConvert.DeserializeObject<JsonObject>(json);
            _DatasList = jsonObject.Data.DataSeries;
        }

        public IList<DataRow> Datas
        {
            get { return _DatasList; }
            set { _DatasList = value; }
        }

        private ICommand mUpdater;
        public ICommand UpdateCommand
        {
            get
            {
                if (mUpdater == null)
                    mUpdater = new Updater();
                return mUpdater;
            }
            set
            {
                mUpdater = value;
            }
        }

        private class Updater : ICommand
        {
            #region ICommand Members  

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {

            }

            #endregion
        }
    }
}
