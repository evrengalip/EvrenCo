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
    public class DepartmentsController : CustomBaseController
    {
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;

        public DepartmentsController(IDepartmentService departmentService, IMapper mapper)
        {
            _departmentService = departmentService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var departments = _departmentService.GetAll();
            var dtos = _mapper.Map<List<DepartmentDto>>(departments).ToList();

            return CreateActionResult(CustomResponseDto<List<DepartmentDto>>.Success(200, dtos));

        }

        [ServiceFilter(typeof(NotFoundFilter<Department>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            // url/api/departments/34
            var department = await _departmentService.GetByIdAsync(id);

            var departmentDto = _mapper.Map<DepartmentDto>(department);

            return CreateActionResult(CustomResponseDto<DepartmentDto>.Success(200, departmentDto));

        }

        [ServiceFilter(typeof(NotFoundFilter<Department>))]
        [HttpGet("[action]")]
        public async Task<IActionResult> Remove(int id)
        {
            //get user from token
            int userId = 1;

            var department = await _departmentService.GetByIdAsync(id);
            department.UpdatedBy = userId;

            _departmentService.ChangeStatus(department);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpPost]
        public async Task<IActionResult> Save(DepartmentDto departmentDto)
        {
            //get user from token
            int userId = GetUserFromToken();

            var processedEntity = _mapper.Map<Department>(departmentDto);

            processedEntity.UpdatedBy = userId;
            processedEntity.CreatedBy = userId;

            var department = await _departmentService.AddAsync(processedEntity);

            var departmentResponseDto = _mapper.Map<DepartmentDto>(department);

            return CreateActionResult(CustomResponseDto<DepartmentDto>.Success(201, departmentDto));

        }

        [HttpPut]
        public async Task<IActionResult> Update(DepartmentUpdateDto departmentDto)
        {
            var userId = 1;

            var currentDepartment = await _departmentService.GetByIdAsync(departmentDto.Id);

            currentDepartment.UpdatedBy = userId;
            currentDepartment.Name = departmentDto.Name;

            _departmentService.Update(currentDepartment);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));

        }
    }
}
