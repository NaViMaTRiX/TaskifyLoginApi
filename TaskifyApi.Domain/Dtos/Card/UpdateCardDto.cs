namespace WebApiTaskify.Dtos.Card;

using System.ComponentModel.DataAnnotations;

public class UpdateCardDto
{
    public string Title { get; set; }
    public int Order { get; set; }
    public string Description { get; set; }
    public bool TimeChecked { get; set; } //хз
    public bool ReadyChecked { get; set; } //хз
    public DateTime? TimeStart { get; set; }
    public DateTime? TimeEnd { get; set; }
}