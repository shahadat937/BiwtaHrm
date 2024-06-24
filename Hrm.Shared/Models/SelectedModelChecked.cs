using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Shared.Models
{
   public class SelectedModelChecked
    {
        public object Value { set; get; }
        public object Text { set; get; }
        public bool IsChecked { set; get; } = false;
    }
}
