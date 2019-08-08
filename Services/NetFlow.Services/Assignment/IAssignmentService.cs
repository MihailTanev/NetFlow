namespace NetFlow.Services.Assignment
{
    using System.Threading.Tasks;

    public interface IAssignmentService
    {
        Task<bool> SaveAssignmentAsync(int courseId, string studentId, byte[] assignment);

        Task<byte[]> DownloadAssignmentAsync(string studentId, int courseId);

    }
}
