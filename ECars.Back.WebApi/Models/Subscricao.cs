namespace ECars.Back.Models
{
    public class Subscricao
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Tipo { get; set; }
        public DateTime DataInicio { get; set; }
        public bool Gratuito { get; set; }
        public decimal Valor { get; set; }
    }
}