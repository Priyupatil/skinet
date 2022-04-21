using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errrors
{
    public class ApiValidationErrorResponce : ApiResponce
    {
        public ApiValidationErrorResponce() : base(400)
        {
        }
        public IEnumerable<string> Errors { get; set; }
    }
}