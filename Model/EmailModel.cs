using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fittings.Model
{
    public class EmailModel
    {
        public string AuthorName { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public bool IsHtmlBody { get; set; } = false;
    }
}
