using System;
using System.ComponentModel.DataAnnotations;

namespace ControleDeGastosApi.Models
{
    // "Enum" limita as opções financeiras 
    public enum TipoTransacao
    {
        Receita,
        Despesa
    }

    public class Transacao
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Descricao { get; set; } = string.Empty;

        [Required]
        public decimal Valor { get; set; }

        [Required]
        public TipoTransacao Tipo { get; set; }

        // Vincula a movimentação ao ID do morador
        [Required]
        public Guid PessoaId { get; set; }
    }
}