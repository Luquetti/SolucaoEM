using System.Linq.Expressions;

namespace EM.Domain.Interface
{
    public interface IRepositorioAluno<T> where T : IEntidade
    {
        public void Add(T obj);
        public void Remover(T obj);
        public void Update(T obj);
        public IEnumerable<T> GetAll();
        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate);
        public IEnumerable<Aluno> GetContendoNoNome(string nome);
        public IEnumerable<Aluno> GetByMatricula(int matricula);
      
    }
    
    
}
