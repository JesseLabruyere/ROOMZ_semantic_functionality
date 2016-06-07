using System;
using System.Collections.Generic;
using VDS.RDF.Query;
using VDS.RDF.Nodes;
using VDS.RDF;

namespace ROOMZ
{
	public class SemanticBrowserTest
	{
		public static void Main()
		{
			Console.WriteLine ("Starting");

			if (true) 
			{
				Uri uri = new Uri ("http://localhost/wiki/hzportfoliotest/wiki/index.php/Speciaal:URIResolver/TZW-3Awijkondernemingen");
				SPARQLQueryDispatcher queryDispatcher = new SPARQLQueryDispatcher (new Uri ("http://195.93.238.56:3030/portfolios/query"));
				RemoteSPARQLStore store = new RemoteSPARQLStore (queryDispatcher);
				SemanticBrowser browser = new SemanticBrowser (store);
				browser.browse (new QueryParameter (uri), true, 1000);
				browser.browseInput (true, 1000);
			}
		}
	}
}

