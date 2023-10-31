namespace CaseBrasserie.Application.Repositories.Commands.Grossistes
{
    public class QuotationCommand
    {
        public int GrossisteId { get; set; }
        public double PrixTotal { get; set; }
        public IEnumerable<ItemCommand> Items { get; set; } = new HashSet<ItemCommand>();
    }
}
