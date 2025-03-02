using WebApiTaskify.Dtos.Board;

namespace TaskifyApi.Application.Mappers;

using Domain.Dtos.Board;
using Domain.Models;

public static class BoardMappers
{
    public static BoardDto ToBoardDto(this Boards boardModel) // изпользуется для вывода данных на страницу
    {
        return new BoardDto
        {
            Id = boardModel.Id,
            OrgId = boardModel.OrgId,
            Title = boardModel.Title,
            Lists = boardModel.Lists!.Select(list => list?.ToBoardWithListDto()).ToList(),
            ImageId = boardModel.ImageId,
            ImageFullUrl = boardModel.ImageFullUrl,
            ImageThumbUrl = boardModel.ImageThumbUrl,
            ImageLinkHtml = boardModel.ImageLinkHTML,
            CreatedTime = boardModel.CreatedTime,
            LastModifyTime = boardModel.LastModifyTime,
            LastModifyUser = boardModel.LastModifyUser,
            CreatedUser = boardModel.CreatedUser,
        };
    }

    public static Boards ToBoardFromCreate(this CreateBoardDto createBoardModel, string orgId) // изпользуется для получения данных из формы
    {
        return new Boards
        {
            Id = Guid.NewGuid(),
            OrgId = orgId,
            Title = createBoardModel.Title,
            ImageId = createBoardModel.ImageId,
            ImageFullUrl = createBoardModel.ImageFullUrl,
            ImageUserName = createBoardModel.ImageUserName,
            ImageThumbUrl = createBoardModel.ImageThumbUrl,
            ImageLinkHTML = createBoardModel.ImageLinkHtml,
        };
    }

    public static Boards ToBoardFromUpdate(this UpdateBoardDto updateBoardModel) // изпользуется для получения данных из формы
    {
        return new Boards
        {
            OrgId = updateBoardModel.OrgId,
            Title = updateBoardModel.Title,
            ImageId = updateBoardModel.ImageId,
            ImageFullUrl = updateBoardModel.ImageFullUrl,
            ImageUserName = updateBoardModel.ImageUserName,
            ImageThumbUrl = updateBoardModel.ImageThumbUrl,
            ImageLinkHTML = updateBoardModel.ImageLinkHtml,
        };
    }
}