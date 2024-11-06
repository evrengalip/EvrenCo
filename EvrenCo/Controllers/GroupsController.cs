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
    public class GroupsController : CustomBaseController
    {
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;

        public GroupsController(IGroupService groupService, IMapper mapper)
        {
            _groupService = groupService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var groups = _groupService.GetAll();
            var dtos = _mapper.Map<List<GroupDto>>(groups).ToList();

            return CreateActionResult(CustomResponseDto<List<GroupDto>>.Success(200, dtos));

        }

        [ServiceFilter(typeof(NotFoundFilter<Group>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            // url/api/groups/34
            var group = await _groupService.GetByIdAsync(id);

            var groupDto = _mapper.Map<GroupDto>(group);

            return CreateActionResult(CustomResponseDto<GroupDto>.Success(200, groupDto));

        }

        [ServiceFilter(typeof(NotFoundFilter<Group>))]
        [HttpGet("[action]")]
        public async Task<IActionResult> Remove(int id)
        {
            //get user from token
            int userId = 1;

            var group = await _groupService.GetByIdAsync(id);
            group.UpdatedBy = userId;

            _groupService.ChangeStatus(group);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpPost]
        public async Task<IActionResult> Save(GroupDto groupDto)
        {
            //get user from token
            int userId = 1;

            var processedEntity = _mapper.Map<Group>(groupDto);

            processedEntity.UpdatedBy = userId;
            processedEntity.CreatedBy = userId;

            var group = await _groupService.AddAsync(processedEntity);

            var groupResponseDto = _mapper.Map<GroupDto>(group);

            return CreateActionResult(CustomResponseDto<GroupDto>.Success(201, groupDto));

        }

        [HttpPut]
        public async Task<IActionResult> Update(GroupUpdateDto groupDto)
        {
            var userId = 1;

            var currentGroup = await _groupService.GetByIdAsync(groupDto.Id);

            currentGroup.UpdatedBy = userId;
            currentGroup.Name = groupDto.Name;

            _groupService.Update(currentGroup);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));

        }
    }
}
