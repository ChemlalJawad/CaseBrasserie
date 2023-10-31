namespace CaseBrasserie.Core.Entities
{
    public class Grossiste
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public ICollection<GrossisteBiere> GrossistesBieres { get; set; } = new HashSet<GrossisteBiere>();
    }

}
