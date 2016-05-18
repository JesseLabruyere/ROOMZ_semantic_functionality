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

			// Test1: browse
			if (false)
			{
				Uri uri = new Uri ("http://localhost/wiki/hzportfoliotest/wiki/index.php/Speciaal:URIResolver/Property-3AAddress");
				SPARQLQueryDispatcher queryDispatcher = new SPARQLQueryDispatcher (new Uri ("http://195.93.238.56:3030/portfolios/query"));
				RemoteSPARQLStore store = new RemoteSPARQLStore (queryDispatcher);
				SemanticBrowser browser = new SemanticBrowser (store);
				//List<SparqlResult> triples = browser.browseInteractive (new QueryParameter(uri), false, 0);
				browser.browse (new QueryParameter(uri), false, 0);

				/*
				Console.WriteLine ("");
				Console.WriteLine ("");
				Console.WriteLine ("|---------- Results ----------|");
				Console.WriteLine ("");
				Console.WriteLine ("[Uri]: <" + uri.ToString() + ">");
				Console.WriteLine ("[TripleCount]: " + triples.Count);

				foreach(SparqlResult triple in triples) 
				{
					IEnumerable<string> variables = triple.Variables;
					bool test = triple.HasBoundValue("subject");

					Object subjectValue = new Empty();
					Object predicateValue = new Empty();
					Object objectValue = new Empty();

					if (triple.HasBoundValue ("subject")) 
					{
						subjectValue = triple.Value ("subject");
					}
					if (triple.HasBoundValue ("predicate")) 
					{
						predicateValue = triple.Value ("predicate");
					}
					if (triple.HasBoundValue ("object")) 
					{
						objectValue = triple.Value ("predicate");
					}

					Console.WriteLine (triple);
				} */
			}

			if (true) 
			{
				Uri uri = new Uri ("http://localhost/wiki/hzportfoliotest/wiki/index.php/Speciaal:URIResolver/Property-3AAddress");
				SPARQLQueryDispatcher queryDispatcher = new SPARQLQueryDispatcher (new Uri ("http://195.93.238.56:3030/portfolios/query"));
				RemoteSPARQLStore store = new RemoteSPARQLStore (queryDispatcher);
				SemanticBrowser browser = new SemanticBrowser (store);
				//List<SparqlResult> triples = browser.browseInteractive (new QueryParameter(uri), false, 0);
				browser.browse (new QueryParameter (uri), false, 0);
				browser.browseInput (false, 0);
			}
		}
	}
}

