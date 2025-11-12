namespace hombresYMujeresFacturan.Components.Data
{
    public class factura
    {
        public int identificador { get; set; }
        public DateOnly fecha { get; set; }
        public string cliente { get; set; }
        public string articulos { get; set; }
        public decimal total { get; set; }

    }
}
