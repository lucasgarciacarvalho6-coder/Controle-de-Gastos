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
    public class TransacoesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TransacoesController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> Listar() => Ok(await _context.Transacoes.ToListAsync());

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] TransacaoCreateDto dto)
        {
            // Valida se o morador realmente existe
            var pessoa = await _context.Pessoas.FindAsync(dto.PessoaId);
            if (pessoa == null) return BadRequest("Erro: O morador informado não existe.");

            if (!Enum.TryParse<TipoTransacao>(dto.Tipo, true, out var tipoEnum))
                return BadRequest("Tipo inválido. Use 'Receita' ou 'Despesa'.");

            // Caso a pessoa seja menor de 18 anos, apenas despesas poderão ser cadastradas
            if (pessoa.Idade < 18 && tipoEnum == TipoTransacao.Receita)
            {
                return BadRequest("Regra do Sistema: Menores de 18 anos só podem ter despesas registradas.");
            }

            var transacao = new Transacao
            {
                Descricao = dto.Descricao,
                Valor = dto.Valor,
                Tipo = tipoEnum,
                PessoaId = dto.PessoaId
            };

            _context.Transacoes.Add(transacao);
            await _context.SaveChangesAsync();
            return Ok(transacao);
        }
    }
}