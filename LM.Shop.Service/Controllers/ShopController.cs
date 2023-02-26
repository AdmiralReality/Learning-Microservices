using LM.Shop.Service.Entities;
using LM.Shop.Service.Extensions;
using LM.Shop.Service.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LM.Shop.Service.Controllers
{
    [Route("shop")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IRepository<ShopItem> _repository;

        public ShopController(IRepository<ShopItem> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<ShopItemDto>> GetAsync()
        {
            return (await _repository.GetAllAsync()).Select(x => x.ToDto());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var item = await _repository.GetAsync(id);

            if (item is null)
                return NotFound();

            return new JsonResult(item.ToDto());
        }

        // Put supposed to be idempotent, i.g. same request always leads to the same result (changes state, not iterates it)
        [HttpPut]
        public async Task<IActionResult> PutAsync(UpdateShopItemDto item)
        {
            var newItem = new ShopItem
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                CreateDate = DateTimeOffset.UtcNow
            };

            await _repository.UpdateAsync(newItem);

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(CreateShopItemDto item)
        {
            var newItem = new ShopItem
            {
                Id = Guid.NewGuid(),
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                CreateDate = DateTimeOffset.UtcNow,
            };

            await _repository.CreateAsync(newItem);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = newItem.Id }, newItem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _repository.RemoveAsync(id);
            return NoContent();
        }
    }
}
