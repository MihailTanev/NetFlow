namespace NetFlow.Services.Assignment
{
    using System.Threading.Tasks;
    using NetFlow.Data;
    using NetFlow.Data.Models;

    public class AssignmentService : IAssignmentService
    {
        private readonly NetFlowDbContext context;

        public AssignmentService(NetFlowDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> SaveAssignmentAsync(int courseId, string studentId, byte[] assignment)
        {
            var studentAssignInCourse = await this.context.FindAsync<Enrollment>(studentId, courseId);

            if(studentAssignInCourse == null)
            {
                return false;
            }

            studentAssignInCourse.Assignment = assignment;

            await this.context.SaveChangesAsync();

            return true;
        }

        public async Task<byte[]> DownloadAssignmentAsync(string studentId, int courseId)
        {
            var studentAssignment = await this.context.FindAsync<Enrollment>(studentId, courseId);

            if (studentAssignment == null)
            {
                return null;
            }

            return studentAssignment.Assignment;
        }

    }
}
