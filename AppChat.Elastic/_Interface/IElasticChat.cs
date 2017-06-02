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
    public interface IElasticChat : IElastic<ChatInfo>
    {
        IEnumerable<ChatInfo> HitsToList(SearchResult<ChatInfo>.SearchHits hits);
    }
}
