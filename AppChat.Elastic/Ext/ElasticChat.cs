using AppChat.ElasticSearch.Core;
using AppChat.ElasticSearch.Models;
using PlainElastic.Net.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppChat.ElasticSearch.Ext
{
    public class ElasticChat : Elastic<ChatInfo>, IElasticChat
    {
        public override IEnumerable<ChatInfo> HitsToList(SearchResult<ChatInfo>.SearchHits hits)
        {
            var result = new List<ChatInfo>();

            var hitsList = hits.hits.ToList();
            hitsList.ForEach(x => {
                if (x.highlight != null)
                {
                    x._source.content = x.highlight["content"][0];
                }
                result.Add(x._source);
            });
            return result;
        }
    }
}
