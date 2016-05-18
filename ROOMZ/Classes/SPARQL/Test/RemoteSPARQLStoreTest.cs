using System;
using System.Collections.Generic;
using VDS.RDF.Query;
using VDS.RDF.Nodes;
using VDS.RDF;

namespace ROOMZ
{
	public class RemoteSPARQLStoreTest
	{
		public static void Main()
		{
			Console.WriteLine ("Starting");
		
			// Test: getTriplesBySubject
			if (false) 
			{
				SPARQLQueryDispatcher queryDispatcher = new SPARQLQueryDispatcher (new Uri ("http://195.93.238.56:3030/portfolios/query"));
				RemoteSPARQLStore store = new RemoteSPARQLStore (queryDispatcher);
				List<SparqlResult> triples = store.getTriplesBySubject (new Uri("http://localhost/wiki/hzportfoliotest/wiki/index.php/Speciaal:URIResolver/Property-3AAddress"), true, 20);
				Console.WriteLine (triples.Count);
				foreach(SparqlResult triple in triples) 
				{
					Console.WriteLine (triple);
				}
			}

			// Test: getTriplesByPredicate
			if (false) 
			{
				SPARQLQueryDispatcher queryDispatcher = new SPARQLQueryDispatcher (new Uri ("http://195.93.238.56:3030/portfolios/query"));
				RemoteSPARQLStore store = new RemoteSPARQLStore (queryDispatcher);
				List<SparqlResult> triples = store.getTriplesByPredicate(new Uri("http://www.w3.org/1999/02/22-rdf-syntax-ns#type"), true, 20);
				Console.WriteLine (triples.Count);
				foreach(SparqlResult triple in triples) 
				{
					Console.WriteLine (triple);
				}
			}

			// Test: getTriplesByObject
			if (false) 
			{
				SPARQLQueryDispatcher queryDispatcher = new SPARQLQueryDispatcher (new Uri ("http://195.93.238.56:3030/portfolios/query"));
				RemoteSPARQLStore store = new RemoteSPARQLStore (queryDispatcher);
				List<SparqlResult> triples = store.getTriplesByObject(new Uri("http://www.w3.org/2002/07/owl#DatatypeProperty"), true, 20);
				Console.WriteLine (triples.Count);
				foreach(SparqlResult triple in triples) 
				{
					Console.WriteLine (triple);
				}
			}

			// Test: getTriplesBySubjectAndObject
			if (false) 
			{
				SPARQLQueryDispatcher queryDispatcher = new SPARQLQueryDispatcher (new Uri ("http://195.93.238.56:3030/portfolios/query"));
				RemoteSPARQLStore store = new RemoteSPARQLStore (queryDispatcher);
				List<SparqlResult> triples = store.getTriplesBySubjectAndObject (new Uri("http://localhost/wiki/hzportfoliotest/wiki/index.php/Speciaal:URIResolver/Property-3AAddress"), true, 20);
				Console.WriteLine (triples.Count);
				foreach(SparqlResult triple in triples) 
				{
					Console.WriteLine (triple);
				}
			}

			// Test: getPredicatesBySubject
			if (false)
			{
				SPARQLQueryDispatcher queryDispatcher = new SPARQLQueryDispatcher (new Uri ("http://195.93.238.56:3030/portfolios/query"));
				RemoteSPARQLStore store = new RemoteSPARQLStore (queryDispatcher);
				List<SparqlResult> triples = store.getPredicatesBySubject (new Uri("http://localhost/wiki/hzportfoliotest/wiki/index.php/Speciaal:URIResolver/Property-3AAddress"), true, 20);
				Console.WriteLine (triples.Count);
				foreach(SparqlResult triple in triples) 
				{
					Console.WriteLine (triple);
				}
			}

			// Test: getPredicatesByObject
			if (false)
			{
				SPARQLQueryDispatcher queryDispatcher = new SPARQLQueryDispatcher (new Uri ("http://195.93.238.56:3030/portfolios/query"));
				RemoteSPARQLStore store = new RemoteSPARQLStore (queryDispatcher);
				List<SparqlResult> triples = store.getPredicatesByObject (new Uri("http://www.w3.org/2002/07/owl#DatatypeProperty"), true, 20);
				Console.WriteLine (triples.Count);
				foreach(SparqlResult triple in triples) 
				{
					Console.WriteLine (triple);
				}
			}

			// Test: getPredicatesBySubjectAndObject
			if (false)
			{
				SPARQLQueryDispatcher queryDispatcher = new SPARQLQueryDispatcher (new Uri ("http://195.93.238.56:3030/portfolios/query"));
				RemoteSPARQLStore store = new RemoteSPARQLStore (queryDispatcher);
				List<SparqlResult> triples = store.getPredicatesBySubjectAndObject (new Uri("http://localhost/wiki/hzportfoliotest/wiki/index.php/Speciaal:URIResolver/Property-3AAddress"), true, 20);
				Console.WriteLine (triples.Count);
				foreach(SparqlResult triple in triples) 
				{
					Console.WriteLine (triple);
				}
			}
				
			// Test1: getPredicatesByCriteria
			if (false)
			{
				SPARQLQueryDispatcher queryDispatcher = new SPARQLQueryDispatcher (new Uri ("http://195.93.238.56:3030/portfolios/query"));
				RemoteSPARQLStore store = new RemoteSPARQLStore (queryDispatcher);
				List<SparqlResult> triples = store.getTriplesByCriteria (new QueryParameter(new Uri("http://localhost/wiki/hzportfoliotest/wiki/index.php/Speciaal:URIResolver/Property-3AAddress")), new QueryParameter(new Empty()), new QueryParameter(new Empty()), true, 20);
				Console.WriteLine (triples.Count);
				foreach(SparqlResult triple in triples) 
				{
					Console.WriteLine (triple);
				}
			}

			// Test2: getPredicatesByCriteria
			if (true)
			{
				SPARQLQueryDispatcher queryDispatcher = new SPARQLQueryDispatcher (new Uri ("http://195.93.238.56:3030/portfolios/query"));
				RemoteSPARQLStore store = new RemoteSPARQLStore (queryDispatcher);
				List<SparqlResult> triples = store.getTriplesByCriteria (new QueryParameter(new Empty()), new QueryParameter(new Empty()), new QueryParameter("Address"), true, 20);
				Console.WriteLine (triples.Count);
				foreach(SparqlResult triple in triples) 
				{
					Console.WriteLine (triple);
				}
			}



		}
	}
}

