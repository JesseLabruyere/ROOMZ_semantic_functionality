using System;
using System.Net.Http;

namespace ROOMZ
{
	public abstract class AbstractFileFactory
	{
		public File file { get; protected set;}

		public Auth auth { get; protected set;}

		public abstract Auth Authenticate();

		public abstract File newFile();

	}



}

