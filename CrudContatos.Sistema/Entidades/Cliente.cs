using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudContatos.Sistema.Entidades
{
    /// <summary>
    /// Classe responsável pelo modelo do Cliente
    /// </summary>
    [Table("Clientes")]
    public class Cliente
    {
        public Cliente()
        {
            TelefoneCliente = new List<TelefoneCliente>();
            //Telefones = new List<string>();
        }

        [Key]
        public int CodCliente { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        [MaxLength(255)]
        public string Email { get; set; }

        public DateTime DataCriacao { get; set; }

        public virtual List<TelefoneCliente> TelefoneCliente { get; set; }

        /// Campo utilizado para armazenar os telefones em uma textarea
        [NotMapped]
        [DataType(DataType.MultilineText)]
        public string Telefones { get; set; }
    }
}
