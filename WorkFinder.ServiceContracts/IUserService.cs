using WorkFinder.ServiceContracts.DTOs.Authentication;
using WorkFinder.ServiceContracts.DTOs.User;

namespace WorkFinder.ServiceContracts
{
    public interface IUserService
    {
        /// <summary>
        /// Registers a user
        /// </summary>
        /// <returns></returns>
        Task<Guid> RegisterUserAsync(RegisterRequestDto registerRequestDto,string passwordHash);

        /// <summary>
        /// Gets a user by an email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>User</returns>
        Task<UserResponseDto?> GetUserByEmailAsync(string email);

        /// <summary>
        /// Gets all existing users.
        /// </summary>
        /// <returns>All Users</returns>
        Task<IEnumerable<UserResponseDto>> GetAllUsers();

        /// <summary>
        /// Gets the password hash of a user for the given Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Password Hash of a User</returns>
        Task<string?> GetUserPasswordHashById(Guid userId);
    }
}
