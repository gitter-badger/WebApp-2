
using System.Collections.Generic;

namespace Wiki.Models.HomeViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Article> Articles { get; set; }
        public int CurrentArticleId { get; set; }
        public string Content { get; set; }
    }
}