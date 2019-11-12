using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace TCM_Elastic
{
    public class Document
    {
          public Guid Id { get; set; }
          public string Path { get; set; }
          public string Content { get; set; }
          public Attachment Attachment { get; set; }
        
    }
}
