using ApiTestTask.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ApiTestTask.ViewModels
{
    class DataViewModel
    {
        private IList<DataRowModel> _DatasList;

        public DataViewModel()
        {
            GetData();
        }

        public IList<DataRowModel> Datas
        {
            get { return _DatasList; }
            set { _DatasList = value; }
        }

        async void GetData()
        {
            var dataJson = await DownloadInformationJson();
            if (dataJson != null)
            {
                var dataObject = JsonConvert.DeserializeObject<JsonObjectModel>(dataJson);
                _DatasList = dataObject.Data.DataSeries;
            }
        }

        async Task<string> DownloadInformationJson()
        {

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
                    Xamarin.Forms.Page ourPage = App.Current.MainPage;
                    if (ourPage != null)
                    {
                        await ourPage.DisplayAlert("Server error", "Unable to receive data", "Cancel");
                    }
                }
            }

            if (!string.IsNullOrEmpty(json))
            {
                return json;
            }

            return null;

            //_DatasList = new List<DataRowModel>
            //{
            //    new DataRowModel { DateValueDisplay = "01", Plan = 18, Fact = 18, Diff = 0 },
            //    new DataRowModel { DateValueDisplay = "02", Plan = 33, Fact = 28, Diff = -5 },
            //    new DataRowModel { DateValueDisplay = "03", Plan = 36, Fact = 28, Diff = -8 }
            //};
        }
    }
}
