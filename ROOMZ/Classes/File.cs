using System;
using System.Net.Http;

namespace ROOMZ
{
	public abstract class File
	{

	public abstract bool SaveFile (string @path, string[] input);

	public abstract bool DeleteFile (string @path);

	public abstract string[] GetFileContents (string @path);

	}

}

