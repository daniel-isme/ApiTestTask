using ApiTestTask.ViewModels;
using ApiTestTask.Views;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ApiTestTask.Models
{
    public class DataRowModel
    {
        public double Diff { get; set; }
        public double Fact { get; set; }
        public double Plan { get; set; }
        public string DateValueDisplay { get; set; }


        public string DateValue { get; set; }
        public double Percent { get; set; }
        public string Comment { get; set; }
        public List<CommentModel> Comments { get; set; }
    }

    public class JsonObjectModel
    {
        public string Version { get; set; }
        public int StatusCode { get; set; }
        public DataModel Data { get; set; }
    }

    public class DataModel
    {
        public int ObjectId { get; set; }
        public string IndicatorId { get; set; }
        public string PeriodType { get; set; }
        public string PeriodValue { get; set; }
        public string DetailType { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public bool CacheUsed { get; set; }
        public string MinDate { get; set; }
        public string MaxDate { get; set; }
        public string LastDate { get; set; }
        public DataRowModel DataSeriesHeader { get; set; }
        public List<DataRowModel> DataSeries { get; set; }
    }
}
