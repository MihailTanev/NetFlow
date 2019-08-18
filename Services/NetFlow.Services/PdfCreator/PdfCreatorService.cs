namespace NetFlow.Services.PdfCreator
{
    using iTextSharp.text;
    using iTextSharp.text.html.simpleparser;
    using iTextSharp.text.pdf;
    using Microsoft.EntityFrameworkCore;
    using NetFlow.Common.GlobalConstants;
    using NetFlow.Data;
    using NetFlow.Data.Models;
    using NetFlow.Services.Assignment;
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public class PdfCreatorService : IPdfCreatorService
    {
        private readonly NetFlowDbContext context;

        public PdfCreatorService(NetFlowDbContext context)
        {
            this.context = context;
        }
        public byte[] CreateCertificateInPdf(string html)
        {
            Document pdfDocument = new Document(PageSize.A4);

            HtmlWorker worker = new HtmlWorker(pdfDocument);

            using (var mem = new MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(pdfDocument, mem);

                pdfDocument.Open();

                using (var stringReader = new StringReader(html))
                {
                    worker.Parse(stringReader);
                }

                pdfDocument.Close();

                return mem.ToArray();
            }
        }

        public async Task<byte[]> GetPdfCertificateAsync(string studentId, int courseId)
        {
            var studentInCourse = await this.context.FindAsync<Enrollment>(studentId, courseId);

            if (studentInCourse == null)
            {
                return null;
            }


            var certificateData = await this.context
                .Courses
                .Where(c => c.Id == courseId)
                .Select(c => new
                {
                    CourseName = c.Name,
                    CourseStartDate = c.StartDate,
                    CourseEndDate = c.EndDate,
                    StudentFirstName = c.Enrollments
                                   .Where(s => s.StudentId == studentId)
                                   .Select(s => s.Student.FirstName)
                                   .FirstOrDefault(),
                    StudentLastName = c.Enrollments
                                   .Where(s => s.StudentId == studentId)
                                   .Select(s => s.Student.LastName)
                                   .FirstOrDefault(),
                    StudentGrade = c.Enrollments
                                   .Where(s => s.StudentId == studentId)
                                   .Select(s => s.Grade)
                                   .FirstOrDefault(),
                    CourseCredit = c.Credit,                
                    Trainer = c.Teacher.UserName

                })
                .FirstOrDefaultAsync();

            var generator = this.CreateCertificateInPdf(string.Format(
                                                                       PdfConstants.PDF_CERTIFICATE_DOCUMENT,
                                                                       certificateData.CourseName,
                                                                       certificateData.CourseStartDate.ToString("dd MMM yyyy"),
                                                                       certificateData.CourseEndDate.ToString("dd MMM yyyy"),
                                                                       certificateData.StudentFirstName,
                                                                       certificateData.StudentLastName,
                                                                       certificateData.StudentGrade,
                                                                       certificateData.Trainer,
                                                                       certificateData.CourseCredit,
                                                                       DateTime.UtcNow.ToString("dd MMM yyyy")));

            return generator;
        }
    }
}
