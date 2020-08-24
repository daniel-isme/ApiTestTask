using ApiTestTask.Models;
using ApiTestTask.Views;
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
            CommentsCommand = new Command<DataRowModel>(async (parameter) =>
            {
                var commentVM = new CommentViewModel(parameter.Comments);
                var commentsPage = new CommentsPage();
                commentsPage.BindingContext = commentVM;
                await Application.Current.MainPage.Navigation.PushAsync(commentsPage);
            });
        }

        public Command CommentsCommand { get; set; }


        public double Diff { get; set; }
        public double Fact { get; set; }
        public double Plan { get; set; }
        public string DateValueDisplay { get; set; }
        public Color Color { get; set; }

    }
}
