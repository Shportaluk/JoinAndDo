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
        private SqlCommand _cmdJoins = new SqlCommand("SELECT * FROM Joins");
        private SqlCommand _cmdMyAccession = new SqlCommand("SELECT * FROM My_accession");
        private SqlCommand _cmdDealsAccession = new SqlCommand("SELECT * FROM Deals_accession");


        public SqlRepository()
        {
            _con = new SqlConnection(_conStr);
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
