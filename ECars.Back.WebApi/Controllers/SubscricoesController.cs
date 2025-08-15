using Microsoft.AspNetCore.Mvc;
using ECars.Back.Data;
using ECars.Back.Models;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace ECars.Back.Controllers
{
    [ApiController]
    [Route("api/subscricoes")]
    public class SubscricoesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public SubscricoesController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
            StripeConfiguration.ApiKey = _config["Stripe:SecretKey"];
        }

        [HttpPost("vendedor")]
        public async Task<IActionResult> CriarVendedor(string userId, decimal precoVeiculo, bool profissional = false)
        {
            var existing = _context.Subscricoes.FirstOrDefault(s => s.UserId == userId && s.Tipo == "Vendedor");
            bool gratuito = existing == null || (DateTime.Now - existing.DataInicio).TotalDays < 365;

            decimal valor = 0;
            if (!gratuito)
            {
                if (profissional)
                {
                    valor = _config.GetValue<decimal>("Subscricoes:Vendedor:PrecosProfissionais");
                }
                else
                {
                    var precos = _config.GetSection("Subscricoes:Vendedor:PrecosParticulares");
                    if (precoVeiculo <= 10000) valor = precos.GetValue<decimal>("Ate10000");
                    else if (precoVeiculo <= 30000) valor = precos.GetValue<decimal>("De10000A30000");
                    else valor = precos.GetValue<decimal>("Acima30000");
                }
                // Cobranca Stripe
                var options = new ChargeCreateOptions { Amount = (long)(valor * 100), Currency = "eur", Description = "Subscricao Vendedor" };
                var service = new ChargeService();
                await service.CreateAsync(options);
            }

            var sub = new Subscricao { UserId = userId, Tipo = "Vendedor", DataInicio = DateTime.Now, Gratuito = gratuito, Valor = valor };
            _context.Subscricoes.Add(sub);
            await _context.SaveChangesAsync();
            return Ok(sub);
        }

        [HttpPost("usuario-alertas")]
        public async Task<IActionResult> CriarAlertas(string userId)
        {
            var existing = _context.Subscricoes.FirstOrDefault(s => s.UserId == userId && s.Tipo == "UsuarioAlertas");
            bool gratuito = existing == null || (DateTime.Now - existing.DataInicio).TotalDays < 365;
            decimal valor = gratuito ? 0 : _config.GetValue<decimal>("Subscricoes:UsuarioAlertas:PrecoMensalApos");

            if (!gratuito)
            {
                // Cobranca mensal Stripe
                var options = new ChargeCreateOptions { Amount = (long)(valor * 100), Currency = "eur", Description = "Alertas Usuario" };
                var service = new ChargeService();
                await service.CreateAsync(options);
            }

            var sub = new Subscricao { UserId = userId, Tipo = "UsuarioAlertas", DataInicio = DateTime.Now, Gratuito = gratuito, Valor = valor };
            _context.Subscricoes.Add(sub);
            await _context.SaveChangesAsync();
            return Ok(sub);
        }
    }
}