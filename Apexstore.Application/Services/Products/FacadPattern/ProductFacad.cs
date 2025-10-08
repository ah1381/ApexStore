using ApexStore.Application.Interface.Contexts;
using ApexStore.Application.Interfaces.FacadPatterns;
using ApexStore.Application.Services.Products.Commands.AddNewCategory;
using ApexStore.Application.Services.Products.Commands.AddNewProduct;
using ApexStore.Application.Services.Products.Queries.GetAllCategories;
using ApexStore.Application.Services.Products.Queries.GetCategories;
using ApexStore.Application.Services.Products.Queries.GetProductDetailForAdmin;
using ApexStore.Application.Services.Products.Queries.GetProductDetailForSite;
using ApexStore.Application.Services.Products.Queries.GetProductForAdmin;
using ApexStore.Application.Services.Products.Queries.GetProductForSite;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexStore.Application.Services.Products.FacadPattern
{
    public class ProductFacad : IProductFacad
    {
        private readonly IMainDbContext _context;
        private readonly IHostingEnvironment _environment;
        public ProductFacad(IMainDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _environment = hostingEnvironment;

        }

        private AddNewCategoryService _addNewCategory;
        public AddNewCategoryService AddNewCategoryService
        {
            get
            {
                return _addNewCategory = _addNewCategory ?? new AddNewCategoryService(_context);
            }
        }


        private IGetCategoriesService _getCategoriesService;
        public IGetCategoriesService GetCategoriesService
        {
            get
            {
                return _getCategoriesService = _getCategoriesService ?? new GetCategoriesService(_context);
            }
        }

        private AddNewProductService _addNewProductService;
        public AddNewProductService AddNewProductService
        {
            get
            {
                return _addNewProductService = _addNewProductService ?? new AddNewProductService(_context, _environment);
            }
        }

        private IGetAllCategoriesService _getAllCategoriesService;
        public IGetAllCategoriesService GetAllCategoriesService
        {
            get
            {
                return _getAllCategoriesService = _getAllCategoriesService ?? new GetAllCategoriesService(_context);
            }
        }


        private IGetProductForAdminService _getProductForAdminService;
        public IGetProductForAdminService GetProductForAdminService
        {
            get
            {
                return _getProductForAdminService = _getProductForAdminService ?? new GetProductForAdminService(_context);
            }
        }


        private IGetProductDetailForAdminService _getProductDetailForAdminService;
        public IGetProductDetailForAdminService GetProductDetailForAdminService
        {
            get
            {
                return _getProductDetailForAdminService = _getProductDetailForAdminService ?? new GetProductDetailForAdminService(_context);
            }
        }    
        
        
        private IGetProductForSiteService   _getProductForSiteService;
        public IGetProductForSiteService  GetProductForSiteService
        {
            get
            {
                return _getProductForSiteService = _getProductForSiteService ?? new GetProductForSiteService(_context);
            }
        }    
        
        
        private IGetProductDetailForSiteService  _getProductDetailForSiteService;
        public IGetProductDetailForSiteService  GetProductDetailForSiteService
        {
            get
            {
                return _getProductDetailForSiteService = _getProductDetailForSiteService ?? new GetProductDetailForSiteService(_context);
            }
        }

        private IGetProductDetailForSiteBySlugService _GetProductDetailForSiteBySlugService;
        public IGetProductDetailForSiteBySlugService GetProductDetailForSiteBySlugService
        {
            get
            {
                return _GetProductDetailForSiteBySlugService = _GetProductDetailForSiteBySlugService ?? new GetProductDetailForSiteBySlugService(_context);
            }
        }
        

    }
}
