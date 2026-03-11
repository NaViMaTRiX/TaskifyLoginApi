using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TaskifyApi.Application.Mappers;
using TaskifyApi.Domain.Dtos.Card;
using TaskifyApi.Domain.Interface;

namespace TaskifyApi.Api.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/card")]
public class CardController(ICardRepository cardRepository, IValidator<CreateCardDto> validator)
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
    
    [HttpGet("board/{boardId:guid}")]
    public async Task<IActionResult> GetAllByBoardAsync(Guid boardId, CancellationToken token)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var cards = await cardRepository.GetAllByBoardAsync(boardId, token);
        var result = cards.Select(x => x.ToCardDto());
        return Ok(result);
    }
    
    [HttpGet("list/{listId:guid}")]
    public async Task<IActionResult> GetAllByListAsync(Guid listId, CancellationToken token)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var cards = await cardRepository.GetAllByListAsync(listId, token);
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
        
        var validationResult = await validator.ValidateAsync(createCardDto, token);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        
        var cardModel = createCardDto.ToCardFromCreate(listId);
        var card = await cardRepository.CreateAsync(cardModel, listId, token);
        
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