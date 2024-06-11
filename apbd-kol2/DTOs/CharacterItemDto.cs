namespace apbd_kol2.DTOs;

public class CharacterItemDto
{
    
}

public class NewItemsDto
{
    public ICollection<int> itemIds { get; set; }
}

public class AddedItemDto
{
    public int amount { get; set; }
    public int ItemId { get; set; }
    public int CharacterId { get; set; }
}