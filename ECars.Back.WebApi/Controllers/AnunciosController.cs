//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using ECars.Back.Data;
//using ECars.Back.Models;
//using Microsoft.AspNetCore.Authorization;

//namespace ECars.Back.Controllers
//{
//    [ApiController]
//    [Route("api/anuncios")]
//    [Authorize(Roles = "Vendedor")]
//    public class AnunciosController : ControllerBase
//    {
//        private readonly AppDbContext _context;

//        public AnunciosController(AppDbContext context) => _context = context;

//        [HttpPost]
//        public async Task<IActionResult> Cadastrar([FromBody] Anuncio anuncio)
//        {
//            // Verifique subscricao ativa antes de cadastrar
//            anuncio.DataCadastro = DateTime.Now;
//            _context.Anuncios.Add(anuncio);
//            await _context.SaveChangesAsync();
//            return Ok(anuncio);
//        }

//        [HttpGet("meus")]
//        public async Task<IActionResult> GetMeusAnuncios(string vendedorId)
//        {
//            var anuncios = await _context.Anuncios.Where(a => a.VendedorId == vendedorId).ToListAsync();
//            return Ok(anuncios);
//        }

//        [HttpPut("{id}")]
//        public async Task<IActionResult> Editar(int id, [FromBody] Anuncio anuncio)
//        {
//            var existing = await _context.Anuncios.FindAsync(id);
//            if (existing == null) return NotFound();
//            // Atualize campos
//            await _context.SaveChangesAsync();
//            return Ok();
//        }

//        [HttpPut("{id}/pausar")]
//        public async Task<IActionResult> Pausar(int id)
//        {
//            var anuncio = await _context.Anuncios.FindAsync(id);
//            if (anuncio == null) return NotFound();
//            anuncio.Ativo = false;
//            await _context.SaveChangesAsync();
//            return Ok();
//        }

//        [HttpDelete("{id}")]
//        public async Task<IActionResult> Remover(int id)
//        {
//            var anuncio = await _context.Anuncios.FindAsync(id);
//            if (anuncio == null) return NotFound();
//            _context.Anuncios.Remove(anuncio);
//            await _context.SaveChangesAsync();
//            return Ok();
//        }

//        [HttpGet("{id}/estatisticas")]
//        public async Task<IActionResult> GetEstatisticas(int id)
//        {
//            var anuncio = await _context.Anuncios.FindAsync(id);
//            if (anuncio == null) return NotFound();
//            return Ok(new { Visualizacoes = anuncio.Visualizacoes, Contactos = anuncio.Contactos });
//        }

//        [HttpPut("{id}/destaque")]
//        public async Task<IActionResult> AtivarDestaque(int id)
//        {
//            // Verifique plano pago via subscricao
//            var anuncio = await _context.Anuncios.FindAsync(id);
//            if (anuncio == null) return NotFound();
//            anuncio.Destaque = true;
//            await _context.SaveChangesAsync();
//            return Ok();
//        }
//    }
//}