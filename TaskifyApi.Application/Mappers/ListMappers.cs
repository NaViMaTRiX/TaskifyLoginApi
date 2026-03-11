using TaskifyApi.Domain.Dtos.List;
using TaskifyApi.Domain.Models;

namespace TaskifyApi.Application.Mappers;

public static class ListMappers
{
    public static ListDto ToListDto(this Lists listModel) // изпользуется для вывода данных на страницу
    {
        return new ListDto
        {
            Id = listModel.Id,
            BoardId = listModel.BoardId,
            Title = listModel.Title!,
            Order = listModel.Order,
            Cards = listModel.Cards!.Select(x => x?.ToCardDto()).ToList(),
            CreatedTime = listModel.CreatedTime,
            LastModifyTime = listModel.LastModifyTime,
            CreatedUser = listModel.CreatedUser,
            LastModifyUser = listModel.LastModifyUser,
        };
    }
    
    public static ListDto ToBoardWithListDto(this Lists listModel) // изпользуется для вывода данных на страницу
    {
        return new ListDto
        {
            Id = listModel.Id,
            BoardId = listModel.BoardId,
            Title = listModel.Title!,
            Order = listModel.Order,
            CreatedTime = listModel.CreatedTime,
            LastModifyTime = listModel.LastModifyTime,
            CreatedUser = listModel.CreatedUser,
            LastModifyUser = listModel.LastModifyUser,
        };
    }

    // изпользуется для ввода данных на форму
    public static Lists ToListFromCreate(this CreateListDto createListModel, Guid boardId)
    {
        return new Lists
        {
            Id = Guid.NewGuid(),
            BoardId = boardId,
            Title = createListModel.Title,
            Order = createListModel.Order,
            CreatedTime = DateTime.UtcNow,
            LastModifyTime = DateTime.UtcNow
        };
    }

    public static Lists ToListFromUpdate(this UpdateListDto updateListModel)
    {
        return new Lists
        {
            Title = updateListModel.Title,
            Order = updateListModel.Order,
            LastModifyTime = DateTime.UtcNow
        };
    }
    
    public static ListDto ToListWithoutCardDto(this Lists listModel) // изпользуется для вывода данных на страницу
    {
        return new ListDto
        {
            Id = listModel.Id,
            BoardId = listModel.BoardId,
            Title = listModel.Title!,
            Order = listModel.Order,
            CreatedTime = listModel.CreatedTime,
            LastModifyTime = listModel.LastModifyTime,
            CreatedUser = listModel.CreatedUser,
            LastModifyUser = listModel.LastModifyUser,
        };
    }
}