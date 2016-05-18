using System;

namespace ROOMZ
{

	public class LocalFileFactory : AbstractFileFactory
	{
		#region implemented abstract members of AbstractFileFactory

		public override Auth Authenticate ()
		{
			this.auth = new Auth();
			return this.auth;
		}

		public override File newFile ()
		{
			this.file = new LocalFile();
			return this.file;
		}


		#endregion
					

	}

}
