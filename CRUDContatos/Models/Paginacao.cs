using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUDContatos.Models
{
    /// <summary>
    /// Classe responsável por criar a paginação de um item
    /// </summary>
    public class Paginacao
    {
        public int ItensTotal { get; set; }

        public int ItensPorPagina { get; set; }

        public int PaginaAtual { get; set; }

        public int TotalPaginas
        {
            get
            {
                return (int)Math.Ceiling((decimal)ItensTotal / ItensPorPagina);
            }
        }
    }
}