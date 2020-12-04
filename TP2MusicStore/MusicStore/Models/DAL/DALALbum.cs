namespace MusicStore.Models.DAL {
    using MusicStore.Models.DataModels;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.SqlClient;

    public class DALALbum {
        protected string ChaineConnexion = ConfigurationManager.ConnectionStrings["MusicStore"].ConnectionString;
        protected const string Album_INSERT = @"INSERT INTO Album(Titre,AnneeParution,Description,GenreId,Artiste,Prix) OUTPUT INSERTED.AlbumId VALUES(@Titre,@AnneeParution,@Description,@GenreId,@Artiste,@Prix);";
        protected const string Album_DELETE = @"DELETE Album WHERE AlbumId=@AlbumId";
        protected const string Album_UPDATE = @"UPDATE Album SET Titre=@Titre,AnneeParution=@AnneeParution,Artiste=@Artiste,Description=@Description,Prix=@Prix,GenreId=@GenreId WHERE AlbumId=@AlbumId";
        protected const string Album_SELECTALL = @"SELECT * from Album";
        protected const string Album_FINDBYID = @"SELECT * FROM Album WHERE AlbumId=@AlbumId";
        protected const string Album_FINDBYNAME = @"SELECT * FROM Album WHERE Titre=@Titre";

        public void Add(Album a)
        {
            using (SqlConnection connection = new SqlConnection(this.ChaineConnexion))
            {
                SqlCommand command = new SqlCommand(Album_INSERT, connection);
                command.Parameters.AddWithValue("Titre", a.Titre);
                command.Parameters.AddWithValue("AnneeParution", a.AnneeParution);
                command.Parameters.AddWithValue("Artiste", a.Artiste);
                command.Parameters.AddWithValue("Description", a.Description);
                command.Parameters.AddWithValue("Prix", a.Prix);
                command.Parameters.AddWithValue("GenreId", a.GenreId);

                connection.Open();
                a.AlbumId = Convert.ToInt32(command.ExecuteScalar());
            }
        }
        public void Remove(Album a) { this.Remove(a.AlbumId); }
        public void Remove(int AlbumId)
        {
            using (SqlConnection connection = new SqlConnection(this.ChaineConnexion))
            {
                SqlCommand command = new SqlCommand(Album_DELETE, connection);
                command.Parameters.AddWithValue("AlbumId", AlbumId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public void Update(Album entity)
        {
            using (SqlConnection connection = new SqlConnection(this.ChaineConnexion))
            {
                SqlCommand command = new SqlCommand(Album_UPDATE, connection);
                command.Parameters.AddWithValue("AlbumId", entity.AlbumId);
                command.Parameters.AddWithValue("Titre", entity.Titre);
                command.Parameters.AddWithValue("AnneeParution", entity.AnneeParution);
                command.Parameters.AddWithValue("Artiste", entity.Artiste);
                command.Parameters.AddWithValue("Description", entity.Description);
                command.Parameters.AddWithValue("Prix", entity.Prix);
                command.Parameters.AddWithValue("GenreId", entity.GenreId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public Album Find(int AlbumId)
        {
            using (SqlConnection connection = new SqlConnection(this.ChaineConnexion))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(Album_FINDBYID, connection);
                command.Parameters.AddWithValue("AlbumId", AlbumId);
                SqlDataReader dr = command.ExecuteReader();
                Album a = null;
                if (dr.Read())
                {
                    a = new Album
                    {
                        AlbumId = (int)dr["AlbumId"],
                        Titre = dr["Titre"].ToString(),
                        AnneeParution = (int)dr["AnneeParution"],
                        Artiste = dr["Artiste"].ToString(),
                        Description = dr["Description"].ToString(),
                        Prix = double.Parse(dr["Prix"].ToString()),
                        GenreId = (int)dr["GenreId"],

                    };
                }
                dr.Close();
                return a;
            }
        }
        public Album FindByTitre(string nom)
        {
            using (SqlConnection connection = new SqlConnection(this.ChaineConnexion))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(Album_FINDBYNAME, connection);
                command.Parameters.AddWithValue("Titre", nom);
                SqlDataReader dr = command.ExecuteReader();
                Album a = null;
                if (dr.Read())
                {
                    a = new Album
                    {
                        AlbumId = (int)dr["AlbumId"],
                        Titre = dr["Titre"].ToString(),
                        AnneeParution = (int)dr["AnneeParution"],
                        Artiste = dr["Artiste"].ToString(),
                        Description = dr["Description"].ToString(),
                        Prix = double.Parse(dr["Prix"].ToString()),
                        GenreId = (int)dr["GenreId"],
                    
                    };
                }
                dr.Close();
                return a;
            }
        }
        public List<Album> List()
        {
            using (SqlConnection connection = new SqlConnection(this.ChaineConnexion))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(Album_SELECTALL, connection);
                SqlDataReader dr = command.ExecuteReader();
                List<Album> la = new List<Album>();
                while (dr.Read())
                {
                    la.Add(new Album
                    {
                        AlbumId = (int)dr["AlbumId"],
                        Titre = dr["Titre"].ToString(),
                        AnneeParution =(int)dr["AnneeParution"],
                        Artiste= dr["Artiste"].ToString(),
                        Description = dr["Description"].ToString(),
                        Prix = double.Parse(dr["Prix"].ToString()),
                        GenreId = (int)dr["GenreId"],
                    });
                }
                dr.Close();
                return la;
            }
        }
    }
}