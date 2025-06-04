using TaskifyApi.Application.Mappers;
using TaskifyApi.Domain.Interface;

namespace WebApiTaskify.Controllers;

using Dtos.OrgLimit;
using Microsoft.AspNetCore.Mvc;

[Route("api/v{version:apiVersion}/orglimit")]
[ApiController]
public class OrgLimitController : ControllerBase
{
    private readonly IOrgLimitRepository _orgLimitRepository;
    public OrgLimitController(IOrgLimitRepository orgLimitRepository)
    {
        _orgLimitRepository = orgLimitRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page, [FromQuery] int pageSize, CancellationToken token)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        var orgLimits = await _orgLimitRepository.GetAllAsync(page, pageSize, token);

        var result = orgLimits.Select(x => x.ToOrgLimitDto());
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken token)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var orgLimit = await _orgLimitRepository.GetByIdAsync(id, token);
        
        if(orgLimit is null)
            return NotFound("Organization count not found");
        
        return Ok(orgLimit.ToOrgLimitDto());
    }

    [HttpPost("{OrgId}")]
    public async Task<IActionResult> Create([FromBody] CreateOrgLimitDto createOrgLimitDto, string orgId, CancellationToken token)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        var orgLimitModel = createOrgLimitDto.ToCreateFromOrgLimitDto(orgId);
        var orgLimit = await _orgLimitRepository.CreateAsync(orgLimitModel, token);
        
        if(orgLimit is null)
            return BadRequest("Organization count could not be created");
        return CreatedAtAction(nameof(GetById), new {id = orgLimit.Id}, orgLimit.ToOrgLimitDto());
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromBody] UpdateOrgLimitDto updateOrgLimitDto, Guid id, CancellationToken token)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        var orgLimit = updateOrgLimitDto.ToUpdateFromOrgLimitDto();
        var result = await _orgLimitRepository.UpdateAsync(id, orgLimit, token);
        
        if(result is null)
            return NotFound("Organization count not found");
        
        return Ok(result.ToOrgLimitDto());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken token)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var result = await _orgLimitRepository.DeleteAsync(id, token);
        if(result is null)
            return NotFound("Organization count not found");
        
        return NoContent();
    }
}
