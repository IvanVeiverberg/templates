using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Templates.Api.Data;
using Templates.Api.DTOs;
using Templates.Api.Services;
using Templates.Api.Utils;
using Entities = Templates.Api.Entities;

public class TemplateService : ITemplateService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public TemplateService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Entities.Template?> GetTemplateByIdAsync(int id) => await _context.Templates.FindAsync(id);

    public async Task<Entities.Template> CreateTemplateAsync(TemplateDto templateDto)
    {
        var template = _mapper.Map<Entities.Template>(templateDto);
        _context.Templates.Add(template);
        await _context.SaveChangesAsync();
        return template;
    }

    public async Task<bool> UpdateTemplateAsync(int id, TemplateDto templateDto)
    {
        var template = await _context.Templates.FindAsync(id);
        if (template == null) return false;

        _mapper.Map(templateDto, template);
        _context.Entry(template).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteTemplateAsync(int id)
    {
        var template = await _context.Templates.FindAsync(id);
        if (template == null) return false;

        _context.Templates.Remove(template);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<string?> CompileTemplateAsync(int templateId, int userId)
    {
        var template = await _context.Templates.FindAsync(templateId);
        var user = await _context.Users.FindAsync(userId);
        if (template == null || user == null) return null;

        var userDto = _mapper.Map<UserDto>(user);

        var compiled = TemplateRenderer.RenderTemplate(template.Value, new { user = userDto });
        return compiled;
    }

    public async Task<string?> CompileHtmlForUserAsync(int templateId, int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        var compiled = await CompileTemplateAsync(templateId, userId);
        if (user == null || compiled == null) return null;

        var style = string.IsNullOrWhiteSpace(user.CustomStyle)
            ? Defaults.DefaultStyle
            : user.CustomStyle;

        var html = TemplateRenderer.RenderTemplate(Defaults.HtmlTemplate, new { style = style, content = compiled });
        return html;
    }
}