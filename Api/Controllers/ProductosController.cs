using Api.Contracts;
using Api.Data;
using Api.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Producto>> GetAll()
        {
            var listaProductos = DataSet.Productos.ToList();

            if (listaProductos.Count < 2)
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
            var listaProductos = DataSet.Productos
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
            var listaProductos = DataSet.Productos.Where(x => x.Precio >= valor).ToList();

            if (!listaProductos.Any())
            {
                return NotFound();
            }

            return Ok(listaProductos);
        }

        [HttpGet("{id}")]
        public ActionResult<Producto> GetById([FromRoute] int id)
        {
            var producto = DataSet.Productos.FirstOrDefault(x => x.Id == id);

            if (producto == null)
            {
                return NotFound("Producto no encontrado");
            }

            return Ok(producto);
        }

        [HttpGet("total")]
        public ActionResult<int> GetTotalOfProducts()
        {
            return Ok(DataSet.Productos.Count());
        }

        [HttpPut("{id}/asociar/{categoriaId}")]
        public IActionResult AssociateCategory([FromRoute] int id, [FromRoute] int categoriaId)
        {
            var producto = DataSet.Productos.FirstOrDefault(x => x.Id == id);

            var categoria = DataSet.Categorias.FirstOrDefault(x => x.Id == categoriaId);

            if (producto == null)
            {
                return NotFound("Producto no encontrado");
            }

            if (categoria == null)
            {
                return NotFound("Categoria no encontrada");
            }

            producto.Categoria = categoria;

            return NoContent();
        }

        [HttpDelete("{id}/desasociar")]
        public IActionResult DisassociateCategory([FromRoute] int id)
        {
            var producto = DataSet.Productos.FirstOrDefault(x => x.Id == id);

            if (producto == null)
            {
                return NotFound("Producto no encontrado");
            }

            producto.Categoria = null;

            return NoContent();
        }

        [HttpPost]
        public ActionResult<ProductoResponse> Create([FromBody] ProductoRequest producto)
        {
            if (string.IsNullOrEmpty(producto.Nombre) || producto.Precio <= 0 || producto.Stock < 0)
            {
                return BadRequest("Campos erroneos.");
            }

            producto.Id = DataSet.Productos.Any() ? DataSet.Productos.Max(x => x.Id) + 1 : 1;

            int stock = producto.Stock == null ? 10 : producto.Stock.Value;

            var newProducto = new Producto(producto.Id, producto.Nombre, producto.Precio, stock);

            DataSet.Productos.Add(newProducto);

            var returnProducto = new ProductoResponse()
            {
                Id = newProducto.Id,
                Nombre = newProducto.Nombre,
                Precio = newProducto.Precio
            };

            return CreatedAtAction(nameof(GetById), new { id = returnProducto.Id }, returnProducto);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] ProductoRequest producto)
        {
            var productoExistente = DataSet.Productos.FirstOrDefault(x => x.Id == id);

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
            var productoExistente = DataSet.Productos.FirstOrDefault(x => x.Id == id);

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
            var productoExistente = DataSet.Productos.FirstOrDefault(x => x.Id == id);

            if (productoExistente == null)
            {
                return NotFound("Producto no encontrado");
            }

            DataSet.Productos.Remove(productoExistente);

            return NoContent();
        }
    }
}
