using ApiTestTask.Models;
using ApiTestTask.Views;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ApiTestTask.ViewModels
{
    public class DataRowViewModel : BaseViewModel
    {
        public DataRowViewModel()
        {
            CommentsCommand = new Command<DataRowViewModel>(async (parameter) =>
            {
                var commentsPage = new CommentsPage();
                commentsPage.BindingContext = this;
                await Application.Current.MainPage.Navigation.PushAsync(commentsPage);
            });
        }

        public Command CommentsCommand { get; set; }

        public List<CommentModel> Comments { get; set; }
        public double Diff { get; set; }
        public double Fact { get; set; }
        public double Plan { get; set; }
        public string DateValueDisplay { get; set; }
        public Color Color { get; set; }
        public bool ShowCommentsButton { get; set; }

        public bool isRead = false;
        public bool IsRead
        {
            get => isRead; 
            set 
            { 
                isRead = value;
                OnPropertyChanged(nameof(GetCommentsImage));
            }
        }

        public string GetCommentsImage
        {
            get { return IsRead ? "message_white.png" : "message.png"; }
        }
    }
}
