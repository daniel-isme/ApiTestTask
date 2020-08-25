using ApiTestTask.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTestTask.ViewModels
{
    public class CommentViewModel : BaseViewModel
    {
        public CommentViewModel(List<CommentModel> _comments, bool isRead)
        {
            Comments = _comments;
            IsRead = isRead;
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
