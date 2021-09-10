﻿using System.Drawing;

namespace Nmkoder.Data.Streams
{
    class VideoStream : Stream
    {
        public string ColorSpace;
        public int Kbits;
        public Size Resolution;
        public Size Sar;
        public Size Dar;
        public Fraction Rate;
        public string Language;

        public VideoStream(string codec, string codecLong, string colorSpace, int kbits, Size resolution, Size sar, Size dar, Fraction rate, string language)
        {
            base.Type = StreamType.Video;
            Codec = codec;
            CodecLong = codecLong;
            ColorSpace = colorSpace;
            Kbits = kbits;
            Resolution = resolution;
            Sar = sar;
            Dar = dar;
            Rate = rate;
            Language = language;
        }

        public override string ToString()
        {
            return $"{base.ToString()} - Language: {Language} - Colors: {ColorSpace} - Size: {Resolution.Width}x{Resolution.Height} - FPS: {Rate}";
        }
    }
}