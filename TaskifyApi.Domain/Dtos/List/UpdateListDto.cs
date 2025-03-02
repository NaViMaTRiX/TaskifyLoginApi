namespace WebApiTaskify.Dtos.List;

using System.ComponentModel.DataAnnotations;

public class UpdateListDto
{
    public string Title { get; set; }
    public int Order { get; set; }
}