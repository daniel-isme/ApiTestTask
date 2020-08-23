using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ApiTestTask.Models
{
    public class DataDisplayModel : INotifyPropertyChanged
    {
        private string dateValueDisplay;
        private double plan;
        private double fact;
        private double diff;

        public double Diff
        {
            get { return diff; }
            set 
            { 
                diff = value;
                OnPropertyChanged(nameof(Diff));
            }
        }


        public double Fact
        {
            get { return fact; }
            set 
            { 
                fact = value;
                OnPropertyChanged(nameof(Fact));
            }
        }


        public double Plan
        {
            get { return plan; }
            set
            {
                plan = value;
                OnPropertyChanged(nameof(Plan));
            }
        }


        public string DateValueDisplay
        {
            get { return dateValueDisplay; }
            set
            {
                dateValueDisplay = value;
                OnPropertyChanged(nameof(DateValueDisplay));
            }
        }

        public string DateValue { get; set; }
        public double Percent { get; set; }
        public string Comment { get; set; }
        public List<Comment> Comments { get; set; }


        #region INotifyPropertyChanged Members  

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

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
        public DataDisplayModel DataSeriesHeader { get; set; }
        public List<DataDisplayModel> DataSeries { get; set; }
    }
}
