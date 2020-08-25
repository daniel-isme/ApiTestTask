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
                var commentVM = CommentsVM;
                var commentsPage = new CommentsPage();
                commentsPage.BindingContext = commentVM;
                await Application.Current.MainPage.Navigation.PushAsync(commentsPage);
            });
        }

        public Command CommentsCommand { get; set; }


        public CommentsViewModel CommentsVM { get; set; }
        public double Diff { get; set; }
        public double Fact { get; set; }
        public double Plan { get; set; }
        public string DateValueDisplay { get; set; }
        public Color Color { get; set; }
        public bool ShowCommentsButton { get; set; }

        public string GetCommentsImage
        {
            get { return CommentsVM.IsRead ? "message_white.png" : "message.png"; }
        }
    }
}
