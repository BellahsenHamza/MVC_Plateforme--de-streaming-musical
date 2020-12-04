namespace MusicStore.Models {
    using MusicStore.Models.DAL;
    public class Depot {
        public DALUtilisateur Utilisateurs { get; private set; } = new DALUtilisateur();

        public DALGenre Genres { get; private set; } = new DALGenre();
        public DALALbum Albums { get; private set; } = new DALALbum();

        public DALPanier Paniers { get; private set; } = new DALPanier();

    }

}