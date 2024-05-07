﻿
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebirdSql.Data.FirebirdClient;
namespace EM.REPOSITORY.ConexaoBancoDeDados
{
	public class BancoDeDados
	{
		private static string _caminho = @"Server=localhost; Port=3054;Database=C:\\Work.Luquetti\\Bd\\CADASTRARALUNO.fdb;User=SYSDBA;Password=masterkey;";
        
		
		private static FbConnection? conn = null;
		public static FbConnection GetConexao()
		{
			if (conn == null || conn.State != System.Data.ConnectionState.Open)
			{
				FbConnection.ClearAllPools();
				conn = new FbConnection(_caminho);
				conn.Open();
			}
			return conn;
		}
	}
}