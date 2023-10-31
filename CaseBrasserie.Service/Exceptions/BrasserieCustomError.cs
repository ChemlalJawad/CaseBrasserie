namespace CaseBrasserie.Application.Exceptions
{
    [Serializable]
    public class BrasserieCustomError : Exception
    {
        public class CommandeVideException : Exception
        {
            public CommandeVideException() : base("La commande ne peut pas être vide.") { }
        }
        public class BiereNomVideException : Exception
        {
            public BiereNomVideException() : base("Le nom de la bière ne peut pas être vide.") { }
        }
        public class BierePrixException : Exception
        {
            public BierePrixException() : base("Le prix de la bière ne peut pas être vide ou inférieur à 0.") { }
        }
        public class BiereInexistantException : Exception
        {
            public BiereInexistantException() : base("La bière n'existe pas.") { }
        }
        public class StockModificationException : Exception
        {
            public StockModificationException() : base("Le stcok ne peut pas être inférieur à 0") { }
        }
        public class GrossisteBiereException : Exception
        {
            public GrossisteBiereException() : base("Il n'existe pas de grossiste avec cette biere.") { }
        }

        public class GrossisteInexistantException : Exception
        {
            public GrossisteInexistantException() : base("Le grossiste n'existe pas.") { }
        }
        public class BrasserieInexistantException : Exception
        {
            public BrasserieInexistantException() : base("La brasserie n'existe pas.") { }
        }
        public class BiereNonVendueParGrossisteException : Exception
        {
            public BiereNonVendueParGrossisteException() : base("La bière n'est pas vendue par ce grossiste.") { }
        }
        public class BiereDejaVendueParGrossisteException : Exception
        {
            public BiereDejaVendueParGrossisteException() : base("La bière est déjà vendue par ce grossiste.") { }
        }

        public class StockInsuffisantException : Exception
        {
            public StockInsuffisantException() : base("Le nombre de bières commandé est supérieur au stock du grossiste.") { }
        }

        public class DoublonCommandeException : Exception
        {
            public DoublonCommandeException() : base("Il y a un doublon dans la commande.") { }
        }
        public class TransactionAjoutError : Exception
        {
            public TransactionAjoutError() : base("Il y a un probleme de transaction lors de l'ajout d'une bière.") { }
        }
        public class TransactionAjoutBiereDansGrossisteError : Exception
        {
            public TransactionAjoutBiereDansGrossisteError() : base("Il y a un probleme de transaction lors de l'ajout d'une bière chez le grossiste.") { }
        }
        public class TransactionModificationStockBiereDansGrossisteError : Exception
        {
            public TransactionModificationStockBiereDansGrossisteError() : base("Il y a un probleme de transaction lors de la modification d'un stock d'une bière chez le grossiste.") { }
        }

    }
}
