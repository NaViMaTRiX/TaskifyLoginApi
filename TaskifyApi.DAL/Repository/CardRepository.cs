using Microsoft.EntityFrameworkCore;
using TaskifyApi.DAL.Data;
using TaskifyApi.Domain.Interface;
using TaskifyApi.Domain.Models;

namespace TaskifyApi.DAL.Repository;

public class CardRepository(AppDbContext context) : ICardRepository
{
    public async Task<List<Cards>> GetAllAsync(CancellationToken token)
    {
        var cards = await context.Card.AsNoTracking().ToListAsync(token);
        
        if (cards is null)
            throw new ArgumentException($"{nameof(cards)} does not exist, ID : {cards}");
        
        return cards;
    }

    public async Task<List<Cards>> GetAllByBoardAsync(Guid boardId, CancellationToken token)
    {
        if(!await context.Board.AnyAsync(x => x.Id == boardId, token))
            throw new ArgumentNullException($"{nameof(boardId)} does not exist, Id: {boardId}");
        
        var cards = await context.Card.Where(x => x.List!.BoardId == boardId)
            .AsNoTracking()
            .ToListAsync(token);
        
        if (cards is null)
            throw new ArgumentException($"{nameof(cards)} does not exist, Id : {cards}");
        return cards;
    }

    public async Task<List<Cards>> GetAllByListAsync(Guid listId, CancellationToken token)
    {
        if(!await context.List.AnyAsync(x => x.Id == listId, token))
            throw new ArgumentNullException($"{nameof(listId)} does not exist, Id: {listId}");
        
        var cards = await context.Card.Where(x => x.ListId == listId)
            .AsNoTracking()
            .ToListAsync(token);
        
        if(cards is null)
            throw new ArgumentNullException($"{nameof(listId)} does not exist, Id: {listId}");

        return cards;
    }

    public async Task<Cards?> GetByIdAsync(Guid cardId, CancellationToken token)
    {
        var cards = await context.Card
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == cardId, token);
        
        if(cards is null)
            throw new ArgumentNullException($"{nameof(cards)} does not exist, Id: {cardId}");
        
        return cards;
    }

    public async Task<Cards?> CreateAsync(Cards cardModel, Guid listId, CancellationToken token)
    {
        if(!await context.List.Where(x => x.Id == listId).AnyAsync(token))
            throw new ArgumentNullException($"{nameof(cardModel.List)} does not exist, Id: {cardModel.ListId}");
        
        await context.Card.AddAsync(cardModel, token);
        await context.SaveChangesAsync(token);
        return cardModel;
    }

    public async Task<Cards?> UpdateAsync(Guid cardId, Cards cardModel, CancellationToken token)
    {
        var checkCard = await GetByIdAsync(cardId, token);
        
        if(checkCard is null)
            throw new ArgumentNullException($"{nameof(checkCard)} does not exist, Id: {cardId}");
        
        checkCard.Title = cardModel.Title;
        checkCard.Description = cardModel.Description;
        checkCard.Position = cardModel.Position;
        checkCard.Timer = cardModel.Timer;
        checkCard.TimeStart = cardModel.TimeStart;
        checkCard.TimeEnd = cardModel.TimeEnd;
        checkCard.Completed = cardModel.Completed;
        checkCard.LastModifyTime = cardModel.LastModifyTime;
        
        await context.SaveChangesAsync(token);
        return checkCard;
    }

    public async Task<Cards?> DeleteAsync(Guid cardId, CancellationToken token)
    {
        var checkCard = await GetByIdAsync(cardId, token);
        
        if(checkCard is null)
            throw new ArgumentNullException($"{nameof(checkCard)} does not exist, Id: {cardId}");
        
        context.Card.Remove(checkCard);
        await context.SaveChangesAsync(token);
        return checkCard;
    }

    public  Task<bool> ExistAsync(Guid cardId, CancellationToken token)
    {
        return context.Card.AnyAsync(x=>x.Id == cardId, token);
    }
}