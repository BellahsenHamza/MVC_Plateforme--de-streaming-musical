namespace MusicStore.Models.DataModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Album
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AlbumId { get; set; }

        [Required, StringLength(100)]
        public string Titre { get; set; }

        [Required, Range(1930, 2100)]
        public int AnneeParution { get; set; }

        [Required, StringLength(50)]
        public string Artiste { get; set; }

        [Required, DisplayFormat(HtmlEncode = true), DataType(DataType.MultilineText), MaxLength(2500)]
        public string Description { get; set; }



        [DataType(DataType.Currency)]
        [Required, Range(10, 100)]
        public double Prix { get; set; }

        [Required]
        public int GenreId { get; set; }

        [ForeignKey("GenreId")]
        public Genre genre { get; set; }

        [NotMapped]
        public string Cover { get => $"/Content/Images/Albums/{AlbumId}.jpg"; }
    }
}