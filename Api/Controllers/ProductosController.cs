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
                return Conflict("El numero de productos es menor a 2");
            }

            return Ok(listaProductos);
        }

        [HttpGet("buscar")]
        public ActionResult<List<Producto>> Search([FromQuery] string? name,
            [FromQuery] int? categoriaId,
            [FromQuery] decimal? pMin,
            [FromQuery] decimal? pMax)
        {
            var listaProductos = DataTables.Productos
                .Where(x =>
                    (string.IsNullOrWhiteSpace(name) || x.Nombre.Contains(name, StringComparison.OrdinalIgnoreCase)) &&
                    (categoriaId is null || (x.Categoria is not null && x.Categoria.Id == categoriaId.Value)) &&
                    (pMin is null || x.Precio >= pMin.Value) &&
                    (pMax is null || x.Precio <= pMax.Value))
                .ToList();

            if (!listaProductos.Any())
            {
                return NotFound();
            }

            return Ok(listaProductos);
        }

        [HttpGet("precio-minimo/{valor}")]
        public ActionResult<List<Producto>> GetByValue([FromRoute] decimal valor)
        {
            var listaProductos = DataTables.Productos.Where(x => x.Precio >= valor).ToList();

            if (!listaProductos.Any())
            {
                return NotFound();
            }

            return Ok(listaProductos);
        }

        [HttpGet("{id}")]
        public ActionResult<Producto> GetById([FromRoute] int id)
        {
            var producto = DataTables.Productos.FirstOrDefault(x => x.Id == id);

            if (producto == null)
            {
                return NotFound("Producto no encontrado");
            }

            return Ok(producto);
        }

        [HttpGet("total")]
        public ActionResult<int> GetTotalOfProducts()
        {
            return Ok(DataTables.Productos.Count());
        }

        [HttpPut("{id}/asociar/{categoriaId}")]
        public IActionResult AssociateCategory([FromRoute] int id, [FromRoute] int categoriaId)
        {
            var producto = DataTables.Productos.FirstOrDefault(x => x.Id == id);

            var categoria = DataTables.Categorias.FirstOrDefault(x => x.Id == categoriaId);

            if (producto == null || categoria == null)
            {
                return NotFound("Producto o categoria no encontrado");
            }

            if (producto.Categoria != null)
            {
                return Conflict("El producto ya está asociado a esta categoría");
            }

            producto.Categoria = categoria;

            return NoContent();
        }

        [HttpDelete("{id}/desasociar")]
        public IActionResult DisassociateCategory([FromRoute] int id)
        {
            var producto = DataTables.Productos.FirstOrDefault(x => x.Id == id);

            if (producto == null)
            {
                return NotFound("Producto no encontrado");
            }

            producto.Categoria = null;

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

            return Created(nameof(GetById), producto.Id);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] ProductoRequest producto)
        {
            var productoExistente = DataTables.Productos.FirstOrDefault(x => x.Id == id);

            if (productoExistente == null)
            {
                return NotFound("Producto no encontrado");
            }

            if (string.IsNullOrEmpty(producto.Nombre) || producto.Precio <= 0)
            {
                return BadRequest("nombre vacio y/o precio igual a 0.");
            }

            productoExistente.Nombre = producto.Nombre;
            productoExistente.Precio = producto.Precio;

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateKeyMetadata([FromRoute] int id, [FromBody] UpdateKeyMetadataRequest producto)
        {
            var productoExistente = DataTables.Productos.FirstOrDefault(x => x.Id == id);

            if (productoExistente == null)
            {
                return NotFound("Producto no encontrado");
            }

            productoExistente.Nombre = producto.Nombre ?? productoExistente.Nombre;
            productoExistente.Precio = producto.Precio ?? productoExistente.Precio;
            productoExistente.Stock = producto.Stock ?? productoExistente.Stock;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var productoExistente = DataTables.Productos.FirstOrDefault(x => x.Id == id);

            if (productoExistente == null)
            {
                return NotFound("Producto no encontrado");
            }

            DataTables.Productos.Remove(productoExistente);

            return NoContent();
        }
    }
}
