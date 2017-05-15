﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRI_NaszSamochód.Models
{
    public class PostViewModel
    {
        public string Text { get; set; }
        public List<CommentViewModel> Comments { get; set; }
        public List<LikeModel> Likes { get; set; }

        public PostViewModel(PostModel post)
        {
            Text = post.Text;
            foreach(var i in post.Comments)
            {
                Comments.Add(new CommentViewModel(i));
            }
            Likes = post.Likes.ToList();
        }
    }

    public class CommentViewModel
    {
        public string Text { get; set; }
        public string Author { get; set; }
        public DateTime Added { get; set; }

        public CommentViewModel(CommentModel comment)
        {
            this.Text = comment.Text;
            this.Author = comment.Creator.Name;
            this.Added = comment.AddedTime;
        }
    }
}