using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Http;
using System.Web.Http.Results;
using WebApi.ListItem;
using WebApi.Request;
using WebApi.Response;

namespace WebApi.Controllers
{
    public class ArtistController : ApiController
    {

        // Endpoint to search for an artist by name (api.multitracks.com/artist/search)
        [HttpPost]
        public JsonResult<List<ArtistListItem>> Search(ArtistSearchRequest request)
        {
            var sql = new SQL();
            sql.Parameters.Clear();
            sql.Parameters.Add("@title", SqlDbType.VarChar, "%" + request.Name + "%");
            DataTable dtArtist = sql.ExecuteDT("SELECT artistID, title FROM Artist WHERE title LIKE @title");
            return Json(CommonCode.ConvertDataTable<ArtistListItem>(dtArtist));
        }

        // Endpoint to add an Artist to the Artist table (api.multitracks.com/artist/add)
        [HttpPost]
        public JsonResult<ArtistAddResponse> Add(ArtistAddRequest request)
        {
            var sql = new SQL();
            sql.Parameters.Clear();
            sql.Parameters.Add("@title", SqlDbType.VarChar, request.Title);
            sql.Parameters.Add("@biography", SqlDbType.VarChar, request.Biography);
            sql.Parameters.Add("@imageURL", SqlDbType.VarChar, request.ImageURL);
            sql.Parameters.Add("@heroURL", SqlDbType.VarChar, request.HeroURL);
            DataTable dtArtist = sql.ExecuteDT("INSERT INTO Artist " +
                "(biography, title, imageURL, heroURL) " +
                "OUTPUT Inserted.artistID " +
                "VALUES (@title, @biography, @imageURL, @heroURL)");
            return Json(new ArtistAddResponse() { ArtistId = Convert.ToInt32(dtArtist.Rows[0]["artistId"]) });
        }

    }
}
