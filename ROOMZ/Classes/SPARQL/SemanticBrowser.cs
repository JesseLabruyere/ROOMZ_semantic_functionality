using System;
using System.Collections.Generic;
using VDS.RDF.Query;
using VDS.RDF.Nodes;
using VDS.RDF;

namespace ROOMZ
{
	/**
	 * A browser that has functionality to browse the nodes in a SPARQL store. 
	 */ 
	public class SemanticBrowser
	{

		RemoteSPARQLStore store;

		public SemanticBrowser (RemoteSPARQLStore store)
		{
			this.store = store;
		}

		/**
		 * Returns alle the connections and usages of a node.
		 * 
		 * @param value, the node value within the QueryParameter wrapper, wrap an Uri object for uris.
		 * @param limit, if a result limit should be applied.
		 * @param limitAmount, the amount of the result limit.
		 */
		public List<SparqlResult> browseNode (QueryParameter value, bool limit, int limitAmount) 
		{
			List<SparqlResult> subjectResults = store.getTriplesByCustomCriteria (value, new QueryParameter (new Empty ()), new QueryParameter (new Empty ()), limit, (limitAmount / 3));
			List<SparqlResult> predicateResults = store.getTriplesByCustomCriteria (new QueryParameter (new Empty ()), value, new QueryParameter (new Empty ()), limit, (limitAmount / 3));
			List<SparqlResult> objectResults = store.getTriplesByCustomCriteria (new QueryParameter (new Empty ()), new QueryParameter (new Empty ()), value, limit, (limitAmount / 3));

			List<SparqlResult> results = new List<SparqlResult> ();
			results.AddRange (subjectResults);
			results.AddRange (predicateResults);
			results.AddRange (objectResults);

			return results;
		}

		/**
		 * Returns alle the connections and usages of a node, makes interactive browsing using the console possible
		 * 
		 * @param value, the node value within the QueryParameter wrapper, wrap an Uri object for uris.
		 * @param limit, if a result limit should be applied.
		 * @param limitAmount, the amount of the result limit.
		 */
		public void browse (QueryParameter value, bool limit, int limitAmount) 
		{
			List<SparqlResult> subjectResults = store.getTriplesByCustomCriteria (value, new QueryParameter (new Empty ()), new QueryParameter (new Empty ()), limit, (limitAmount / 3));
			List<SparqlResult> predicateResults = new List<SparqlResult> ();
			if (value.getValueType () == typeof(Uri)) {
				predicateResults = store.getTriplesByCustomCriteria (new QueryParameter (new Empty ()), value, new QueryParameter (new Empty ()), limit, (limitAmount / 3));
			}
			List<SparqlResult> objectResults = store.getTriplesByCustomCriteria (new QueryParameter (new Empty ()), new QueryParameter (new Empty ()), value, limit, (limitAmount / 3));

			//List<SparqlResult> results = new List<SparqlResult> ();

			Console.WriteLine ("\n\n|---------- Results for: " + value.getValue().ToString() + " ----------|");
			if (value.getValueType() == typeof (Uri)) {
				Console.WriteLine ("\n[Uri]: <" + value.getValue ().ToString () + ">");
			} else {
				Console.WriteLine ("\n[Value]: " + value.getValue ().ToString () + " ");
			}
			Console.WriteLine ("[Total results]: " + (subjectResults.Count + predicateResults.Count + objectResults.Count));

			List<SparqlResult> triples = new List<SparqlResult> ();

			for (int i = 0; i < 3; i++)
			{
				Console.WriteLine ("");
				if (i == 0)
				{
					Console.WriteLine ("---------------------------------------------------------\n");
					Console.WriteLine ("# [" + subjectResults.Count + " Properties], triples with the Uri/Value as subject \n");
					triples = subjectResults;
				} 
				else if (i == 1)
				{
					Console.WriteLine ("---------------------------------------------------------\n");
					Console.WriteLine ("# [" + predicateResults.Count + " Uses as Predicate], triples with the Uri/Value as predicate \n");
					triples = predicateResults;
				} 
				else 
				{
					Console.WriteLine ("---------------------------------------------------------\n");
					Console.WriteLine ("# [" + objectResults.Count + " Usage as Property], triples with the Uri/Value as object \n");

					/*int testsize2 = "test13333333".Length;
					Console.WriteLine ("{0,10}{1," + (60 - testsize2) + "}",
						"test13333333",
						"test2");*/
					triples = objectResults;
				}

				if (triples.Count == 0) 
				{
					Console.WriteLine ("No triples\n");
				}

				foreach (SparqlResult triple in triples) 
				{
					IEnumerable<string> variables = triple.Variables;
			
					Object subjectValue = new Empty ();
					Object predicateValue = new Empty ();
					Object objectValue = new Empty ();

					if (triple.HasBoundValue ("subject")) {
						subjectValue = triple.Value ("subject");
					}
					if (triple.HasBoundValue ("predicate")) {
						predicateValue = triple.Value ("predicate");
					}
					if (triple.HasBoundValue ("object")) {
						objectValue = triple.Value ("object");
					}

					if (i == 0) 
					{
						Console.WriteLine("[predicate]: " + (predicateValue.GetType () == typeof(UriNode) ? "<" + predicateValue.ToString() + ">" : predicateValue.ToString()) + " [object]: " + (objectValue.GetType () == typeof(UriNode) ? "<" + objectValue.ToString() + ">" : objectValue.ToString()));
					} 
					else if (i == 1) 
					{
						//Type test = subjectValue.GetType ();
						Console.WriteLine ("[subject]: " + (subjectValue.GetType () == typeof(UriNode) ? "<" + subjectValue.ToString() + ">" : subjectValue.ToString()) + " [object]: " + (objectValue.GetType () == typeof(UriNode) ? "<" + objectValue.ToString() + ">" : objectValue.ToString()));
					} 
					else 
					{
						Console.WriteLine("[subject]: " + (subjectValue.GetType () == typeof(UriNode) ? "<" + subjectValue.ToString() + ">" : subjectValue.ToString()) + " [predicate]: " + (predicateValue.GetType () == typeof(UriNode) ? "<" + predicateValue.ToString() + ">" : predicateValue.ToString()));
					}
				}
			}
			Console.WriteLine ("---------------------------------------------------------\n");
		}

		public void browseInput(bool limit, int limitAmount) 
		{
			Console.Write("\nGive a value to browse to: ");
			string browseTo = Console.ReadLine();

			if (browseTo.Length > 0) {
				Uri outUri;
				if (Uri.TryCreate (browseTo, UriKind.Absolute, out outUri)) {
					browse (new QueryParameter (outUri), limit, limitAmount);
				} else {
					browse (new QueryParameter (browseTo), limit, limitAmount);
				}
			} else {
				Console.Write("\nNo value given.");
			}
			browseInput(limit, limitAmount);
		}
	}
}

