namespace Templates.Api.DTOs
{
    using System.ComponentModel.DataAnnotations;

    public class TemplateDto
    {
        public int? Id { get; set; } 

        [Required]
        public string Value { get; set; } = string.Empty;
    }
}
