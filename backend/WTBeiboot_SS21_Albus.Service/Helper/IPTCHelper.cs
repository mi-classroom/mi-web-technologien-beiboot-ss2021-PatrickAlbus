using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WTBeiboot_SS21_Albus.Service.Contracts.DTO;
using WTBeiboot_SS21_Albus.Service.Contracts.Helper;

namespace WTBeiboot_SS21_Albus.Service.Helper
{
    public class IPTCHelper : IIPTCHelper
    {
        public async Task<IEnumerable<ExifDTO>> GetIPTCProfile(string path, List<ExifDTO> currentValues, IEnumerable<IConfigurationSection> section)
        {
            using (var image = Image.Load(path))
            {
                var iptcProfile = image.Metadata.IptcProfile;
                foreach (var _section in section)
                {
                    foreach (var data in iptcProfile.Values)
                    {
                        if (_section.GetValue<string>("Name") == data.Tag.ToString())
                        {
                            currentValues.Add(new ExifDTO
                            {
                                ExifName = _section.GetValue<string>("Name"),
                                ExifDescription = data.Value,
                                ExifIsEditable = _section.GetValue<bool>("IsEditable")
                            });
                        }
                    }
                }
            
            }

            return currentValues;
        }
    }
}
