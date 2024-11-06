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
    public class PaymentsController : CustomBaseController
    {
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;

        public PaymentsController(IPaymentService paymentService, IMapper mapper)
        {
            _paymentService = paymentService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var payments = _paymentService.GetAll();
            var dtos = _mapper.Map<List<PaymentDto>>(payments).ToList();

            return CreateActionResult(CustomResponseDto<List<PaymentDto>>.Success(200, dtos));

        }

        [ServiceFilter(typeof(NotFoundFilter<Payment>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            // url/api/payments/34
            var payment = await _paymentService.GetByIdAsync(id);

            var paymentDto = _mapper.Map<PaymentDto>(payment);

            return CreateActionResult(CustomResponseDto<PaymentDto>.Success(200, paymentDto));

        }

        [ServiceFilter(typeof(NotFoundFilter<Payment>))]
        [HttpGet("[action]")]
        public async Task<IActionResult> Remove(int id)
        {
            //get user from token
            int userId = 1;

            var payment = await _paymentService.GetByIdAsync(id);
            payment.UpdatedBy = userId;

            _paymentService.ChangeStatus(payment);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpPost]
        public async Task<IActionResult> Save(PaymentDto paymentDto)
        {
            //get user from token
            int userId = 1;

            var processedEntity = _mapper.Map<Payment>(paymentDto);

            processedEntity.UpdatedBy = userId;
            processedEntity.CreatedBy = userId;

            var payment = await _paymentService.AddAsync(processedEntity);

            var paymentResponseDto = _mapper.Map<PaymentDto>(payment);

            return CreateActionResult(CustomResponseDto<PaymentDto>.Success(201, paymentDto));

        }

        [HttpPut]
        public async Task<IActionResult> Update(PaymentUpdateDto paymentDto)
        {
            var userId = 1;

            var currentPayment = await _paymentService.GetByIdAsync(paymentDto.Id);

            currentPayment.UpdatedBy = userId;
            currentPayment.CustomerId = paymentDto.CustomerId;
            currentPayment.Amount = paymentDto.Amount;

            _paymentService.Update(currentPayment);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));

        }
    }
}
