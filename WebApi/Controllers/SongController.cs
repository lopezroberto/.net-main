using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Http;
using System.Web.Http.Results;
using WebApi.ListItem;
using WebApi.Request;

namespace WebApi.Controllers
{
    public class SongController : ApiController
    {
        // Endpoint to list all songs with paging support using page size, page number, etc. (api.multitracks.com/song/list)
        [HttpPost]
        public JsonResult<List<SongListItem>> List(QueryRequest request)
        {
            var sql = new SQL();

            // FilterBy
            // TODO: Make this section more generic
            var strFilter = "";
            foreach (var item in request.FilterBy)
            {
                strFilter += item.FieldName + " LIKE '%" + item.FieldValue + "%'";
            }

            if (strFilter != String.Empty)
                strFilter = "WHERE " + strFilter + " ";

            // OrderBy, etc


            sql.Parameters.Clear();
            sql.Parameters.Add("@pageSize", SqlDbType.Int, request.PageSize);
            sql.Parameters.Add("@offset", SqlDbType.Int, request.PageSize * (request.PageNumber - 1));
            DataTable dtSong = sql.ExecuteDT("SELECT songID, title " +
                "FROM Song " +
                strFilter +
                "ORDER BY songID " +
                "OFFSET @offset ROWS " +
                "FETCH NEXT @pageSize ROWS ONLY");
            return Json(CommonCode.ConvertDataTable<SongListItem>(dtSong));
        }

    }
}
