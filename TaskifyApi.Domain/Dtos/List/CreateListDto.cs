namespace WebApiTaskify.Dtos.List;

using System.ComponentModel.DataAnnotations;

public class CreateListDto
{
    public string Title { get; set; } 
    public int Order { get; set; }
}