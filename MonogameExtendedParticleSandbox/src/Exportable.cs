using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.Tiled.Serialization;

namespace MonogameExtendedParticleSandbox.src
{
    public interface Exportable
    {
        public string export();

        public static readonly string TEXTURE_NAME = "particleTexture";
        public static readonly string TEXTURE_REGION_NAME = "textureRegion";
        public static readonly string EFFECT_NAME = "particleEffect";

        public static List<string> textures = new List<string>();

        private static int _textureNum = 0;
        public static int textureNum
        {
            get => _textureNum++;
        }

        public static void startExport()
        {
            _textureNum = 0;
            textures = new List<string>();
        }

        public static string generateTextureRegions()
        {
            string result = "";
            foreach (var text in textures) 
            {
                result += text + ";\n";
            }
            return result;
        }

        public static void writeToFile(string data, string path)
        {
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.Write(data);
            }
        }
    }
}
