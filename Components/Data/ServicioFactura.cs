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
            comando.CommandText = "insert into facturas (identificador,fecha,nombrecliente,articulos,total) values ($IDENTIFICADOR,$FECHA,$NOMBRECLIENTE,$ARTICULOS,$TOTAL)";
            comando.Parameters.AddWithValue("$IDENTIFICADOR", nuevaFactura.identificador);
            comando.Parameters.AddWithValue("$FECHA", nuevaFactura.fecha.ToLongDateString);
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
            comando.CommandText = "SELECT IDENTIFICADOR,FECHA,NOMBRECLIENTE,ARTICULOS,TOTAL FROM facturas";
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

        public async Task EliminarFactura (int identificador, int t)
        {
            string ruta = "mibase.db";
            using var conexion = new SqliteConnection($"DataSource={ruta}");
            await conexion.OpenAsync();

            var comando = conexion.CreateCommand();
            comando.CommandText = "DELETE FROM facturas WHERE IDENTIFICADOR = $identificador";
            comando.Parameters.AddWithValue("$identificador", identificador);

            comando.ExecuteNonQueryAsync();

            var comando1 = conexion.CreateCommand();
            for (int i = identificador; i <= t; i++)
            {
                comando1.CommandText = "UPDATE facturas SET IDENTIFICADOR = $newId WHERE IDENTIFICADOR = $oldId";
                comando1.Parameters.AddWithValue("$newId", i - 1);
                comando1.Parameters.AddWithValue("$oldId", i);
                comando1.ExecuteNonQueryAsync();
                comando1.Parameters.Clear();
            }
        }

        public async Task ActualizarFactura(int identificador,string fecha, string nombrecliente, string articulos, decimal total)
        {
            string ruta = "mibase.db";
            using var conexion = new SqliteConnection($"DataSource={ruta}");
            await conexion.OpenAsync();

            var comando = conexion.CreateCommand();
            comando.CommandText = "UPDATE facturas SET FECHA = @fecha ,NOMBRECLIENTE = @nombrecliente, ARTICULOS = @articulos, TOTAL = @total WHERE IDENTIFICADOR =@identificador";
            comando.Parameters.AddWithValue("@identificador", identificador);
            comando.Parameters.AddWithValue("@fecha", fecha);
            comando.Parameters.AddWithValue("@nombrecliente", nombrecliente);
            comando.Parameters.AddWithValue("@articulos", articulos);
            comando.Parameters.AddWithValue("@total", total);

            comando.ExecuteNonQueryAsync();
        }

    }
}
