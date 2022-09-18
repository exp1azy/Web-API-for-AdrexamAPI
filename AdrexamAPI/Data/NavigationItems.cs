using System.ComponentModel.DataAnnotations;

namespace AdrexamAPI.Data
{
    public class NavigationItems
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Route1 { get; set; }
        public string Route2 { get; set; }
        public string Route3 { get; set; }
        public string PreviousRoute2 { get; set; }
        public string PreviousTopic { get; set; }
        public int ParentId { get; set; }
        public int AreaNumber { get; set; }
        public bool IsDeleted { get; set; }
        public int SortingOrder { get; set; }
    }
}