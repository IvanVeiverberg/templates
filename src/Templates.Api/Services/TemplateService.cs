using Microsoft.EntityFrameworkCore;
using Templates.Api.Data;
using Templates.Api.Entities;

namespace Templates.Api.Services
{
    public class TemplateService : ITemplateService
    {
        private readonly AppDbContext _context;

        public TemplateService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Template?> GetTemplateByIdAsync(int id)
        {
            return await _context.Templates.FindAsync(id);
        }

        public async Task<Template> CreateTemplateAsync(Template template)
        {
            _context.Templates.Add(template);
            await _context.SaveChangesAsync();
            return template;
        }

        public async Task<bool> UpdateTemplateAsync(Template template)
        {
            if (!_context.Templates.Any(u => u.Id == template.Id)) return false;

            _context.Entry(template).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Templates.Any(u => u.Id == template.Id))
                    return false;

                throw;
            }
        }

        public async Task<bool> DeleteTemplateAsync(int id)
        {
            var template = await _context.Templates.FindAsync(id);
            if (template == null) return false;

            _context.Templates.Remove(template);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
