﻿using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;
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
                        foreach (var data in iptcProfile.Values)
                        {
                            if (_section.GetValue<string>("Name") == data.Tag.ToString())
                            {
                                currentValues.Add(new ExifDataDTO
                                {
                                    ExifName = _section.GetValue<string>("Name"),
                                    ExifDescription = data.Value,
                                    ExifIsEditable = _section.GetValue<bool>("IsEditable")
                                });
                            }
                        }
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
                    var iptcProfile = image.Metadata.IptcProfile;
                    List<IPTCTmp> iptcTmp = new List<IPTCTmp>();
                    if (iptcProfile != null)
                    {
                        foreach (ExifDataDTO rawData in exifData)
                        {
                            foreach (var iptcData in iptcProfile.Values)
                            {
                                if (rawData.ExifIsEditable == true && rawData.ExifName == iptcData.Tag.ToString())
                                {
                                    iptcTmp.Add(new IPTCTmp
                                    {
                                        Tag = iptcData.Tag,
                                        Value = rawData.ExifDescription
                                    });
                                }
                            }
                        }
                        foreach (IPTCTmp data in iptcTmp)
                        {
                            iptcProfile.SetValue(data.Tag, data.Value);
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

    public class IPTCTmp
    {
        public SixLabors.ImageSharp.Metadata.Profiles.Iptc.IptcTag Tag { get; set; }
        public string Value { get; set; }
    }

}
