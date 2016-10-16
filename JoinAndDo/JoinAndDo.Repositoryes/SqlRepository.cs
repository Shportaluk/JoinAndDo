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

        public SqlRepository()
        {
            _con = new SqlConnection( _conStr );
        }


        public string AddSkillToUser(string login, string hash, string pathImg, string name)
        {
            string res = null;
            string comm = "EXEC AddSkillToUser @login = @Login, @hash = @Hash, @pathImg = @PathImg, @name = @Name";

            SqlCommand sqlComm = new SqlCommand(comm, _con);

            SqlParameter Param1 = new SqlParameter("@Login", System.Data.SqlDbType.NVarChar);
            SqlParameter Param2 = new SqlParameter("@Hash", System.Data.SqlDbType.NVarChar);
            SqlParameter Param3 = new SqlParameter("@PathImg", System.Data.SqlDbType.NVarChar);
            SqlParameter Param4 = new SqlParameter("@Name", System.Data.SqlDbType.NVarChar);

            Param1.Value = login;
            Param2.Value = hash;
            Param3.Value = pathImg;
            Param4.Value = name;

            sqlComm.Parameters.Add(Param1);
            sqlComm.Parameters.Add(Param2);
            sqlComm.Parameters.Add(Param3);
            sqlComm.Parameters.Add(Param4);

            _con.Open();
            SqlDataReader reader = sqlComm.ExecuteReader();

            while (reader.Read())
            {
                res = reader[0].ToString();
            }

            _con.Close();
            return res;
        }
        public string AddUserToAccession(string login, string hash, string loginUserAdded, string role, int idAccession)
        {
            string res = null;
            string comm = "EXEC AddUserToAccession @login = @Login, @hash = @Hash, @loginUserAdded = @LoginUserAdded, @role = @Role, @idAccession = @IdAccession";

            SqlCommand sqlComm = new SqlCommand(comm, _con);

            SqlParameter Param1 = new SqlParameter("@Login", System.Data.SqlDbType.NVarChar);
            SqlParameter Param2 = new SqlParameter("@Hash", System.Data.SqlDbType.NVarChar);
            SqlParameter Param3 = new SqlParameter("@LoginUserAdded", System.Data.SqlDbType.NVarChar);
            SqlParameter Param4 = new SqlParameter("@Role", System.Data.SqlDbType.NVarChar);
            SqlParameter Param5 = new SqlParameter("@IdAccession", System.Data.SqlDbType.Int);

            Param1.Value = login;
            Param2.Value = hash;
            Param3.Value = loginUserAdded;
            Param4.Value = role;
            Param5.Value = idAccession;


            sqlComm.Parameters.Add(Param1);
            sqlComm.Parameters.Add(Param2);
            sqlComm.Parameters.Add(Param3);
            sqlComm.Parameters.Add(Param4);
            sqlComm.Parameters.Add(Param5);
            
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
            string res = null;
            string comm = "EXEC Registration @login = @Login, @pass = @Pass, @firstName = @FirstName, @lastName = @LastName";

            SqlCommand sqlComm = new SqlCommand(comm, _con);

            SqlParameter Param1 = new SqlParameter("@Login", System.Data.SqlDbType.NVarChar);
            SqlParameter Param2 = new SqlParameter("@Pass", System.Data.SqlDbType.NVarChar);
            SqlParameter Param3 = new SqlParameter("@FirstName", System.Data.SqlDbType.NVarChar);
            SqlParameter Param4 = new SqlParameter("@LastName", System.Data.SqlDbType.NVarChar);

            Param1.Value = login;
            Param2.Value = pass;
            Param3.Value = firstName;
            Param4.Value = lastName;

            sqlComm.Parameters.Add(Param1);
            sqlComm.Parameters.Add(Param2);
            sqlComm.Parameters.Add(Param3);
            sqlComm.Parameters.Add(Param4);

            _con.Open();
            SqlDataReader reader = sqlComm.ExecuteReader();

            while( reader.Read() )
            {
                res = reader[0].ToString();
            }
            _con.Close();
            return res;
        }
        public bool IsAuthenticated( string login, string hash )
        {
            bool isAuth = false;
            string comm = "SELECT Login FROM Users WHERE Login = @Login and Hash = @Hash";

            SqlCommand sqlComm = new SqlCommand(comm, _con);

            SqlParameter Param1 = new SqlParameter("@Login", System.Data.SqlDbType.NVarChar);
            SqlParameter Param2 = new SqlParameter("@Pass", System.Data.SqlDbType.NVarChar);

            Param1.Value = login;
            Param2.Value = hash;

            sqlComm.Parameters.Add(Param1);
            sqlComm.Parameters.Add(Param2);

            _con.Open();
            SqlDataReader reader = sqlComm.ExecuteReader();


            while (reader.Read())
            {
                if (!string.IsNullOrEmpty(reader[0].ToString()))
                {
                    isAuth = true;
                }
            }

            _con.Close();
            return isAuth;
        }
        public User Authentication( string login, string pass )
        {
            User user = new User();

            string comm = "SELECT Id, Login, FirstName, LastName, Hash, HaveNewMsg FROM Users where Login = @Login and Pass = @Pass";
            SqlCommand sqlComm = new SqlCommand(comm, _con);

            SqlParameter Param1 = new SqlParameter("@Login", System.Data.SqlDbType.NVarChar);
            SqlParameter Param2 = new SqlParameter("@Pass", System.Data.SqlDbType.NVarChar);

            Param1.Value = login;
            Param2.Value = pass;

            sqlComm.Parameters.Add(Param1);
            sqlComm.Parameters.Add(Param2);
            
            _con.Open();
            SqlDataReader reader = sqlComm.ExecuteReader();

            while (reader.Read())
            {
                user.Id = reader[0].ToString();
                user.Login = reader[1].ToString();
                user.FirstName = reader[2].ToString();
                user.LastName = reader[3].ToString();
                user.Hash = reader[4].ToString();
                user.NewMsg = reader[5].ToString();
            }
            _con.Close();
            return user;
        }
        public void DeleteHash(string login, string hash)
        {
            string comm = "UPDATE Users SET Hash = NULL where Login = @Login and Hash = @Hash";
            SqlCommand sqlComm = new SqlCommand(comm, _con);

            SqlParameter Param1 = new SqlParameter("@Login", System.Data.SqlDbType.NVarChar);
            SqlParameter Param2 = new SqlParameter("@Pass", System.Data.SqlDbType.NVarChar);

            Param1.Value = login;
            Param2.Value = hash;

            sqlComm.Parameters.Add(Param1);
            sqlComm.Parameters.Add(Param2);

            _con.Open();
            SqlDataReader reader = sqlComm.ExecuteReader();
            _con.Close();
        }

        public string LoadProfileImg(string login, string hash, string pathImg)
        {
            string res = null;
            string comm = "EXEC LoadProfileImg @login = @Login, @hash = @Hash, @pathImg = @PathImg";
            SqlCommand sqlComm = new SqlCommand(comm, _con);

            SqlParameter Param1 = new SqlParameter("@Login", System.Data.SqlDbType.NVarChar);
            SqlParameter Param2 = new SqlParameter("@Hash", System.Data.SqlDbType.NVarChar);
            SqlParameter Param3 = new SqlParameter("@PathImg", System.Data.SqlDbType.NVarChar);

            Param1.Value = login;
            Param2.Value = hash;
            Param3.Value = pathImg;

            sqlComm.Parameters.Add(Param1);
            sqlComm.Parameters.Add(Param2);
            sqlComm.Parameters.Add(Param3);

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
            
            string comm = "UPDATE Users SET Hash = @Hash WHERE Login = @Login and Pass = @Pass";
            SqlCommand sqlComm = new SqlCommand(comm, _con);

            SqlParameter Param1 = new SqlParameter("@Hash", System.Data.SqlDbType.NVarChar);
            SqlParameter Param2 = new SqlParameter("@Login", System.Data.SqlDbType.NVarChar);
            SqlParameter Param3 = new SqlParameter("@Pass", System.Data.SqlDbType.NVarChar);

            Param1.Value = hash;
            Param2.Value = login;
            Param3.Value = pass;

            sqlComm.Parameters.Add(Param1);
            sqlComm.Parameters.Add(Param2);
            sqlComm.Parameters.Add(Param3);

            _con.Open();
            SqlDataReader reader = sqlComm.ExecuteReader();
            _con.Close();
            

            return hash;
        }
        public string SendMsg( string login, string hash, string to, string text )
        {
            string res = null;
            string comm = "EXEC SendMsg @login = @Login, @hash = @Hash, @to = @To, @text = @Text";
            SqlCommand sqlComm = new SqlCommand(comm, _con);

            SqlParameter Param1 = new SqlParameter("@Login", System.Data.SqlDbType.NVarChar);
            SqlParameter Param2 = new SqlParameter("@Hash", System.Data.SqlDbType.NVarChar);
            SqlParameter Param3 = new SqlParameter("@To", System.Data.SqlDbType.NVarChar);
            SqlParameter Param4 = new SqlParameter("@Text", System.Data.SqlDbType.NVarChar);

            Param1.Value = login;
            Param2.Value = hash;
            Param3.Value = to;
            Param4.Value = text;

            sqlComm.Parameters.Add(Param1);
            sqlComm.Parameters.Add(Param2);
            sqlComm.Parameters.Add(Param3);
            sqlComm.Parameters.Add(Param4);

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
            string comm = "SELECT PathImg, Name FROM ListSkillsOfUsers WHERE Login = @Login";
            SqlCommand sqlComm = new SqlCommand(comm, _con);

            SqlParameter Param1 = new SqlParameter("@Login", System.Data.SqlDbType.NVarChar);

            Param1.Value = login;

            sqlComm.Parameters.Add(Param1);

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
            string comm = "EXEC GetMyInvitation @login = Login, @hash = @Hash";
            SqlCommand sqlComm = new SqlCommand(comm, _con);

            SqlParameter Param1 = new SqlParameter("@Login", System.Data.SqlDbType.NVarChar);
            SqlParameter Param2 = new SqlParameter("@Hash", System.Data.SqlDbType.NVarChar);

            Param1.Value = login;
            Param2.Value = hash;

            sqlComm.Parameters.Add(Param1);
            sqlComm.Parameters.Add(Param2);

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
        public List<User> GetUsers( string full_name )
        {
            List<User> listUser = new List<User>();

            string comm;
            SqlCommand sqlComm;
            if (String.IsNullOrEmpty(full_name))
            {
                comm = "SELECT Id, FirstName, LastName, Hash, PathImg FROM Users";
                sqlComm = new SqlCommand(comm, _con);
            }
            else
            {
                comm = "EXEC GetUserByName @name = @Name";
                sqlComm = new SqlCommand(comm, _con);
                SqlParameter Param1 = new SqlParameter("@Name", System.Data.SqlDbType.NVarChar);
                Param1.Value = full_name;
                sqlComm.Parameters.Add(Param1);
            }


            _con.Open();
            SqlDataReader reader = sqlComm.ExecuteReader();
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
            string comm = "EXEC GetInterlocutor @login = Login";
            SqlCommand sqlComm = new SqlCommand(comm, _con);

            SqlParameter Param1 = new SqlParameter("@Login", System.Data.SqlDbType.NVarChar);
            Param1.Value = login;
            sqlComm.Parameters.Add(Param1);

            _con.Open();
            SqlDataReader reader = sqlComm.ExecuteReader();
            while (reader.Read())
            {
                interlocutors.Add( reader[0].ToString() );
            }
            _con.Close();

            return interlocutors;
        }
        public List<Message> GetDialog( string login, string hash, string loginInterlocutor )
        {
            List < Message > dialog = new List<Message>();
            
            string comm = "EXEC GetDialog @login = @Login, @hash = @Hash, @loginInterlocutor = @LoginInterlocutor";
            SqlCommand sqlComm = new SqlCommand(comm, _con);

            SqlParameter Param1 = new SqlParameter("@Login", System.Data.SqlDbType.NVarChar);
            SqlParameter Param2 = new SqlParameter("@Hash", System.Data.SqlDbType.NVarChar);
            SqlParameter Param3 = new SqlParameter("@LoginInterlocutor", System.Data.SqlDbType.NVarChar);

            Param1.Value = login;
            Param2.Value = hash;
            Param3.Value = loginInterlocutor;

            sqlComm.Parameters.Add(Param1);
            sqlComm.Parameters.Add(Param2);
            sqlComm.Parameters.Add(Param3);

            _con.Open();
            SqlDataReader reader = sqlComm.ExecuteReader();
            while (reader.Read())
            {
                Message msg = new Message();
                msg.Login = reader[0].ToString();
                msg.Text = reader[1].ToString();
                msg.LoginInterlocutor = reader[2].ToString();
                msg.Date = reader[3].ToString();
                dialog.Add( msg );
            }

            _con.Close();
            return dialog;
        }
        public User GetUserById(int? iD)
        {
            User user = null;
            string comm = "SELECT Id, Login, FirstName, LastName, Hash, PathImg, CompletedAccessions, AbandonedAccessions, CurrentlyAccessions, AllAccessions FROM Users where Id = @Id";
            SqlCommand sqlComm = new SqlCommand(comm, _con);

            SqlParameter Param1 = new SqlParameter("@Id", System.Data.SqlDbType.NVarChar);
            Param1.Value = iD;
            sqlComm.Parameters.Add(Param1);

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
            User user = null;
            string comm = "SELECT Id, Login, FirstName, LastName, Hash, PathImg, CompletedAccessions, AbandonedAccessions, CurrentlyAccessions, AllAccessions FROM Users WHERE Login = @Login";
            SqlCommand sqlComm = new SqlCommand(comm, _con);

            SqlParameter Param1 = new SqlParameter("@Login", System.Data.SqlDbType.NVarChar);
            Param1.Value = login;
            sqlComm.Parameters.Add(Param1);

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
            
            string comm = "EXEC GetUsersByIdOfAccession @idAccession = @Id";
            SqlCommand sqlComm = new SqlCommand(comm, _con);

            SqlParameter Param1 = new SqlParameter("@Id", System.Data.SqlDbType.NVarChar);
            Param1.Value = id;
            sqlComm.Parameters.Add(Param1);

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
        
        public List<Accession> GetMyAccessionsManagement( string login, string hash )
        {
            List<Accession> listMyAccession = new List<Accession>();
            string comm = "EXEC GetMyAccessionsManagement @login = @Login, @hash = @Hash";

            SqlCommand sqlComm = new SqlCommand(comm, _con);
            SqlParameter Param1 = new SqlParameter("@Login", System.Data.SqlDbType.NVarChar);
            SqlParameter Param2 = new SqlParameter("@Hash", System.Data.SqlDbType.NVarChar);
            Param1.Value = login;
            Param2.Value = hash;
            sqlComm.Parameters.Add(Param1);
            sqlComm.Parameters.Add(Param2);

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
                listMyAccession.Add(accession);
            }

            _con.Close();
            return listMyAccession;
        }
        public List<Accession> GetMyAccessions(string login, string hash)
        {
            List<Accession> listMyAccession = new List<Accession>();
            string comm = "EXEC GetMyAccessions @login = @Login, @hash = @Hash";

            SqlCommand sqlComm = new SqlCommand(comm, _con);
            SqlParameter Param1 = new SqlParameter("@Login", System.Data.SqlDbType.NVarChar);
            SqlParameter Param2 = new SqlParameter("@Hash", System.Data.SqlDbType.NVarChar);
            Param1.Value = login;
            Param2.Value = hash;
            sqlComm.Parameters.Add(Param1);
            sqlComm.Parameters.Add(Param2);

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
                listMyAccession.Add(accession);
            }

            _con.Close();
            return listMyAccession;
        }
        public List<Accession> GetAccessions( string text )
        {
            List < Accession > listAccession = new List < Accession >();
            
            string comm = "SELECT * FROM Accession WHERE Text Like @Text";
            SqlCommand sqlComm = new SqlCommand(comm, _con);
            SqlParameter Param1 = new SqlParameter("@Text", System.Data.SqlDbType.NVarChar);
            Param1.Value = "%"+text+"%";
            sqlComm.Parameters.Add(Param1);

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
            string comm = "SELECT RoleName FROM RoleOfUserInAccession WHERE IdAccession = @Id and Login IS NULL";
            SqlCommand sqlComm = new SqlCommand(comm, _con);
            SqlParameter Param1 = new SqlParameter("@Id", System.Data.SqlDbType.NVarChar);
            Param1.Value = id;
            sqlComm.Parameters.Add(Param1);

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
            string comm = "SELECT * FROM Accession WHERE Id = @Id";
            SqlCommand sqlComm = new SqlCommand(comm, _con);
            SqlParameter Param1 = new SqlParameter("@Id", System.Data.SqlDbType.NVarChar);
            Param1.Value = id;
            sqlComm.Parameters.Add(Param1);

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
        
        public List<RequestJoinToAccession> GetRequestsAdditionToAccession( int? id )
        {
            List<RequestJoinToAccession> listRequest = new List<RequestJoinToAccession>();
            
            string comm = "SELECT * FROM RequestJoinToAccession WHERE ToIdAccession = @Id and Status = 'Waiting'";
            SqlCommand sqlComm = new SqlCommand(comm, _con);
            SqlParameter Param1 = new SqlParameter("@Id", System.Data.SqlDbType.NVarChar);
            Param1.Value = id;
            sqlComm.Parameters.Add(Param1);

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
            string comm = "EXEC RemoveUserFromAccession @login = @Login, @hash = @Hash, @idAccession = @IdAccession, @user = @User";
            SqlCommand sqlComm = new SqlCommand(comm, _con);

            SqlParameter Param1 = new SqlParameter("@Login", System.Data.SqlDbType.NVarChar);
            SqlParameter Param2 = new SqlParameter("@Hash", System.Data.SqlDbType.NVarChar);
            SqlParameter Param3 = new SqlParameter("@IdAccession", System.Data.SqlDbType.Int);
            SqlParameter Param4 = new SqlParameter("@User", System.Data.SqlDbType.NVarChar);

            Param1.Value = login;
            Param2.Value = hash;
            Param3.Value = idAccession;
            Param4.Value = user;

            sqlComm.Parameters.Add(Param1);
            sqlComm.Parameters.Add(Param2);
            sqlComm.Parameters.Add(Param3);
            sqlComm.Parameters.Add(Param4);

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
            string comm = "EXEC SendRequestToAccession @login = @Login, @hash = @Hash, @text = @Text, @category = @Category, @idAccession = @IdAccession";
            SqlCommand sqlComm = new SqlCommand(comm, _con);

            SqlParameter Param1 = new SqlParameter("@Login", System.Data.SqlDbType.NVarChar);
            SqlParameter Param2 = new SqlParameter("@Hash", System.Data.SqlDbType.NVarChar);
            SqlParameter Param3 = new SqlParameter("@Text", System.Data.SqlDbType.NVarChar);
            SqlParameter Param4 = new SqlParameter("@Category", System.Data.SqlDbType.NVarChar);
            SqlParameter Param5 = new SqlParameter("@IdAccession", System.Data.SqlDbType.Int);

            Param1.Value = login;
            Param2.Value = hash;
            Param3.Value = text;
            Param4.Value = category;
            Param5.Value = idAccession;

            sqlComm.Parameters.Add(Param1);
            sqlComm.Parameters.Add(Param2);
            sqlComm.Parameters.Add(Param3);
            sqlComm.Parameters.Add(Param4);
            sqlComm.Parameters.Add(Param5);

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
            string comm = "DECLARE @res NVARCHAR(255) EXEC RequestComplateAccession @login = @Login, @hash = @Hash, @idAccession = @IdAccession, @res = @res OUTPUT SELECT @res";
            SqlCommand sqlComm = new SqlCommand(comm, _con);

            SqlParameter Param1 = new SqlParameter("@Login", System.Data.SqlDbType.NVarChar);
            SqlParameter Param2 = new SqlParameter("@Hash", System.Data.SqlDbType.NVarChar);
            SqlParameter Param3 = new SqlParameter("@IdAccession", System.Data.SqlDbType.Int);

            Param1.Value = login;
            Param2.Value = hash;
            Param3.Value = idAccession;

            sqlComm.Parameters.Add(Param1);
            sqlComm.Parameters.Add(Param2);
            sqlComm.Parameters.Add(Param3);

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
            
            string comm = "EXEC EditTitleOfAccession @login = @Login, @hash = @Hash, @idAccession = @IdAccession, @title = @Title";
            SqlCommand sqlComm = new SqlCommand(comm, _con);

            SqlParameter Param1 = new SqlParameter("@Login", System.Data.SqlDbType.NVarChar);
            SqlParameter Param2 = new SqlParameter("@Hash", System.Data.SqlDbType.NVarChar);
            SqlParameter Param3 = new SqlParameter("@IdAccession", System.Data.SqlDbType.Int);
            SqlParameter Param4 = new SqlParameter("@Title", System.Data.SqlDbType.NVarChar);

            Param1.Value = login;
            Param2.Value = hash;
            Param3.Value = idAccession;
            Param4.Value = title;

            sqlComm.Parameters.Add(Param1);
            sqlComm.Parameters.Add(Param2);
            sqlComm.Parameters.Add(Param3);
            sqlComm.Parameters.Add(Param4);

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
            
            string comm = "EXEC EditDescriptionOfAccession @login = @Login, @hash = @Hash, @idAccession = @IdAccession, @description = @Description";
            SqlCommand sqlComm = new SqlCommand(comm, _con);

            SqlParameter Param1 = new SqlParameter("@Login", System.Data.SqlDbType.NVarChar);
            SqlParameter Param2 = new SqlParameter("@Hash", System.Data.SqlDbType.NVarChar);
            SqlParameter Param3 = new SqlParameter("@IdAccession", System.Data.SqlDbType.Int);
            SqlParameter Param4 = new SqlParameter("@Description", System.Data.SqlDbType.NVarChar);

            Param1.Value = login;
            Param2.Value = hash;
            Param3.Value = idAccession;
            Param4.Value = description;

            sqlComm.Parameters.Add(Param1);
            sqlComm.Parameters.Add(Param2);
            sqlComm.Parameters.Add(Param3);
            sqlComm.Parameters.Add(Param4);

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
            string idAccession = null;
            string comm = "EXEC NewJoin @login = @Login, @hash = @Hash, @title = @Title, @text = @Text, @category = @Category, @needPeople = @NeedPeople";
            SqlCommand sqlComm = new SqlCommand(comm, _con);

            SqlParameter Param1 = new SqlParameter("@Login", System.Data.SqlDbType.NVarChar);
            SqlParameter Param2 = new SqlParameter("@Hash", System.Data.SqlDbType.NVarChar);
            SqlParameter Param3 = new SqlParameter("@Title", System.Data.SqlDbType.NVarChar);
            SqlParameter Param4 = new SqlParameter("@Text", System.Data.SqlDbType.NVarChar);
            SqlParameter Param5 = new SqlParameter("@Category", System.Data.SqlDbType.NVarChar);
            SqlParameter Param6 = new SqlParameter("@NeedPeople", System.Data.SqlDbType.Int);

            Param1.Value = login;
            Param2.Value = hash;
            Param3.Value = title;
            Param4.Value = text;
            Param5.Value = category;
            Param6.Value = needPeople;

            sqlComm.Parameters.Add(Param1);
            sqlComm.Parameters.Add(Param2);
            sqlComm.Parameters.Add(Param3);
            sqlComm.Parameters.Add(Param4);
            sqlComm.Parameters.Add(Param5);
            sqlComm.Parameters.Add(Param6);

            _con.Open();
            SqlDataReader reader = sqlComm.ExecuteReader();
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
            string comm = "INSERT INTO RoleOfUserInAccession VALUES ( @Creator, 'Creator', @IdAccession )\n";
            string[] separator = { "," };
            string[] listRoles = arrayRoles.Split( separator, StringSplitOptions.RemoveEmptyEntries );
            foreach( string role in listRoles )
            {
                comm += String.Format("INSERT INTO RoleOfUserInAccession VALUES ( NULL, '"+role+"', @IdAccession )\n" );
            }
            SqlCommand sqlComm = new SqlCommand(comm, _con);

            SqlParameter Param1 = new SqlParameter("@Creator", System.Data.SqlDbType.NVarChar);
            SqlParameter Param2 = new SqlParameter("@IdAccession", System.Data.SqlDbType.Int);

            Param1.Value = creator;
            Param2.Value = idAccession;

            sqlComm.Parameters.Add(Param1);
            sqlComm.Parameters.Add(Param2);

            _con.Open();
            SqlDataReader reader = sqlComm.ExecuteReader();
            _con.Close();
        }
        public string RequestDeleteAccession(string login, string hash, int idAccession)
        {
            string res = null;
            string comm = "DECLARE @res NVARCHAR(255) EXEC RequestDeleteAccession @login = @Login, @hash = @Hash, @idAccession = @IdAccession, @res = @res OUTPUT SELECT @res";
            SqlCommand sqlComm = new SqlCommand(comm, _con);

            SqlParameter Param1 = new SqlParameter("@Login", System.Data.SqlDbType.NVarChar);
            SqlParameter Param2 = new SqlParameter("@Hash", System.Data.SqlDbType.NVarChar);
            SqlParameter Param3 = new SqlParameter("@IdAccession", System.Data.SqlDbType.Int);

            Param1.Value = login;
            Param2.Value = hash;
            Param3.Value = idAccession;

            sqlComm.Parameters.Add(Param1);
            sqlComm.Parameters.Add(Param2);
            sqlComm.Parameters.Add(Param3);

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
            string comm = "EXEC AcceptRequestOfUserToAccession @login = @Login, @hash = @Hash, @user = @User, @role = @Role, @idAccession = @IdAccession";
            SqlCommand sqlComm = new SqlCommand(comm, _con);

            SqlParameter Param1 = new SqlParameter("@Login", System.Data.SqlDbType.NVarChar);
            SqlParameter Param2 = new SqlParameter("@Hash", System.Data.SqlDbType.NVarChar);
            SqlParameter Param3 = new SqlParameter("@User", System.Data.SqlDbType.NVarChar);
            SqlParameter Param4 = new SqlParameter("@Role", System.Data.SqlDbType.NVarChar);
            SqlParameter Param5 = new SqlParameter("@IdAccession", System.Data.SqlDbType.Int);

            Param1.Value = login;
            Param2.Value = hash;
            Param3.Value = user;
            Param4.Value = role;
            Param5.Value = idAccession;

            sqlComm.Parameters.Add(Param1);
            sqlComm.Parameters.Add(Param2);
            sqlComm.Parameters.Add(Param3);
            sqlComm.Parameters.Add(Param4);
            sqlComm.Parameters.Add(Param5);

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
            string comm = "EXEC RejectRequestOfUserToAccession @login = @Login, @hash = @Hash, @user = @User, @idAccession = @IdAccession";
            SqlCommand sqlComm = new SqlCommand(comm, _con);

            SqlParameter Param1 = new SqlParameter("@Login", System.Data.SqlDbType.NVarChar);
            SqlParameter Param2 = new SqlParameter("@Hash", System.Data.SqlDbType.NVarChar);
            SqlParameter Param3 = new SqlParameter("@User", System.Data.SqlDbType.NVarChar);
            SqlParameter Param4 = new SqlParameter("@IdAccession", System.Data.SqlDbType.Int);

            Param1.Value = login;
            Param2.Value = hash;
            Param3.Value = user;
            Param4.Value = idAccession;

            sqlComm.Parameters.Add(Param1);
            sqlComm.Parameters.Add(Param2);
            sqlComm.Parameters.Add(Param3);
            sqlComm.Parameters.Add(Param4);

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
            string comm = "EXEC SendMsgToAccession @login = @Login, @hash = @Hash, @idAccession = @IdAccession, @text = @Text";
            SqlCommand sqlComm = new SqlCommand(comm, _con);

            SqlParameter Param1 = new SqlParameter("@Login", System.Data.SqlDbType.NVarChar);
            SqlParameter Param2 = new SqlParameter("@Hash", System.Data.SqlDbType.NVarChar);
            SqlParameter Param3 = new SqlParameter("@IdAccession", System.Data.SqlDbType.Int);
            SqlParameter Param4 = new SqlParameter("@Text", System.Data.SqlDbType.NVarChar);

            Param1.Value = login;
            Param2.Value = hash;
            Param3.Value = idAccession;
            Param4.Value = text;

            sqlComm.Parameters.Add(Param1);
            sqlComm.Parameters.Add(Param2);
            sqlComm.Parameters.Add(Param3);
            sqlComm.Parameters.Add(Param4);

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
            
            string comm = "EXEC GetDialogOfAccession @login = @Login, @hash = @Hash, @idAccession = @IdAccession";
            SqlCommand sqlComm = new SqlCommand(comm, _con);

            SqlParameter Param1 = new SqlParameter("@Login", System.Data.SqlDbType.NVarChar);
            SqlParameter Param2 = new SqlParameter("@Hash", System.Data.SqlDbType.NVarChar);
            SqlParameter Param3 = new SqlParameter("@IdAccession", System.Data.SqlDbType.Int);

            Param1.Value = login;
            Param2.Value = hash;
            Param3.Value = idAccession;

            sqlComm.Parameters.Add(Param1);
            sqlComm.Parameters.Add(Param2);
            sqlComm.Parameters.Add(Param3);

            _con.Open();
            SqlDataReader reader = sqlComm.ExecuteReader();
            while (reader.Read())
            {
                Message msg = new Message();
                msg.Login = reader[0].ToString();
                msg.Text = reader[1].ToString();
                dialog.Add(msg);
            }
            _con.Close();

            return dialog;
        }
        public string ExitWithAccession(string login, string hash, int idAccession)
        {
            string res = null;
            string comm = "EXEC ExitWithAccession @login = @Login, @hash = @Hash, @idAccession = @IdAccession";
            SqlCommand sqlComm = new SqlCommand(comm, _con);

            SqlParameter Param1 = new SqlParameter("@Login", System.Data.SqlDbType.NVarChar);
            SqlParameter Param2 = new SqlParameter("@Hash", System.Data.SqlDbType.NVarChar);
            SqlParameter Param3 = new SqlParameter("@IdAccession", System.Data.SqlDbType.Int);

            Param1.Value = login;
            Param2.Value = hash;
            Param3.Value = idAccession;

            sqlComm.Parameters.Add(Param1);
            sqlComm.Parameters.Add(Param2);
            sqlComm.Parameters.Add(Param3);

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
