using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WTBeiboot_SS21_Albus.Service.Contracts.DTO.ExifDTO
{
    public class ExifDTO
    {
        public string Size { get; set; }
        public IEnumerable<ExifDataDTO> ExifData { get; set; }
    }
}
