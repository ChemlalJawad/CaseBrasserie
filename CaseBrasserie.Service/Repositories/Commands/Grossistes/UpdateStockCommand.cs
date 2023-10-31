namespace CaseBrasserie.Application.Repositories.Commands.Grossistes
{
    public class UpdateStockCommand
    {
        public int BiereId { get; set; }
        public int GrossisteId { get; set; }
        public int Stock { get; set; }
    }
}
