using AutoMapper;
using EvrenCo.API.Filters;
using EvrenCo.Core.DTOs.UpdateDTOs;
using EvrenCo.Core.DTOs;
using EvrenCo.Core.Models;
using EvrenCo.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EvrenCo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : CustomBaseController
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var products = _productService.GetAll();
            var dtos = _mapper.Map<List<ProductDto>>(products).ToList();

            return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(200, dtos));

        }

        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            // url/api/products/34
            var product = await _productService.GetByIdAsync(id);

            var productDto = _mapper.Map<ProductDto>(product);

            return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, productDto));

        }

        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpGet("[action]")]
        public async Task<IActionResult> Remove(int id)
        {
            //get user from token
            int userId = 1;

            var product = await _productService.GetByIdAsync(id);
            product.UpdatedBy = userId;

            _productService.ChangeStatus(product);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            //get user from token
            int userId = 1;

            var processedEntity = _mapper.Map<Product>(productDto);

            processedEntity.UpdatedBy = userId;
            processedEntity.CreatedBy = userId;

            var product = await _productService.AddAsync(processedEntity);

            var productResponseDto = _mapper.Map<ProductDto>(product);

            return CreateActionResult(CustomResponseDto<ProductDto>.Success(201, productDto));

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> BuyProduct(ProductDto productDto)
        {
            //get user from token
            int userId = 1;

            var processedEntity = _mapper.Map<Product>(productDto);

            processedEntity.UpdatedBy = userId;
            processedEntity.CreatedBy = userId;

            await _productService.BuyProduct(processedEntity);

            

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));

        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto productDto)
        {
            var userId = 1;

            var currentProduct = await _productService.GetByIdAsync(productDto.Id);

            currentProduct.UpdatedBy = userId;
            currentProduct.Name = productDto.Name;

            _productService.Update(currentProduct);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));

        }
    }
}
