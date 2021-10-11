﻿using Nmkoder.Extensions;
using System;
using System.Collections.Generic;

namespace Nmkoder.Data.Codecs
{
    class H264 : IEncoder
    {
        public Streams.Stream.StreamType Type { get; } = Streams.Stream.StreamType.Video;
        public string Name { get; } = "H264";
        public string FriendlyName { get; } = "H.264 / AVC (x264)";
        public string[] Presets { get; } = new string[] { "veryslow", "slower", "slow", "medium", "fast", "faster", "veryfast", "superfast" };
        public int PresetDefault { get; } = 2;
        public string[] ColorFormats { get; } = new string[] { "yuv420p", "yuv444p", "yuv420p10le", "yuv444p10le" };
        public int ColorFormatDefault { get; } = 0;
        public int QMin { get; } = 0;
        public int QMax { get; } = 51;
        public int QDefault { get; } = 18;
        public string QInfo { get; } = "CRF (0-51 - Lower is better)";
        public string PresetInfo { get; } = "Higher = Better compression";

        public bool DoesNotEncode { get; } = false;
        public bool IsFixedFormat { get; } = false;
        public bool IsSequence { get; } = false;

        public CodecArgs GetArgs(Dictionary<string, string> encArgs = null, MediaFile mediaFile = null)
        {
            bool vbr = encArgs.ContainsKey("qMode") && (UI.Tasks.QuickConvert.QualityMode)encArgs["qMode"].GetInt() != UI.Tasks.QuickConvert.QualityMode.Crf;
            string q = encArgs.ContainsKey("q") ? encArgs["q"] : QDefault.ToString();
            string preset = encArgs.ContainsKey("preset") ? encArgs["preset"] : Presets[PresetDefault];
            string pixFmt = encArgs.ContainsKey("pixFmt") ? encArgs["pixFmt"] : ColorFormats[ColorFormatDefault];
            string rc = vbr ? $"-b:v {(encArgs.ContainsKey("bitrate") ? encArgs["bitrate"] : "0")}" : $"-crf {q}";
            string cust = encArgs.ContainsKey("custom") ? encArgs["custom"] : "";
            return new CodecArgs($"-c:v libx264 {rc} -preset {preset} -pix_fmt {pixFmt} {cust}");
        }
    }
}