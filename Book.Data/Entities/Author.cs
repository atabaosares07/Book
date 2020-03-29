using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Book.Data.Entities
{
	public class Author
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int AuthorId { get; set; }
		[Required]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }
		public ICollection<BookAuthor> BookAuthors { get; set; }

		public Author()
		{
			BookAuthors = new HashSet<BookAuthor>();
		}
	}
}
