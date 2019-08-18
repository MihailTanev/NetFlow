using System.Threading.Tasks;

namespace NetFlow.Services.PdfCreator
{
    public interface IPdfCreatorService
    {
        byte[] CreateCertificateInPdf(string html);

        Task<byte[]> GetPdfCertificateAsync(string studentId, int courseId);

    }
}
