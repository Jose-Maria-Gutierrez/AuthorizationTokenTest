using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using TestApis.Datos;
using TestApis.DTO;
using TestApis.Models;
using TestApis.Utilidades;

namespace TestApis.Controllers
{
    [Route("productos")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public ProductoController(ApplicationDbContext _context)
        {
            this.context = _context;
        }
        
        [HttpGet]
        public IEnumerable<ProductoDTO> GetProductos()
        {
            var lista = this.context.productos.ToList().Select(x => x.productoToDto());
            return lista;
        }

        [HttpGet("{id}")]
        public ActionResult<ProductoDTO> GetProducto(int id)
        {
            var producto = this.context.productos.SingleOrDefault(p => p.Id == id);
            if(producto == null)
            {
                return NotFound();
            }
            return producto.productoToDto();
        }
        
        [HttpPost]
        public ActionResult<Producto> AgregarProducto([FromBody]ProductoDTO producto)
        {
            Producto nuevo = producto.productoDTOtoProducto();
            this.context.productos.Add(nuevo);
            this.context.SaveChanges();
            return nuevo;
        }

        [HttpPut("{id}")]
        public ActionResult<Producto> ModificarProducto(int id,[FromBody] ProductoDTO producto)
        {
            if (ModelState.IsValid)
            {
                Producto productoExistente = this.context.productos.First(x => x.Id == id);
                if (productoExistente == null)
                {
                    return NotFound();
                }
                productoExistente.Nombre = producto.Nombre;
                productoExistente.Descripcion = producto.Descripcion;
                productoExistente.SKU = producto.SKU;
                productoExistente.Precio = producto.Precio;

                this.context.productos.Update(productoExistente);
                this.context.SaveChanges();
                return productoExistente;
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            Producto pExiste = this.context.productos.First(x=>x.Id==id);
            if (pExiste == null)
            {
                return NotFound();
            }
            this.context.productos.Remove(pExiste);
            this.context.SaveChanges();
            return NoContent();
        }
    }
}
