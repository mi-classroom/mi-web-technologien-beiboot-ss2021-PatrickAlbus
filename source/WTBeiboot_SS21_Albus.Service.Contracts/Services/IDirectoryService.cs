using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WTBeiboot_SS21_Albus.Service.Contracts.DTO;

namespace WTBeiboot_SS21_Albus.Service.Contracts.Services
{
    public interface IDirectoryService
    {
        Task<IEnumerable<DirectoryDTO>> GetDirectory(string path = null, string previousPath = null);
    }
}
