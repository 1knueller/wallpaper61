using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace wallpaper61
{
    public class BG61
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SystemParametersInfo(uint uiAction, uint uiParam, String pvParam, uint fWinIni);

        private const uint SPI_SETDESKWALLPAPER = 0x14;
        private const uint SPIF_UPDATEINIFILE = 0x1;
        private const uint SPIF_SENDWININICHANGE = 0x2;

        private const string fp1 = @"F:\src\wallpaper61\wp1.jpg";

        public static void RunDisTing()
        {
            //SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, fp1, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);

            uint flags = 0;
            bool iswpok = SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, fp1, flags);
            if (!iswpok)
            {
                Console.WriteLine("wp change not ok?");
            }
        }


    }
}
