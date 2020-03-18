using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Book.Data.Entities
{
	public class Book
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int BookId { get; set; }
		[Required]
		public string BookName { get; set; }
		[Required]
		[Column(TypeName = "decimal(18,2)")]
		public decimal Price { get; set; }
		[Required]
		public string Isbn { get; set; }
		public int CategoryId { get; set; }
		public virtual Category Category { get; set; }
		public int PublisherId { get; set; }
		public virtual Publisher Publisher { get; set; }
		public virtual ICollection<BookAuthor> BookAuthors { get; set; }

		public Book()
		{
			BookAuthors = new HashSet<BookAuthor>();
		}
	}
}
