using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRequestMAL
{
    class AnimeInfo
    {
        private string nombre;
        private string url;

        public AnimeInfo(string nombre, string url)
        {
            this.nombre = nombre;
            this.url = url;
        }

        public string GetNombre()
        {
            return nombre;
        }

        public string GetUrl()
        {
            return url;
        }
    }
}
