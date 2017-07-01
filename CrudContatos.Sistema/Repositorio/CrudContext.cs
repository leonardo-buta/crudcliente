using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace CrudContatos.Sistema.Repositorio
{
    /// <summary>
    /// Classe responsável pelo acesso ao banco no padrão de projeto Unit of Work
    /// </summary>
    public class CrudContext<T> : DbContext where T : class
    {
        #region Construtor
        public CrudContext() : base("EFConnectionString")
        {
            
        }
        #endregion

        #region Propriedades
        public DbSet<T> DbSet { get; set; }
        #endregion

        #region Metodos
        public virtual void ChangeObjectState(object model, EntityState state)
        {
            ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager.ChangeObjectState(model, state);
        }
        
        /// <summary>
        /// Salva um registro no banco
        /// </summary>
        public virtual int Save(T modelo)
        {
            DbSet.Add(modelo);
            return SaveChanges();
        }

        /// <summary>
        /// Atualiza um registro no banco
        /// </summary>
        public virtual int Update(T modelo)
        {
            string[] camposExcluidos = new string[] { "DataCriacao" };
            var entry = Entry(modelo);            

            if (entry.State == EntityState.Detached)
            {
                DbSet.Attach(modelo);
            }

            ChangeObjectState(modelo, EntityState.Modified);

            // Realiza um laço para não atualizar determinados campos
            foreach (string campoExcluido in camposExcluidos)
            {
                entry.Property(campoExcluido).IsModified = false;
            }            

            return SaveChanges();
        }

        /// <summary>
        /// Apaga um registro no banco
        /// </summary>
        public virtual void Delete(T modelo)
        {
            var entry = Entry(modelo);

            if (entry.State == EntityState.Detached)
            {
                DbSet.Attach(modelo);
            }
            ChangeObjectState(modelo, EntityState.Deleted);
            SaveChanges();
        }

        /// <summary>
        /// Retorna todos os registros do banco
        /// </summary>
        public virtual IEnumerable<T> GetAll()
        {
            return DbSet.ToList();
        }

        /// <summary>
        /// Retorna um registro único do banco
        /// </summary>
        public virtual T GetById(object id)
        {
            return DbSet.Find(id);
        }

        /// <summary>
        /// Retorna um enumerable com uma condição WHERE
        /// </summary>
        public virtual IEnumerable<T> Where(Expression<Func<T, bool>> expression)
        {
            return DbSet.Where(expression);
        }

        /// <summary>
        /// Retorna um enumerable ordenado pelo ORDER BY
        /// </summary>
        public IEnumerable<T> OrderBy(Expression<Func<T, bool>> expression)
        {
            return DbSet.OrderBy(expression);
        }

        #endregion
    }
}
