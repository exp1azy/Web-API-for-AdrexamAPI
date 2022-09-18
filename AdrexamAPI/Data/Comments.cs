using System.ComponentModel.DataAnnotations;

namespace AdrexamAPI.Data
{
    public class Comments
    {
        [Key]
        public int Id { get; set; }
        public string CommentText { get; set; }
        public int QuestionId { get; set; }
        public bool IsDeleted { get; set; }
    }
}