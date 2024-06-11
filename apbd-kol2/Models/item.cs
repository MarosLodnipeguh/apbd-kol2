using System.ComponentModel.DataAnnotations;

namespace apbd_kol2.Models;

public class item
{
    [Key]
    public int Id { get; set; }
    [MaxLength(100)]
    public string Name { get; set; }
    public int Weight { get; set; }
    
    public ICollection<backpack> backpacks { get; set; } = new HashSet<backpack>();
}