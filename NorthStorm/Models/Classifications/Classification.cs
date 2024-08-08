using System.ComponentModel.DataAnnotations;

namespace NorthStorm.Models.Classifications
{
    public class Classification
    {
        [Display(Name ="ترميز كركوك")]
        public string KirkukSymbol { get; set; }

        [Display(Name ="ترميز بغداد")]
        public string BaghdadSymbol { get; set; }
    }
}
