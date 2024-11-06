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
    public class SalesController : CustomBaseController
    {
        private readonly ISaleService _saleService;
        private readonly IMapper _mapper;

        public SalesController(ISaleService saleService, IMapper mapper)
        {
            _saleService = saleService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var sales = _saleService.GetAll();
            var dtos = _mapper.Map<List<SaleDto>>(sales).ToList();

            return CreateActionResult(CustomResponseDto<List<SaleDto>>.Success(200, dtos));

        }

        [ServiceFilter(typeof(NotFoundFilter<Sale>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            // url/api/sales/34
            var sale = await _saleService.GetByIdAsync(id);

            var saleDto = _mapper.Map<SaleDto>(sale);

            return CreateActionResult(CustomResponseDto<SaleDto>.Success(200, saleDto));

        }

        [ServiceFilter(typeof(NotFoundFilter<Sale>))]
        [HttpGet("[action]")]
        public async Task<IActionResult> Remove(int id)
        {
            //get user from token
            int userId = 1;

            var sale = await _saleService.GetByIdAsync(id);
            sale.UpdatedBy = userId;

            _saleService.ChangeStatus(sale);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpPost]
        public async Task<IActionResult> Save(SaleDto saleDto)
        {
            //get user from token
            int userId = 1;

            var processedEntity = _mapper.Map<Sale>(saleDto);

            processedEntity.UpdatedBy = userId;
            processedEntity.CreatedBy = userId;

            var sale = await _saleService.AddAsync(processedEntity);

            var saleResponseDto = _mapper.Map<SaleDto>(sale);

            return CreateActionResult(CustomResponseDto<SaleDto>.Success(201, saleDto));

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SaleProduct(SaleDto saleDto)
        {
            //get user from token
            int userId = 1;

            var processedEntity = _mapper.Map<Sale>(saleDto);

            processedEntity.UpdatedBy = userId;
            processedEntity.CreatedBy = userId;

            await _saleService. SaleProduct(processedEntity);

            

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));

        }

        [HttpPut]
        public async Task<IActionResult> Update(SaleUpdateDto saleDto)
        {
            var userId = 1;

            var currentSale = await _saleService.GetByIdAsync(saleDto.Id);

            currentSale.UpdatedBy = userId;
            currentSale.CustomerId = saleDto.CustomerId;
            currentSale.ProductId = saleDto.ProductId;
            currentSale.Quantity = saleDto.Quantity;
            currentSale.UnitPrice = saleDto.UnitPrice;
            currentSale.TotalPrice = saleDto.TotalPrice;

            _saleService.Update(currentSale);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));

        }
    }
}
