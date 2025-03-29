namespace Fringe.Service
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<IEnumerable<RoleDto>> GetAllRolesAsync()
        {
            var roles = await _roleRepository.GetAllRolesAsync();
            return roles.Select(role => MapToDto(role));
        }

        public async Task<RoleDto> GetRoleByIdAsync(int roleId)
        {
            var role = await _roleRepository.GetRoleByIdAsync(roleId);
            if (role == null)
                return null;

            return MapToDto(role);
        }

        public async Task<RoleDto> CreateRoleAsync(RoleDto createRoleDto)
        {
            // Check if role name already exists
            if (await _roleRepository.RoleNameExistsAsync(createRoleDto.RoleName))
                throw new InvalidOperationException($"Role with name '{createRoleDto.RoleName}' already exists");

            var role = new Role
            {
                RoleName = createRoleDto.RoleName,
                CanCreate = createRoleDto.CanCreate,
                CanRead = createRoleDto.CanRead,
                CanEdit = createRoleDto.CanEdit,
                CanDelete = createRoleDto.CanDelete
            };

            var createdRole = await _roleRepository.CreateRoleAsync(role);
            return MapToDto(createdRole);
        }

        public async Task<RoleDto> UpdateRoleAsync(int roleId, RoleDto updateRoleDto)
        {
            var existingRole = await _roleRepository.GetRoleByIdAsync(roleId);
            if (existingRole == null)
                throw new InvalidOperationException($"Role with ID {roleId} not found");

            // Check if the new role name already exists (if it's being changed)
            if (updateRoleDto.RoleName != existingRole.RoleName)
            {
                if (await _roleRepository.RoleNameExistsAsync(updateRoleDto.RoleName))
                    throw new InvalidOperationException($"Role with name '{updateRoleDto.RoleName}' already exists");
            }

            existingRole.RoleName = updateRoleDto.RoleName;
            existingRole.CanCreate = updateRoleDto.CanCreate;
            existingRole.CanRead = updateRoleDto.CanRead;
            existingRole.CanEdit = updateRoleDto.CanEdit;
            existingRole.CanDelete = updateRoleDto.CanDelete;

            var updatedRole = await _roleRepository.UpdateRoleAsync(existingRole);
            return MapToDto(updatedRole);
        }

        public async Task<bool> DeleteRoleAsync(int roleId)
        {
            if (!await _roleRepository.RoleExistsAsync(roleId))
                throw new InvalidOperationException($"Role with ID {roleId} not found");

            return await _roleRepository.DeleteRoleAsync(roleId);
        }

        private static RoleDto MapToDto(Role role)
        {
            return new RoleDto
            {
                RoleId = role.RoleId,
                RoleName = role.RoleName,
                CanCreate = role.CanCreate,
                CanRead = role.CanRead,
                CanEdit = role.CanEdit,
                CanDelete = role.CanDelete
            };
        }
    }
}