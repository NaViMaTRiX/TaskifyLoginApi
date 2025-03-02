using TaskifyApi.Domain.Dtos.Card;
using TaskifyApi.Domain.Models;
using WebApiTaskify.Dtos.Card;

namespace TaskifyApi.Application.Mappers;

public static class CardMappers
{
    public static CardDto ToCardDto(this Cards cardDto)
    {
        return new CardDto
        {
            Id = cardDto.Id,
            ListId = cardDto.ListId,
            Title = cardDto.Title,
            Description = cardDto.Description,
            Order = cardDto.Order,
            ReadyChecked = cardDto.Ready,
            TimeStart = cardDto.TimeStart,
            TimeEnd = cardDto.TimeEnd,
            TimeChecked = cardDto.Timer,
            CreatedTime = cardDto.CreatedTime,
            LastModifyTime = cardDto.LastModifyTime,
            LastModifyUser = cardDto.LastModifyUser,
            CreatedUser = cardDto.CreatedUser,
        };
    }

    public static Cards ToCardFromCreate(this CreateCardDto createCardDto, Guid listId)
    {
        return new Cards
        {
            Id = Guid.NewGuid(),
            ListId = listId,
            Title = createCardDto.Title,
            Description = createCardDto.Description,
            Order = createCardDto.Order,
            Timer = createCardDto.TimeChecked,
            Ready = createCardDto.ReadyChecked,
            TimeStart = createCardDto.TimeStart,
            TimeEnd = createCardDto.TimeEnd,
        };
    }

    public static Cards ToCardFromUpdate(this UpdateCardDto updateCardDto)
    {
        return new Cards
        {
            Title = updateCardDto.Title,
            Description = updateCardDto.Description,
            Order = updateCardDto.Order,
            Ready = updateCardDto.ReadyChecked,
            Timer = updateCardDto.TimeChecked,
            TimeStart = updateCardDto.TimeStart,
            TimeEnd = updateCardDto.TimeEnd,
        };
    }
}