using System.ComponentModel.DataAnnotations;

namespace apbd_kol2.Models;

public class title
{
    [Key]
    public int Id { get; set; }
    [MaxLength(100)]
    public string Name { get; set; }
    
    public ICollection<characterTitle> characterTitles { get; set; } = new HashSet<characterTitle>();
}