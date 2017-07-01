using CrudContatos.Sistema.Entidades;

namespace CrudContatos.Sistema.Repositorio
{
    /// <summary>
    /// Classe utilizada para o repositório de clientes
    /// </summary>
    public class ClienteRepositorio : CrudContext<Cliente>, IUnityOfWork<Cliente>
    {
        
    }
}
