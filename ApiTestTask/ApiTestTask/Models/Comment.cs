using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ApiTestTask.Models
{
    public class Comment : INotifyPropertyChanged
    {
        private int id;
        private string name;
        private string commentText;

        public string CommentText
        {
            get { return commentText; }
            set 
            { 
                commentText = value;
                OnPropertyChanged(nameof(CommentText));
            }
        }


        public string Name
        {
            get { return name; }
            set 
            { 
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }


        public int Id
        {
            get { return id; }
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
