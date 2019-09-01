using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Net.Chdk.Meta.Providers.Platform.Exif
{
    public abstract class ExifPlatformProvider : IInnerPlatformProvider
    {
        private const int MinModelId = 0x1540000;

        private static readonly KeyValuePair<string, string>[] AddedPlatforms =
        {
            new KeyValuePair<string, string>
            (
                 "0x3380000",
                 "PowerShot N Facebook"
            )
        };

        IEnumerable<KeyValuePair<string, string>> IInnerPlatformProvider.GetPlatforms(TextReader reader)
        {
            return GetPlatforms(reader)
                .Where(IsIncluded)
                .Concat(AddedPlatforms);
        }

        protected abstract IEnumerable<KeyValuePair<string, string>> GetPlatforms(TextReader reader);

        public abstract string Extension { get; }

        private static bool IsIncluded(KeyValuePair<string, string> kvp)
        {
            return GetModelId(kvp.Key) >= MinModelId;
        }

        private static uint GetModelId(string id)
        {
            return Convert.ToUInt32(id, 16);
        }
    }
}
