namespace NetFlow.Services.Profile.Interface
{
    using NetFlow.Services.Profile.Models;
    using System.Threading.Tasks;

    public interface IProfileService
    {
        Task<UserProfileServiceModel> GetProfileByIdAsync(string userId);
    }
}
