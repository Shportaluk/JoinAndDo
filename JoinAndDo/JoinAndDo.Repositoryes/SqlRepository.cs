using JoinAndDo.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;

namespace JoinAndDo.Repositoryes
{
    public class SqlRepository
    {
        private string conStr = ConfigurationManager.ConnectionStrings["con_str"].ConnectionString;
        private SqlConnection con { get; set; }
        private SqlCommand cmdJoins = new SqlCommand("SELECT * FROM Joins");
        private SqlCommand cmdMyAccession = new SqlCommand("SELECT * FROM My_accession");
        private SqlCommand cmdDealsAccession = new SqlCommand("SELECT * FROM Deals_accession");


        public SqlRepository()
        {
            con = new SqlConnection(conStr);
        }

        public List<JoinsEntity> GetAllFromJoins(  )
        {
            List<JoinsEntity> listJoins = new List<JoinsEntity>();
            cmdJoins.Connection = con;

            con.Open();
            SqlDataReader reader = cmdJoins.ExecuteReader();
            
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

            con.Close();
            return listJoins;
        }
        public List<MyAccession> GetAllFromMyAccession(  )
        {
            List<MyAccession> listMyAccession = new List<MyAccession>();
            cmdMyAccession.Connection = con;

            con.Open();
            SqlDataReader reader = cmdMyAccession.ExecuteReader();

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

            con.Close();
            return listMyAccession;
        }
        public List<DealsAccession> GetAllFromDealsAccession()
        {
            List<DealsAccession> listDealsAccession = new List<DealsAccession>();
            cmdDealsAccession.Connection = con;

            con.Open();
            SqlDataReader reader = cmdDealsAccession.ExecuteReader();


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

            con.Close();
            return listDealsAccession;
        }
    }
}
