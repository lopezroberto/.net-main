using System.Collections.Generic;

namespace WebApi.Request
{
    public class QueryRequest
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public List<QueryOrderBy> FilterBy { get; set; }
    }

    public class QueryOrderBy
    {
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
    }
}