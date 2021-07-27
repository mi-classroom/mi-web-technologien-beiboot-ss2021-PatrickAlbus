using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WTBeiboot_SS21_Albus.Service.Contracts.DTO;
using WTBeiboot_SS21_Albus.Service.Contracts.Helper;

namespace WTBeiboot_SS21_Albus.Service.Helper
{
    public class ExifHelper : IExifHelper
    {
        public async Task<IEnumerable<ExifDTO>> GetExifProfile(string path, List<ExifDTO> currentValues, IEnumerable<IConfigurationSection> section)
        {
            using (var image = Image.Load(path))
            {
                var exifProfile = image.Metadata.ExifProfile;
                if (exifProfile != null)
                {
                    foreach (var _section in section)
                    {
                        foreach (var data in exifProfile.Values)
                        {
                            if (_section.GetValue<string>("Name") == data.Tag.ToString())
                            {
                                currentValues.Add(new ExifDTO
                                {
                                    ExifName = _section.GetValue<string>("Name"),
                                    ExifDescription = data.GetValue().ToString(),
                                    //ExifIsEditable = _section.GetValue<bool>("IsEditable")
                                    ExifIsEditable = false
                                });
                            }
                        }
                    }
                }
            }

            return currentValues;
        }
    }
}
