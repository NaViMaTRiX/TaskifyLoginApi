using Microsoft.AspNetCore.Mvc;
using TaskifyApi.Application.Mappers;
using TaskifyApi.Domain.Dtos.List;
using TaskifyApi.Domain.Interface;

namespace TaskifyApi.Api.Controllers;

[Route("api/v{version:apiVersion}/list")]
[ApiController]
public class ListController(IListRepository listRepository, IBoardRepository boardRepository)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken token)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var lists = await listRepository.GetAllAsync(token); 
        var result = lists.Select(x => x.ToListDto());
        return Ok(result);
    }
    
    [HttpGet("board/{boardId:guid}")]
    public async Task<IActionResult> GetAllByBoard(Guid boardId, CancellationToken token)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var lists = await listRepository.GetAllByBoardAsync(boardId, token); 
        var result = lists.Select(x => x.ToListWithoutCardDto());
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken token)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var list = await listRepository.GetByIdAsync(id, token);
        if (list is null)
            return NotFound();

        return Ok(list.ToListDto());
    }

    [HttpPost("{BoardId:guid}")]
    public async Task<IActionResult> Create([FromRoute] Guid boardId, [FromBody] CreateListDto createListDto, CancellationToken token)
    {
         if (!ModelState.IsValid)
             return BadRequest(ModelState);

         if (!await boardRepository.ExistAsync(boardId, token))
         {
             return BadRequest(ModelState);
         }

         var listModel = createListDto.ToListFromCreate(boardId);
         var list = await listRepository.CreateAsync(listModel, token);
         
         if (list is null)
             return BadRequest("Failed to create list");
         return CreatedAtAction(nameof(GetById), new { id = list.Id }, list.ToListDto());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateListDto updateListDto, CancellationToken token)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var checkList = await listRepository.UpdateAsync(id, updateListDto.ToListFromUpdate(), token);
        
        if (checkList is null)
            return NotFound("List not found");

        return Ok(checkList.ToListDto());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken token) //TODO: make full delete list with all cards
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var checkList = await listRepository.DeleteAsync(id, token);
        
        if (checkList is null)
            return NotFound("List not found");
        
        return NoContent();
    }
    
}