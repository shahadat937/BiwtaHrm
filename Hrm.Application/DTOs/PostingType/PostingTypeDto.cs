using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.PostingType
{
    public class PostingTypeDto : IPostingTypeDto
    {
        public int Id { get; set; }
        public string? TypeName { get; set; }
        public string? TypeNameBangla { get; set; }
        public bool? PostingInfo { get; set; }
        public bool? PromotionInfo { get; set; }
        public bool? ShowAllDesignation { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
