using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace crowsoftmvc.Models
{
    public partial class BuildingImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdBuildingImage { get; set; }

        [Display(Name = "Description")]
        [StringLength(100)]
        [Required(ErrorMessage = "{0} is required.")]
        public string Description { get; set; }

        [Display(Name = "Select Image Folder")]
        [StringLength(200)]
        //[Required(ErrorMessage = "{0} is required.")]
        public string ImagePath { get; set; }

        [ForeignKey("idBuildingQuote")]
        public int BuildingQuoteIdBuildingQuote { get; set; }

        public virtual BuildingQuote BuildingQuoteIdBuildingQuoteNavigation { get; set; }
    }

    public static class IFormFileExtensions
    {
        public static string GetFilename(this IFormFile file)
        {
            return ContentDispositionHeaderValue.Parse(
                            file.ContentDisposition).FileName.ToString().Trim('"');
        }

        public static async Task<MemoryStream> GetFileStream(this IFormFile file)
        {
            MemoryStream filestream = new MemoryStream();
            await file.CopyToAsync(filestream);
            return filestream;
        }

        public static async Task<byte[]> GetFileArray(this IFormFile file)
        {
            MemoryStream filestream = new MemoryStream();
            await file.CopyToAsync(filestream);
            return filestream.ToArray();
        }
    }
}
