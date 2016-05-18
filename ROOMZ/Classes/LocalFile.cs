using System;

namespace ROOMZ
{

	public class LocalFile: File
	{
		#region implemented abstract members of File
		public override bool SaveFile (string @path, string[] input)
		{
			try {
				System.IO.File.WriteAllLines (@path, input);
				return true;
			} catch (Exception e) {
				Console.WriteLine (e);
				return false;
			}

		}

		public override bool DeleteFile (string @path)
		{
			try{
				System.IO.File.Delete (@path);
				return true;
			}catch(Exception e){
				Console.WriteLine (e);
				return false;
			}
		}

		public override string[] GetFileContents (string @path)
		{
			try{
				return System.IO.File.ReadAllLines (@path);
			}catch(Exception e){
				Console.WriteLine (e);
				return null;
			}
		}
		#endregion
		
	}


}
