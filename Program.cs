/********************************
 * Bobby Chapa
 * 4/12/2019 7:32 am
 * SQLite Console Application Movie Database Demo
 * Main Entry
 *******************************/

using System;
using System.Data.SQLite;

namespace Movies_SQLite
{
    class Program
    {
        #region Main()
        static void Main(string[] args)
        {
            ManageApp();
        }
        #endregion

        #region ManageApp()
        public static void ManageApp()
        {
            // DB Connection string
            SQLiteConnection sc = Database_SQLite.CreateConnection();

            // Select all Movie data from table Movie for display
            Console.WriteLine(Database_SQLite.Select_MovieData(sc) + "\r\n");

            while (true)
            {
                // Determine what user wants to do; insert, update, delete
                GetUserCommand(sc);
            }
        }
        #endregion

        #region GetUserCommand()
        private static char GetUserCommand(SQLiteConnection sc)
        {  
            getInfo:
                Console.Write("Enter: 'i' for insert, 'u' for update, 'd' for delete, 's' for select, or 9 for exit 'Required' " + "\r\n");

                bool t = false;

                t = char.TryParse(Console.ReadLine(), out char c);
                if (t == true && c == '9') { Environment.Exit(0); }
                if (t == true && (c == 'i' || c == 'u' || c == 'd' || c == 's')) {} else { goto getInfo; }

                switch (c)
                {
                    case 'i':
                        Insert(sc);
                        break;
                    case 'u':
                        Update(sc);
                        break;
                    case 'd':
                        Delete(sc);
                        break;
                    case 's':
                        Console.WriteLine(Database_SQLite.Select_MovieData(sc));
                        break;
                    default:
                        Environment.Exit(0);
                        break;
                }

                return '0';
        }
        #endregion

        #region Insert()
        private static void Insert(SQLiteConnection sc)
        {
                int ms = 0; bool t = false; char c;
                string mt = "", mg = "", mra = "", mre = "", notes = "";

                Console.Write("\r\n");

                Console.Write("Movie Title" + "\r\n");
                mt = Console.ReadLine();

                Console.Write("Movie Genre" + "\r\n");
                mg = Console.ReadLine();

            // Movie Status required 
            getInfo:
                Console.Write("Movie Status, 0 or 1 'Required' " + "\r\n");

                t = char.TryParse(Console.ReadLine(), out c);
                if (t == true && (c == '0' || c == '1')) { ms = int.Parse(c.ToString()); } else { goto getInfo; }
            // End

                Console.Write("Movie Rating" + "\r\n");
                mra = Console.ReadLine();

                Console.Write("Movie Review" + "\r\n");
                mre = Console.ReadLine();

                Console.Write("Movie Notes" + "\r\n");
                notes = Console.ReadLine();

                Console.WriteLine(Database_SQLite.InsertData_Movie(mt, mg, ms, mra, mre, notes, sc));
        }
        #endregion

        #region Update
        private static void Update(SQLiteConnection sc)
        {
                int ms = 0, objid = 0; bool t = false; char c; bool ti = false; int ci;
                string mt = "", mg = "", mra = "", mre = "", notes = "";

                Console.Write("\r\n");

            // Movie ObjectID required
            getInfo_i:
                Console.Write("Movie Objectid  'Required' " + "\r\n");

                ti = int.TryParse(Console.ReadLine(), out ci);
                if (ti == true) { objid = int.Parse(ci.ToString()); } else { goto getInfo_i; }
            // End

                Console.Write("Movie Title" + "\r\n");
                mt = Console.ReadLine();

                Console.Write("Movie Genre" + "\r\n");
                mg = Console.ReadLine();

            // Movie Status required
            getInfo_c:
                Console.Write("Movie Status, 0 or 1 'Required' " + "\r\n");

                t = char.TryParse(Console.ReadLine(), out c);
                if (t == true && (c == '0' || c == '1')) { ms = int.Parse(c.ToString()); } else { goto getInfo_c; }
            // End

                Console.Write("Movie Rating" + "\r\n");
                mra = Console.ReadLine();

                Console.Write("Movie Review" + "\r\n");
                mre = Console.ReadLine();

                Console.Write("Movie Notes" + "\r\n");
                notes = Console.ReadLine();

                Console.WriteLine(Database_SQLite.UpdateData_Movie(objid, mt, mg, ms, mra, mre, notes, sc));
        }
        #endregion

        #region Delete()
        private static void Delete(SQLiteConnection sc)
        {
                int objid = 0; bool t = false;

                Console.Write("\r\n");

            // Movie Objectid required 
            getInfo:
                Console.Write("Enter the Objectid of the Movie to delete 'Required'" + "\r\n");

                t = int.TryParse(Console.ReadLine(), out objid);
                if (t == true) { objid = int.Parse(objid.ToString()); } else { goto getInfo; }
            // End

                Console.WriteLine(Database_SQLite.DeleteData_Movie(objid, sc));
        }
        #endregion
    }
}
