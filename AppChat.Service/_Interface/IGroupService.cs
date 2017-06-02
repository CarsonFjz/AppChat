using AppChat.ElasticSearch.Models;
using AppChat.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppChat.Service._Interface
{
    public interface IElasticGroupService
    {
        bool IndexGroup(LayImGroup group);
        LayImGroup IndexGroup(DataTable dt);
        JsonResultModel SearchLayimGroup(string keyword, int pageIndex = 1, int pageSize = 50);
    }
}
