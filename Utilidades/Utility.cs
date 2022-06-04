using TestApis.DTO;
using TestApis.Models;

namespace TestApis.Utilidades
{
    public static class Utility
    {
        public static ProductoDTO productoToDto(this Producto producto)
        {
            ProductoDTO retorno = new ProductoDTO();
            retorno.Nombre = producto.Nombre;  
            retorno.Descripcion = producto.Descripcion;
            retorno.Precio = producto.Precio;
            retorno.SKU = producto.SKU;
            return retorno;
        }

        public static Producto productoDTOtoProducto(this ProductoDTO productoDTO)
        {
            Producto retorno = new Producto();
            retorno.Nombre = productoDTO.Nombre;
            retorno.Descripcion = productoDTO.Descripcion;
            retorno.Precio = productoDTO.Precio;
            retorno.SKU = productoDTO.SKU;
            retorno.FechaAlta = DateTime.Now;
            return retorno;
        }

    }
}
