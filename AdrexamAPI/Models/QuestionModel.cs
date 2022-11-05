using AdrexamAPI.Data;
using AdrexamAPI.Extensions;

namespace AdrexamAPI.Models
{
    public class QuestionModel
    {
        public int Id { get; set; }
        public string? QuestionText { get; set; }
        public int NavigationItemId { get; set; }
        public string? Image { get; set; }
        public bool IsGrid { get; set; }
        public List<string> Comments { get; set; }
        public List<AnswerModel?> Answers { get; set; }

        static public QuestionModel? Map(Questions question) => question == null ? null : new()
        {
            Id = question.Id,
            QuestionText = question.QuestionText,
            NavigationItemId = question.NavigationItemId,
            Image = question.Image?.GetRealUrl(),
            IsGrid = question.IsGrid == true,
            Comments = question.Comments.Where(q => !q.IsDeleted).Select(c => c.CommentText).ToList(),
            Answers = question.Answers.Where(q => !q.IsDeleted).Select(a => AnswerModel.Map(a)).ToList()
        };
    }
}