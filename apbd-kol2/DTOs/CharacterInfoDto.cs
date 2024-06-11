using System.ComponentModel.DataAnnotations;

namespace apbd_kol2.DTOs;

public class CharacterInfoDto
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public int CurrentWeight { get; set; }
    [Required]
    public int MaxWeight { get; set; }
    [Required]
    public ICollection<BackpackInfoDto> backpackItems { get; set; }
    [Required]
    public ICollection<TitleInfoDto> titles { get; set; }
}

public class BackpackInfoDto
{
    [Required]
    public string itemName { get; set; }
    [Required]
    public int itemWeight { get; set; }
    [Required]
    public int amount { get; set; }
}

public class TitleInfoDto
{
    [Required]
    public string title { get; set; }
    [Required]
    public DateTime? acquiredAt { get; set; }
}

