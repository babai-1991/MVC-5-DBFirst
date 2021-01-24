using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ScratchPad.Models
{
    [MetadataType(typeof(ProductMetadata))]
    public partial class Product
    {
    }

    public class ProductMetadata
    {
        [Required(ErrorMessage = "Product Name is mandatory")]
        [MaxLength(10,ErrorMessage = "Cannot exceed 10 character")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Product price is mandatory")]
        [DataType(DataType.Currency,ErrorMessage = "Price must be valid")]
        public decimal? Price { get; set; }
    }
}