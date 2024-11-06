using AutoMapper;
using EvrenCo.API.Filters;
using EvrenCo.Core.DTOs.UpdateDTOs;
using EvrenCo.Core.DTOs;
using EvrenCo.Core.Models;
using EvrenCo.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace EvrenCo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : CustomBaseController
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public RolesController(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var roles = _roleService.GetAll();
            var dtos = _mapper.Map<List<RoleDto>>(roles).ToList();

            return CreateActionResult(CustomResponseDto<List<RoleDto>>.Success(200, dtos));

        }

        [ServiceFilter(typeof(NotFoundFilter<Role>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            // url/api/roles/34
            var role = await _roleService.GetByIdAsync(id);

            var roleDto = _mapper.Map<RoleDto>(role);

            return CreateActionResult(CustomResponseDto<RoleDto>.Success(200, roleDto));

        }

        [ServiceFilter(typeof(NotFoundFilter<Role>))]
        [HttpGet("[action]")]
        public async Task<IActionResult> Remove(int id)
        {
            //get user from token
            int userId = GetUserFromToken();

            var role = await _roleService.GetByIdAsync(id);
            role.UpdatedBy = userId;

            _roleService.ChangeStatus(role);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
        
        [HttpPost]
        public async Task<IActionResult> Save(RoleDto roleDto)
        {
            //get user from token
            int userId = GetUserFromToken();

            var processedEntity = _mapper.Map<Role>(roleDto);

            processedEntity.UpdatedBy = userId;
            processedEntity.CreatedBy = userId;

            var role = await _roleService.AddAsync(processedEntity);

            var roleResponseDto = _mapper.Map<RoleDto>(role);

            return CreateActionResult(CustomResponseDto<RoleDto>.Success(201, roleDto));

        }

        [HttpPut]
        public async Task<IActionResult> Update(RoleUpdateDto roleDto)
        {
            var userId = 1;

            var currentRole = await _roleService.GetByIdAsync(roleDto.Id);

            currentRole.UpdatedBy = userId;
            currentRole.Name = roleDto.Name;

            _roleService.Update(currentRole);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));

        }
    }
}
