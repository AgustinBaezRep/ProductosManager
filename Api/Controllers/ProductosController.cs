using Application.Data;
using Application.Services;
using Contracts.Requests;
using Contracts.Responses;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductosController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        public ActionResult<List<ProductoResponse>> GetAll()
        {
            var listaProductos = _productoService.GetAll();

            if (!listaProductos.Any())
            {
                return NotFound();
            }

            return Ok(listaProductos);
        }

        [HttpGet("buscar")]
        public ActionResult<List<Producto>> Search([FromQuery] string? name, 
            [FromQuery] int? categoriaId, 
            [FromQuery] decimal? pMin, 
            [FromQuery] decimal? pMax)
        {
            var listaProductos = _productoService.Search(name, categoriaId, pMin, pMax);

            if (!listaProductos.Any())
            {
                return NotFound();
            }

            return Ok(listaProductos);
        }

        [HttpGet("precio-minimo/{valor}")]
        public ActionResult<List<Producto>> GetByValue([FromRoute] decimal valor)
        {
            var listaProductos = _productoService.GetByValue(valor);

            if (!listaProductos.Any())
            {
                return NotFound();
            }

            return Ok(listaProductos);
        }

        [HttpGet("{id}")]
        public ActionResult<Producto> GetById([FromRoute] int id)
        {
            var producto = _productoService.GetById(id);

            if (producto == null)
            {
                return NotFound("Producto no encontrado");
            }

            return Ok(producto);
        }

        [HttpGet("total")]
        public ActionResult<int> GetTotalOfProducts()
        {
            return Ok(_productoService.GetTotalProductos());
        }

        [HttpPut("{id}/asociar/{categoriaId}")]
        public IActionResult AssociateCategory([FromRoute] int id, [FromRoute] int categoriaId)
        {
            var associated = _productoService.AssociateCategory(id, categoriaId);

            if (!associated)
            {
                return Conflict($"Ocurrio un error al asociar el producto de id {id}, a la categoria de id {categoriaId}");
            }

            return NoContent();
        }

        [HttpDelete("{id}/desasociar")]
        public IActionResult DisassociateCategory([FromRoute] int id)
        {
            var disassociated = _productoService.DisassociateCategory(id);

            if (!disassociated)
            {
                return Conflict($"Ocurrio un error al desasociar la categoria, al producto de id {id}");
            }

            return NoContent();
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProductoRequest producto)
        {
            var isCreated = _productoService.Create(producto);

            if (!isCreated)
            {
                return Conflict("Error al crear el producto");
            }

            return CreatedAtAction(nameof(GetById), new { id = producto.Id }, producto.Id);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] ProductoRequest producto)
        {
            var isUpdated = _productoService.Update(id, producto);

            if (!isUpdated)
            {
                return Conflict("Error al actualizar el producto");
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateKeyMetadata([FromRoute] int id, [FromBody] UpdateKeyMetadataRequest producto)
        {
            var isUpdated = _productoService.UpdateKeyMetadata(id, producto);

            if (!isUpdated)
            {
                return Conflict("Error al actualizar el producto");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var isDeleted = _productoService.Delete(id);

            if (!isDeleted)
            {
                return Conflict("Error al eliminar el producto");
            }

            return NoContent();
        }
    }
}
