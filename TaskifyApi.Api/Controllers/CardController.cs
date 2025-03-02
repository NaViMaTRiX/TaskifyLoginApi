using Microsoft.AspNetCore.Mvc;
using TaskifyApi.Application.Mappers;
using TaskifyApi.Domain.Dtos.Card;
using TaskifyApi.Domain.Interface;
using WebApiTaskify.Dtos.Card;

namespace TaskifyApi.Api.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/card")]
public class CardController(ICardRepository cardRepository, IListRepository listRepository)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken token)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var cards = await cardRepository.GetAllAsync(token);
        var result = cards.Select(x => x.ToCardDto());
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken token)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var card = await cardRepository.GetByIdAsync(id, token);
        if (card is null)
            return NotFound("Card not found");
        
        return Ok(card.ToCardDto());
    }

    [HttpPost("{listId:guid}")]
    public async Task<IActionResult> Create([FromBody] CreateCardDto createCardDto, Guid listId, CancellationToken token)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        if(!await listRepository.ExistAsync(listId, token))
            return BadRequest("List not found");
        
        var cardModel = createCardDto.ToCardFromCreate(listId);
        var card = await cardRepository.CreateAsync(cardModel, token);
        
        if (card is null)
            return BadRequest("Failed to create card");
        
        return CreatedAtAction(nameof(GetById), new { id = card.Id }, card.ToCardDto());
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromBody] UpdateCardDto updateCardDto, Guid id, CancellationToken token)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var cardModel = await cardRepository.UpdateAsync(id, updateCardDto.ToCardFromUpdate(), token);
        
        if (cardModel is null)
            return NotFound("Card not found");
        
        return Ok(cardModel.ToCardDto());
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken token)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var card = await cardRepository.DeleteAsync(id, token);
        
        if (card is null)
            return NotFound("Card not found");
        
        return NoContent();
    }
}