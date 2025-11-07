using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WBAPI.Api.DTOs;
using WBAPI.Api.Models;
using WBAPI.Api.Repositories;

namespace WBAPI.Api.Controllers
{
    [ApiController]
    [Route("api/products")]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repo;
        public ProductsController(IProductRepository repo) => _repo = repo;

        [HttpGet]
        public IActionResult Get() => Ok(_repo.GetAll());

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var p = _repo.GetById(id);
            if (p == null) return NotFound();
            return Ok(p);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ProductDto dto)
        {
            var p = new Product { Name = dto.Name, Price = dto.Price };
            var created = _repo.Add(p);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] ProductDto dto)
        {
            var p = new Product { Name = dto.Name, Price = dto.Price };
            var updated = _repo.Update(id, p);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var deleted = _repo.Delete(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
