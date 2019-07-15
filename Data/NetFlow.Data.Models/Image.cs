namespace NetFlow.Data.Models
{
    public class Image
    {
        public int ImageId { get; set; }

        public byte[] FileContent { get; set; }

        public string FileExtension { get; set; }

        public string FileName { get; set; }
    }
}
