using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace apbd_kol2.Models;

[Table("backpacks")]
[PrimaryKey(nameof(CharacterId), nameof(ItemId))]
public class backpack
{
    public int CharacterId { get; set; }
    public int ItemId { get; set; }
    public int Amount { get; set; }
    
    [ForeignKey(nameof(CharacterId))]
    public character Character { get; set; }
    [ForeignKey(nameof(ItemId))]
    public item Item { get; set; }

}