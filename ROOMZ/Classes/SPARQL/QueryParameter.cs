using System;

namespace ROOMZ
{
	public class QueryParameter
	{
		protected Object value;
		protected Type valueType; 

		public QueryParameter (Object value)
		{
			this.value = value;
			this.valueType = value.GetType();
		}

		public Object getValue () 
		{
			return value;
		}

		public void setValue (Object value) 
		{
			this.value = value;
		}

		public Type getValueType() 
		{
			return valueType;
		}
	}
}

