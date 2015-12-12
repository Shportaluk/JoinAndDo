using JoinAndDo.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinAndDo.Repositoryes
{
    public class SqlRepository
    {
        List<MyAccession> myAccession = new List<MyAccession>();

        public string con_str = "Data Source=.\\SQLExpress;Initial Catalog=master;Integrated Security=True";

        public List<JoinsEntity> GetAllFromJoins(  )
        {
            List<JoinsEntity> listJoins = new List<JoinsEntity>();

            SqlConnection con = new SqlConnection(con_str);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = String.Format( "SELECT * FROM Joins");
            cmd.Connection = con;

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            
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

            SqlConnection con = new SqlConnection(con_str);
            SqlCommand cmd = new SqlCommand( );
            cmd.CommandText = String.Format( "SELECT * FROM My_accession" );
            cmd.Connection = con;

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

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

            SqlConnection con = new SqlConnection(con_str);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = String.Format( "SELECT * FROM Deals_accession" );
            cmd.Connection = con;

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            string title;
            string text;
            
            while (reader.Read())
            {
                title = reader[1].ToString();
                text = reader[2].ToString();
                listDealsAccession.Add( new DealsAccession( title, text ) );
            }

            con.Close();
            return listDealsAccession;
        }
        //
    }
}
