using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Core.Dtos
{
    public class ResponseDto<T>
    {
        public ResponseDto()
        {
            Errors = new List<ErrorDto>();
        }
        public T Result { get; set; }
        public bool Success => !Errors.Any();
        public List<ErrorDto> Errors { get; set; }
    }
}
