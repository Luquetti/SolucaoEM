using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EM.Domain.Interface
{
    public interface IRepositorioCidade<T> where T : IEntidade
    {
        void Add(T obj);
        void Update(T obj);
        void Remover(T obj);
        IEnumerable<T> GetAll();
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);
    }
    
    
}
