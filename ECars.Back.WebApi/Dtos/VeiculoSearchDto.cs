namespace ECars.Back.Dtos
{
    public class VeiculoSearchDto
    {
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int? AnoMin { get; set; }
        public int? AnoMax { get; set; }
        public decimal? PrecoMin { get; set; }
        public decimal? PrecoMax { get; set; }
        public int? KmMin { get; set; }
        public int? KmMax { get; set; }
        public string TipoVendedor { get; set; }
        public string Combustivel { get; set; }
        public string CaixaVelocidades { get; set; }
        public decimal? CilindradaMin { get; set; }
        public decimal? CilindradaMax { get; set; }
        public int? PotenciaMin { get; set; }
        public int? PotenciaMax { get; set; }
        public int? NumPortas { get; set; }
        public string Tracao { get; set; }
        public string Estado { get; set; }
        public bool? IPOValida { get; set; }
        public bool? IUCPago { get; set; }
        public bool? UnicoDono { get; set; }
        public bool? HistoricoAcidentes { get; set; }
        public bool? ArCondicionado { get; set; }
        public bool? DirecaoAssistida { get; set; }
        public bool? BancosCouro { get; set; }
        public bool? VidrosEletricos { get; set; }
        public bool? GPS { get; set; }
        public bool? CamaraMarchaAtras { get; set; }
        public bool? CruiseControl { get; set; }
        public bool? StartStop { get; set; }
        public bool? AssistenciaFaixa { get; set; }
        public string ClasseEmissao { get; set; }
        public decimal? ConsumoMin { get; set; }
        public decimal? ConsumoMax { get; set; }
        public string Distrito { get; set; }
        public string Concelho { get; set; }
        public double? RaioDistancia { get; set; }
        public double? LatitudeUser { get; set; }
        public double? LongitudeUser { get; set; } // Para calculo de raio
        public string Ordenacao { get; set; } // ex: precoAsc, anoDesc
    }
}