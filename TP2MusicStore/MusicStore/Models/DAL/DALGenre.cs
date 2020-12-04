namespace MusicStore.Models.DAL {
    using MusicStore.Models.DataModels;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.SqlClient;

    public class DALGenre {
        protected string ChaineConnexion = ConfigurationManager.ConnectionStrings["MusicStore"].ConnectionString;
        protected const string Genre_INSERT = @"INSERT INTO Genre(Nom) OUTPUT INSERTED.GenreId VALUES(@Nom);";
        protected const string Genre_DELETE = @"DELETE Genre WHERE GenreId=@GenreId";
        protected const string Genre_UPDATE = @"UPDATE Genre SET Nom=@Nom WHERE GenreId=@GenreId";
        protected const string Genre_SELECTALL = @"SELECT GenreId,Nom FROM Genre";
        protected const string Genre_FINDBYID = @"SELECT * FROM Genre WHERE GenreId=@GenreId";
        protected const string Genre_FINDBYNAME = @"SELECT * FROM Genre WHERE Nom=@Nom";

        public void Add(Genre g)
        {
            using (SqlConnection connection = new SqlConnection(this.ChaineConnexion))
            {
                SqlCommand command = new SqlCommand(Genre_INSERT, connection);
                command.Parameters.AddWithValue("Nom", g.Nom);

                connection.Open();
                g.GenreId = Convert.ToInt32(command.ExecuteScalar());
            }
        }
        public void Remove(Genre g) { this.Remove(g.GenreId); }
        public void Remove(int GenreId)
        {
            using (SqlConnection connection = new SqlConnection(this.ChaineConnexion))
            {
                SqlCommand command = new SqlCommand(Genre_DELETE, connection);
                command.Parameters.AddWithValue("GenreId", GenreId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public void Update(Genre entity)
        {
            using (SqlConnection connection = new SqlConnection(this.ChaineConnexion))
            {
                SqlCommand command = new SqlCommand(Genre_UPDATE, connection);
                command.Parameters.AddWithValue("GenreId", entity.GenreId);
                command.Parameters.AddWithValue("Nom", entity.Nom);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public Genre Find(int GenreId)
        {
            using (SqlConnection connection = new SqlConnection(this.ChaineConnexion))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(Genre_FINDBYID, connection);
                command.Parameters.AddWithValue("GenreId", GenreId);
                SqlDataReader dr = command.ExecuteReader();
                Genre g = null;
                if (dr.Read())
                {
                    g = new Genre
                    {
                        GenreId = (int)dr["GenreId"],
                        Nom = dr["Nom"].ToString(),
                    };
                }
                dr.Close();
                return g;
            }
        }
        public Genre FindByNom(string nom)
        {
            using (SqlConnection connection = new SqlConnection(this.ChaineConnexion))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(Genre_FINDBYNAME, connection);
                command.Parameters.AddWithValue("Nom", nom );
                SqlDataReader dr = command.ExecuteReader();
                Genre g = null;
                if (dr.Read())
                {
                    g = new Genre
                    {
                        GenreId = (int)dr["GenreId"],
                        Nom = dr["Nom"].ToString(),

                    };
                }
                dr.Close();
                return g;
            }
        }
        public List<Genre> List()
        {
            using (SqlConnection connection = new SqlConnection(this.ChaineConnexion))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(Genre_SELECTALL, connection);
                SqlDataReader dr = command.ExecuteReader();
                List<Genre> lu = new List<Genre>();
                while (dr.Read())
                {
                    lu.Add(new Genre
                    {
                        GenreId = (int)dr["GenreId"],
                        Nom = dr["Nom"].ToString(),

                    });
                }
                dr.Close();
                return lu;
            }
        }
    }
}