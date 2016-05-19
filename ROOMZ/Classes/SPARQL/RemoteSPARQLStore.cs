using System;
using System.Collections.Generic;
using VDS.RDF.Query;
using VDS.RDF.Nodes;
using VDS.RDF;

namespace ROOMZ
{
	/**
	 * This class contains sevaral methods that build and execute queries based on parameters.
	 */
	public class RemoteSPARQLStore
	{
		SPARQLQueryDispatcher queryDispatcher;

		public RemoteSPARQLStore (SPARQLQueryDispatcher queryDispatcher)
		{
			this.queryDispatcher = queryDispatcher;
		}

		/**
		 * Get all the triples with a specific Uri as Subject.
		 * 
		 * @param uri, the uri that will be used as subject in the where clause.
		 * @param limit, if a result limit should be applied.
		 * @param limitAmount, the result limit amount.
		 */
		public List<SparqlResult> getTriplesBySubject (Uri uri,  bool limit, int limitAmount) 
		{
			SparqlParameterizedString queryString = new SparqlParameterizedString();
			queryString.CommandText = "SELECT (@uri AS ?subject) ?predicate ?object WHERE { @uri ?predicate ?object } ";
			queryString.SetUri ("uri", uri);

			if (limit) 
			{
				queryString.Append ("LIMIT @limit");
				queryString.SetLiteral ("limit",  limitAmount);
			}

			SparqlResultSet result = queryDispatcher.dispatchQuery (queryString.ToString ());
			return result.Results;
		}

		/**
		 * Get all the triples with a specific Uri as Predicate.
		 * 
		 * @param uri, the uri that will be used as predicate in the where clause.
		 * @param limit, if a result limit should be applied.
		 * @param limitAmount, the result limit amount.
		 */
		public List<SparqlResult> getTriplesByPredicate (Uri uri,  bool limit, int limitAmount) 
		{
			SparqlParameterizedString queryString = new SparqlParameterizedString();
			queryString.CommandText = "SELECT ?subject (@uri as ?predicate) ?object WHERE { ?subject @uri ?object} ";
			queryString.SetUri ("uri", uri);

			if (limit) 
			{
				queryString.Append ("LIMIT @limit");
				queryString.SetLiteral ("limit",  limitAmount);
			}

			SparqlResultSet result = queryDispatcher.dispatchQuery (queryString.ToString ());
			return result.Results;
		}

		/**
		 * Get all the triples with a specific Uri as Object.
		 * 
		 * @param uri, the uri that will be used as object in the where clause.
		 * @param limit, if a result limit should be applied.
		 * @param limitAmount, the result limit amount.
		 */
		public List<SparqlResult> getTriplesByObject (Uri uri,  bool limit, int limitAmount) 
		{
			SparqlParameterizedString queryString = new SparqlParameterizedString();
			queryString.CommandText = "SELECT ?subject ?predicate (@uri as ?object) WHERE { ?subject ?predicate @uri } ";
			queryString.SetUri ("uri", uri);

			if (limit) 
			{
				queryString.Append ("LIMIT @limit");
				queryString.SetLiteral ("limit",  limitAmount);
			}

			SparqlResultSet result = queryDispatcher.dispatchQuery (queryString.ToString ());
			return result.Results;
		}

		/**
		 * Get all the triples with a specific Uri as Predicate or Object.
		 * 
		 * @param uri, the uri that will be used as subject and predicate in the where clauses.
		 * @param limit, if a result limit should be applied.
		 * @param limitAmount, the result limit amount.
		 */
		public List<SparqlResult> getTriplesBySubjectAndObject(Uri uri,  bool limit, int limitAmount) 
		{
			//Add a namespace declaration
			//queryString.Namespaces.AddNamespace("ex", new Uri("http://example.org/ns#"));
			//queryString.CommandText = "SELECT ?subject ?predicate ?object WHERE { @uri ?predicate ?object . ?subject ?predicate @uri}";

			SparqlParameterizedString queryString = new SparqlParameterizedString();
			queryString.CommandText = "SELECT * { { SELECT (@uri AS ?subject) ?predicate ?object WHERE { @uri ?predicate ?object } } UNION { SELECT ?subject ?predicate (@uri AS ?object) WHERE { ?subject ?predicate @uri } } } ";
			queryString.SetUri ("uri", uri);

			if (limit) 
			{
				queryString.Append ("LIMIT @limit");
				queryString.SetLiteral ("limit",  limitAmount);
			}

			SparqlResultSet result = queryDispatcher.dispatchQuery (queryString.ToString ());
			return result.Results;
		}

		/**
		 * Get all the predicates that are used with a specific Uri as Subject or Object.
		 */
		public List<SparqlResult> getPredicatesByObject(Uri uri,  bool limit, int limitAmount) 
		{
			SparqlParameterizedString queryString = new SparqlParameterizedString();
			queryString.CommandText = "SELECT ?predicate WHERE { ?subject ?predicate @uri } GROUP BY ?predicate ";
			queryString.SetUri ("uri", uri);

			if (limit) 
			{
				queryString.Append ("LIMIT @limit");
				queryString.SetLiteral ("limit",  limitAmount);
			}

			SparqlResultSet result = queryDispatcher.dispatchQuery (queryString.ToString ());
			return result.Results;
		}
			
		/**
		 * Get all the triples with specific criteria.
		 * 
		 * @param subjectValue
		 *   The object, pass an uri or any other value wrapped within its respective QueryParameter subclass.
		 *   Pass a QueryParameter object with a value object of type Empty if the subject should not have any special criteria within the WHERE clause.
		 * @param predicateValue
		 *   The object, pass an uri or any other value wrapped within its respective QueryParameter subclass.
		 *   Pass a QueryParameter object with a value object of type Empty if the subject should not have any special criteria within the WHERE clause.
		 * @param objectValue
		 *   The object, pass an uri or any other value wrapped within its respective QueryParameter subclass.
		 *   Pass a QueryParameter object with a value object of type Empty if the subject should not have any special criteria within the WHERE clause.
		 * @param limit
		 *   Define if the query enforce a size limit.
		 * @param limitAmount
		 *   The limit size.
		 */
		public List<SparqlResult> getTriplesByCustomCriteria (QueryParameter subjectValue, QueryParameter predicateValue, QueryParameter objectValue, bool limit, int limitAmount) 
		{

			SparqlParameterizedString queryString = new SparqlParameterizedString();
			queryString.CommandText = "SELECT @subjectSelect @predicateSelect @objectSelect WHERE { @subject @predicate @object } ";

			setParameter (queryString, "subject", subjectValue);
			setParameter (queryString, "predicate", predicateValue);
			setParameter (queryString, "object", objectValue);

			if (limit) 
			{
				queryString.Append ("LIMIT @limit");
				queryString.SetLiteral ("limit",  limitAmount);
			}
				
			SparqlResultSet result = queryDispatcher.dispatchQuery (queryString.ToString ());
			return result.Results;
		}

		/**
		 * Replace either the subject, predicate or object parameters inside a queryString.
		 * 
		 * @param queryString
		 *   The queryString in which the parameter will be set
		 * @param parameterName
		 *   The name of the parameter which will be set: subject, predicate, object
		 * @param parameterValue
		 *   The value that will be used, Pass a QueryParameter object with a value object of type Empty if the parameter should not have any special criteria within the WHERE clause.
		 */
		protected SparqlParameterizedString setParameter(SparqlParameterizedString queryString, string parameterName, QueryParameter parameterValue) 
		{
			if (parameterValue.getValueType () == typeof(Empty)) 
			{
				queryString.CommandText = queryString.CommandText.Replace ("@" + parameterName + "Select", "?" + parameterName);
				queryString.CommandText = queryString.CommandText.Replace ("@" + parameterName, "?" + parameterName);
			} 
			else if (parameterValue.getValueType () == typeof(Uri)) 
			{
				queryString.CommandText = queryString.CommandText.Replace ("@" + parameterName + "Select", "(<" + parameterValue.getValue ().ToString () + "> AS ?" + parameterName + ")");
				queryString.SetUri (parameterName, (Uri)parameterValue.getValue ());
			} 
			else if (parameterValue.getValueType () == typeof(string)) 
			{
				queryString.CommandText = queryString.CommandText.Replace ("@" + parameterName + "Select", "(\"" + parameterValue.getValue().ToString() + "\" AS ?" + parameterName + ")");
				queryString.CommandText = queryString.CommandText.Replace ("@" + parameterName, "\"" + parameterValue.getValue().ToString() + "\"");
			}
			else 
			{
				queryString.CommandText = queryString.CommandText.Replace ("@" + parameterName + "Select", "(" + parameterValue.getValue().ToString() + " AS ?" + parameterName + ")");
				queryString.CommandText = queryString.CommandText.Replace ("@" + parameterName, parameterValue.getValue().ToString());
			}

			return queryString;
		}

		/**
		 * Execute a custom query.
		 */
		public List<SparqlResult> getTriplesByCustomQuery (string query) 
		{
			SparqlResultSet result = queryDispatcher.dispatchQuery (query);
			return result.Results;
		}

		/**
		 * Execute a custom update query. 
		 */
		public List<SparqlResult> updateByCustomQuery (string query)
		{
			SparqlResultSet result = queryDispatcher.dispatchUpdate (query);
			return result.Results;
		}
	}
}

