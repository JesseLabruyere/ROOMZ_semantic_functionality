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
			List<SparqlResult> subjectResults = store.getTriplesByCustomCriteria (value, new QueryParameter (), new QueryParameter (), limit, (limitAmount / 3));
			List<SparqlResult> predicateResults = store.getTriplesByCustomCriteria (new QueryParameter (), value, new QueryParameter (), limit, (limitAmount / 3));
			List<SparqlResult> objectResults = store.getTriplesByCustomCriteria (new QueryParameter (), new QueryParameter (), value, limit, (limitAmount / 3));

			List<SparqlResult> results = new List<SparqlResult> ();
			results.AddRange (subjectResults);
			results.AddRange (predicateResults);
			results.AddRange (objectResults);

			return results;
		}

		/**
		 * Prints all the triples of a given node or value.
		 * 
		 * @param value, the node value within the QueryParameter wrapper, wrap an Uri object for uris.
		 * @param limit, if a result limit should be applied.
		 * @param limitAmount, the amount of the result limit.
		 */
		public void browse (QueryParameter value, bool limit, int limitAmount) 
		{
			List<SparqlResult> subjectResults = store.getTriplesByCustomCriteria (value, new QueryParameter (), new QueryParameter (), limit, (limitAmount / 3));
			List<SparqlResult> predicateResults = new List<SparqlResult> ();
			// The predicate needs to be an Uri otherwise it will throw an error.
			if (value.getValueType () == typeof(Uri)) {
				predicateResults = store.getTriplesByCustomCriteria (new QueryParameter (), value, new QueryParameter (), limit, (limitAmount / 3));
			}
			List<SparqlResult> objectResults = store.getTriplesByCustomCriteria (new QueryParameter (), new QueryParameter (), value, limit, (limitAmount / 3));

			printBrowseResults (value, subjectResults, predicateResults, objectResults);
		}

		/**
		 * Prints the data of a browse action.
		 * 
		 * @param value, the browse value.
		 * @param subjectResults, a list with triples with the value as subject.
		 * @param predicateResults, a list with triples with the value as predicate.
		 * @param objectResults, a list with triples with the value as object.
		 */ 
		private void printBrowseResults(QueryParameter value, List<SparqlResult> subjectResults, List<SparqlResult> predicateResults, List<SparqlResult> objectResults) 
		{
			Console.WriteLine ("\n\n|---------- Results for: " + value.getValue().ToString() + " ----------|");
			if (value.getValueType() == typeof (Uri)) {
				Console.WriteLine ("\n[Uri]: <" + value.getValue ().ToString () + ">");
			} else {
				Console.WriteLine ("\n[Value]: " + value.getValue ().ToString () + " ");
			}
			Console.WriteLine ("[Total results]: " + (subjectResults.Count + predicateResults.Count + objectResults.Count));

			for (int i = 0; i < 3; i++)
			{
				Console.WriteLine ("");
				if (i == 0)
				{
					Console.WriteLine ("---------------------------------------------------------\n");
					Console.WriteLine ("# [" + subjectResults.Count + " Properties], triples with the Uri/Value as subject\n");
					printTriples(subjectResults, "subject");
				} 
				else if (i == 1)
				{
					Console.WriteLine ("---------------------------------------------------------\n");
					Console.WriteLine ("# [" + predicateResults.Count + " Uses as Predicate], triples with the Uri/Value as predicate\n");
					printTriples(predicateResults, "predicate");
				} 
				else 
				{
					Console.WriteLine ("---------------------------------------------------------\n");
					Console.WriteLine ("# [" + objectResults.Count + " Usage as Property], triples with the Uri/Value as object\n");
					printTriples(objectResults, "object");
				}
			}
			Console.WriteLine ("---------------------------------------------------------\n");
		}

		/**
		 * Prints a list of SparqlResults
		 * 
		 * @param triples, the list with triples.
		 * @param resultType, subject, predicate or object.
		 */
		private void printTriples(List<SparqlResult> triples, string resultType ) 
		{
			if (triples.Count == 0) 
			{
				Console.WriteLine ("No triples");
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

				if (resultType == "subject") 
				{
					Console.WriteLine("[predicate]: " + (predicateValue.GetType () == typeof(UriNode) ? "<" + predicateValue.ToString() + ">" : predicateValue.ToString()) + " [object]: " + (objectValue.GetType () == typeof(UriNode) ? "<" + objectValue.ToString() + ">" : objectValue.ToString()));
				} 
				else if (resultType == "predicate") 
				{
					Console.WriteLine ("[subject]: " + (subjectValue.GetType () == typeof(UriNode) ? "<" + subjectValue.ToString() + ">" : subjectValue.ToString()) + " [object]: " + (objectValue.GetType () == typeof(UriNode) ? "<" + objectValue.ToString() + ">" : objectValue.ToString()));
				} 
				else 
				{
					Console.WriteLine("[subject]: " + (subjectValue.GetType () == typeof(UriNode) ? "<" + subjectValue.ToString() + ">" : subjectValue.ToString()) + " [predicate]: " + (predicateValue.GetType () == typeof(UriNode) ? "<" + predicateValue.ToString() + ">" : predicateValue.ToString()));
				}
			}
		}
	
		/**
		 * Waits for input and executes a query when input is given.
		 * 
		 * @param limit, if a resultsLimit should be used.
		 * @param limitAmount, the result limit amount.
		 */ 
		public void browseInput(bool limit, int limitAmount) 
		{
			Console.WriteLine("Warning request Uri's without surrounding them with <>");
			Console.Write("Give a value to browse to : ");
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
	