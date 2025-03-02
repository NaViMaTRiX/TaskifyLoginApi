using TaskifyApi.Application.Mappers;
using TaskifyApi.Domain.Interface;

namespace WebApiTaskify.Controllers;

using Dtos.OrgSubscription;
using Microsoft.AspNetCore.Mvc;

[Route("api/v{version:apiVersion}/orgSubscription")]
[ApiController]
public class OrgSubscriptionController : ControllerBase
{
    private readonly IOrgSubscriptionRepository _orgSubscriptionRepository;

    public OrgSubscriptionController(IOrgSubscriptionRepository orgSubscriptionRepository)
    {
        _orgSubscriptionRepository = orgSubscriptionRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken token)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var orgSubscriptions = await _orgSubscriptionRepository.GetAllAsync(token);
        var result = orgSubscriptions.Select(x => x.ToOrgSubscriptionDto());
        return Ok(orgSubscriptions);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken token)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var orgSubscription = await _orgSubscriptionRepository.GetByIdAsync(id, token);
        if (orgSubscription is null)
            return NotFound("OrgSubscription not found");
        
        return Ok(orgSubscription.ToOrgSubscriptionDto());
    }

    [HttpPost("{OrgId}")]
    public async Task<IActionResult> Create([FromBody] CreateOrgSubscriptionDto createOrgSubscriptionDto, string orgId, CancellationToken token)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var orgSubscription = createOrgSubscriptionDto.ToOrgSubscriptionDtoFromCreate(orgId);
        var result = await _orgSubscriptionRepository.CreateAsync(orgSubscription, token);
        
        if (result is null)
            return BadRequest("Failed to create orgSubscription");
        
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result.ToOrgSubscriptionDto());
    }
    
    //[HttpPut("{id:guid}")]

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken token)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var orgSubscription = await _orgSubscriptionRepository.DeleteAsync(id, token);
        
        if (orgSubscription is null)
            return NotFound("OrgSubscription not found");

        return NoContent();
    }
}