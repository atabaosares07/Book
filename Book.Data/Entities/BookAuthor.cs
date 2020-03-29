using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Book.Data.Entities
{
    public class BookAuthor
    {
		[Key]
		public int BookId { get; set; }
		public Book Book { get; set; }
		[Key]
		public int AuthorId { get; set; }
		public Author Author { get; set; }
	}
}
