using System;

namespace ROOMZ
{
	public class IntParameter
	{
		
		private int value;

		public IntParameter ()
		{
		}

		/**
		 * @override
		 */
		public int getValue() 
		{
			return value;
		}

		/**
		 * @override
		 */
		public void setValue(int value) 
		{
			this.value = value;
		}
	}
}

