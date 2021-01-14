using System;
using Nop.Core.Domain.Media;

namespace VintageCars.Domain.Utils
{
    public class PictureModel
    {
        public Picture Picture { get; set; }
        public string DataAsBase64 { get; set; }
        public byte[] DataAsByteArray { get; set; }

        public byte[] GetDataAsByteArray()
        {
            return DataAsByteArray.Length > 0 ? DataAsByteArray : Convert.FromBase64String(DataAsBase64);
        }

        public string GetDataAsBase64()
        {
            return DataAsByteArray.Length > 0 ? Convert.ToBase64String(DataAsByteArray) : DataAsBase64;
        }
    }
}
