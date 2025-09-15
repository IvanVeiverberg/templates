using System.ComponentModel.DataAnnotations;

namespace Templates.Api.Entities
{
    public class Template
    {
        public int Id { get; set; }

        [Required]
        public string Value { get; set; } = string.Empty;
    }
}
