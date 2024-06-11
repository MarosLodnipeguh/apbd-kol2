using apbd_kol2_example.Services;
using apbd_kol2.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace apbd_kol2.Controllers;

[ApiController]
[Route("api")]
public class Controller : ControllerBase
{
    private readonly IDbService _service;

    public Controller(IDbService dbService)
    {
        _service = dbService;
    }


    [HttpGet("characters/{characterId}")]
    public async Task<IActionResult> GetCharacterData(int characterId)
    {
        if (!await _service.DoesCharacterExist(characterId))
        {
            return NotFound("Character not found.");
        }

        var character = await _service.GetCharacterData(characterId);
        return Ok(character);
    }


    [HttpPost("characters/{characterId}/backpacks")]
    public async Task<IActionResult> AddBackpack(int characterId, [FromBody] NewItemsDto items)
    {
        if (!await _service.DoesCharacterExist(characterId))
        {
            return NotFound("Character not found.");
        }

        foreach (var itemId in items.itemIds)
        {
            if (!await _service.DoesItemExist(itemId))
            {
                return NotFound("Item with id: " + items.itemIds + "not found.");
            }
        }

        if (!await _service.DoesCharacterHaveCapacity(characterId, await _service.GetItemsTotalWeight(items.itemIds)))
        {
            return BadRequest("Character does not have enough capacity.");
        }
        
        // add items to character and update his current weight
        var addItemsToCharacter = _service.AddItemsToCharacter(characterId, items);
        
        
        // return Ok(addItemsToCharacter);
        return Ok("items added");
    }

    
    
    
}