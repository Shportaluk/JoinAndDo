﻿using JoinAndDo.Entities;
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

        private SqlCommand _cmdNewJoin;

        // Send
        private SqlCommand _cmdSendMsg;


        // Get
        private SqlCommand _cmdGetUsers;
        private SqlCommand _cmdGetInterlocutors;
        private SqlCommand _cmdGetDialog;
        private SqlCommand _cmdGetLastMessages;
        private SqlCommand _cmdGetUserById;
        private SqlCommand _cmdGetCountMessages;
        private SqlCommand _cmdJoins = new SqlCommand("SELECT * FROM Joins");
        private SqlCommand _cmdMyAccession;
        private SqlCommand _cmdDealsAccession = new SqlCommand("SELECT * FROM Deals_accession");


        public SqlRepository()
        {
            _con = new SqlConnection( _conStr );
        }

        public void NewJoin( string login, string hash, string title, string text, string category, string needPeople)
        {
            string comm = "EXEC NewJoin @login = '"+login+"', @hash = '"+hash+"', @title = '"+title+"', @text = '"+text+"', @category = '"+category+"', @needPeople = " + needPeople;
            _cmdNewJoin = new SqlCommand(comm);
            _cmdNewJoin.Connection = _con;
            _con.Open();
            SqlDataReader reader = _cmdNewJoin.ExecuteReader();
            _con.Close();
        }

        public string Registration( string login, string pass, string firstName, string lastName )
        {
            _cmdRegistration = new SqlCommand("DECLARE @res NVARCHAR(30) EXEC Registration @login = '" + login + "', @pass = '" + pass + "', @firstName = '"+ firstName +"', @lastName = '" + lastName + "', @res = @res OUTPUT SELECT @res");
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

            using ( _cmdUser = new SqlCommand( "SELECT Id, Login, FirstName, LastName, Hash FROM Users where Login = '" + login + "' and Pass = '" + pass + "'") )
            {
                _cmdUser.Connection = _con;
                _con.Open();
                SqlDataReader reader = _cmdUser.ExecuteReader();

                while (reader.Read())
                {
                    user.Id = reader[0].ToString();
                    user.Login = reader[1].ToString();
                    user.FirstName = reader[2].ToString();
                    user.LastName = reader[3].ToString();
                    user.Hash = reader[4].ToString();
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
        public string SendMsg( string login, string hash, string to, string text )
        {
            string res = "-";
            _cmdSendMsg = new SqlCommand("DECLARE @res NVARCHAR(20) EXEC SendMsg @login = '"+login+"', @hash = '"+hash+"', @to = '"+to+"', @text = '"+text+"', @res = @res OUTPUT SELECT @res" );
            _cmdSendMsg.Connection = _con;
            _con.Open();
            SqlDataReader reader = _cmdSendMsg.ExecuteReader();
            _con.Close();
            
            return res;
        }

        public List<User> GetUsers( string full_name )
        {
            List<User> listUser = new List<User>();

            string comm;
            if (String.IsNullOrEmpty(full_name))
            {
                comm = "SELECT Id, FirstName, LastName, Hash FROM Users";
            }
            else
            {
                comm = "EXEC GetUserByName @name = '" + full_name + "'";
            }
            _cmdGetUsers = new SqlCommand( comm );
            _cmdGetUsers.Connection = _con;
            _con.Open();

            SqlDataReader reader = _cmdGetUsers.ExecuteReader();
            while (reader.Read())
            {
                User user = new User();
                user.Id = reader[0].ToString();
                user.FirstName = reader[1].ToString();
                user.LastName = reader[2].ToString();
                if (!String.IsNullOrEmpty(reader[3].ToString()))
                {
                    user.IsOnline = true;
                }
                listUser.Add(user);
            }
            _con.Close();

            return listUser;
        }
        public List<string> GetInterlocutors(string login)
        {
            List<string> interlocutors = new List<string>();
            _cmdGetInterlocutors = new SqlCommand("EXEC GetInterlocutor @login = '"+login+"'");
            _cmdGetInterlocutors.Connection = _con;
            _con.Open();

            SqlDataReader reader = _cmdGetInterlocutors.ExecuteReader();
            while (reader.Read())
            {
                interlocutors.Add( reader[0].ToString() );
            }
            _con.Close();

            return interlocutors;
        }
        public List<Message> GetDialog( string login, string hash, string LoginInterlocutor )
        {
            List < Message > dialog = new List<Message>();
            //
            string comm = "EXEC GetDialog @login = '"+login+"', @hash = '"+hash+"', @loginInterlocutor = '"+LoginInterlocutor+"'";
            _cmdGetDialog = new SqlCommand(comm);
            _cmdGetDialog.Connection = _con;
            _con.Open();

            SqlDataReader reader = _cmdGetDialog.ExecuteReader();
            while (reader.Read())
            {
                Message msg = new Message();
                msg.login = reader[0].ToString();
                msg.text = reader[1].ToString();
                msg.loginInterlocutor = reader[2].ToString();
                msg.date = reader[3].ToString();
                dialog.Add( msg );
            }

            _con.Close();
            return dialog;
        }
        public string GetLastMessages( string login, string hash )
        {
            string res = "";
            string comm = "SELECT TOP 1 Login, Text  FROM Messages WHERE ToLogin = ( SELECT Login FROM Users WHERE Login = '" + login + "' and Hash = '" + hash + "' ) ORDER BY Id DESC";
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
            _cmdGetCountMessages = new SqlCommand("EXEC GetCountMessages @login = '" + login + "', @hash = '" + hash + "'");
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
        public User GetUserById(int? iD)
        {
            User user = null;
            using (_cmdGetUserById = new SqlCommand("SELECT Login, FirstName, LastName, Hash, FulfillmentAccession, AcceptedConnections, TimeWorking FROM Users where Id = " + iD))
            {
                _cmdGetUserById.Connection = _con;
                _con.Open();
                SqlDataReader reader = _cmdGetUserById.ExecuteReader();

                while (reader.Read())
                {
                    user = new User();
                    user.Login = reader[0].ToString();
                    user.FirstName = reader[1].ToString();
                    user.LastName = reader[2].ToString();
                    if (!String.IsNullOrEmpty(reader[3].ToString()))
                    {
                        user.IsOnline = true;
                    }
                    user.FulfillmentAccession = reader[4].ToString();
                    user.AcceptedConnections = reader[5].ToString();
                    user.TimeWorking = reader[6].ToString();
                }

                _con.Close();
            }
            return user;
        }
        public User GetUserByLogin(string login)
        {
            User user = null;
            using (SqlCommand sqlComm = new SqlCommand( "SELECT Id, FirstName, LastName FROM Users where Login = '" + login + "'" ))
            {
                sqlComm.Connection = _con;
                _con.Open();
                SqlDataReader reader = sqlComm.ExecuteReader();

                while (reader.Read())
                {
                    user = new User();
                    user.Id = reader[0].ToString();
                    user.FirstName = reader[1].ToString();
                    user.LastName = reader[2].ToString();
                }

                _con.Close();
            }
            return user;
        }
        public List<User> GetUsersByIdOfAccession( int? id )
        {
            List<User> users = new List<User>();

            SqlCommand sqlComm = new SqlCommand( "SELECT Login, RoleName FROM Role WHERE IdAccession = " + id );
            sqlComm.Connection = _con;

            _con.Open();
            SqlDataReader reader = sqlComm.ExecuteReader();

            while (reader.Read())
            {
                User user = new User();
                user.Login = reader[0].ToString();
                user.Role = reader[1].ToString();
                users.Add(user);
            }

            _con.Close();
            return users;
        }
        public List<JoinsEntity> GetAllFromJoins(  )
        {
            List<JoinsEntity> listJoins = new List<JoinsEntity>();
            _cmdJoins.Connection = _con;
            _con.Open();
            SqlDataReader reader = _cmdJoins.ExecuteReader();
            
            //string title;
            //string text;
            //int people;
            //int allPeople;
            //
            //
            //while ( reader.Read() )
            //{
            //    title = reader[1].ToString();
            //    text = reader[2].ToString();
            //    people = int.Parse( reader[3].ToString() );
            //    allPeople = int.Parse(reader[4].ToString());
            //
            //    listJoins.Add( new JoinsEntity( title, text, people, allPeople ) );
            //}

            _con.Close();
            return listJoins;
        }
        public List<MyAccession> GetMyAccessions( string login, string hash )
        {
            List<MyAccession> listMyAccession = new List<MyAccession>();
            _cmdMyAccession = new SqlCommand( "EXEC GetAccessions @login = '"+login+"', @hash = '"+hash+"'" );
            _cmdMyAccession.Connection = _con;

            _con.Open();
            SqlDataReader reader = _cmdMyAccession.ExecuteReader();

            while (reader.Read())
            {
                MyAccession accession = new MyAccession();
                accession.title = reader[0].ToString();
                accession.text = reader[1].ToString();
                accession.People = int.Parse(reader[2].ToString());
                accession.AllPeople = int.Parse(reader[3].ToString());
                accession.isComplete = Boolean.Parse( reader[4].ToString() );
                listMyAccession.Add(accession);
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

            
            while (reader.Read())
            {
                DealsAccession dealsAccession = new DealsAccession();
                dealsAccession.title = reader[1].ToString();
                dealsAccession.text = reader[2].ToString();
                dealsAccession.user = reader[3].ToString();
                dealsAccession.People = int.Parse(reader[4].ToString());
                dealsAccession.AllPeople = int.Parse(reader[5].ToString());
                listDealsAccession.Add( dealsAccession );
            }

            _con.Close();
            return listDealsAccession;
        }
        public List<Accession> GetAccessions( string text )
        {
            List < Accession > listAccession = new List < Accession >();

            SqlCommand sqlComm = new SqlCommand( "SELECT * FROM Accession WHERE Text LIKE '%"+text+"%'" );
            sqlComm.Connection = _con;

            _con.Open();
            SqlDataReader reader = sqlComm.ExecuteReader();

            while (reader.Read())
            {
                Accession accession = new Accession();
                accession.Id = int.Parse(reader[0].ToString());
                accession.Title = reader[1].ToString();
                accession.Text = reader[2].ToString();
                accession.Category = reader[3].ToString();
                accession.Creator = reader[4].ToString();
                accession.People = int.Parse(reader[5].ToString());
                accession.AllPeople = int.Parse(reader[6].ToString());
                listAccession.Add(accession);
            }

            _con.Close();
            return listAccession;
        }
        public Accession GetAccessionById(int? id)
        {
            Accession accession = null;
            SqlCommand sqlComm = new SqlCommand( "SELECT * FROM Accession WHERE Id = " + id );
            sqlComm.Connection = _con;

            _con.Open();
            SqlDataReader reader = sqlComm.ExecuteReader();

            while (reader.Read())
            {
                accession = new Accession();
                accession.Id = int.Parse(reader[0].ToString());
                accession.Title = reader[1].ToString();
                accession.Text = reader[2].ToString();
                accession.Category = reader[3].ToString();
                accession.Creator = reader[4].ToString();
                accession.People = int.Parse(reader[5].ToString());
                accession.AllPeople = int.Parse(reader[6].ToString());
            }

            _con.Close();
            return accession;
        }
    }
}
