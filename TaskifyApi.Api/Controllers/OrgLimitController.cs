using Microsoft.AspNetCore.Mvc;
using TaskifyApi.Application.Mappers;
using TaskifyApi.Domain.Dtos.OrgLimit;
using TaskifyApi.Domain.Interface;

namespace TaskifyApi.Api.Controllers;

[Route("api/v{version:apiVersion}/orglimit")]
[ApiController]
public class OrgLimitController(IOrgLimitRepository orgLimitRepository) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken token)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var orgLimits = await orgLimitRepository.GetAllAsync(token);                                 
        var result = orgLimits.Select(x => x.ToOrgLimitDto());
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken token)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var orgLimit = await orgLimitRepository.GetByIdAsync(id, token);
        
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
        var orgLimit = await orgLimitRepository.CreateAsync(orgLimitModel, token);
        
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
        var result = await orgLimitRepository.UpdateAsync(id, orgLimit, token);
        
        if(result is null)
            return NotFound("Organization count not found");
        
        return Ok(result.ToOrgLimitDto());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken token)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var result = await orgLimitRepository.DeleteAsync(id, token);
        if(result is null)
            return NotFound("Organization count not found");
        
        return NoContent();
    }
}