using ApexStore.Application.Services.Products.Commands.AddNewCategory;
using ApexStore.Application.Services.Products.Commands.AddNewProduct;
using ApexStore.Application.Services.Products.Queries.GetAllCategories;
using ApexStore.Application.Services.Products.Queries.GetCategories;
using ApexStore.Application.Services.Products.Queries.GetProductDetailForAdmin;
using ApexStore.Application.Services.Products.Queries.GetProductDetailForSite;
using ApexStore.Application.Services.Products.Queries.GetProductForAdmin;
using ApexStore.Application.Services.Products.Queries.GetProductForSite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexStore.Application.Interfaces.FacadPatterns
{
    public interface IProductFacad
    {
        AddNewCategoryService AddNewCategoryService { get; }
        IGetCategoriesService GetCategoriesService { get; }
        AddNewProductService AddNewProductService { get; }
        IGetAllCategoriesService GetAllCategoriesService { get; }
        /// <summary>
        /// دریافت لیست محصولات
        /// </summary>
        IGetProductForAdminService GetProductForAdminService { get; }
        IGetProductDetailForAdminService GetProductDetailForAdminService { get; }
        IGetProductForSiteService GetProductForSiteService { get; }
        IGetProductDetailForSiteService GetProductDetailForSiteService { get; }
        IGetProductDetailForSiteBySlugService GetProductDetailForSiteBySlugService { get; }
    }
}
