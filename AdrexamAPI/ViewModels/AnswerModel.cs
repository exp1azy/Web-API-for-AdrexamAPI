using AdrexamAPI.Data;
using AdrexamAPI.Extensions;

namespace AdrexamAPI.Models
{
    public class AnswerModel
    {
        public int Id { get; set; }
        public string AnswerText { get; set; }
        public string? AnswerImg { get; set; }
        public bool IsCorrect { get; set; }

        public static AnswerModel? Map(Answers answer) => answer == null ? null : new()
        {
            Id = answer.Id,
            AnswerText = answer.AnswerText,
            AnswerImg = answer.AnswerImg.GetRealUrl(),
            IsCorrect = answer.IsCorrect
        };
    }
}
