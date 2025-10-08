using ApexStore.Application.Interface.Contexts;
using ApexStore.Common.Dto;
using ApexStore.Domain.Entities.Products;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;
using System.Text;

namespace ApexStore.Application.Services.Products.Commands.AddNewProduct
{
    public class AddNewProductService : IAddNewProductService
    {
        private readonly IMainDbContext _context;
        private readonly IHostingEnvironment _environment;

        public AddNewProductService(IMainDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _environment = hostingEnvironment;
        }


        public ResultDto Execute(RequestAddNewProductDto request)
        {

            try
            {

                var category = _context.Categories.Find(request.CategoryId);

                Product product = new Product()
                {
                    Slug = PersianToEnglishSlug(request.Name),
                    Brand = request.Brand,
                    Description = request.Description,
                    Name = request.Name,
                    Price = request.Price,
                    Inventory = request.Inventory,
                    Category = category,
                    Displayed = request.Displayed,
                };
                _context.Products.Add(product);

                List<ProductImages> productImages = new List<ProductImages>();
                foreach (var item in request.Images)
                {
                    var uploadedResult = UploadFile(item);
                    productImages.Add(new ProductImages
                    {
                        Product = product,
                        Src = uploadedResult.FileNameAddress,
                    });
                }

                _context.ProductImages.AddRange(productImages);


                List<ProductFeatures> productFeatures = new List<ProductFeatures>();
                foreach (var item in request.Features)
                {
                    productFeatures.Add(new ProductFeatures
                    {
                        DisplayName = item.DisplayName,
                        Value = item.Value,
                        Product = product,
                    });
                }
                _context.ProductFeatures.AddRange(productFeatures);

                _context.SaveChanges();

                return new ResultDto
                {
                    IsSuccess = true,
                    Message = "محصول با موفقیت به محصولات فروشگاه اضافه شد",
                };
            }
            catch (Exception ex)
            {

                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "خطا رخ داد ",
                };
            }

        }

        public string PersianToEnglishSlug(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                return string.Empty;


            Dictionary<char, string> transliteration = new Dictionary<char, string>()
            {
                {'ا', "a"}, {'ب', "b"}, {'پ', "p"}, {'ت', "t"}, {'ث', "s"}, {'ج', "j"},
                {'چ', "ch"}, {'ح', "h"}, {'خ', "kh"}, {'د', "d"}, {'ذ', "z"}, {'ر', "r"},
                {'ز', "z"}, {'ژ', "zh"}, {'س', "s"}, {'ش', "sh"}, {'ص', "s"}, {'ض', "z"},
                {'ط', "t"}, {'ظ', "z"}, {'ع', "a"}, {'غ', "gh"}, {'ف', "f"}, {'ق', "gh"},
                {'ک', "k"}, {'گ', "g"}, {'ل', "l"}, {'م', "m"}, {'ن', "n"}, {'و', "v"},
                {'ه', "h"}, {'ی', "y"}
            };


            StringBuilder result = new StringBuilder();
            foreach (char c in title)
            {
                if (transliteration.ContainsKey(c))
                {
                    result.Append(transliteration[c]);
                }
                else if (char.IsLetterOrDigit(c)) 
                {
                    result.Append(c);
                }
                else if (char.IsWhiteSpace(c))  
                {
                    result.Append("-");
                }
            }

            string slug = result.ToString().ToLower();
            slug = Regex.Replace(slug, @"-+", "-").Trim('-');

            return slug;
        }

        private UploadDto UploadFile(IFormFile file)
        {
            if (file != null)
            {
                string folder = $@"images\ProductImages\";
                var uploadsRootFolder = Path.Combine(_environment.WebRootPath, folder);
                if (!Directory.Exists(uploadsRootFolder))
                {
                    Directory.CreateDirectory(uploadsRootFolder);
                }


                if (file == null || file.Length == 0)
                {
                    return new UploadDto()
                    {
                        Status = false,
                        FileNameAddress = "",
                    };
                }

                string fileName = DateTime.Now.Ticks.ToString() + file.FileName;
                var filePath = Path.Combine(uploadsRootFolder, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                return new UploadDto()
                {
                    FileNameAddress = folder + fileName,
                    Status = true,
                };
            }
            return null;
        }
    }
}
