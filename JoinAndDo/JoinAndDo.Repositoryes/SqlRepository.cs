using JoinAndDo.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;

namespace JoinAndDo.Repositoryes
{
    public class SqlRepository
    {
        private string _conStr = ConfigurationManager.ConnectionStrings["con_str"].ConnectionString;
        private SqlConnection _con { get; set; }
        private SqlCommand _cmdRegistration = new SqlCommand();
        private SqlCommand _cmdUser = new SqlCommand();
        private SqlCommand _cmdDeleteHash = new SqlCommand();
        private SqlCommand _cmdSetHash = new SqlCommand();
        private SqlCommand _cmdGetHash = new SqlCommand();

        // Get
        private SqlCommand _cmdGetLastMessages;
        private SqlCommand _cmdGetUserById;
        private SqlCommand _cmdGetCountMessages;
        private SqlCommand _cmdJoins = new SqlCommand("SELECT * FROM Joins");
        private SqlCommand _cmdMyAccession = new SqlCommand("SELECT * FROM My_accession");
        private SqlCommand _cmdDealsAccession = new SqlCommand("SELECT * FROM Deals_accession");


        public SqlRepository()
        {
            _con = new SqlConnection( _conStr );
        }


        public string Registration( string login, string pass, string firstName, string lastName )
        {
            _cmdRegistration = new SqlCommand( "DECLARE @res NVARCHAR(30) EXEC registration @login = '" + login + "', @pass = '" + pass + "', @firstName = '"+ firstName +"', @lastName = '" + lastName + "', @res = @res OUTPUT SELECT @res");
            _cmdRegistration.Connection = _con;
            _con.Open();
            SqlDataReader reader = _cmdRegistration.ExecuteReader();

            while( reader.Read() )
            {
                return reader[0].ToString();
            }
            return null;
        }
        public bool IsAuthenticated( string login, string hash )
        {
            bool isAutn = false;
            using ( _cmdGetHash = new SqlCommand( "SELECT Login FROM Users where Hash = '" + hash + "'") )
            {
                _cmdGetHash.Connection = _con;
                _con.Open();
                SqlDataReader reader = _cmdGetHash.ExecuteReader();
                

                while ( reader.Read() )
                {
                    if (login == reader[0].ToString())
                    {
                        isAutn = true;
                    }
                }

                _con.Close();
                return isAutn;
            }
        }
        public User Authentication( string login, string pass )
        {
            User user = new User();

            using ( _cmdUser = new SqlCommand( "SELECT Login, FirstName, LastName, Hash FROM Users where Login = '" + login + "' and Pass = '" + pass + "'") )
            {
                _cmdUser.Connection = _con;
                _con.Open();
                SqlDataReader reader = _cmdUser.ExecuteReader();

                while (reader.Read())
                {
                    user.login = reader[0].ToString();
                    user.firstName = reader[1].ToString();
                    user.lastName = reader[2].ToString();
                    user.hash = reader[3].ToString();
                }
                _con.Close();
                return user;
            }
        }
        public void DeleteHash(string login, string hash)
        {
            _cmdDeleteHash = new SqlCommand( "UPDATE Users SET Hash = NULL where Login = '" + login + "' and hash = '" + hash + "'" );
            _cmdDeleteHash.Connection = _con;
            _con.Open();
            SqlDataReader reader = _cmdDeleteHash.ExecuteReader();
            _con.Close();
        }
        public string SetHash( string login, string pass )
        {
            Guid g = Guid.NewGuid();
            string hash = Convert.ToBase64String(g.ToByteArray());
            hash = hash.Replace("=", "").Replace("+", "");

            _cmdSetHash = new SqlCommand("UPDATE Users SET Hash = '" + hash + "' where Login = '" + login + "' and Pass = '" + pass + "' ");
            _cmdSetHash.Connection = _con;
            _con.Open();
            SqlDataReader reader = _cmdSetHash.ExecuteReader();
            _con.Close();
            

            return hash;
        }

        public string GetLastMessages( string login, string hash )
        {
            string res = "";
            string comm = "SELECT TOP 1 Name, Text  FROM Messages WHERE Id_user = ( SELECT Id FROM Users WHERE Login = '" + login + "' and Hash = '" + hash + "' ) ORDER BY Id DESC";
            _cmdGetLastMessages = new SqlCommand(comm);
            _cmdGetLastMessages.Connection = _con;
            _con.Open();

            SqlDataReader reader = _cmdGetLastMessages.ExecuteReader();
            while (reader.Read())
            {
                res = reader[0].ToString() + ":" + reader[1].ToString();
            }

            _con.Close();
            return res;
        }
        public string GetCountMessages(string login, string hash)
        {
            string count = "-";
            _cmdGetCountMessages = new SqlCommand("DECLARE @res INT EXEC GetCountMessages @login = '" + login + "', @hash = '" + hash + "', @res = @res OUTPUT SELECT @res");
            _cmdGetCountMessages.Connection = _con;
            _con.Open();

            SqlDataReader reader = _cmdGetCountMessages.ExecuteReader();
            while (reader.Read())
            {
                count = reader[0].ToString();
            }

            _con.Close();

            return count;
        }
        public User GetUserById(string iD)
        {
            User user = null;
            using (_cmdGetUserById = new SqlCommand( "SELECT Login, FirstName, LastName FROM Users where Id = " + iD ))
            {
                _cmdGetUserById.Connection = _con;
                _con.Open();
                SqlDataReader reader = _cmdGetUserById.ExecuteReader();

                while (reader.Read())
                {
                    user = new User();
                    user.login = reader[0].ToString();
                    user.firstName = reader[1].ToString();
                    user.lastName = reader[2].ToString();
                }

                _con.Close();
            }
            return user;
        }
        public List<JoinsEntity> GetAllFromJoins(  )
        {
            List<JoinsEntity> listJoins = new List<JoinsEntity>();
            _cmdJoins.Connection = _con;
            _con.Open();
            SqlDataReader reader = _cmdJoins.ExecuteReader();
            
            string title;
            string text;
            int people;
            int allPeople;


            while ( reader.Read() )
            {
                title = reader[1].ToString();
                text = reader[2].ToString();
                people = int.Parse( reader[3].ToString() );
                allPeople = int.Parse(reader[4].ToString());

                listJoins.Add( new JoinsEntity( title, text, people, allPeople ) );
            }

            _con.Close();
            return listJoins;
        }
        public List<MyAccession> GetAllFromMyAccession(  )
        {
            List<MyAccession> listMyAccession = new List<MyAccession>();
            _cmdMyAccession.Connection = _con;

            _con.Open();
            SqlDataReader reader = _cmdMyAccession.ExecuteReader();

            string title;
            string text;
            int people;
            int allPeople;
            bool IsComplate;


            while (reader.Read())
            {
                title = reader[1].ToString();
                text = reader[2].ToString();
                people = int.Parse(reader[3].ToString());
                allPeople = int.Parse(reader[4].ToString());
                IsComplate = Boolean.Parse( reader[5].ToString() );
                listMyAccession.Add(new MyAccession(title, text, people, allPeople, IsComplate ));
            }

            _con.Close();
            return listMyAccession;
        }
        public List<DealsAccession> GetAllFromDealsAccession(  )
        {
            List<DealsAccession> listDealsAccession = new List<DealsAccession>();
            _cmdDealsAccession.Connection = _con;

            _con.Open();
            SqlDataReader reader = _cmdDealsAccession.ExecuteReader();


            string title;
            string text;
            string user;
            int people;
            int allPeople;


            while (reader.Read())
            {
                title = reader[1].ToString();
                text = reader[2].ToString();
                user = reader[3].ToString();
                people = int.Parse(reader[4].ToString());
                allPeople = int.Parse(reader[5].ToString());
                listDealsAccession.Add( new DealsAccession( title, text, user, people, allPeople ) );
            }

            _con.Close();
            return listDealsAccession;
        }
    }
}
