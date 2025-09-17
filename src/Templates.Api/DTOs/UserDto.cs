using System.ComponentModel.DataAnnotations;

namespace Templates.Api.DTOs
{
    public class UserDto
    {
        public int? Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(200)]
        public string Street { get; set; } = string.Empty;

        [MaxLength(100)]
        public string City { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Country { get; set; } = string.Empty;

        [MaxLength(20)]
        public string HouseNumber { get; set; } = string.Empty;

        public string CustomStyle { get; set; } = string.Empty;
    }
}
