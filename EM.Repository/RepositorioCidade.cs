using System.Data.Common;
using System.Linq.Expressions;
using EM.Domain;
using EM.REPOSITORY.ConexaoBancoDeDados;
using EM.Domain.ExtensionMethods;
using EM.Domain.Interface;



namespace EM.Repository
{
	public class RepositorioCidade : IRepositorioCidade<Cidade>
    {
		
		public   void Add(Cidade cidade)
        {
            using DbConnection cn = BancoDeDados.GetConexao();
			using DbCommand cmd = cn.CreateCommand();
            cmd.CommandText = "INSERT INTO CIDADES(NOME,UF) VALUES(@NomeCidade,@UF)";// o primeiro se refere a como esta na tabela , o segundo é como eu desejo repassar esses valores

            cmd.Parameters.CreateParameter("@NomeCidade",cidade.NomeCidade);//o parametro q criei no sql acima,cidade é o valor passado no parametro da funçao, nomeCidade é uma propriedade da instancia  
            cmd.Parameters.CreateParameter("@UF", cidade.UF);
            
            cmd.ExecuteNonQuery();
        }

	

		public  IEnumerable<Cidade> Get(Expression<Func<Cidade, bool>> predicate)
        {
           return GetAll().Where(predicate.Compile());
        }

        public  IEnumerable<Cidade> GetAll()
        {
            using DbConnection cn = BancoDeDados.GetConexao();
            using DbCommand cmd = cn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM CIDADES ORDER BY CIDADES.UF, CIDADES.NOME";
            List<Cidade> cidades = new List<Cidade>();
            DbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Cidade cidade = new Cidade();
                cidade.ID_Cidade = Convert.ToInt32(reader["ID"]);
                cidade.NomeCidade = reader["nome"].ToString();
                cidade.UF = reader["UF"].ToString();
                cidades.Add(cidade);
            }
            reader.Close();

            return cidades;
        }


        public  void Remover(Cidade cidade)
        {
            throw new NotImplementedException();
        }

        public void Update(Cidade cidade)
        {
            using DbConnection cn = BancoDeDados.GetConexao();
            using DbCommand cmd = cn.CreateCommand();
            cmd.CommandText = @"UPDATE CIDADES SET NOME=@Nome,UF=@UF WHERE ID=@ID_Cidade";
            cmd.Parameters.CreateParameter("@Nome", cidade.NomeCidade);
            cmd.Parameters.CreateParameter("@UF",cidade.UF);
            cmd.Parameters.CreateParameter("@ID_Cidade", cidade.ID_Cidade);
            cmd.ExecuteNonQuery();

        }
    }
}
