namespace ECars.Back.Models
{
    public class Veiculo
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }
        public decimal Preco { get; set; }
        public int Quilometragem { get; set; }
        public string TipoVendedor { get; set; } // particular/stand/concessionario
        public string Combustivel { get; set; } // gasolina, gasoleo, etc.
        public string CaixaVelocidades { get; set; } // manual, automatica, CVT
        public decimal Cilindrada { get; set; }
        public int Potencia { get; set; }
        public int NumPortas { get; set; }
        public string Tracao { get; set; }
        public string Estado { get; set; } // novo, usado, seminovo, para pecas
        public bool IPOValida { get; set; }
        public DateTime? DataProximaIPO { get; set; }
        public bool IUCPago { get; set; }
        public bool UnicoDono { get; set; }
        public bool HistoricoAcidentes { get; set; }
        // Equipamentos
        public bool ArCondicionado { get; set; }
        public bool DirecaoAssistida { get; set; }
        public bool BancosCouro { get; set; }
        public bool VidrosEletricos { get; set; }
        public bool GPS { get; set; }
        public bool CamaraMarchaAtras { get; set; }
        public bool CruiseControl { get; set; }
        public bool StartStop { get; set; }
        public bool AssistenciaFaixa { get; set; }
        // Ambientais
        public string ClasseEmissao { get; set; }
        public decimal ConsumoCombinado { get; set; }
        // Localizacao
        public string Distrito { get; set; }
        public string Concelho { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; } // Para raio de distancia
    }
}