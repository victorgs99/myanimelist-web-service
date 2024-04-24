using System;
using System.Collections.Generic;
// System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using HtmlAgilityPack;

namespace WebRequestMAL
{
    public partial class Form1 : Form
    {
        List<AnimeInfo> animes = new List<AnimeInfo>();

        public Form1()
        {
            InitializeComponent();
        }

        public void BusquedaNivelUno()
        {
            int indexTam = 0;
            List<string> nombres = new List<string>();
            List<string> urls = new List<string>();

            string anime = TxtNombre.Text;

            var url = "https://myanimelist.net/anime.php?cat=anime&q=" + anime;
            var web = new HtmlWeb();
            var doc = web.Load(url);

            var nombresAnimes = doc.DocumentNode.SelectNodes("//*[@class=\"hoverinfo_trigger fw-b fl-l\"]/strong");
            var urlsAnimes = doc.DocumentNode.SelectNodes("//*[@class=\"hoverinfo_trigger fw-b fl-l\"]");

            foreach (var node in nombresAnimes)
            {
                animeList.Items.Add(node.InnerText);
                nombres.Add(node.InnerText);
                indexTam++;
            }
 
            foreach (var node in urlsAnimes)
            {
                urls.Add(node.Attributes["href"].Value);
            }

            for(int i = 0; i < indexTam; i++)
            {
                AnimeInfo animeAux = new AnimeInfo(nombres[i], urls[i]);
                animes.Add(animeAux);
            }
        }

        public void BusquedaNivelDos()
        {
            var url = animes[animeList.SelectedIndex].GetUrl();
            var web = new HtmlWeb();
            var doc = web.Load(url);

            var imagen = doc.DocumentNode.SelectSingleNode("//*[@class=\"lazyload\"]").Attributes["data-src"].Value;
            var descripcion = doc.DocumentNode.SelectSingleNode("//*[@itemprop=\"description\"]").InnerText;

            ImagenAnime.ImageLocation = imagen;
            LabelNombre.Text = animes[animeList.SelectedIndex].GetNombre();
            TxtDescripcion.Text = descripcion;
        }

        private void BtnBusqueda_Click(object sender, EventArgs e)
        {
            LimpiaDatos();
            BusquedaNivelUno();
        }

        private void animeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            BusquedaNivelDos();
        }

        private void LimpiaDatos()
        {
            animes.Clear();
            animeList.Items.Clear();
            LabelNombre.Text = "";
            ImagenAnime.Image = null;
            TxtDescripcion.Text = "";
        }
    }
}
