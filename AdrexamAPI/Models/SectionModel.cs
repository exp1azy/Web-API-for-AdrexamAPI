using AdrexamAPI.Data;

namespace AdrexamAPI.Models
{
    public class SectionModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ParentId { get; set; }

        public static SectionModel? Map(NavigationItems navItem) => navItem == null ? null : new()
        {
            Id = navItem.Id,
            Title = navItem.Title,
            ParentId = navItem.ParentId ?? 0
        };
    }
}