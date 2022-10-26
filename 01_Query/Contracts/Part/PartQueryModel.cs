namespace _01_Query.Contracts.Part
{
    public class PartQueryModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string DownloadLink { get; set; }
        public long SectionId { get; set; }
        public string Time { get; set; }
        public bool IsRemoved { get; set; }
    }
}
