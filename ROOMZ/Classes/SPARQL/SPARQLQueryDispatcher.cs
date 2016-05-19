using System;
using VDS.RDF.Query;

namespace ROOMZ
{
	/**
	 * This class can execute SPARQL queries on a remote SPARQL endpoint making use of dotNetRDF.
	 */
	public class SPARQLQueryDispatcher
	{
		protected SparqlRemoteEndpoint sparqlQueryEndpoint;
		protected SparqlRemoteEndpoint sparqlUpdateEndpoint;

		public SPARQLQueryDispatcher ()
		{
		}

		public SPARQLQueryDispatcher (Uri queryEndpointUri)
		{
			setQueryEndpoint(queryEndpointUri);
		}

		public SPARQLQueryDispatcher (Uri queryEndpointUri, Uri UpdateEndpointUri)
		{
			setQueryEndpoint(queryEndpointUri);
			setUpdateEndpoint(UpdateEndpointUri);
		}

		public void setQueryEndpoint (Uri queryEndpointUri) 
		{
			sparqlQueryEndpoint = new SparqlRemoteEndpoint (queryEndpointUri);
		}

		public void setUpdateEndpoint (Uri updateEndpointUri) 
		{
			this.sparqlUpdateEndpoint = new SparqlRemoteEndpoint (updateEndpointUri);
		}

		public SparqlRemoteEndpoint getSparqlQueryEndpoint () 
		{
			return sparqlQueryEndpoint;
		}

		public void setSparqlQueryEndpoint (SparqlRemoteEndpoint sparqlQueryEndpoint)
		{
			this.sparqlQueryEndpoint = sparqlQueryEndpoint;
		}

		public SparqlRemoteEndpoint getSparqlUpdateEndpoint () 
		{
			return sparqlUpdateEndpoint;
		}

		public void setSparqlUpdateEndpoint (SparqlRemoteEndpoint sparqlUpdateEndpoint) 
		{
			this.sparqlUpdateEndpoint = sparqlUpdateEndpoint;
		}

		/**
		 * Run a query against the queryEndpoint and return the results
		 * 
		 * @param query, the query that will be run.
		 */
		public SparqlResultSet dispatchQuery (string query) 
		{
			//Console.Write (query);
			return sparqlQueryEndpoint.QueryWithResultSet (query);
		}

		/**
		 * Run an update query against the queryEndpoint and return the results
		 * 
		 * @param query, the query that will be run.
		 */
		public SparqlResultSet dispatchUpdate (string query)
		{
			return sparqlUpdateEndpoint.QueryWithResultSet (query);
		}

		/**
		 * Returns true of a queryEndpoint is present.
		 */
		public bool queryEndpointExists () {
			return (sparqlQueryEndpoint != null);
		}

		/**
		 * Returns true of a queryEndpoint is present.
		 */
		public bool updateEndpointExists () {
			return (sparqlUpdateEndpoint != null);
		}
	}
}
	