using hombresYMujeresFacturan.Components.Data;

namespace hombresYMujeresFacturan.Components.Servicios
{
    public class ServicioControlador
    {
        private readonly ServicioFactura _servicioFactura;

        public ServicioControlador()
        {
            _servicioFactura = new ServicioFactura();
        }
        public async Task AgregarFactura(factura nuevaFactura)
        {
            await _servicioFactura.AgregarFactura(nuevaFactura);
        }
        public async Task<List<factura>> ObtenerFacturas()
        {
            return await _servicioFactura.ObtenerFacturas();
        }
    }
}
