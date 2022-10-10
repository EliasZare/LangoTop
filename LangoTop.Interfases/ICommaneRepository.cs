using System.Collections.Generic;
using _0_Framework.Domain;
using LangoTop.Application.Contract.Comment;
using LangoTop.Domain;

namespace LangoTop.Interfaces
{
    public interface ICommentRepository : IRepository<long, Comment>
    {
        List<CommentViewModel> Search(CommentSearchModel searchModel);
    }
}
