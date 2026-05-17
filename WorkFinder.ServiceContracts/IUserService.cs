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
        Task<Guid> RegisterUserAsync(RegisterRequestDto registerRequestDto, string passwordHash);

        /// <summary>
        /// Update User profile pic in the system
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="profilePicName"></param>
        /// <returns></returns>
        Task UpdateUserProfilePic(Guid userId, string profilePicName);

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
        /// Get user stripe id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<string> GetUserStripeId(Guid userId);

        Task UpdateUserPassword(string password, Guid userId);

        /// <summary>
        /// Gets the password hash of a user for the given Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Password Hash of a User</returns>
        Task<string?> GetUserPasswordHashById(Guid userId);
        Task<bool> DeleteUserAsync(Guid userId);
        Task<bool?> UpdateUserStatusAsync(Guid userId, bool isActive);

        Task InsertUserVerificationToken(Guid userId, Guid verificationToken);
        Task<Guid> GetUserVerificationToken(Guid userId);
        Task<UserResponseDto?> GetUserByVerificationToken(Guid verificationToken);
    }
}
