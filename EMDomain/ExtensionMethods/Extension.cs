using System.Data.Common;
using FirebirdSql.Data.FirebirdClient;

namespace EM.Domain.ExtensionMethods
{
    public static  class Extension
	{
		public static void CreateParameter(this DbParameterCollection dbParameter, string parameterName, object value) =>
		dbParameter.Add(new FbParameter(parameterName,value));
	}
}
