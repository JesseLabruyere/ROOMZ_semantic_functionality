using System;

namespace ROOMZ
{
	public class EmptyParameter: QueryParameter
	{
		public EmptyParameter ()
		{
		}
			
		/**
		 * @override
		 */
		public Uri getValue() 
		{
			return "";
		}
	}
}

