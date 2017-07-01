using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CrudContatos.Sistema.Repositorio
{
    /// <summary>
    /// Interface responsável por implementar o padrão de projeto Unity of Work
    /// </summary>
    public interface IUnityOfWork<T> where T : class
    {
        int Save(T model);

        int Update(T model);

        void Delete(T model);

        IEnumerable<T> GetAll();

        T GetById(object id);

        IEnumerable<T> Where(Expression<Func<T, bool>> expression);

        IEnumerable<T> OrderBy(Expression<Func<T, bool>> expression);
    }
}