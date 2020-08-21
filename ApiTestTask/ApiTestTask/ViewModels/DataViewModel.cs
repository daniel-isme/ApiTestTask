using ApiTestTask.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ApiTestTask.ViewModels
{
    class DataViewModel
    {
        private IList<Data> _DatasList;

        public DataViewModel()
        {
            _DatasList = new List<Data>
            {
                new Data { DateValueDisplay = "01", Diff = -5, Fact = 28, Plan = 33 },
                new Data { DateValueDisplay = "02", Diff = 0, Fact = 20, Plan = 20 },
                new Data { DateValueDisplay = "03", Diff = -10, Fact = 28, Plan = 38 }
            };
        }

        public IList<Data> Datas
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
