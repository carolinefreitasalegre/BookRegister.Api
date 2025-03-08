using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RegisterBoook.Api.Models
{
    public class Author
    {

        public Author()
        {
            
        }

        public Author(string name, ICollection<Book> books, DateTime register)
        {
            Name = name;
            Books = books;
            Register = register;
        }

        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<Book> Books { get; set; } = [];

        public DateTime Register { get; set; } = DateTime.Now;


    }
}
