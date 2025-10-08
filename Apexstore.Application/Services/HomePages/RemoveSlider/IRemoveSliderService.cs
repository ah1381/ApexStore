using ApexStore.Application.Interface.Contexts;
using ApexStore.Application.Services.HomePages.AddHomePageImages;
using ApexStore.Common.Dto;
using ApexStore.Domain.Entities.HomePages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexStore.Application.Services.HomePages.RemoveSlider
{

    public interface IRemoveSliderService
    {
        ResultDto Execute(long SliderId);
    }


    public class RemoveSliderService : IRemoveSliderService
    {
        private readonly IMainDbContext _context;

        public RemoveSliderService(IMainDbContext context)
        {
            _context = context;
        }
        public ResultDto Execute(long SliderId)
        {
            var slider = _context.Sliders.Find(SliderId);
            _context.Sliders.Remove(slider);
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
            };
        }

    }
}


