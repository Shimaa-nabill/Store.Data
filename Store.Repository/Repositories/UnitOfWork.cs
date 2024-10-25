using Store.Data.Contexts;
using Store.Data.Entities;
using Store.Repository.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _context;

        private Hashtable _repositories ;
        public UnitOfWork(StoreDbContext context)
        {
           _context = context;
        }
        public async Task<int> CountAsync()
        => await _context.SaveChangesAsync();

        public IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
            
        {
            if (_repositories is null)
            
                _repositories = new Hashtable();

                var entitykey = typeof(TEntity).Name;

                if (!_repositories.ContainsKey(entitykey))
                {
                    var repositorytype = typeof(GenericRepository<,>);
                    var repostitoryInstance = Activator.CreateInstance(repositorytype.MakeGenericType(typeof(TEntity),typeof(TKey)),_context);

                    _repositories.Add(entitykey, repostitoryInstance);


                }


                 return (IGenericRepository<TEntity, TKey>)_repositories[entitykey];

        }


    }




    
}
