using ApiTestTask.Models;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTestTask.ViewModels
{
    public class CommentsViewModel : BaseViewModel
    {
        public CommentsViewModel()
        {
        }

        List<CommentModel> comments;
        public List<CommentModel> Comments
        {
            get => comments;
            set => SetProperty(ref comments, value);
        }

        public bool IsRead { get; set; }
    }
}
