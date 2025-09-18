using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.Entities.Entities
{
    public class File
    {
        public int FileId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string UploadFileName { get; set; } = string.Empty;
        public string FileType { get; set; } = string.Empty;
    }
}
