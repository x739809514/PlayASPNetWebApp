using api.Dtos.Comment;
using api.Model;

namespace api.Interfaces
{
    public interface ICommentRepository
    {
         Task<List<Comment>> GetAllAsync();
         Task<Comment?> GetByIdAsync(int id);
         Task<Comment> CreateAsync(Comment comment);
         Task<Comment?> UpdateAsync(int id, Comment CommentModel);
         Task<Comment?> DeleteAsync(int id);
    }
}