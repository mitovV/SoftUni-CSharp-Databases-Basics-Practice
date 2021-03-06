﻿namespace ProductShop
{
    using Dtos.Export;
    using Dtos.Import;
    using Models;

    using AutoMapper;

    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            this.CreateMap<ImportUserDto, User>();
            this.CreateMap<ImportProductDto, Product>();
            this.CreateMap<ImportCategorieDto, Category>();
            this.CreateMap<ImportCategoryProductDto, CategoryProduct>();
        }
    }
}
