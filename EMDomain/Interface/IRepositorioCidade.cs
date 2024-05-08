using System.Linq.Expressions;

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
