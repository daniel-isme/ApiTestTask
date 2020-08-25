using ApiTestTask.Models;
using ApiTestTask.Views;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
    public class DataViewModel : BaseViewModel
    {

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

        private ObservableCollection<DataRowViewModel> dataSeries;
        public ObservableCollection<DataRowViewModel> DataSeries
        {
            get => dataSeries;
            set => SetProperty(ref dataSeries, value);
        }

        private DataRowViewModel dataSeriesHeader;
        public DataRowViewModel DataSeriesHeader
        {
            get => dataSeriesHeader;
            set => SetProperty(ref dataSeriesHeader, value);
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

                var dataSeriesOC = new ObservableCollection<DataRowViewModel>();
                foreach (var dataRow in allDataObject.Data.DataSeries)
                {
                    dataSeriesOC.Add(new DataRowViewModel { 
                        DateValueDisplay = dataRow.DateValueDisplay,
                        Diff = dataRow.Diff,
                        Fact = dataRow.Fact,
                        Plan = dataRow.Plan,
                        Comments = dataRow.Comments,
                        ShowCommentsButton = dataRow.Comments?.Any() ?? false,
                        Color = DefineColor(dataRow.Diff),
                    });
                }

                DataSeries = dataSeriesOC;

                var header = allDataObject.Data.DataSeriesHeader;
                DataSeriesHeader = new DataRowViewModel
                {
                    Diff = header.Diff,
                    Fact = header.Fact,
                    Plan = header.Plan,
                    Color = DefineColor(header.Diff),
                };
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
                    //json = await wc.DownloadStringTaskAsync(new Uri(url));
                    json = "{'Version':'1','StatusCode':200,'Data':{'ObjectId':2292,'IndicatorId':'Mine_Sinking','PeriodType':'month','PeriodValue':'2020-08-01T00:00:00','DetailType':'day','Name':'Проходка','Unit':'м','CacheUsed':true,'MinDate':'2018-01-01T00:00:00','MaxDate':'2020-08-31T00:00:00','LastDate':null,'DataSeriesHeader':{'DateValue':'0001-01-01T00:00:00','DateValueDisplay':null,'Plan':654,'Fact':612,'Diff':-42,'Percent':93.57798,'Comment':null,'Comments':null},'DataSeries':[{'DateValue':'2020-08-01T00:00:00','DateValueDisplay':'01','Plan':18,'Fact':18,'Diff':0,'Percent':100,'Comment':null,'Comments':[{'Id':9628,'Name':'Вент.штрек 6-1-23, камера','Comment':'Зачистка выработки.'},{'Id':9862,'Name':'Конв.уклон 3/6, камера','Comment':'Зачистка выработки.'}]},{'DateValue':'2020-08-02T00:00:00','DateValueDisplay':'02','Plan':18,'Fact':18,'Diff':0,'Percent':100,'Comment':null,'Comments':[{'Id':9626,'Name':'Газодр.штрек 6-1-23, камера','Comment':'Зачистка выработки.'},{'Id':9862,'Name':'Конв.уклон 3/6, камера','Comment':'Зачистка'}]},{'DateValue':'2020-08-03T00:00:00','DateValueDisplay':'03','Plan':33,'Fact':28,'Diff':-5,'Percent':84.84849,'Comment':null,'Comments':null},{'DateValue':'2020-08-04T00:00:00','DateValueDisplay':'04','Plan':36,'Fact':28,'Diff':-8,'Percent':77.77778,'Comment':null,'Comments':null},{'DateValue':'2020-08-05T00:00:00','DateValueDisplay':'05','Plan':34,'Fact':28,'Diff':-6,'Percent':82.35294,'Comment':null,'Comments':null},{'DateValue':'2020-08-06T00:00:00','DateValueDisplay':'06','Plan':34,'Fact':29,'Diff':-5,'Percent':85.29411,'Comment':null,'Comments':null},{'DateValue':'2020-08-07T00:00:00','DateValueDisplay':'07','Plan':34,'Fact':27,'Diff':-7,'Percent':79.411766,'Comment':null,'Comments':[{'Id':9626,'Name':'Газодр.штрек 6-1-23, камера','Comment':'Взятие дег.ниши. Дост-ка бур.станка.'}]},{'DateValue':'2020-08-08T00:00:00','DateValueDisplay':'08','Plan':33,'Fact':30,'Diff':-3,'Percent':90.909096,'Comment':null,'Comments':null},{'DateValue':'2020-08-09T00:00:00','DateValueDisplay':'09','Plan':31,'Fact':26,'Diff':-5,'Percent':83.870964,'Comment':null,'Comments':null},{'DateValue':'2020-08-10T00:00:00','DateValueDisplay':'10','Plan':32,'Fact':30,'Diff':-2,'Percent':93.75,'Comment':null,'Comments':[{'Id':9626,'Name':'Газодр.штрек 6-1-23, камера','Comment':'Зачистка'}]},{'DateValue':'2020-08-11T00:00:00','DateValueDisplay':'11','Plan':31,'Fact':30,'Diff':-1,'Percent':96.77419,'Comment':null,'Comments':[{'Id':9626,'Name':'Газодр.штрек 6-1-23, камера','Comment':'Зачистка выработки.'},{'Id':9756,'Name':'Люд.уклон (Вост.блок), сбойка, транс.уклон (Вост.блок)','Comment':'14:00-20:00 устранение нарушений ПБ(комиссия по приемке забоя).'}]},{'DateValue':'2020-08-12T00:00:00','DateValueDisplay':'12','Plan':31,'Fact':30,'Diff':-1,'Percent':96.77419,'Comment':null,'Comments':[{'Id':9626,'Name':'Газодр.штрек 6-1-23, камера','Comment':'Зачистка выработки.'}]},{'DateValue':'2020-08-13T00:00:00','DateValueDisplay':'13','Plan':30,'Fact':31,'Diff':1,'Percent':103.33333,'Comment':null,'Comments':null},{'DateValue':'2020-08-14T00:00:00','DateValueDisplay':'14','Plan':33,'Fact':32,'Diff':-1,'Percent':96.969696,'Comment':null,'Comments':[{'Id':9628,'Name':'Вент.штрек 6-1-23, камера','Comment':'12:00-18:00 Устранение нарушений ПБ. НТО.'}]},{'DateValue':'2020-08-15T00:00:00','DateValueDisplay':'15','Plan':30,'Fact':32,'Diff':2,'Percent':106.66667,'Comment':null,'Comments':null},{'DateValue':'2020-08-16T00:00:00','DateValueDisplay':'16','Plan':32,'Fact':32,'Diff':0,'Percent':100,'Comment':null,'Comments':[{'Id':8706,'Name':'Конв.уклон 4/6, камера','Comment':'Зачистка выработки.14:00-22:00устранение нарушений ПБ(НТО).'}]},{'DateValue':'2020-08-17T00:00:00','DateValueDisplay':'17','Plan':32,'Fact':32,'Diff':0,'Percent':100,'Comment':null,'Comments':[{'Id':8706,'Name':'Конв.уклон 4/6, камера','Comment':'Зачистка выработки.'}]},{'DateValue':'2020-08-18T00:00:00','DateValueDisplay':'18','Plan':31,'Fact':31,'Diff':0,'Percent':100,'Comment':null,'Comments':[{'Id':8706,'Name':'Конв.уклон 4/6, камера','Comment':'Зачистка выработки.'}]},{'DateValue':'2020-08-19T00:00:00','DateValueDisplay':'19','Plan':31,'Fact':31,'Diff':0,'Percent':100,'Comment':null,'Comments':[{'Id':9756,'Name':'Люд.уклон (Вост.блок), сбойка, транс.уклон (Вост.блок)','Comment':'Зачистка выработки.'},{'Id':9862,'Name':'Конв.уклон 3/6, камера','Comment':'11:00-20:00 Устранение замечаний приемной комиссии.'}]},{'DateValue':'2020-08-20T00:00:00','DateValueDisplay':'20','Plan':35,'Fact':34,'Diff':-1,'Percent':97.14286,'Comment':null,'Comments':null},{'DateValue':'2020-08-21T00:00:00','DateValueDisplay':'21','Plan':35,'Fact':35,'Diff':0,'Percent':100,'Comment':null,'Comments':[{'Id':9626,'Name':'Газодр.штрек 6-1-23, камера','Comment':'12:00-18:00 устранение нарушений ПБ(НТО).'},{'Id':9862,'Name':'Конв.уклон 3/6, камера','Comment':'12:00-18:00 устранение нарушений ПБ(НТО).'}]}]}}";
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

        Color DefineColor(double diff)
        {
            if (diff < 0) return ColorConverters.FromHex("#f98883");
            else if (diff > 0) return Color.Green;
            else return ColorConverters.FromHex("#b2b5bc");
        }
    }
}
