namespace NetFlow.Services.Profile
{
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
    using NetFlow.Data;
    using NetFlow.Services.Profile.Interface;
    using NetFlow.Services.Profile.Models;

    public class ProfileService : IProfileService
    {
        private readonly NetFlowDbContext context;

        public ProfileService(NetFlowDbContext context)
        {
            this.context = context;
        }
        public async Task<UserProfileServiceModel> GetProfileByIdAsync(string userId)
        {
            var profile = await this.context
                .Users
                .Where(u => u.Id == userId)
                .ProjectTo<UserProfileServiceModel>(new { studentId = userId })
                .FirstOrDefaultAsync();

            return profile;
        }       
    }
}
