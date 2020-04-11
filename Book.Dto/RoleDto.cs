using System.ComponentModel.DataAnnotations;

namespace Book.Dto
{
    public class RoleDto
    {
        public int RoleId { get; set; }
        [Required]
        public string RoleName { get; set; }
    }
}
