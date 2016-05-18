using System;
using System.Net.Http;
using System.Collections.Generic;

namespace ROOMZ
{

	public class Auth
	{
		private HttpClient _client;

		public Auth ()
		{
			_client = new HttpClient ();
			
		}

		public async void SendApiCall (KeyValuePair <string,string> input, string call)
		{
			Console.WriteLine ("We got here");
			
			var requestContent = new FormUrlEncodedContent (new[] {
				input
			});

			HttpResponseMessage response = await _client.PostAsync (
				                               call,
				                               requestContent
			                               );
			HttpContent responseContent = response.Content;
			Console.WriteLine ("Starting with the using stuff now ");

			using (var reader = new System.IO.StreamReader (await responseContent.ReadAsStreamAsync ())) {
				Console.WriteLine ("asdfasdfasdfasdf");
				Console.WriteLine (await reader.ReadToEndAsync ());

			}
			
			
		}
		
	}




}

