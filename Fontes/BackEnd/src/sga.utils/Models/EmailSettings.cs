using System;

namespace sga.utils.Models
{
    public class EmailSettings
    {
        public String Domain { get; set; }
        public int Port { get; set; }
        public bool Ssl { get; set; }
        public String UserName { get; set; }
        public String Password { get; set; }
        public String SenderName { get; set; }
        public String SenderEmail { get; set; }
        public String FolderTemplate { get; set; }
    }
}