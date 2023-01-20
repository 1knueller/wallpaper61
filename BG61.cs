using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace wallpaper61
{
    public static class BG61
    {
        static HttpClient client = new();

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SystemParametersInfo(uint uiAction, uint uiParam, String pvParam, uint fWinIni);

        private const uint SPI_SETDESKWALLPAPER = 0x14;
        private const uint SPIF_UPDATEINIFILE = 0x1;
        private const uint SPIF_SENDWININICHANGE = 0x2;

        private static string fp1 => Path.Combine(AppContext.BaseDirectory, "wp1.jpg");
        private static string fplog => Path.Combine(AppContext.BaseDirectory, "wplog.md");

        public static async Task RunDisTing()
        {
            await GetImage();

            //uint flags = 0;
            uint flags = SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE;
            bool iswpok = SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, fp1, flags);
            if (!iswpok)
            {
                Console.WriteLine("wp change not ok?");
            }
        }

        public static async Task GetImage()
        {
            string? res = await client.GetStringAsync(new Uri("https://www.reddit.com/r/earthporn.json"));
            dynamic earthpornJson = JsonConvert.DeserializeObject<dynamic>(res);
            var posts = earthpornJson.data.children;
            dynamic chosenpost = posts[Random.Shared.Next(0, posts.Count)];

            // IMG
            var imglink0 = chosenpost.data["url"].ToString();
            var imgbytes = await client.GetByteArrayAsync(imglink0);
            File.WriteAllBytes(fp1, imgbytes);

            // LOG
            var postid = chosenpost.data["id"].ToString();
            var posttitle = chosenpost.data["title"].ToString();

            var loginfo = $@"
////////~~~~~~~~~~~
{DateTime.Now}
{postid}
{string.Join("_", posttitle.Split(Path.GetInvalidFileNameChars()))}";

            File.AppendAllText(fplog, loginfo);
        }
    }
}
