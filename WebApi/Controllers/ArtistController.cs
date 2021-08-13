using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using WebApi.ListItem;
using WebApi.Request;
using WebApi.Response;

// https://docs.microsoft.com/en-us/aspnet/web-api/overview/error-handling/exception-handling

namespace WebApi.Controllers
{
    public class ArtistController : ApiController
    {

        // Endpoint to search for an artist by name (api.multitracks.com/artist/search)

        //[HttpPost]
        //public JsonResult<List<ArtistListItem>> Search(ArtistSearchRequest request)
        //{
        //    var sql = new SQL();
        //    sql.Parameters.Clear();
        //    sql.Parameters.Add("@title", SqlDbType.VarChar, "%" + request.Name + "%");
        //    DataTable dtArtist = sql.ExecuteDT("SELECT artistID, title FROM Artist WHERE title LIKE @title");
        //    return Json(CommonCode.ConvertDataTable<ArtistListItem>(dtArtist));
        //}

        //[HttpPost]
        //public HttpResponseMessage Search(ArtistSearchRequest request)
        //{
        //    var sql = new SQL();
        //    sql.Parameters.Clear();
        //    sql.Parameters.Add("@title", SqlDbType.VarChar, "%" + request.Name + "%");
        //    DataTable dtArtist = sql.ExecuteDT("SELECT artistID, title FROM Artist WHERE title LIKE @title");
        //    return Request.CreateResponse(HttpStatusCode.OK, dtArtist);
        //}

        [HttpPost]
        public IHttpActionResult Search(ArtistSearchRequest request)
        {
            var sql = new SQL();
            sql.Parameters.Clear();
            sql.Parameters.Add("@title", SqlDbType.VarChar, "%" + request.Name + "%");
            DataTable dtArtist = sql.ExecuteDT("SELECT artistID, title FROM Artist WHERE title LIKE @title");
            return Ok(dtArtist);
        }

        // Endpoint to add an Artist to the Artist table (api.multitracks.com/artist/add)

        //[HttpPost]
        //public JsonResult<ArtistAddResponse> Add(ArtistAddRequest request)
        //{
        //    var sql = new SQL();
        //    sql.Parameters.Clear();
        //    sql.Parameters.Add("@title", SqlDbType.VarChar, request.Title);
        //    sql.Parameters.Add("@biography", SqlDbType.VarChar, request.Biography);
        //    sql.Parameters.Add("@imageURL", SqlDbType.VarChar, request.ImageURL);
        //    sql.Parameters.Add("@heroURL", SqlDbType.VarChar, request.HeroURL);
        //    DataTable dtArtist = sql.ExecuteDT("INSERT INTO Artist " +
        //        "(biography, title, imageURL, heroURL) " +
        //        "OUTPUT Inserted.artistID " +
        //        "VALUES (@title, @biography, @imageURL, @heroURL)");
        //    return Json(new ArtistAddResponse() { ArtistId = Convert.ToInt32(dtArtist.Rows[0]["artistID"]) });
        //}

        //[HttpPost]
        //public HttpResponseMessage Add(ArtistAddRequest request)
        //{
        //    var sql = new SQL();
        //    sql.Parameters.Clear();
        //    sql.Parameters.Add("@title", SqlDbType.VarChar, request.Title);
        //    sql.Parameters.Add("@biography", SqlDbType.VarChar, request.Biography);
        //    sql.Parameters.Add("@imageURL", SqlDbType.VarChar, request.ImageURL);
        //    sql.Parameters.Add("@heroURL", SqlDbType.VarChar, request.HeroURL);
        //    DataTable dtArtist = sql.ExecuteDT("INSERT INTO Artist " +
        //        "(biography, title, imageURL, heroURL) " +
        //        "OUTPUT Inserted.artistID " +
        //        "VALUES (@title, @biography, @imageURL, @heroURL)");
        //    return Request.CreateResponse(HttpStatusCode.OK, new ArtistAddResponse() { ArtistId = Convert.ToInt32(dtArtist.Rows[0]["artistID"]) });
        //}

        [HttpPost]
        public IHttpActionResult Add(ArtistAddRequest request)
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
            return Ok(new ArtistAddResponse() { ArtistId = Convert.ToInt32(dtArtist.Rows[0]["artistID"]) });
        }

    }
}
