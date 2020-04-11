using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Book.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Username { get; set; }
        public IEnumerable<RoleDto> Roles { get; set; }
    }
}