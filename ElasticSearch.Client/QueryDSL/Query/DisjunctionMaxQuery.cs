using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ElasticSearch.Client.QueryDSL
{
	/// <summary>
	/// A query that generates the union of documents produced by its subqueries, and that scores each document with the maximum score for that document as produced by any subquery, plus a tie breaking increment for any additional matching subqueries. This is useful when searching for a word in multiple fields with different boost factors (so that the fields cannot be combined equivalently into a single search field). We want the primary score to be the one associated with the highest boost, not the sum of the field scores (as Boolean Query would give). If the query is ��albino elephant�� this ensures that ��albino�� matching one field and ��elephant�� matching another gets a higher score than ��albino�� matching both fields. To get this result, use both Boolean Query and DisjunctionMax Query: for each term a DisjunctionMaxQuery searches for it in each field, while the set of these DisjunctionMaxQuery��s is combined into a BooleanQuery.The tie breaker capability allows results that include the same term in multiple fields to be judged better than results that include this term in only the best of those multiple fields, without confusing this with the better case of two different terms in the multiple fields.The default tie_breaker is 0.0. This query maps to Lucene DisjunctionMaxQuery.	
	/// </summary>
	[JsonObject("dis_max")]
	[JsonConverter(typeof(DisjunctionMaxQueryConverter))]
	public class DisjunctionMaxQuery : IQuery
	{
		/// <summary>
		/// The tie breaker capability allows results that include the same term in multiple fields to be judged better than results that include this term in only the best of those multiple fields, without confusing this with the better case of two different terms in the multiple fields.The default tie_breaker is 0.0.
		/// </summary>
		public float TieBreaker;
		public float Boost;
		public List<IQuery> Queries;

		public DisjunctionMaxQuery(float tiebreakder,float boost,params IQuery[] queries)
		{
			TieBreaker = tiebreakder;
			Boost = boost;
			if(queries.Length>0)
			{
				Queries=new List<IQuery>();
				Queries.AddRange(queries);
			}
		}
		public DisjunctionMaxQuery(float tiebreakder):this(tiebreakder,1)
		{
			
		}

		public DisjunctionMaxQuery AddQuery(IQuery query)
		{
			if(Queries==null)
			{
				Queries=new List<IQuery>();
			}
			Queries.Add(query);
			return this;
		}

	}
}