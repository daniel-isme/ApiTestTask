using ApiTestTask.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTestTask.ViewModels
{
    public class CommentViewModel : BaseViewModel
    {
        public CommentViewModel(List<CommentModel> _comments)
        {
            Comments = _comments;
        }

        List<CommentModel> comments;

        public List<CommentModel> Comments
        {
            get { return comments; }
            set
            {
                comments = value;
                OnPropertyChanged(nameof(Comments));
            }
        }
    }
}
