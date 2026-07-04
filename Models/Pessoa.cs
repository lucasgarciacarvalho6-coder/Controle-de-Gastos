using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.SignalR;

namespace ControleDeGastosApi.Models
{
    // Esta Classe a pessoa no sistema.
    public class Pessoa
    {
        
        //Usa Guid para ID
        [Key]
        public Guid Id { get; set;} = Guid.NewGuid();

        [Required]
        public string Nome { get; set; } = string.Empty;

        [Range(0,150)]
        public int Idade { get; set; }

        // Uma pessoa pode ter várias transações
        public List <Transacao> Transacoes {get; set; } = new();
    }
}
