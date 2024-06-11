using apbd_kol2.DTOs;
using apbd_kol2.Models;

namespace apbd_kol2_example.Services;

public interface IDbService
{
    Task<bool> DoesCharacterExist(int characterId);
    Task<CharacterInfoDto> GetCharacterData(int characterId);
    Task<ICollection<BackpackInfoDto>> GetCharacterItems(int characterId);
    Task<ICollection<TitleInfoDto>> GetCharacterTitles(int characterId);
    
    Task<bool> DoesItemExist(int itemId);
    Task<int> GetItemsTotalWeight(ICollection<int> itemIds);
    Task<bool> DoesCharacterHaveCapacity(int characterId, int addedWeight);
    // Task<ICollection<backpack>> AddItemsToCharacter(int characterId, NewItemsDto items);
    Task AddItemsToCharacter(int characterId, NewItemsDto items);
    // Task UpdateCharacterCurrentWeight(int characterId, int newWeight);
}