using Templates.Api.Entities;

namespace Templates.Api.Services
{
    public interface ITemplateService
    {
        Task<Template?> GetTemplateByIdAsync(int id);
        Task<Template> CreateTemplateAsync(Template template);
        Task<bool> UpdateTemplateAsync(Template template);
        Task<bool> DeleteTemplateAsync(int id);
    }
}