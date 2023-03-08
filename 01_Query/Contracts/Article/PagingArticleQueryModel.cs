using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Query.Contracts.Article
{
    public class PagingArticleQueryModel
    {
        public List<ArticleQueryModel> Articles { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }
}
