namespace CaseBrasserie.Web.DTOs
{
    public class DevisDetails
    {
        public int GrossisteId { get; set; }
        public double PrixTotal { get; set; }
        public string Reduction { get; set; }
        public IEnumerable<DetailsBiereQuotation> Items { get; set; }
    }
}
