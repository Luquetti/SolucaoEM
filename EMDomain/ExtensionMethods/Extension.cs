﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebirdSql.Data.FirebirdClient;

namespace EM.Domain.ExtensionMethods
{
	public static  class Extension
	{
		public static void CreateParameter(this DbParameterCollection dbParameter, string parameterName, object value) =>
		dbParameter.Add(new FbParameter(parameterName,value));
	}
}
