using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baseDeDatos
{
    public class BeerDB : DB
    {
        public BeerDB(string server, string db, bool trusted_connection)
            :base(server, db, trusted_connection)
        {
        }


        // obtener todos los elementos de la lista
        public List<Beer> GetAll()
        {
            Connect();
            List<Beer> beers = new List<Beer>();
            string query = "SELECT Id, Name, BrandId FROM BEER";

            // recibe la query (consulta) y la conexión
            SqlCommand cmd = new SqlCommand(query, _connection);

            // ejecutar comando (reader -> lee uno a uno de los elementos)
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                int brandId = reader.GetInt32(2);
                beers.Add(new Beer(id, name, brandId));
            }



            Close();

            return beers;
        }


        // obtener elemento de la lista por Id
        public Beer Get(int id)
        {
            Connect();
            Beer beer = null;
            string query = "SELECT Id, Name, BrandId FROM BEER " +
                           "WHERE id = @id";
            SqlCommand command = new SqlCommand(query, _connection);
            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                string name = reader.GetString(1);
                int brandId = reader.GetInt32(2);
                beer = new Beer(id, name, brandId);
            }


            Close();

            return beer;
        }


        // añadir un nuevo elemento a la lista
        public void Add(Beer beer)
        {
            Connect();
            string query = "INSERT INTO Beer(Name, BrandId) " +
                "Values (@name, @brandId)";
            SqlCommand command = new SqlCommand(query, _connection);
            command.Parameters.AddWithValue("@name", beer.Name);
            command.Parameters.AddWithValue("@brandId", beer.BrandId);
            command.ExecuteNonQuery();



            Close();
        }

        public void Edit(Beer beer)
        {
            Connect();
            string query = "UPDATE beer SET name=@name, brandId=@brandId " +
                           "WHERE id=@id";
            SqlCommand command = new SqlCommand(query, _connection);
            command.Parameters.AddWithValue("@name", beer.Name);
            command.Parameters.AddWithValue("@brandId", beer.BrandId);
            command.Parameters.AddWithValue("@id", beer.Id);
            command.ExecuteNonQuery();

            Close();
        }

        public void delete(int id)
        {
            Connect();

            string query = "DELETE FROM beer WHERE id=@id";
            SqlCommand cmd = new SqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();

            Close();
        }
    }
}
