using System.ComponentModel.DataAnnotations;

namespace apbd_kol2.Models;

public class character
{
    [Key]
    public int Id { get; set; }
    [MaxLength(50)]
    public string FirstName { get; set; }
    [MaxLength(120)]
    public string LastName { get; set; }
    public int CurrentWeight { get; set; }
    public int MaxWeight { get; set; }
    
    public ICollection<backpack> Backpacks { get; set; } = new HashSet<backpack>();
    
    public ICollection<characterTitle> CharacterTitles { get; set; } = new HashSet<characterTitle>();
}