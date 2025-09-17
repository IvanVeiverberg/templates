using Microsoft.AspNetCore.Mvc;
using Templates.Api.DTOs;
using Templates.Api.Entities;
using Templates.Api.Services;

[ApiController]
[Route("api/[controller]")]
public class TemplatesController : ControllerBase
{
    private readonly ITemplateService _templateService;

    public TemplatesController(ITemplateService templateService)
    {
        _templateService = templateService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Template>> GetTemplate(int id)
    {
        var template = await _templateService.GetTemplateByIdAsync(id);
        return template == null ? NotFound("Template not found.") : Ok(template);
    }

    [HttpPost]
    public async Task<ActionResult<Template>> CreateTemplate([FromBody] TemplateDto dto)
    {
        var created = await _templateService.CreateTemplateAsync(dto);
        return CreatedAtAction(nameof(GetTemplate), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTemplate(int id, [FromBody] TemplateDto dto)
    {
        if (dto.Id.HasValue && dto.Id.Value != id) return BadRequest();

        var updated = await _templateService.UpdateTemplateAsync(id, dto);
        return updated ? NoContent() : NotFound("Template not found.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTemplate(int id)
    {
        var deleted = await _templateService.DeleteTemplateAsync(id);
        return deleted ? NoContent() : NotFound("Template not found.");
    }

    [HttpGet("{id}/compile/{userId}")]
    public async Task<IActionResult> CompileTemplate(int id, int userId)
    {
        try
        {
            var compiled = await _templateService.CompileTemplateAsync(id, userId);
            return compiled == null ? NotFound("Template or User not found.") : Ok(new { compiled });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet("{id}/compile/{userId}/html")]
    public async Task<IActionResult> CompileHtmlForUser(int id, int userId)
    {
        try
        {
            var html = await _templateService.CompileHtmlForUserAsync(id, userId);
            return html == null ? NotFound("Template or User not found.") : Content(html, "text/html");
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
