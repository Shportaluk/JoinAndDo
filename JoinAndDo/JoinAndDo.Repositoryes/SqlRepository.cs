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

        private SqlCommand _cmdNewJoin;

        // Send
        private SqlCommand _cmdSendMsg;


        // Get
        private SqlCommand _cmdGetUsers;
        private SqlCommand _cmdGetInterlocutors;
        private SqlCommand _cmdGetDialog;
        private SqlCommand _cmdGetLastMessages;
        private SqlCommand _cmdGetCountMessages;
        private SqlCommand _cmdJoins = new SqlCommand("SELECT * FROM Joins");
        private SqlCommand _cmdMyAccession;
        private SqlCommand _cmdDealsAccession = new SqlCommand("SELECT * FROM Deals_accession");


        public SqlRepository()
        {
            _con = new SqlConnection( _conStr );
        }


        public string AddSkillToUser(string login, string hash, string pathImg, string name)
        {
            string res = null;
            string comm = String.Format("EXEC AddSkillToUser @login = '{0}', @hash = '{1}', @pathImg = '{2}', @name = '{3}'", login, hash, pathImg, name);

            SqlCommand sqlComm = new SqlCommand(comm);
            sqlComm.Connection = _con;
            _con.Open();
            SqlDataReader reader = sqlComm.ExecuteReader();

            while (reader.Read())
            {
                res = reader[0].ToString();
            }

            _con.Close();
            return res;
        }
        public List<Skill> GetSkillsOfUserByLogin(string login)
        {
            List<Skill> skills = new List<Skill>();
            string comm = String.Format("SELECT PathImg, Name FROM ListSkillsOfUsers WHERE Login = '"+login+"'");

            SqlCommand sqlComm = new SqlCommand(comm);
            sqlComm.Connection = _con;
            _con.Open();
            SqlDataReader reader = sqlComm.ExecuteReader();

            while (reader.Read())
            {
                Skill skill = new Skill();
                skill.PathImg = reader[0].ToString();
                skill.Name = reader[1].ToString();
                skills.Add(skill);
            }

            _con.Close();
            return skills;
        }
        public List<Accession> GetMyInvitation(string login, string hash)
        {
            List<Accession> listInvitation = new List<Accession>();
            string comm = String.Format("EXEC GetMyInvitation @login = '{0}', @hash = '{1}'", login, hash);

            SqlCommand sqlComm = new SqlCommand(comm);
            sqlComm.Connection = _con;
            _con.Open();
            SqlDataReader reader = sqlComm.ExecuteReader();

            while (reader.Read())
            {
                Accession accession = new Accession();
                accession.Text = reader[0].ToString();
                accession.Category = reader[1].ToString();
                accession.Id = int.Parse(reader[2].ToString());
                accession.Status = reader[3].ToString();
                listInvitation.Add(accession);
            }

            _con.Close();
            return listInvitation;
        }
        public string AddUserToAccession(string login, string hash, string loginUserAdded, string role, int idAccession)
        {
            string res = null;
            string comm = String.Format("DECLARE @res NVARCHAR(255) EXEC AddUserToAccession @login = '{0}', @hash = '{1}', @loginUserAdded = '{2}', @role = '{3}', @idAccession = {4}, @res = @res OUTPUT", login, hash, loginUserAdded, role, idAccession);

            SqlCommand sqlComm = new SqlCommand(comm);
            sqlComm.Connection = _con;
            _con.Open();
            SqlDataReader reader = sqlComm.ExecuteReader();

            while (reader.Read())
            {
                res = reader[0].ToString();
            }

            _con.Close();
            return res;
        }
        public string Registration( string login, string pass, string firstName, string lastName )
        {
            _cmdRegistration = new SqlCommand("EXEC Registration @login = '" + login + "', @pass = '" + pass + "', @firstName = '"+ firstName +"', @lastName = '" + lastName + "'");
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

        public string LoadProfileImg(string login, string hash, string pathImg)
        {
            string res = null;
            string comm = String.Format("EXEC LoadProfileImg @login = '{0}', @hash = '{1}', @pathImg = '{2}'", login, hash, pathImg);

            SqlCommand sqlComm = new SqlCommand(comm);
            sqlComm.Connection = _con;
            _con.Open();
            SqlDataReader reader = sqlComm.ExecuteReader();

            while (reader.Read())
            {
                res = reader[0].ToString();
            }
            _con.Close();

            return res;
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
                comm = "SELECT Id, FirstName, LastName, Hash, PathImg FROM Users";
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
                user.PathImgMini = "mini_" + reader[4].ToString();
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
            string comm = "SELECT Id, Login, FirstName, LastName, Hash, PathImg, CompletedAccessions, AbandonedAccessions, CurrentlyAccessions, AllAccessions FROM Users where Id = " + iD;
            SqlCommand sqlComm = new SqlCommand(comm);
            sqlComm.Connection = _con;

            User user = null;

            _con.Open();
            SqlDataReader reader = sqlComm.ExecuteReader();

            while (reader.Read())
            {
                user = new User();
                user.Id = reader[0].ToString();
                user.Login = reader[1].ToString();
                user.FirstName = reader[2].ToString();
                user.LastName = reader[3].ToString();
                if (!String.IsNullOrEmpty(reader[4].ToString()))
                {
                    user.IsOnline = true;
                }
                user.PathImg = reader[5].ToString();

                user.CompletedAccessions = int.Parse(reader[6].ToString());
                user.AbandonedAccessions = int.Parse(reader[7].ToString());
                user.CurrentlyAccessions = int.Parse(reader[8].ToString());
                user.AllAccessions = int.Parse(reader[9].ToString());

                if (user.AllAccessions != 0)
                {
                    user.CompletedAccessionsPercent = user.CompletedAccessions * 100 / user.AllAccessions;
                    user.AbandonedAccessionsPercent = user.AbandonedAccessions * 100 / user.AllAccessions;
                    user.CurrentlyAccessionsPercent = user.CurrentlyAccessions * 100 / user.AllAccessions;
                }
            }

            _con.Close();
            return user;
        }
        public User GetUserByLogin(string login)
        {
            string comm = "SELECT Id, Login, FirstName, LastName, Hash, PathImg, CompletedAccessions, AbandonedAccessions, CurrentlyAccessions, AllAccessions FROM Users WHERE Login = '" + login + "'";
            SqlCommand sqlComm = new SqlCommand(comm);
            sqlComm.Connection = _con;

            User user = null;

            _con.Open();
            SqlDataReader reader = sqlComm.ExecuteReader();

            while (reader.Read())
            {
                user = new User();
                user.Id = reader[0].ToString();
                user.Login = reader[1].ToString();
                user.FirstName = reader[2].ToString();
                user.LastName = reader[3].ToString();
                if (!String.IsNullOrEmpty(reader[4].ToString()))
                {
                    user.IsOnline = true;
                }
                user.PathImg = reader[5].ToString();

                user.CompletedAccessions = int.Parse(reader[6].ToString());
                user.AbandonedAccessions = int.Parse(reader[7].ToString());
                user.CurrentlyAccessions = int.Parse(reader[8].ToString());
                user.AllAccessions = int.Parse(reader[9].ToString());

                if (user.AllAccessions != 0)
                {
                    user.CompletedAccessionsPercent = user.CompletedAccessions * 100 / user.AllAccessions;
                    user.AbandonedAccessionsPercent = user.AbandonedAccessions * 100 / user.AllAccessions;
                    user.CurrentlyAccessionsPercent = user.CurrentlyAccessions * 100 / user.AllAccessions;
                }
            }

            _con.Close();
            return user;
        }
        public List<User> GetUsersByIdOfAccession( int? id )
        {
            List<User> users = new List<User>();

            SqlCommand sqlComm = new SqlCommand( "EXEC GetUsersByIdOfAccession @idAccession = " + id);
            sqlComm.Connection = _con;

            _con.Open();
            SqlDataReader reader = sqlComm.ExecuteReader();

            while (reader.Read())
            {
                User user = new User();
                user.Id = reader[0].ToString();
                user.Login = reader[1].ToString();
                if (!String.IsNullOrEmpty(reader[2].ToString()))
                {
                    user.IsOnline = true;
                }
                user.PathImgMini = "mini_" + reader[3].ToString();
                
                user.FirstName = reader[4].ToString();
                user.LastName = reader[5].ToString();
                user.Role = reader[6].ToString();
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

    
        // Отримання приєднання якими я керую
        public List<Accession> GetMyAccessionsManagement( string login, string hash )
        {
            List<Accession> listMyAccession = new List<Accession>();
            _cmdMyAccession = new SqlCommand("EXEC GetMyAccessionsManagement @login = '" + login+"', @hash = '"+hash+"'" );
            _cmdMyAccession.Connection = _con;

            _con.Open();
            SqlDataReader reader = _cmdMyAccession.ExecuteReader();

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
                listMyAccession.Add(accession);
            }

            _con.Close();
            return listMyAccession;
        }
        public List<Accession> GetMyAccessions(string login, string hash)
        {
            List<Accession> listMyAccession = new List<Accession>();
            _cmdMyAccession = new SqlCommand("EXEC GetMyAccessions @login = '" + login + "', @hash = '" + hash + "'");
            _cmdMyAccession.Connection = _con;

            _con.Open();
            SqlDataReader reader = _cmdMyAccession.ExecuteReader();

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
        public List<string> GetListAvailableRolesOfAccessionById(int? id)
        {
            List<string> listRoles = new List<string>();
            SqlCommand sqlComm = new SqlCommand("SELECT RoleName FROM RoleOfUserInAccession WHERE IdAccession = "+id+" and Login IS NULL");
            sqlComm.Connection = _con;

            _con.Open();
            SqlDataReader reader = sqlComm.ExecuteReader();

            while (reader.Read())
            {
                listRoles.Add( reader[0].ToString() );
            }

            _con.Close();
            return listRoles;
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


        // Отримання заявок приєднання до Accession
        public List<RequestJoinToAccession> GetRequestsAdditionToAccession( int? id )
        {
            List<RequestJoinToAccession> listRequest = new List<RequestJoinToAccession>();

            SqlCommand sqlComm = new SqlCommand( "SELECT * FROM RequestJoinToAccession WHERE ToIdAccession = '" + id + "' and Status = 'Waiting'" );
            sqlComm.Connection = _con;

            _con.Open();
            SqlDataReader reader = sqlComm.ExecuteReader();

            while (reader.Read())
            {
                RequestJoinToAccession requestToAccession = new RequestJoinToAccession();
                requestToAccession.Id = int.Parse(reader[0].ToString());
                requestToAccession.Login = reader[1].ToString();
                requestToAccession.Text = reader[2].ToString();
                requestToAccession.Category = reader[3].ToString();
                requestToAccession.ToIdAccession = reader[4].ToString();
                requestToAccession.Status = reader[5].ToString();
                listRequest.Add(requestToAccession);
            }

            _con.Close();
            return listRequest;
        }

        public string RemoveUserFromAccession(string login, string hash, int idAccession, string user)
        {
            string res = "";
            string command = String.Format("EXEC RemoveUserFromAccession @login = '{0}', @hash = '{1}', @idAccession = {2}, @user = '{3}'", login, hash, idAccession, user);
            SqlCommand sqlComm = new SqlCommand(command);
            sqlComm.Connection = _con;

            _con.Open();
            SqlDataReader reader = sqlComm.ExecuteReader();

            while (reader.Read())
            {
                res = reader[0].ToString();
            }

            _con.Close();

            return res;
        }
        public string SendRequestToAccession(string login, string hash, string text, string category, int idAccession)
        {
            string res = "";
            string command = String.Format("EXEC SendRequestToAccession @login = '{0}', @hash = '{1}', @text = '{2}', @category = '{3}', @idAccession = '{4}'", login, hash, text, category, idAccession );
            SqlCommand sqlComm = new SqlCommand( command );
            sqlComm.Connection = _con;

            _con.Open();
            SqlDataReader reader = sqlComm.ExecuteReader();

            while (reader.Read())
            {
                res = reader[0].ToString();
            }

            _con.Close();

            return res;
        }
        public string SendRequestCompleteAccession(string login, string hash, int idAccession)
        {
            string res = "";
            string command = String.Format("EXEC RequestComplateAccession @login = '{0}', @hash = '{1}', @idAccession = '{2}'", login, hash, idAccession);
            SqlCommand sqlComm = new SqlCommand(command);
            sqlComm.Connection = _con;

            _con.Open();
            SqlDataReader reader = sqlComm.ExecuteReader();

            while (reader.Read())
            {
                res = reader[0].ToString();
            }

            _con.Close();

            return res;
        }
        public string EditTitleOfAccession(string login, string hash, int idAccession, string title)
        {
            string res = null;

            string command = String.Format("EXEC EditTitleOfAccession @login = '{0}', @hash = '{1}', @idAccession = {2}, @title = '{3}'", login, hash, idAccession, title);
            SqlCommand sqlComm = new SqlCommand(command);
            sqlComm.Connection = _con;

            _con.Open();
            SqlDataReader reader = sqlComm.ExecuteReader();

            while (reader.Read())
            {
                res = reader[0].ToString();
            }

            _con.Close();
            return res;
        }
        public string EditDescriptionOfAccession(string login, string hash, int idAccession, string description)
        {
            string res = null;

            string command = String.Format("EXEC EditDescriptionOfAccession @login = '{0}', @hash = '{1}', @idAccession = {2}, @description = '{3}'", login, hash, idAccession, description);
            SqlCommand sqlComm = new SqlCommand(command);
            sqlComm.Connection = _con;

            _con.Open();
            SqlDataReader reader = sqlComm.ExecuteReader();

            while (reader.Read())
            {
                res = reader[0].ToString();
            }

            _con.Close();
            return res;
        }
        public string NewJoin(string login, string hash, string title, string text, string category, string needPeople, string arrayRoles)
        {
            string comm = "EXEC NewJoin @login = '" + login + "', @hash = '" + hash + "', @title = '" + title + "', @text = '" + text + "', @category = '" + category + "', @needPeople = " + needPeople;
            string idAccession = null;
            _cmdNewJoin = new SqlCommand(comm);
            _cmdNewJoin.Connection = _con;
            _con.Open();
            SqlDataReader reader = _cmdNewJoin.ExecuteReader();
            while (reader.Read())
            {
                idAccession = reader[0].ToString();
            }
            _con.Close();
            if (!string.IsNullOrWhiteSpace(idAccession))
            {
                InsertRoleOfHumanInAccession( login, arrayRoles, idAccession);
                //AddUserToAccession(login, hash, login, "Creator", int.Parse(idAccession));
            }

            return idAccession;
        }
        public void InsertRoleOfHumanInAccession( string creator, string arrayRoles, string idAccession )
        {
            string command = "INSERT INTO RoleOfUserInAccession VALUES ( '"+creator+"', 'Creator', "+idAccession+" )\n";
            string[] separator = { "," };
            string[] listRoles = arrayRoles.Split( separator, StringSplitOptions.RemoveEmptyEntries );
            foreach( string role in listRoles )
            {
                command += String.Format("INSERT INTO RoleOfUserInAccession VALUES ( NULL, '{0}', '{1}' )\n", role, idAccession );
            }
           SqlCommand sqlComm = new SqlCommand(command);
            sqlComm.Connection = _con;

            _con.Open();
            SqlDataReader reader = sqlComm.ExecuteReader();
            _con.Close();
        }
        public string DeleteJoin(string login, string hash, int idAccession)
        {
            string res = null;
            string comm = String.Format("EXEC DeleteJoin @login = '{0}', @hash = '{1}', @idAccession = {2}", login, hash, idAccession);

            SqlCommand sqlComm = new SqlCommand(comm);
            sqlComm.Connection = _con;
            _con.Open();
            SqlDataReader reader = sqlComm.ExecuteReader();

            while (reader.Read())
            {
                res = reader[0].ToString();
            }
            _con.Close();

            return res;
        }
        public string AcceptRequestOfUserToAccession(string login, string hash, string user, string role, string idAccession)
        {
            string res = null;
            string comm = String.Format("EXEC AcceptRequestOfUserToAccession @login = '{0}', @hash = '{1}', @user = '{2}', @role = '{3}', @idAccession = {4}", login, hash, user, role, idAccession);

            SqlCommand sqlComm = new SqlCommand(comm);
            sqlComm.Connection = _con;
            _con.Open();
            SqlDataReader reader = sqlComm.ExecuteReader();

            while (reader.Read())
            {
                res = reader[0].ToString();
            }
            _con.Close();

            return res;
        }
        public string RejectRequestOfUserToAccession(string login, string hash, string user, string idAccession)
        {
            string res = null;
            string comm = String.Format("EXEC RejectRequestOfUserToAccession @login = '{0}', @hash = '{1}', @user = '{2}', @idAccession = {3}", login, hash, user, idAccession);

            SqlCommand sqlComm = new SqlCommand(comm);
            sqlComm.Connection = _con;
            _con.Open();
            SqlDataReader reader = sqlComm.ExecuteReader();

            while (reader.Read())
            {
                res = reader[0].ToString();
            }
            _con.Close();

            return res;
        }
        public string SendMsgToAccession(string login, string hash, int idAccession, string text)
        {
            string res = null;
            string comm = String.Format("EXEC SendMsgToAccession @login = '{0}', @hash = '{1}', @idAccession = {2}, @text = '{3}'", login, hash, idAccession, text);

            SqlCommand sqlComm = new SqlCommand(comm);
            sqlComm.Connection = _con;
            _con.Open();
            SqlDataReader reader = sqlComm.ExecuteReader();

            while (reader.Read())
            {
                res = reader[0].ToString();
            }
            _con.Close();

            return res;
        }
        public List<Message> GetDialogOfAccession(string login, string hash, int idAccession)
        {
            List<Message> dialog = new List<Message>();
            
            string comm = String.Format("EXEC GetDialogOfAccession @login = '{0}', @hash = '{1}', @idAccession = {2}", login, hash, idAccession);

            SqlCommand sqlComm = new SqlCommand(comm);
            sqlComm.Connection = _con;
            _con.Open();
            SqlDataReader reader = sqlComm.ExecuteReader();

            while (reader.Read())
            {
                Message msg = new Message();
                msg.login = reader[0].ToString();
                msg.text = reader[1].ToString();
                dialog.Add(msg);
            }
            _con.Close();

            return dialog;
        }
        public string ExitWithAccession(string login, string hash, int idAccession)
        {
            string res = null;
            string comm = String.Format("EXEC ExitWithAccession @login = '{0}', @hash = '{1}', @idAccession = {2}", login, hash, idAccession);

            SqlCommand sqlComm = new SqlCommand(comm);
            sqlComm.Connection = _con;
            _con.Open();
            SqlDataReader reader = sqlComm.ExecuteReader();

            while (reader.Read())
            {
                res = reader[0].ToString();
            }
            _con.Close();

            return res;
        }
    }
}
