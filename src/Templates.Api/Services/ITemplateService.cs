using Templates.Api.DTOs;
using Templates.Api.Entities;

namespace Templates.Api.Services
{
    public interface ITemplateService
    {
        Task<Template?> GetTemplateByIdAsync(int id);
        Task<Template> CreateTemplateAsync(TemplateDto templateDto);
        Task<bool> UpdateTemplateAsync(int id, TemplateDto templateDto);
        Task<bool> DeleteTemplateAsync(int id);
        Task<string?> CompileTemplateAsync(int id, int userId);
        Task<string?> CompileHtmlForUserAsync(int id, int userId);

    }
}