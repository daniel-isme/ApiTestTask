using ApiTestTask.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;

namespace ApiTestTask.ViewModels
{
    public class DataViewModel : INotifyPropertyChanged
    {
        private IList<DataRowModel> datas;
        public static ErrorModel Error;

        public DataViewModel()
        {
            GetData();
        }

        public IList<DataRowModel> Datas
        {
            get { return datas; }
            set 
            {
                datas = value;
                OnPropertyChanged(nameof(Datas));
            }
        }

        public class ErrorModel
        {
            public string Title { get; set; }
            public string Message { get; set; }
            public string Cancel { get; set; }
        }

        async void GetData()
        {
            var dataJson = await DownloadInformationJson();
            if (dataJson != null)
            {
                var dataObject = JsonConvert.DeserializeObject<JsonObjectModel>(dataJson);
                Datas = dataObject.Data.DataSeries;
            }

            Datas = new List<DataRowModel>
            {
                new DataRowModel { DateValueDisplay = "01", Plan = 18, Fact = 18, Diff = 0 },
                new DataRowModel { DateValueDisplay = "02", Plan = 33, Fact = 28, Diff = -5 },
                new DataRowModel { DateValueDisplay = "03", Plan = 36, Fact = 28, Diff = -8 }
            };
        }

        async Task<string> DownloadInformationJson()
        {
            if (!HasInternetConnection())
            {
                return null;
            }            

            Uri url = new Uri("https://uc51dcd2fde5d5d87d793279c0af.dl.dropboxusercontent.com/cd/0/get/A93zuT7EnXcCC6MvhEy0F1SCUnt84zmse6jkBnm-Ex3UJoh0dVkiiT5NY9wj4DBtyXCxWFoda4ScMtNGqbgGI48hesP2b0u3G-CcRwmRlguNfdIeNczjZNP6UEqx-S_UvkM/file");
            string json = string.Empty;

            using (WebClient wc = new WebClient())
            {
                try
                {
                    json = await wc.DownloadStringTaskAsync(url);
                }
                catch
                {
                    Error = new ErrorModel
                    {
                        Title = "Server error",
                        Message = "Unable to receive data",
                        Cancel = "Ok"
                    };
                    DisplayError();
                }
            }

            if (!string.IsNullOrEmpty(json))
            {
                return json;
            }

            return null;
        }

        private bool HasInternetConnection()
        {
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                return true;
            }
            
            Error = new ErrorModel
            {
                Title = "Connection error",
                Message = "Check your internet connection and try again",
                Cancel = "Ok"
            };

            return false;
        }

        void DisplayError()
        {
            var mainPage = App.Current.MainPage;
            if (mainPage != null)
            {
                mainPage.DisplayAlert(Error.Title, Error.Message, Error.Cancel);
            }
        }

        #region INotifyPropertyChanged Members  

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
