using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdrexamAPI.Data
{
    public class Questions
    {
        [Key]
        public int Id { get; set; }
        public string? QuestionText { get; set; }
        public string? Image { get; set; }
        public string? AltImage { get; set; }
        public bool? IsGrid { get; set; }
        public int NavigationItemId { get; set; }
        public bool? IsDeleted { get; set; }
        public int? SortingOrder { get; set; }

        [ForeignKey("QuestionId")]
        virtual public ICollection<Answers> Answers { get; set; }

        [ForeignKey("QuestionId")]
        virtual public ICollection<Comments> Comments { get; set; }

        virtual public NavigationItems NavigationItem { get; set; }
    }
}