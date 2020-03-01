using System.Collections.Generic;

namespace Book.Dto
{
    public class AuthorDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<BookAuthorDto> BookAuthors { get; set; }

        public AuthorDto() => BookAuthors = new List<BookAuthorDto>();
    }
}
