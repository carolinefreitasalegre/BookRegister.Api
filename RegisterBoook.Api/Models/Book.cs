using System.ComponentModel.DataAnnotations;

namespace RegisterBoook.Api.Models
{
    public class Book
    {
        public Book(string title, string genere)
        {
            Title = title;
            Genere = genere;
            //Author = author;
        }

        public Book()
        {
        }

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Title { get; set; }
        [Required]
        public string Genere { get; set; }

        public DateTime Register { get; set; } = DateTime.Now;

        public Guid AuthorId { get; set; }
        public Author Author { get; set; } = null!;
    }



}
