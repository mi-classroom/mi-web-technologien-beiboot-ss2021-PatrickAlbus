using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WTBeiboot_SS21_Albus.Service.Contracts.DTO
{
    public class DirectoryDTO
    {
        public string DirectoryName { get; set; }
        public string DirectoryPath { get; set; }
        public IEnumerable<DirectoryDTO> ChildDirectories { get; set; }
        public IEnumerable<FileDTO> Files { get; set; }
    }
}
