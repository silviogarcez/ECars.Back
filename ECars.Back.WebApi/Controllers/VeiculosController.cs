using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ECars.Back.Data;
using ECars.Back.Dtos;
using ECars.Back.Models;
using System.Linq.Dynamic.Core; // Adicione NuGet para ordenacao dinamica

namespace ECars.Back.Controllers
{
    [ApiController]
    [Route("api/veiculos")]
    public class VeiculosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VeiculosController(AppDbContext context) => _context = context;

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] VeiculoSearchDto dto)
        {
            var query = _context.Veiculos.AsQueryable();

            // Helper for equality filters
            void ApplyFilter<T>(T? value, Expression<Func<Veiculo, T>> property) where T : struct
            {
                if (value.HasValue)
                    query = query.Where(Expression.Lambda<Func<Veiculo, bool>>(
                        Expression.Equal(property.Body, Expression.Constant(value.Value)),
                        property.Parameters));
            }
            void ApplyFilter(string value, Expression<Func<Veiculo, string>> property)
            {
                if (!string.IsNullOrEmpty(value))
                    query = query.Where(Expression.Lambda<Func<Veiculo, bool>>(
                        Expression.Equal(property.Body, Expression.Constant(value)),
                        property.Parameters));
            }

            // Apply filters
            ApplyFilter(dto.Marca, v => v.Marca);
            ApplyFilter(dto.Modelo, v => v.Modelo);
            ApplyFilter(dto.TipoVendedor, v => v.TipoVendedor);
            ApplyFilter(dto.Combustivel, v => v.Combustivel);
            ApplyFilter(dto.CaixaVelocidades, v => v.CaixaVelocidades);
            ApplyFilter(dto.Tracao, v => v.Tracao);
            ApplyFilter(dto.Estado, v => v.Estado);
            ApplyFilter(dto.ClasseEmissao, v => v.ClasseEmissao);
            ApplyFilter(dto.Distrito, v => v.Distrito);
            ApplyFilter(dto.Concelho, v => v.Concelho);

            // Range filters
            if (dto.AnoMin.HasValue) query = query.Where(v => v.Ano >= dto.AnoMin.Value);
            if (dto.AnoMax.HasValue) query = query.Where(v => v.Ano <= dto.AnoMax.Value);
            if (dto.PrecoMin.HasValue) query = query.Where(v => v.Preco >= dto.PrecoMin.Value);
            if (dto.PrecoMax.HasValue) query = query.Where(v => v.Preco <= dto.PrecoMax.Value);
            if (dto.KmMin.HasValue) query = query.Where(v => v.Quilometragem >= dto.KmMin.Value);
            if (dto.KmMax.HasValue) query = query.Where(v => v.Quilometragem <= dto.KmMax.Value);
            if (!string.IsNullOrEmpty(dto.TipoVendedor)) query = query.Where(v => v.TipoVendedor == dto.TipoVendedor);
            // Especificacoes tecnicas
            if (!string.IsNullOrEmpty(dto.Combustivel)) query = query.Where(v => v.Combustivel == dto.Combustivel);
            if (!string.IsNullOrEmpty(dto.CaixaVelocidades)) query = query.Where(v => v.CaixaVelocidades == dto.CaixaVelocidades);
            if (dto.CilindradaMin.HasValue) query = query.Where(v => v.Cilindrada >= dto.CilindradaMin.Value);
            if (dto.CilindradaMax.HasValue) query = query.Where(v => v.Cilindrada <= dto.CilindradaMax.Value);
            if (dto.PotenciaMin.HasValue) query = query.Where(v => v.Potencia >= dto.PotenciaMin.Value);
            if (dto.PotenciaMax.HasValue) query = query.Where(v => v.Potencia <= dto.PotenciaMax.Value);
            if (dto.NumPortas.HasValue) query = query.Where(v => v.NumPortas == dto.NumPortas.Value);
            if (!string.IsNullOrEmpty(dto.Tracao)) query = query.Where(v => v.Tracao == dto.Tracao);
            // Estado e legalizacao
            if (!string.IsNullOrEmpty(dto.Estado)) query = query.Where(v => v.Estado == dto.Estado);
            if (dto.IPOValida.HasValue) query = query.Where(v => v.IPOValida == dto.IPOValida.Value);
            if (dto.IUCPago.HasValue) query = query.Where(v => v.IUCPago == dto.IUCPago.Value);
            if (dto.UnicoDono.HasValue) query = query.Where(v => v.UnicoDono == dto.UnicoDono.Value);
            if (dto.HistoricoAcidentes.HasValue) query = query.Where(v => v.HistoricoAcidentes == dto.HistoricoAcidentes.Value);
            // Equipamentos
            if (dto.ArCondicionado.HasValue) query = query.Where(v => v.ArCondicionado == dto.ArCondicionado.Value);
            if (dto.DirecaoAssistida.HasValue) query = query.Where(v => v.DirecaoAssistida == dto.DirecaoAssistida.Value);
            if (dto.BancosCouro.HasValue) query = query.Where(v => v.BancosCouro == dto.BancosCouro.Value);
            if (dto.VidrosEletricos.HasValue) query = query.Where(v => v.VidrosEletricos == dto.VidrosEletricos.Value);
            if (dto.GPS.HasValue) query = query.Where(v => v.GPS == dto.GPS.Value);
            if (dto.CamaraMarchaAtras.HasValue) query = query.Where(v => v.CamaraMarchaAtras == dto.CamaraMarchaAtras.Value);
            if (dto.CruiseControl.HasValue) query = query.Where(v => v.CruiseControl == dto.CruiseControl.Value);
            if (dto.StartStop.HasValue) query = query.Where(v => v.StartStop == dto.StartStop.Value);
            if (dto.AssistenciaFaixa.HasValue) query = query.Where(v => v.AssistenciaFaixa == dto.AssistenciaFaixa.Value);
            // Ambientais
            if (!string.IsNullOrEmpty(dto.ClasseEmissao)) query = query.Where(v => v.ClasseEmissao == dto.ClasseEmissao);
            if (dto.ConsumoMin.HasValue) query = query.Where(v => v.ConsumoCombinado >= dto.ConsumoMin.Value);
            if (dto.ConsumoMax.HasValue) query = query.Where(v => v.ConsumoCombinado <= dto.ConsumoMax.Value);
            // Localizacao
            if (!string.IsNullOrEmpty(dto.Distrito)) query = query.Where(v => v.Distrito == dto.Distrito);
            if (!string.IsNullOrEmpty(dto.Concelho)) query = query.Where(v => v.Concelho == dto.Concelho);
            if (dto.RaioDistancia.HasValue && dto.LatitudeUser.HasValue && dto.LongitudeUser.HasValue)
            {
                // Filtro de raio usando formula Haversine (aproximado)
                query = query.Where(v => CalculateDistance(dto.LatitudeUser.Value, dto.LongitudeUser.Value, v.Latitude, v.Longitude) <= dto.RaioDistancia.Value);
            }

            // Media para termometro
            var mediaPreco = await query.AnyAsync() ? await query.AverageAsync(v => v.Preco) : 0;

            // Ordenacao dinamica
            if (!string.IsNullOrEmpty(dto.Ordenacao))
            {
                switch (dto.Ordenacao)
                {
                    case "precoAsc": query = query.OrderBy(v => v.Preco); break;
                    case "precoDesc": query = query.OrderByDescending(v => v.Preco); break;
                    case "kmAsc": query = query.OrderBy(v => v.Quilometragem); break;
                    case "anoDesc": query = query.OrderByDescending(v => v.Ano); break;
                    // Adicione mais
                    default: query = query.OrderByDescending(v => v.Id); break; // Mais recentes
                }
            }

            var resultados = await query.ToListAsync();

            return Ok(new { Resultados = resultados, MediaPreco = mediaPreco });
        }

        private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double R = 6371; // Raio da Terra em km
            var dLat = ToRadians(lat2 - lat1);
            var dLon = ToRadians(lon2 - lon1);
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c;
        }

        private double ToRadians(double deg) => deg * (Math.PI / 180);
    }
}