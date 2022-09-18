using System.ComponentModel.DataAnnotations;

namespace AdrexamAPI.Data
{
    public class Answers
    {
        [Key]
        public int Id { get; set; }
        public string AnswerText { get; set; }
        public string AnswerImg { get; set; }
        public string AltImage { get; set; }
        public int QuestionId { get; set; }
        public bool IsCorrect { get; set; }
        public bool IsDeleted { get; set; }
        public int SortingOrder { get; set; }
    }
}