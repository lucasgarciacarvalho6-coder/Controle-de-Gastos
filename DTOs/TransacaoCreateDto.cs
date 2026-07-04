using System;

namespace ControleDeGastosApi.DTOs
{
    //entrada de gastos/Receita
    public record TransacaoCreateDto(string Descricao, decimal Valor, string Tipo, Guid PessoaId);

}