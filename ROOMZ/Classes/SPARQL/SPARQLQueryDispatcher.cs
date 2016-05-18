using System;
using VDS.RDF.Query;

namespace ROOMZ
{
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

		public SparqlResultSet dispatchQuery (string query) 
		{
			Console.Write (query);
			return sparqlQueryEndpoint.QueryWithResultSet (query);
		}

		public SparqlResultSet dispatchUpdate (string query)
		{
			return sparqlUpdateEndpoint.QueryWithResultSet (query);
		}

		public bool queryEndpointExists () {
			return (sparqlQueryEndpoint != null);
		}

		public bool updateEndpointExists () {
			return (sparqlUpdateEndpoint != null);
		}
	}
}
	