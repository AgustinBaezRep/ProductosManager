using Api.Contracts;
using Api.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private static List<Producto> productList = new List<Producto>()
        {
            new Producto(1, "Libro A", 25000, 1),
            new Producto(2, "Libro B", 32000, 2),
            new Producto(3, "Libro C", 12000, 3)
        };

        [HttpGet]
        public ActionResult<List<Producto>> GetAll()
        {
            var listaProductos = productList.ToList();

            if (listaProductos.Count < 2)
            {
                return Conflict("El numero de productos es menor a 2");
            }

            return Ok(listaProductos);
        }

        [HttpGet("buscar")]
        public ActionResult<List<Producto>> Search([FromQuery] string name)
        {
            var listaProductos = productList.Where(x => x.Nombre.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!listaProductos.Any())
            {
                return NotFound();
            }

            return Ok(listaProductos);
        }

        [HttpGet("precio-minimo/{valor}")]
        public ActionResult<List<Producto>> GetByValue([FromRoute] decimal valor)
        {
            var listaProductos = productList.Where(x => x.Precio >= valor).ToList();

            if (!listaProductos.Any())
            {
                return NotFound();
            }

            return Ok(listaProductos);
        }

        [HttpGet("{id}")]
        public ActionResult<List<Producto>> GetById([FromRoute] int id)
        {
            var producto = productList.FirstOrDefault(x => x.Id == id);

            if (producto == null)
            {
                return NotFound("Producto no encontrado");
            }

            return Ok(producto);
        }

        [HttpGet("total")]
        public ActionResult<List<Producto>> GetTotalOfProducts()
            => Ok(productList.Count());

        [HttpPost]
        public ActionResult<ProductoResponse> Create([FromBody] ProductoRequest producto)
        {
            if (string.IsNullOrEmpty(producto.Nombre) || producto.Precio <= 0 || producto.Stock < 0)
            {
                return BadRequest("Campos erroneos.");
            }

            if (productList.Any())
            {
                producto.Id = productList.Max(x => x.Id) + 1;
            }
            else
            {
                producto.Id = 1;
            }

            int stock = producto.Stock == null ? 10 : producto.Stock.Value;

            var newProducto = new Producto(producto.Id, producto.Nombre, producto.Precio, stock);

            productList.Add(newProducto);

            var returnProducto = new ProductoResponse()
            {
                Id = newProducto.Id,
                Nombre = newProducto.Nombre,
                Precio = newProducto.Precio
            };

            return CreatedAtAction(nameof(GetById), new { id = returnProducto.Id }, returnProducto);
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromRoute] int id, [FromBody] ProductoRequest producto)
        {
            var productoExistente = productList.FirstOrDefault(x => x.Id == id);

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
        public ActionResult UpdateKeyMetadata([FromRoute] int id, [FromBody] UpdateKeyMetadataRequest producto)
        {
            var productoExistente = productList.FirstOrDefault(x => x.Id == id);

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
        public ActionResult Delete([FromRoute] int id)
        {
            var productoExistente = productList.FirstOrDefault(x => x.Id == id);

            if (productoExistente == null)
            {
                return NotFound("Producto no encontrado");
            }

            productList.Remove(productoExistente);

            return NoContent();
        }
    }
}
