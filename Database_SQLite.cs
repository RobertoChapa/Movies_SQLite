/********************************
 * Bobby Chapa
 * 4/11/2019 3:57 pm
 * SQLite Console Application Move Database Demo
 * Database Functions
 *******************************/

using System;
using System.Data.SQLite;

namespace Movies_SQLite
{
    class Database_SQLite
    {
        #region CreateConnection()
        public static SQLiteConnection CreateConnection()
        {
            SQLiteConnection sc;

            //Will Find the current directory. Database must be located in the ,..\SQLiteItems\DB directory
            string cs = System.IO.Directory.GetCurrentDirectory();
            cs = cs.Replace(@"\Movies_SQLite\bin\Debug", @"\DB\Movies.db");
            cs = "Data Source = " + cs;

            sc = new SQLiteConnection(cs);

            try
            {
                sc.Open();
            }
            catch (Exception ex)
            {
                Console.Write("Database_SQLite.CreateConnection() Error, WRONG STRING: " + ex.Message);
            }

            return sc;
        }
        #endregion

        #region InsertData_Movie()
        public static string InsertData_Movie(string mt, string mg, int ms, string mra, string mre, string notes, SQLiteConnection conn)
        {
            string i = "";
            i += "insert into MovieInfo";
            i += "(MTitle, MGenre, MStatus, MRating, Review, Notes)";
            i += " values ";
            i += "('" + Apost(mt) + "', '" + Apost(mg) + "', '" + ms + "','" + Apost(mra) + "','" + Apost(mre) + "','" + Apost(notes) + "')";

            try
            {
                SQLiteCommand sqlite_cmd;
                sqlite_cmd = conn.CreateCommand();
                sqlite_cmd.CommandText = i;
                sqlite_cmd.ExecuteNonQuery();

                i = "\r\n" + "Insert Completed Successfully." + "\r\n";
            }
            catch (Exception ex)
            {
                i = "Database_SQLite.InsertData_Movie() Error:" + ex.Message;
            }

            return i;
        }
        #endregion

        #region UpdateData_Movie()
        //Modifys two columns in the database 'MovieTitle' and 'Notes'
        public static string UpdateData_Movie(int oid, string mt, string mg, int ms, string mra, string mre, string notes, SQLiteConnection conn)
        {
            string i = "";

            if (oid.ToString().Trim().Length > 0)// ObjectId required to do anything
            {
                i += "  update MovieInfo ";
                i += "  set ";
                if (mt.Trim().Length > 0) { i += "  MTitle = " + '"' + Apost(mt) + '"' + ","; }
                if (mg.Trim().Length > 0) { i += "  MGenre = " + '"' + Apost(mg) + '"' + ","; }
                if (ms.ToString().Trim().Length > 0) { i += "  MStatus = " + '"' + ms.ToString() + '"' + ","; }
                if (mra.Trim().Length > 0) { i += "  MRating = " + '"' + Apost(mra) + '"' + ","; }
                if (mre.Trim().Length > 0) { i += "  Review = " + '"' + Apost(mre) + '"' + ","; }
                if (notes.Trim().Length > 0) { i += "  Notes = " + '"' + Apost(notes) + '"'; } else { i = i.TrimEnd(','); }

                i += " where Objectid = " + oid;
                
                try
                {
                    SQLiteCommand sqlite_cmd;
                    sqlite_cmd = conn.CreateCommand();
                    sqlite_cmd.CommandText = i;
                    sqlite_cmd.ExecuteNonQuery();

                    i = "\r\n" + "Update Completed Successfully. Enter 0 to continue" + "\r\n";
                }
                catch (Exception e)
                {
                    i = "Database_SQLite.UpdateData_Movie() Error: " + e.Message;
                }
            }

            return i;
        }
        #endregion

        #region DeleteData_Movie()
        public static string DeleteData_Movie(int oid, SQLiteConnection conn)
        {
            string i = "";
            i += "Delete from MovieInfo ";
            i += "Where Objectid = " + oid;

            try
            {
                if (oid.ToString().Length > 0)
                {
                    SQLiteCommand sqlite_cmd;
                    sqlite_cmd = conn.CreateCommand();
                    sqlite_cmd.CommandText = i;
                    sqlite_cmd.ExecuteNonQuery();

                    i = "\r\n" + "Delete completed successfully." + "\r\n";
                }
            }
            catch (Exception ex)
            {
                i = "Database_SQLite.DeleteData_Movie() Error:" + ex.Message;
            }

            return i;
        }
        #endregion

        #region Select_MovieData()
        public static string Select_MovieData(SQLiteConnection conn)
        {
            string s = "";
            string q = "select Objectid, MTitle, MGenre, cast(MStatus as int) as MStatus, MRating, Review, Notes from MovieInfo";

            string mt = "", mg = "", mra = "", mre = "", notes = "";
            int Objectid = 0, ms = 0;

            SQLiteDataReader sqlr;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = q;

            sqlr = sqlite_cmd.ExecuteReader();

            while (sqlr.Read())
            {
                try
                {
                    Objectid = int.Parse(sqlr["Objectid"].ToString());
                    mt = sqlr["MTitle"].ToString();
                    mg = sqlr["MGenre"].ToString();
                    ms = int.Parse(sqlr["MStatus"].ToString());
                    mra = sqlr["MRating"].ToString();
                    mre = sqlr["Review"].ToString();
                    notes = sqlr["Notes"].ToString();

                    s += "Objectid: " + Objectid + " ";
                    if (mt.Trim().Length > 0) { s += "  MTitle: " + '"' + mt + '"'; }
                    if (mg.Trim().Length > 0) { s += "  MGenre: " + '"' + mg + '"'; }
                    if (ms.ToString().Trim().Length > 0) { s += "  MStatus: " + '"' + ms + '"'; }
                    if (mra.Trim().Length > 0) { s += "  MRating: " + '"' + mra + '"'; }
                    if (mre.Trim().Length > 0) { s += "  Review: " + '"' + mre + '"'; }
                    if (notes.Trim().Length > 0) { s += "  Notes: " + '"' + notes + '"'; }
                    s += "\r\n";
                }

                catch (Exception ex)
                {
                    s = "Select_MovieData() Error: " + ex.Message;
                }
            }

            return s;
        }
        #endregion

        #region Apost()
        //Check for appostrophes
        public static string Apost(string s)
        {
            s = s.Replace("'", "''").Trim();

            return s;
        }
        #endregion
    }
}

