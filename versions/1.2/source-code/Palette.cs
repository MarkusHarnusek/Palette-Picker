using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalettePicker
{
    internal class Pallete
    {
        public bool valid { get; set; }
        public string? filePath { get; set; }
        public string? paletteName { get; set; }
        public string? primary1 { get; set; }
        public string? primary2 { get; set; }
        public string? secondary1 { get; set; }
        public string? secondary2 { get; set; }
        public string? text { get; set; }
        public bool readOnly { get; set; }
        public bool homeVisible { get; set; }
        public bool pinned { get; set; }
    }
}
