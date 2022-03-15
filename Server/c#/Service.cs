using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.IO;
using System.ServiceModel.Web;
using System.Text;
using System.Data.SQLite;
using AudioLibraryServerRESTful.Discography;
using System.Data;
using System.ServiceModel;
using AudioLibraryServerRESTful;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.ServiceModel.Channels;

namespace AudioLibraryServerRESTful
{
    public class Service : IService
    {
        [return: MessageParameter(Name = "genres")]
        public List<Genre> ReadGenres()
        {
            List<Genre> genreList = new List<Genre>();
            DataTable dt = SqLiteFacade.getDatatableFromQuery("SELECT * FROM genres");
            foreach (DataRow row in dt.Rows)
            {
                Genre t = SqLiteFacade.genreFromRow(row);
                DataTable dt2 = SqLiteFacade.getDatatableFromQuery("SELECT * FROM Tracks WHERE GenreId=" + t.ID.resource);
                //foreach (DataRow row2 in dt2.Rows)
                //    t.TracksList.Add(new Link<long>() { resource = SqLiteFacade.trackFromRow(row2).ID, href = Program.root + "tracks/" + SqLiteFacade.trackFromRow(row2).ID + "/" });
                genreList.Add(t);
        }
            return genreList;
        }
    }
}
