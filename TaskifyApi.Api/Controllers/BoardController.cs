using Microsoft.AspNetCore.Mvc;
using TaskifyApi.Domain.Dtos.Board;
using TaskifyApi.Domain.Interface;
using TaskifyApi.Application.Mappers;
using WebApiTaskify.Dtos.Board;

namespace TaskifyApi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BoardController(IBoardRepository boardRepository) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken token)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var boards = await boardRepository.GetAllAsync(token);
        var result = boards.Select(b => b.ToBoardDto());
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken token)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var board = await boardRepository.GetByIdAsync(id, token);
        if (board is null)
            return NotFound("Board not found");
        
        return Ok(board.ToBoardDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromQuery] string orgId, [FromBody] CreateBoardDto boardDto,
        CancellationToken token)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // TODO: проверка OrgId через станонный сервис. Получается нужно получать OrgId из контроллера другого api.
        
        var boardModel = boardDto.ToBoardFromCreate(orgId);
        var board = await boardRepository.CreateAsync(orgId, boardModel, token);
        
        if (board is null)
            return BadRequest("Failed to create board");
        
        return Ok(board.ToBoardDto());
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBoardDto boardDto, CancellationToken token)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var boardModel = await boardRepository.UpdateAsync(id, boardDto.ToBoardFromUpdate(), token);
        
        if (boardModel is null)
            return NotFound("Board is not exist!");
        
        return Ok(boardModel.ToBoardDto());
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken token)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var result = await boardRepository.DeleteAsync(id, token);
        
        if (result is null)
            return NotFound("Board is not exist!");
        
        return NoContent();
    }
}