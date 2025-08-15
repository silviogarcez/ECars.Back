namespace ECars.Back.Models
{
    public class Anuncio
    {
        public int Id { get; set; }
        public string VendedorId { get; set; }
        public int VeiculoId { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }
        public int Visualizacoes { get; set; }
        public int Contactos { get; set; }
        public bool Destaque { get; set; } // Para planos pagos
        // Fotos/Videos: Lista de strings para URLs (use Azure Blob)
    }
}