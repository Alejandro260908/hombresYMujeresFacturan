using Microsoft.Data.Sqlite;

namespace hombresYMujeresFacturan.Components.Data
{
    public class ServicioFactura
    {
        private List<factura> facturas = new List<factura>();
        public async Task AgregarFactura(factura nuevaFactura)
        {
            string ruta = "mibase.db";
            using var conexion = new SqliteConnection($"DataSource={ruta}");
            await conexion.OpenAsync();

            var comando = conexion.CreateCommand();
            comando.CommandText = "insert into juegos (identificador,nombre,jugado) values ($IDENTIFICADOR,$NOMBRE,$JUGADO)";
            comando.Parameters.AddWithValue("$IDENTIFICADOR", nuevaFactura.identificador);
            comando.Parameters.AddWithValue("$FECHA", nuevaFactura.fecha);
            comando.Parameters.AddWithValue("$NOMBRECLIENTE", nuevaFactura.cliente);
            comando.Parameters.AddWithValue("$ARTICULOS", nuevaFactura.articulos);
            comando.Parameters.AddWithValue("$TOTAL", nuevaFactura.total);

            comando.ExecuteNonQueryAsync();

            facturas.Add(nuevaFactura);
        }
        public async Task<List<factura>> ObtenerFacturas()
        {
            facturas.Clear();
            string ruta = "mibase.db";
            using var conexion = new SqliteConnection($"DataSource={ruta}");
            await conexion.OpenAsync();

            var comando = conexion.CreateCommand();
            comando.CommandText = "SELECT IDENTIFICADOR,FECHA,NOMBRECLIENTE,ARTICULOS,TOTAL FROM FACTURAS";
            using var lector = await comando.ExecuteReaderAsync();

            while (await lector.ReadAsync())
            {
                facturas.Add(new factura
                {
                    identificador = lector.GetInt32(0),
                    fecha = lector.GetString(1),
                    cliente = lector.GetString(2),
                    articulos = lector.GetString(3),
                    total = lector.GetInt32(4)
                });
            }

            return facturas;
        }

    }
}
