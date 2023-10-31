namespace CaseBrasserie.Core.Entities
{
    public class Biere
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public float DegreAlcool { get; set; }
        public decimal Prix { get; set; }

        // Propriétés de navigation pour les relations
        public int BrasserieId { get; set; }
        public Brasserie Brasserie { get; set; }
        public ICollection<GrossisteBiere> GrossistesBieres { get; set; } = new HashSet<GrossisteBiere>();
    }

}
