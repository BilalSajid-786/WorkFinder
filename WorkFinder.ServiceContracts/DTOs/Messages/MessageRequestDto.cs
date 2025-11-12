using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkFinder.ServiceContracts.DTOs.Messages
{
    public class MessageRequestDto
    {
        [Required]
        public Guid SenderId { get; set; }
        public string SenderName { get; set; } = string.Empty;
        [Required]
        public Guid ReceiverId { get; set; }
        public string Text { get; set; } = string.Empty;
    }
}
