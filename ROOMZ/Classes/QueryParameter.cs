using System;

namespace ROOMZ
{
	/**
	 * This wrapper represents a QueryParameter and contains a value.
	 */ 
	public class QueryParameter
	{
		protected Object value;
		protected Type valueType; 


		public QueryParameter ()
		{
			setValue(new Empty());
		}

		public QueryParameter (Object value)
		{
			setValue(value);
		}

		public Object getValue () 
		{
			return value;
		}

		public void setValue (Object value) 
		{
			this.value = value;
			valueType = value.GetType();
		}

		/**
		 * Get the class type of the value object.
		 */ 
		public Type getValueType() 
		{
			return valueType;
		}
	}
}

