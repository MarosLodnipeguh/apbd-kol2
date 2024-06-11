using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace apbd_kol2.Models;

[Table("characterTitles")]
[PrimaryKey(nameof(CharacterId), nameof(TitleId))]
public class characterTitle
{
    public int CharacterId { get; set; }
    public int TitleId { get; set; }
    public DateTime AcquiredAt { get; set; }
    
    [ForeignKey(nameof(CharacterId))]
    public character Character { get; set; }
    [ForeignKey(nameof(TitleId))]
    public title Title { get; set; }


}