using CrudContatos.Sistema.Entidades;
using System.Collections.Generic;

namespace CRUDContatos.Models
{
    /// <summary>
    /// Classe responsável por agregar a model do cliente e a paginação
    /// </summary>
    public class ClienteViewModel
    {
        public IEnumerable<Cliente> Clientes { get; set; }

        public Paginacao Paginacao { get; set; }
    }
}