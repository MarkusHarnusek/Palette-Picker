using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalettePicker
{
    internal class Pallete
    {
        //<summary>
        //Shows if the palette is valid or not.
        //</summary>
        public bool valid { get; set; }
        //<summary>
        //The location of the palette.
        //</summary>
        public string? filePath { get; set; }
        //<summary>
        //The name of the palette.
        //</summary>
        public string? paletteName { get; set; }
        //<summary>
        //The palette Primary1 color.
        //</summary>
        public string? primary1 { get; set; }
        //<summary>
        //The palette Primary2 color.
        //</summary>
        public string? primary2 { get; set; }
        //<summary>
        //The palette Secondary1 color.
        //</summary>
        public string? secondary1 { get; set; }
        //<summary>
        //The palette Secondary2 color.
        //</summary>
        public string? secondary2 { get; set; }
        //<summary>
        //The palette Text color.
        //</summary>
        public string? text { get; set; }
        //<summary>
        //Defines if the palette is read-only.
        //</summary>
        public bool readOnly { get; set; }
        //<summary>
        //Defines if the palette is visible in the home screen.
        //</summary>
        public bool homeVisible { get; set; }
        //<summary>
        //Defines if the palette is pinned to the top of the home screen.
        //</summary>
        public bool pinned { get; set; }
    }
}
