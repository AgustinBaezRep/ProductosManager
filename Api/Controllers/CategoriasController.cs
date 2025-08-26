using Application.Data;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Categoria>> GetAll()
        {
            var listaCategorias = DataTables.Categorias.ToList();

            return Ok(listaCategorias);
        }

        [HttpGet("{id}")]
        public ActionResult<Categoria> GetById([FromRoute] int id)
        {
            var categoria = DataTables.Categorias.FirstOrDefault(x => x.Id == id);

            if (categoria == null)
            {
                return NotFound("Categoria no encontrada");
            }

            return Ok(categoria);
        }

        [HttpGet("{id}/productos")]
        public ActionResult<List<Producto>> GetProductosByCategoriaId([FromRoute] int id)
        {
            var categoria = DataTables.Categorias.FirstOrDefault(x => x.Id == id);

            if (categoria == null)
            {
                return NotFound("Categoria no encontrada");
            }

            var productos = DataTables.Productos.Where(p => p.Categoria?.Id == id).ToList();

            if (!productos.Any())
            {
                return NotFound("No hay productos asociados a esta categoría");
            }

            return Ok(productos);
        }

        [HttpPost]
        public ActionResult<Categoria> Create([FromBody] Categoria categoria)
        {
            if (categoria == null || string.IsNullOrWhiteSpace(categoria.Nombre))
            {
                return BadRequest("Categoria no puede ser nula o tener un nombre vacío");
            }

            var nuevaCategoria = new Categoria
            {
                Id = DataTables.Categorias.Any() ? DataTables.Categorias.Max(x => x.Id) + 1 : 1,
                Nombre = categoria.Nombre
            };

            DataTables.Categorias.Add(nuevaCategoria);

            return CreatedAtAction(nameof(GetById), new { id = nuevaCategoria.Id }, nuevaCategoria);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] Categoria categoria)
        {
            var categoriaExistente = DataTables.Categorias.FirstOrDefault(x => x.Id == id);

            if (categoriaExistente == null)
            {
                return NotFound("Categoria no encontrada");
            }

            if (categoria == null || string.IsNullOrWhiteSpace(categoria.Nombre))
            {
                return BadRequest("Categoria no puede ser nula o tener un nombre vacío");
            }

            if (DataTables.Productos.Any(p => p.Categoria?.Id == id))
            {
                return Conflict("No se puede actualizar la categoría porque hay productos asociados a ella.");
            }

            categoriaExistente.Nombre = categoria.Nombre;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var categoriaExistente = DataTables.Categorias.FirstOrDefault(x => x.Id == id);

            if (categoriaExistente == null)
            {
                return NotFound("Categoria no encontrada");
            }

            if (DataTables.Productos.Any(p => p.Categoria?.Id == id))
            {
                return Conflict("No se puede eliminar la categoría porque hay productos asociados a ella.");
            }

            DataTables.Categorias.Remove(categoriaExistente);

            return NoContent();
        }
    }
}
