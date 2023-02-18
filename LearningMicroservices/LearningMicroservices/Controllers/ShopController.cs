using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearningMicroservices.Controllers
{
    [Route("shop")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        // CRUD

        private static List<ShopItemDto> items = new() {
            new ShopItemDto (Guid.NewGuid(),  "Boots of speed",  "Basic mobility boots",  450),
            new ShopItemDto (Guid.NewGuid(),  "Luden",  "Advanced mage staff",  3400),
            new ShopItemDto (Guid.NewGuid(),  "Rabaddon",  "Powerful hat",  3600)
        };

        [HttpGet]
        public IEnumerable<ShopItemDto> Get()
        {
            return items;
        }

        [HttpGet("{id}")]
        public ShopItemDto GetById(Guid id)
        {
            return items.SingleOrDefault(x => x.Id == id);
        }

        // Put supposed to be idempotent, i.g. same request always leads to the same result
        // (changes state, not iterates it)
        [HttpPut]
        public IActionResult Put(UpdateShopItemDto item)
        {
            var existingItem = items.Where(x => x.Id == item.Id).SingleOrDefault();

            if (existingItem is null)
            {
                return NotFound();
            }

            // TODO replace with context update function.
            var index = items.FindIndex(x => x.Id == item.Id);

            var updatedItem = existingItem with { // TODO change only filled fields???
                Name = item.Name,
                Description = item.Description,
                Price = item.Price
            };

            items[index] = updatedItem;

            return NoContent();
        }

        [HttpPost]
        public IActionResult Post(CreateShopItemDto item)
        {
            var newItem = new ShopItemDto(Guid.NewGuid(), item.Name, item.Description, item.Price);

            items.Add(newItem);
            return CreatedAtAction(nameof(GetById), new { id = newItem.Id }, newItem);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var index = items.FindIndex(x => x.Id == id);

            if (index < 0)
            {
                return NotFound();
            }

            items.RemoveAt(index);
            return NoContent();
        }
    }
}
