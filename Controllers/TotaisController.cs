using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControleDeGastosApi.Data;
using ControleDeGastosApi.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeGastosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TotaisController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TotaisController(AppDbContext context) => _context = context;

        // Rota que calcula os saldos individuais 
        [HttpGet]
        public async Task<IActionResult> ObterTotais()
        {
            var pessoas = await _context.Pessoas.Include(p => p.Transacoes).ToListAsync();

            var relatorioPessoas = pessoas.Select(p => {
                var receitas = p.Transacoes.Where(t => t.Tipo == TipoTransacao.Receita).Sum(t => t.Valor);
                var despesas = p.Transacoes.Where(t => t.Tipo == TipoTransacao.Despesa).Sum(t => t.Valor);
                return new {
                    p.Id,
                    p.Nome,
                    TotalReceitas = receitas,
                    TotalDespesas = despesas,
                    Saldo = receitas - despesas
                };
            }).ToList();

            var receitaGeral = relatorioPessoas.Sum(x => x.TotalReceitas);
            var despesaGeral = relatorioPessoas.Sum(x => x.TotalDespesas);

            return Ok(new {
                Moradores = relatorioPessoas,
                BalancoGeral = new {
                    TotalReceitas = receitaGeral,
                    TotalDespesas = despesaGeral,
                    SaldoLiquido = receitaGeral - despesaGeral
                }
            });
        }
    }
}