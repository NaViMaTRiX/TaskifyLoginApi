namespace WebApiTaskify.Dtos.Board;

using System.ComponentModel.DataAnnotations;

public record CreateBoardDto
{
    public string Title { get; set; }
    public string ImageId { get; set; }
    public string ImageFullUrl { get; set; }
    public string ImageThumbUrl { get; set; }
    public string ImageUserName { get; set; }
    public string ImageLinkHtml { get; set; }
}