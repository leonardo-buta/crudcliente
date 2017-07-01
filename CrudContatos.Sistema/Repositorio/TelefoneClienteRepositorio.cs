using CrudContatos.Sistema.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudContatos.Sistema.Repositorio
{
    /// <summary>
    /// Classe utilizada para o repositório de telefones
    /// </summary>
    public class TelefoneClienteRepositorio : CrudContext<TelefoneCliente>, IUnityOfWork<TelefoneCliente>
    {

    }
}
