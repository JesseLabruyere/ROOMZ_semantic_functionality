using System;

namespace ROOMZ
{
	public class SPARQLEndpoint
	{
		protected Uri queryUri;
		protected Uri updateUri;

		public SPARQLEndpoint (Uri queryUri)
		{
			this.queryUri = queryUri;
			this.updateUri = updateUri;
		}

		public SPARQLEndpoint (Uri queryUri, Uri updateUri)
		{
			this.queryUri = queryUri;
			this.updateUri = updateUri;
		}

		public Uri getQueryUri () 
		{
			return queryUri;
		}

		public Uri getUpdateUri () 
		{
			return updateUri;
		}

		public void setQueryUri (Uri queryUri) 
		{
			this.queryUri = queryUri;
		}

		public void setUpdateUri (Uri updateUri) 
		{
			this.updateUri = updateUri;
		}
	}
}

