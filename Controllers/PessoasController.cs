using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControleDeGastosApi.Data;
using ControleDeGastosApi.DTOs;
using ControleDeGastosApi.Models;
using System;
using System.Threading.Tasks;

namespace ControleDeGastosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PessoasController(AppDbContext context) => _context = context;

        //Rota Front-end listar todos os moradores
        [HttpGet]
        public async Task<IActionResult> Listar() => Ok(await _context.Pessoas.ToListAsync());

        //Rota Cadastrar morador
        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] PessoaCreateDto dto)
        {
            var pessoa = new Pessoa { Nome = dto.Nome, Idade = dto.Idade };
            _context.Pessoas.Add(pessoa);
            await _context.SaveChangesAsync();
            return Ok(pessoa);
        }

        // Rota deletar morador
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            var pessoa = await _context.Pessoas.FindAsync(id);
            if (pessoa == null) return NotFound("Ops! Esse morador não foi encontrado.");

            _context.Pessoas.Remove(pessoa);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}