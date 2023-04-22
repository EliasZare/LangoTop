using _0_Framework.Domain;

namespace LangoTop.Domain
{
    public class Image : EntityBase
    {
        public string Name { get; set; }
        public string File { get; set; }
        public string Link { get; set; }

        public Image(string name, string file, string link)
        {
            Name = name;
            File = file;
            Link = link;
        }
    }
}
