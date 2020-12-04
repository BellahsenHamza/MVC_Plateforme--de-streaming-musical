using MusicStore.Models.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MusicStore.Models.ViewModels.Album
{
    [CustomValidation(typeof(CreerAlbum), "ValiderCreation")]
    public class CreerAlbum
    {
        public static ValidationResult ValiderCreation(CreerAlbum c)
        {
            if (c.image != null && c.image.ContentType != "image/jpeg")
            {
                return new ValidationResult("L'image doit être un fichier jpg.", new[] { "" });
            }
            return ValidationResult.Success;
        }
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
        [ForeignKey("GenreId")]
        public int GenreId { get; set; }


        [Display(Name = "Image")]
        public HttpPostedFileBase image { get; set; }
    }
}