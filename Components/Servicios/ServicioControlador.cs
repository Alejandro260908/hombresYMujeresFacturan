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
            nuevaFactura.identificador = await GenerarNuevoId();
            await _servicioFactura.AgregarFactura(nuevaFactura);
        }

        public async Task<int> GenerarNuevoId()
        {
            var juego = await _servicioFactura.ObtenerFacturas();
            return juego.Any() ? juego.Max(t => t.identificador) + 1 : 1;
        }

        public async Task<List<factura>> ObtenerFacturas()
        {
            return await _servicioFactura.ObtenerFacturas();
        }   
    }
}
