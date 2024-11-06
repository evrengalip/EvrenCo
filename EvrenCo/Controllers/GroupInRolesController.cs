﻿using AutoMapper;
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
    public class GroupInRolesController : CustomBaseController
    {
        private readonly IGroupInRoleService _groupInRoleService;
        private readonly IMapper _mapper;

        public GroupInRolesController(IGroupInRoleService groupInRoleService, IMapper mapper)
        {
            _groupInRoleService = groupInRoleService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var groupInRoles = _groupInRoleService.GetAll();
            var dtos = _mapper.Map<List<GroupInRoleDto>>(groupInRoles).ToList();

            return CreateActionResult(CustomResponseDto<List<GroupInRoleDto>>.Success(200, dtos));

        }

        [ServiceFilter(typeof(NotFoundFilter<GroupInRole>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            // url/api/groupInRoles/34
            var groupInRole = await _groupInRoleService.GetByIdAsync(id);

            var groupInRoleDto = _mapper.Map<GroupInRoleDto>(groupInRole);

            return CreateActionResult(CustomResponseDto<GroupInRoleDto>.Success(200, groupInRoleDto));

        }

        [ServiceFilter(typeof(NotFoundFilter<GroupInRole>))]
        [HttpGet("[action]")]
        public async Task<IActionResult> Remove(int id)
        {
            //get user from token
            int userId = 1;

            var groupInRole = await _groupInRoleService.GetByIdAsync(id);
            groupInRole.UpdatedBy = userId;

            _groupInRoleService.ChangeStatus(groupInRole);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpPost]
        public async Task<IActionResult> Save(GroupInRoleDto groupInRoleDto)
        {
            //get user from token
            int userId = 1;

            var processedEntity = _mapper.Map<GroupInRole>(groupInRoleDto);

            processedEntity.UpdatedBy = userId;
            processedEntity.CreatedBy = userId;

            var groupInRole = await _groupInRoleService.AddAsync(processedEntity);

            var groupInRoleResponseDto = _mapper.Map<GroupInRoleDto>(groupInRole);

            return CreateActionResult(CustomResponseDto<GroupInRoleDto>.Success(201, groupInRoleDto));

        }

        [HttpPut]
        public async Task<IActionResult> Update(GroupInRoleUpdateDto groupInRoleDto)
        {
            var userId = 1;

            var currentGroupInRole = await _groupInRoleService.GetByIdAsync(groupInRoleDto.Id);

            currentGroupInRole.UpdatedBy = userId;
            currentGroupInRole.GroupId = groupInRoleDto.GroupId;
            currentGroupInRole.RoleId = groupInRoleDto.RoleId;

            _groupInRoleService.Update(currentGroupInRole);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));

        }
    }
}
