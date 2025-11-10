using hombresYMujeresFacturan.Components.Data;

namespace hombresYMujeresFacturan.Components.Servicios
{
    public class ServicioControlador
    {
        private readonly ServicioFactura _servicioFactura;

        public ServicioControlador(ServicioFactura servicioFactura)
        {
            _servicioFactura = servicioFactura;
        }

        public async Task<List<factura>> ObtenerFacturas()
        {
            return await _servicioFactura.ObtenerFacturas();
        }   
    
        public async Task AgregarFactura(factura nuevaFactura)
        {
            nuevaFactura.identificador = await GenerarNuevoId();
            await _servicioFactura.AgregarFactura(nuevaFactura);
        }

        public async Task<int> GenerarNuevoId()
        {
            var factura = await _servicioFactura.ObtenerFacturas();
            return factura.Any() ? factura.Max(t => t.identificador) + 1 : 1;
        }

        public async Task EliminarFactura(int identificador, int t)
        {
            await _servicioFactura.EliminarFactura(identificador, t);
        }

    } 
}
