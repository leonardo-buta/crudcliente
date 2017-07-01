using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudContatos.Sistema.Entidades
{
    /// <summary>
    /// Classe responsável pelo modelo do telefone dos clientes
    /// </summary>
    [Table("TelefonesCliente")]
    public class TelefoneCliente
    {
        public TelefoneCliente()
        {
            
        }

        [Key]
        public int CodTelefoneCliente { get; set; }

        public string Telefone { get; set; }

        public int CodCliente { get; set; }

        // Chave Estrangeira
        [ForeignKey("CodCliente")]
        public virtual Cliente Cliente { get; set; }
    }
}
