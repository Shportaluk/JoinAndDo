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

            //int id;
            string title;
            string text;
            //bool isComplete;
            //int priority;


            while ( reader.Read() )
            {

                title = reader[1].ToString();
                text = reader[2].ToString();

                listJoins.Add( new JoinsEntity( title, text ) );
            }

            con.Close();
            return listJoins;
        }
    }
}
