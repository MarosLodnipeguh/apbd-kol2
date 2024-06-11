using apbd_kol2.Data;
using apbd_kol2.DTOs;
using apbd_kol2.Models;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;

namespace apbd_kol2_example.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _context;

    public DbService(DatabaseContext context)
    {
        _context = context;
    }


    public async Task<bool> DoesCharacterExist(int characterId)
    {
        return await _context.Characters.AnyAsync(c => c.Id == characterId);
    }

    public async Task<CharacterInfoDto> GetCharacterData(int characterId)
    {
        var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == characterId);
        var backpackItems = await GetCharacterItems(characterId);
        var titles = await GetCharacterTitles(characterId);
        
        return new CharacterInfoDto
        {
            FirstName = character.FirstName,
            LastName = character.LastName,
            CurrentWeight = character.CurrentWeight,
            MaxWeight = character.MaxWeight,
            backpackItems = backpackItems,
            titles = titles
        };
    }

    public async Task<ICollection<BackpackInfoDto>> GetCharacterItems(int characterId)
    {
        var items = await _context.Backpacks
            .Where(b => b.CharacterId == characterId)
            .Select(b => b.Item)
            .ToListAsync();
        
        var backpackInfoDtos = new List<BackpackInfoDto>();
        
        foreach (var item in items)
        {
            var backpackInfoDto = new BackpackInfoDto
            {
                itemName = item.Name,
                itemWeight = item.Weight,
                amount = await _context.Backpacks
                    .Where(b => b.CharacterId == characterId && b.ItemId == item.Id)
                    .Select(b => b.Amount)
                    .FirstOrDefaultAsync()
            };
            
            backpackInfoDtos.Add(backpackInfoDto);
        }
        return backpackInfoDtos;
    }

    public async Task<ICollection<TitleInfoDto>> GetCharacterTitles(int characterId)
    {
        var titles = await _context.CharacterTitles
            .Where(ct => ct.CharacterId == characterId)
            .Select(ct => ct.Title)
            .ToListAsync();
        
        var titleInfoDtos = new List<TitleInfoDto>();
        
        foreach (var title in titles)
        {
            var titleInfoDto = new TitleInfoDto
            {
                title = title.Name,
                acquiredAt = await _context.CharacterTitles
                    .Where(ct => ct.CharacterId == characterId && ct.TitleId == title.Id)
                    .Select(ct => ct.AcquiredAt)
                    .FirstOrDefaultAsync()
            };
            
            titleInfoDtos.Add(titleInfoDto);
        }
        return titleInfoDtos;
    }
    
    // ========================== ITEM ADDING ==========================

    public async Task<bool> DoesItemExist(int itemId)
    {
        return await _context.Items.AnyAsync(i => i.Id == itemId);
    }

    public Task<int> GetItemsTotalWeight(ICollection<int> itemIds)
    {
        var totalWeight = 0;
        
        foreach (var itemId in itemIds)
        {
            var item = _context.Items.FirstOrDefault(i => i.Id == itemId);
            totalWeight += item.Weight;
        }
        
        return Task.FromResult(totalWeight);
    }

    public async Task<bool> DoesCharacterHaveCapacity(int characterId, int addedWeight)
    {
        var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == characterId);
        var characterCurrentWeight = character.CurrentWeight;
        var characterMaxWeight = character.MaxWeight;
        if (characterCurrentWeight + addedWeight <= characterMaxWeight)
        {
            return true;
        }
        return false;
    }

    public async Task AddItemsToCharacter(int characterId, NewItemsDto items)
    {
        var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == characterId);
        var characterBackpack = await _context.Backpacks.FirstOrDefaultAsync(b => b.CharacterId == characterId);
        
        var itemList = items.itemIds;
        
        var addedItems = new List<backpack>();
        
        foreach(var itemId in itemList)
        {
            var backpack = new backpack
            {
                CharacterId = characterId,
                ItemId = itemId,
                Amount = 1
            };
            
            await _context.Backpacks.AddAsync(backpack);
            await _context.SaveChangesAsync();
            
            // addedItemDtos.Add(new AddedItemDto
            // {
            //     amount = backpack.Amount,
            //     ItemId = backpack.ItemId,
            //     CharacterId = backpack.CharacterId
            // });
        }
        
        character.CurrentWeight += await GetItemsTotalWeight(itemList);
        await _context.SaveChangesAsync();

        // return addedItems;
    }

    // public async Task UpdateCharacterCurrentWeight(int characterId, int newWeight)
    // {
    //     throw new NotImplementedException();
    // }
}