using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using AiursoftBase.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Wiki.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class WikiUser : AiurUserBase
    {
        [InverseProperty(nameof(Comment.CommentUser))]
        public List<Comment> Comments { get; set; }
    }
    public class Article
    {
        [Key]
        public int ArticleId { get; set; }
        public string ArticleTitle { get; set; }
        public string ArticleContent { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;

        [InverseProperty(nameof(Comment.Article))]
        public List<Comment> Comments { get; set; }
    }
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public string CommentContent { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;

        public int ArticleId { get; set; }
        [ForeignKey(nameof(ArticleId))]
        public Article Article { get; set; }

        public string CommentUserId { get; set; }
        [ForeignKey(nameof(CommentUserId))]
        public WikiUser CommentUser { get; set; }
    }
}
