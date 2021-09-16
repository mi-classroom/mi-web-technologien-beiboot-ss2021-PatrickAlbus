using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Metadata.Profiles;
using SixLabors.ImageSharp.Metadata.Profiles.Iptc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WTBeiboot_SS21_Albus.Service.Contracts.DTO;
using WTBeiboot_SS21_Albus.Service.Contracts.DTO.ExifDTO;
using WTBeiboot_SS21_Albus.Service.Contracts.Helper;

namespace WTBeiboot_SS21_Albus.Service.Helper
{
    public class IPTCHelper : IIPTCHelper
    {
        public async Task<IEnumerable<ExifDataDTO>> GetIPTCProfile(string path, List<ExifDataDTO> currentValues, IEnumerable<IConfigurationSection> section)
        {
            using (var image = Image.Load(path))
            {
                var iptcProfile = image.Metadata.IptcProfile;
                if(iptcProfile != null)
                {
                    foreach (var _section in section)
                    {
                        Enum.TryParse(_section.GetValue<string>("Name"), out IptcTag iptcTag);
                        try
                        {
                            currentValues.Add(new ExifDataDTO
                            {
                                ExifName = _section.GetValue<string>("Name"),
                                ExifDescription = iptcProfile.GetValues(iptcTag).FirstOrDefault().ToString(),
                                ExifIsEditable = _section.GetValue<bool>("IsEditable")
                            });
                        }
                        catch { }
                    }
                }
           
            }

            return currentValues;
        }

        public async Task<bool> SetIPTCProfile(string path, IEnumerable<ExifDataDTO> exifData)
        {
            if (File.Exists(path))
            {
                using (var image = await Image.LoadAsync(path))
                {

                    if (image.Metadata.IptcProfile == null)
                    {
                        image.Metadata.IptcProfile = new IptcProfile();
                    }

                    var iptcProfile = image.Metadata.IptcProfile;
                    
                    if (iptcProfile != null)
                    {
                        foreach (ExifDataDTO data in exifData)
                        {
                            if (data.ExifDescription != null && data.ExifDescription != "")
                            {
                                Enum.TryParse(data.ExifName, out IptcTag iptcTag);
                                iptcProfile.RemoveValue(iptcTag);
                                iptcProfile.SetValue(iptcTag, data.ExifDescription);
                            }
                        }
                        iptcProfile.UpdateData();
                        image.Save(path);
                        image.Dispose();
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }

            return false;
        }
    }
}
