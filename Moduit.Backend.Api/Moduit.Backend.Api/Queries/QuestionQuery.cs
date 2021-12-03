using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moduit.Backend.Api.Queries
{
    public class QuestionQuery
    {
        public string Title { get; set; }
        public string Tags { get; set; }
        public int? PageSize { get; set; }
    }
}
