using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ApiTestTask.Models
{
    public class Data : INotifyPropertyChanged
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

        #region INotifyPropertyChanged Members  

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}
