using AudioLibraryServerRESTful;
using AudioLibraryServerRESTful.Discography;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioLibraryServerRESTful
{
    public class SqLiteFacade
    {
        public static Genre genreFromRow(DataRow row)
        {
            Genre a = new Genre()
            {
                ID = new Link<long>() { resource = (long)row["GenreId"], href = Program.baseAddress + "genres/" + (long)row["GenreId"] + "/" },//(long)row["GenreId"],
                Name = (row["Name"] == DBNull.Value) ? string.Empty : row["Name"].ToString()
            };
            return a;
        }

        public static long executeQueryAndGetLastId(String s)
        {
            long ret = 0;
            string cs = @"URI=file:.\chinook.db";
            System.Data.SQLite.SQLiteConnection con = new SQLiteConnection(cs);
            con.Open();
            SQLiteCommand cmd = new SQLiteCommand(con);
            cmd.CommandText = s;
            try
            {
                ret = cmd.ExecuteNonQuery();
                cmd = new SQLiteCommand(con);
                cmd.CommandText = @"select last_insert_rowid()";
                ret = (long)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                ret = -1;
            }
            con.Close();
            return ret;
        }
        public static DataTable getDatatableFromQuery(String s)
        {
            string cs = @"URI=file:.\chinook.db";
            System.Data.SQLite.SQLiteConnection con = new SQLiteConnection(cs);
            con.Open();
            SQLiteCommand cmd = new SQLiteCommand(con);

            try
            {
                SQLiteDataAdapter db = new SQLiteDataAdapter(s, con);

                // Create a dataset
                DataSet ds = new DataSet();

                // Fill dataset
                db.Fill(ds);

                // Create a datatable
                DataTable dt = new DataTable();
                dt = ds.Tables[0];

                // Close connection
                con.Close();
                return dt;
            }
            catch (Exception ex)
            {
            }
            return null;
        }
    }
}
