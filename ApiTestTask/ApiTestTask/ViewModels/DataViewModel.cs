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
using Xamarin.Forms;

namespace ApiTestTask.ViewModels
{
    public class DataViewModel : INotifyPropertyChanged
    {
        private IList<DataRowModel> dataSeries;
        private DataRowModel dataSeriesHeader;

        public static ErrorModel Error;
        public ICommand RefreshCommand { get; }

        public DataViewModel()
        {
            RefreshCommand = new Command(ExecuteRefreshCommand);
            GetData();
        }

        bool isRefreshing;
        public bool IsRefreshing
        {
            get => isRefreshing;
            set
            {
                isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        void ExecuteRefreshCommand()
        {
            GetData();

            // Stop refreshing
            IsRefreshing = false;
        }

        public IList<DataRowModel> DataSeries
        {
            get { return dataSeries; }
            set 
            {
                dataSeries = value;
                OnPropertyChanged(nameof(DataSeries));
            }
        }

        public DataRowModel DataSeriesHeader
        {
            get { return dataSeriesHeader; }
            set 
            {
                dataSeriesHeader = value;
                OnPropertyChanged(nameof(DataSeriesHeader));
            }
        }

        public class ErrorModel
        {
            public string Title { get; set; }
            public string Message { get; set; }
            public string Cancel { get; set; }
        }

        private void GetData()
        {
            var allDataJson = Task.Run(async () => await DownloadInformationJsonAsync()).Result;
            if (allDataJson != null)
            {
                var allDataObject = JsonConvert.DeserializeObject<JsonObjectModel>(allDataJson);
                DataSeries = allDataObject.Data.DataSeries;
                DataSeriesHeader = allDataObject.Data.DataSeriesHeader;
                foreach (var dataRow in DataSeries)
                {
                    if (dataRow.Diff < 0) dataRow.Color = ColorConverters.FromHex("#f98883");
                    else if (dataRow.Diff > 0) dataRow.Color = Color.Green;
                    else dataRow.Color = ColorConverters.FromHex("#b2b5bc");
                }

                if (DataSeriesHeader.Diff < 0) DataSeriesHeader.Color = ColorConverters.FromHex("#f98883");
                else if (DataSeriesHeader.Diff > 0) DataSeriesHeader.Color = Color.Green;
                else DataSeriesHeader.Color = ColorConverters.FromHex("#b2b5bc");
            }
            else
            {
                DisplayError();
            }
        }

        async Task<string> DownloadInformationJsonAsync()
        {
            if (!HasInternetConnection())
            {
                return null;
            }            

            // ошибка 404
            //string url = "https://uc51dcd2fde5d5d87d793279c0af.dl.dropboxusercontent.com/cd/0/get/A93zuT7EnXcCC6MvhEy0F1SCUnt84zmse6jkBnm-Ex3UJoh0dVkiiT5NY9wj4DBtyXCxWFoda4ScMtNGqbgGI48hesP2b0u3G-CcRwmRlguNfdIeNczjZNP6UEqx-S_UvkM/file";

            // здесь все хорошо
            string url = "https://uc2e830cb90ae8f8a417e8b0a5f3.dl.dropboxusercontent.com/cd/0/get/A-ARvkcAScQYq8t3v_n-QXk5YipVKH053C-y8xtwhIaJrf_GB6itLfmXDeRjw0SlLYpK7UJW7ylzIBb53J6pEDrb1V_vUdseKu8TGeUf9jr_RZCp5wexAZxlnzVndttAYec/file#";
            
            // здесь нет данных
            //string url = "https://uce04a6c7e9e2a30135b2bb87acc.dl.dropboxusercontent.com/cd/0/get/A-BZ1tVh9mlMiHy4s_6ZDEG2WNHKwA19ABGVVV-NaPICPuaEUPHJk5HBK6mRsMZMR7CdegrzH9IUasFWZg6Cfr1OP0Q_bikPZ9yJWq5ckTl6FuxHVcoKn43E13vDXKqnBRg/file?dl=1";
            
            string json = string.Empty;
                        
            using (WebClient wc = new WebClient())
            {
                try
                {
                    json = await wc.DownloadStringTaskAsync(new Uri(url));
                }
                catch
                {
                    Error = new ErrorModel
                    {
                        Title = "Server error",
                        Message = "Unable to receive data",
                        Cancel = "Ok"
                    };
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
            if (mainPage != null && Error != null)
            {
                mainPage.DisplayAlert(Error.Title, Error.Message, Error.Cancel);
                Error = null;
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
