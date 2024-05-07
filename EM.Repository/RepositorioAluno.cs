using System.Data.Common;
using System.Linq.Expressions;
using EM.Domain;
using EM.Domain.Enum;
using EM.Domain.ExtensionMethods;
using EM.REPOSITORY.ConexaoBancoDeDados;
using EM.Domain.Interface;

namespace EM.Repository
{
	public class RepositorioAluno : IRepositorioAluno<Aluno>

	{
	

		public  void Add(Aluno aluno)
		{
            
           
        
            using DbConnection cn = BancoDeDados.GetConexao();
            using DbCommand cmd = cn.CreateCommand();
            cmd.CommandText = "INSERT INTO ALUNO ( NOME, CIDADES, CPF, DATANASCIMENTO, SEXO) " +
                                 "VALUES ( @NomeCidade, @Cidades, @CPF, @DataNascimento, @Sexo)";

            cmd.Parameters.CreateParameter("@NomeCidade", aluno.Nome);
      
			cmd.Parameters.CreateParameter("@CPF", aluno.CPF);
			cmd.Parameters.CreateParameter("@DataNascimento",aluno.Nascimento);
			cmd.Parameters.CreateParameter("@Sexo", aluno.Sexo);
			cmd.Parameters.CreateParameter("@Cidades", aluno.Cidade.ID_Cidade);

            cmd.ExecuteNonQuery();
        



    }

    public IEnumerable<Aluno> GetByMatricula(int matricula)
		{
			using DbConnection cn = BancoDeDados.GetConexao();
			using DbCommand cmd =cn.CreateCommand();

			cmd.CommandText = @"SELECT A.matricula, A.nome, A.sexo, A.dataNascimento, A.CPF, C.nome AS nomeCidade, C.UF as UFCidade FROM Aluno A
							INNER JOIN
					Cidades C ON A.cidades = C.ID WHERE A.matricula = @matricula order by A.matricula asc";
			        List<Aluno>alunos= new List<Aluno>();
				 cmd.Parameters.CreateParameter("@matricula", matricula);
					DbDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
						
							{
								Aluno aluno = new Aluno();
								aluno.Matricula = Convert.ToInt32(rdr["matricula"]);
								aluno.Nome = rdr["nome"].ToString();

								aluno.CPF = rdr["CPF"].ToString();
								aluno.Nascimento = Convert.ToDateTime(rdr["dataNascimento"]);
								aluno.Sexo = (Sexo)rdr.GetInt32(rdr.GetOrdinal("SEXO"));
								// Preencher as informações da cidade associada ao aluno
								aluno.Cidade = new Cidade();
								aluno.Cidade.NomeCidade = rdr["NomeCidade"].ToString();
								aluno.Cidade.UF = rdr["UFCidade"].ToString();
								alunos.Add(aluno);
							}
						rdr.Close();
						
					
				

				return alunos;
			
		}


		public IEnumerable<Aluno> GetContendoNoNome(string nome)
		{
			using DbConnection cn = BancoDeDados.GetConexao();
			using DbCommand cmd=cn.CreateCommand();	
			
				cmd.CommandText = @"SELECT A.matricula, A.nome, A.sexo, A.dataNascimento, A.CPF, C.nome AS nomeCidade, C.UF as UFCidade FROM Aluno A
							INNER JOIN
					Cidades C ON A.cidadeS = C.ID WHERE A.nome LIKE '%'|| @nome ||'%' order by A.matricula asc";

			List<Aluno> alunos = new List<Aluno>();
			cmd.Parameters.CreateParameter("@nome",nome);
			DbDataReader rdr = cmd.ExecuteReader();

					
					
					while (rdr.Read())
					
						{
							Aluno aluno = new Aluno();
							aluno.Matricula = Convert.ToInt32(rdr["matricula"]);
							aluno.Nome = rdr["nome"].ToString();

							aluno.CPF = rdr["CPF"].ToString();
							aluno.Nascimento = Convert.ToDateTime(rdr["dataNascimento"]);
							aluno.Sexo = (Sexo)rdr.GetInt32(rdr.GetOrdinal("SEXO"));
							// Preencher as informações da cidade associada ao aluno
							aluno.Cidade = new Cidade();
							aluno.Cidade.NomeCidade = rdr["NomeCidade"].ToString();
							aluno.Cidade.UF = rdr["UFCidade"].ToString();
							alunos.Add(aluno);
						}
					rdr.Close();


					

				return alunos;
			
		}

		





		public  IEnumerable<Aluno> GetAll()
		{
			using DbConnection cn = BancoDeDados.GetConexao();
			using DbCommand cmd = cn.CreateCommand();
			cmd.CommandText = @"SELECT  A.matricula, A.nome, A.sexo, A.dataNascimento, A.CPF, C.nome AS nomeCidade, C.UF as UFCidade, C.ID FROM Aluno A
							INNER JOIN
					Cidades C ON A.cidadeS = C.ID order by A.matricula asc";
			List<Aluno>alunos = new List<Aluno>();
			DbDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
					
						{
							Aluno aluno = new Aluno();
							aluno.Matricula = Convert.ToInt32(rdr["matricula"]);
							aluno.Nome = rdr["nome"].ToString();

							aluno.CPF = rdr["CPF"].ToString();
							aluno.Nascimento = Convert.ToDateTime(rdr["dataNascimento"]);
							aluno.Sexo = (Sexo)rdr.GetInt32(rdr.GetOrdinal("SEXO"));
							// Preencher as informações da cidade associada ao aluno
							aluno.Cidade = new Cidade();
							aluno.Cidade.NomeCidade = rdr["NomeCidade"].ToString();
							aluno.Cidade.UF = rdr["UFCidade"].ToString();
							aluno.Cidade.ID_Cidade = Convert.ToInt32(rdr["ID"]);

							alunos.Add(aluno);
			}


			rdr.Close();
			


			return alunos;
		}


		public  void Remover(Aluno aluno)
		{
			using DbConnection cn = BancoDeDados.GetConexao();
			using DbCommand cmd = cn.CreateCommand();
			cmd.CommandText = "DELETE FROM ALUNO WHERE MATRICULA= @MATRICULA";
			try
			{
				cmd.Parameters.CreateParameter("@MATRICULA", aluno.Matricula);
				cmd.ExecuteNonQuery();
                Console.WriteLine("Aluno removido com Sucesso ");
            }
			catch(Exception erro)
			{
                Console.WriteLine($"Erro ao deletar aluno,detalhe{erro}");
            }
		}

		public void Update(Aluno aluno)
		{
            using DbConnection cn = BancoDeDados.GetConexao();
            using DbCommand cmd = cn.CreateCommand();
            cmd.CommandText = @"UPDATE Aluno SET Nome = @Nome, CPF = @CPF, DataNascimento = @DataNascimento, Sexo = @Sexo, Cidades = @Cidades  WHERE Matricula = @Matricula";
            cmd.Parameters.CreateParameter("@Matricula", aluno.Matricula);
            cmd.Parameters.CreateParameter("@Nome", aluno.Nome);
            cmd.Parameters.CreateParameter("@Cidades", aluno.Cidade.ID_Cidade);
            cmd.Parameters.CreateParameter("@CPF", aluno.CPF);
            cmd.Parameters.CreateParameter("@DataNascimento", aluno.Nascimento);
            cmd.Parameters.CreateParameter("@Sexo", aluno.Sexo);
            cmd.ExecuteNonQuery();
        }
        public  IEnumerable<Aluno> Get(Expression<Func<Aluno, bool>> predicate)
        {
            return GetAll().Where(predicate.Compile());
        }
       
            }
		
	
}
