using Store.Data.Entities;
using Store.Repository.Specification;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Interfaces
{
   public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity <TKey>
    {

        Task<TEntity> GetByIdAsync (TKey? id);

        
        Task< IReadOnlyList <TEntity>> GetAllAsync();

        Task AddAsync (TEntity entity);
       void Update (TEntity entity);
        void Delete (TEntity entity);

        Task<int> GetCountSpecificationsAsync(ISpecification<TEntity> specs);
        Task<TEntity> GetWithSpectificationByIdAsync(ISpecification<TEntity>specs);


        Task<IReadOnlyList<TEntity>> GetWithSpectificationAllAsync(ISpecification<TEntity> specs);



    }
}
