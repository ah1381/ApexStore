using ApexStore.Application.Services.Common.Queries.GetHomePageImages;
using ApexStore.Application.Services.Common.Queries.GetSlider;
using ApexStore.Application.Services.Products.Queries.GetProductForSite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Site.Models.ViewModels.HomePages
{
    public class HomePageViewModel
    {
        public List<SliderDto> Sliders {get;set;}
        public List<HomePageImagesDto> PageImages { get; set; }
        public List<ProductForSiteDto>  Camera { get; set; }
        public List<ProductForSiteDto>  Mobile { get; set; }
        public List<ProductForSiteDto>  Laptop { get; set; }
    }
}
