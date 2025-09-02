using Application.Services;
using Contracts.Requests;
using Contracts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriasController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public ActionResult<List<CategoriaResponse>> GetAll()
        {
            var listaCategorias = _categoriaService.GetAll();

            if (!listaCategorias.Any())
            {
                return NotFound("No hay categorías disponibles");
            }

            return Ok(listaCategorias);
        }

        [HttpGet("{id}")]
        public ActionResult<CategoriaResponse?> GetById([FromRoute] int id)
        {
            var categoria = _categoriaService.GetById(id);

            if (categoria == null)
            {
                return NotFound("Categoria no encontrada");
            }

            return Ok(categoria);
        }

        [HttpGet("{id}/productos")]
        public ActionResult<List<ProductoResponse>> GetProductosByCategoriaId([FromRoute] int id)
        {
            var productos = _categoriaService.GetProductosByCategoriaId(id);

            if (!productos.Any())
            {
                return NotFound("No hay productos para esta categoría");
            }

            return Ok(productos);
        }

        [HttpPost]
        public ActionResult Create([FromBody] CreateCategoriaRequest categoria)
        {
            var isCreated = _categoriaService.Create(categoria);

            if (!isCreated)
            {
                return Conflict("No se pudo crear la categoría");
            }

            return CreatedAtAction(nameof(GetById), new { id = categoria.Id }, categoria.Id);
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromRoute] int id, [FromBody] UpdateCategoriaRequest categoria)
        {
            var isUpdated = _categoriaService.Update(id, categoria);

            if (!isUpdated)
            {
                return Conflict("No se pudo actualizar la categoría");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            var isDeleted = _categoriaService.Delete(id);

            if (!isDeleted)
            {
                return Conflict("No se pudo eliminar la categoría");
            }

            return NoContent();
        }
    }
}
