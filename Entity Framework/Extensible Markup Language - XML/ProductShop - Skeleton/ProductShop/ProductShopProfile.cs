namespace ProductShop
{
    using Dtos.Import;
    using Models;

    using AutoMapper;
    using ProductShop.Dtos.Export;

    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            this.CreateMap<ImportUserDto, User>();
            this.CreateMap<ImportProductDto, Product>();
            this.CreateMap<ImportCategorieDto, Category>();
            this.CreateMap<ImportCategoryProductDto, CategoryProduct>();
            this.CreateMap<Product, ExportProductDto>();
        }
    }
}
