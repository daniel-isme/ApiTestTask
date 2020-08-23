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
        private IList<DataDisplayModel> _DatasList;

        public DataViewModel()
        {
            //var url = "https://uc51dcd2fde5d5d87d793279c0af.dl.dropboxusercontent.com/cd/0/get/A93zuT7EnXcCC6MvhEy0F1SCUnt84zmse6jkBnm-Ex3UJoh0dVkiiT5NY9wj4DBtyXCxWFoda4ScMtNGqbgGI48hesP2b0u3G-CcRwmRlguNfdIeNczjZNP6UEqx-S_UvkM/file";
            //string json;
            //using (WebClient wc = new WebClient())
            //{ 
            //    json = wc.DownloadString(url); 
            //}

            //var jsonObject = JsonConvert.DeserializeObject<JsonObject>(json);
            //_DatasList = jsonObject.Data.DataSeries;

            _DatasList = new List<DataDisplayModel>
            {
                new DataDisplayModel { DateValueDisplay = "01", Plan = 18, Fact = 18, Diff = 0 },
                new DataDisplayModel { DateValueDisplay = "02", Plan = 33, Fact = 28, Diff = -5 },
                new DataDisplayModel { DateValueDisplay = "03", Plan = 36, Fact = 28, Diff = -8 }
            };
        }

        public IList<DataDisplayModel> Datas
        {
            get { return _DatasList; }
            set { _DatasList = value; }
        }
    }
}
