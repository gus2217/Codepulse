using System.ComponentModel.DataAnnotations;

namespace Codepulse.API.DTOs.Auth
{
    public class RoleNameDto
    {
        [Required]
        public string Name { get; set; }
    }
}
