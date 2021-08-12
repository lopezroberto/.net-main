using DataAccess;
using System;
using System.Data;
using System.Web.UI;

// Implement (Sync) Artist Details Page.
//   - Find markup in the folder multitracks.com-dotNet\Web\multitracks.com\multitracks.com\PageToSync
//   - Create a new page artistDetails.aspx using the provided markup
//   - Make the page data driven to display the appropriate images / text for a given parameter of artistID
//   - The page should pull all the needed data from a Stored Procedure (GetArtistDetails)
//   - The page should make use of the MTDataAccess Class Library. Look at the source of this page for an example.
//       (ExecuteStoredProcedureDS will return a DataTable rather than a DataSet if multiple result sets are needed)
public partial class artistDetails : MultitracksPage
{
    public string img1Src = "";
    public string img2Src = "";
    public string bannerTitle = "";
    public string biography = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        // http://localhost:56916/PageToSync/artistDetails.aspx?artistID=31
        var artistID = 31;
        if (Request.QueryString["artistID"] != null)
            artistID = Convert.ToInt32(Request.QueryString["artistID"]);
        else
        {
            MessageBox.Show(this, "To choose artisID, please make changes to browser link. Ex: http://localhost:56916/PageToSync/artistDetails.aspx?artistID=31");
        }

        //Session["artistID"] = 5;
        //Session["artistID"] = 31;
        //if (Session["artistID"] == null)
        //{
        //    return;
        //}
        //var artistID = Session["artistID"].ToString();

        var sql = new SQL();

        sql.Parameters.Add("@artistID", artistID);

        DataSet ds = sql.ExecuteStoredProcedureDS("GetArtistDetails");
        if (ds.Tables.Count < 1)
            return;

        // Artist Info
        DataTable tArtist = ds.Tables[0];
        if (tArtist.Rows.Count < 1)
            return;

        DataRow drArtist = tArtist.Rows[0];

        img1Src = drArtist["heroURL"].ToString();
        img2Src = drArtist["imageURL"].ToString();
        bannerTitle = drArtist["artistName"].ToString();
        biography = drArtist["biography"].ToString();

        // Top Songs Info
        DataTable tSong = ds.Tables[1];
        if (tSong.Rows.Count < 1)
            return;

        DataRow drSong = tSong.Rows[0];

        topSongsItems.DataSource = tSong;
        topSongsItems.DataBind();
        itemsTopSongs.Visible = true;

        // Albums Info
        DataTable tAlbum = ds.Tables[2];
        if (tAlbum.Rows.Count < 1)
            return;

        DataRow drAlbum = tAlbum.Rows[0];

        albumsItems.DataSource = tAlbum;
        albumsItems.DataBind();

        Page.DataBind();
    }

}