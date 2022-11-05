using System.ComponentModel.DataAnnotations.Schema;

namespace AdrexamAPI.Data
{
    public class Tokens
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime? Expired { get; set; }
    }
}