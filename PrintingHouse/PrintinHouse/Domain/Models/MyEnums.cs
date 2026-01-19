using System.ComponentModel;

namespace PrintingHouse.Domain.Models
{
    public enum Gender
    {
        Female  = 1,
        Male = 2
    }
    public enum ProductType
    {
        [Description("Book")]
        Book = 1,
        [Description("Article")]
        Article = 2,
        [Description("ElectronicResource")]
        ElectronicResource = 3
    }
}
