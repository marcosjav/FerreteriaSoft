using FerreteriaNorte.Resources.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerreteriaNorte.Classes.Extra
{
    class TitleHelper
    {
        /// <summary>
        /// Parse a Title JSONObject to a Title class object
        /// </summary>
        /// <param name="str"></param>
        /// <returns>Returns NULL if JSONObject don't contains id and name of title</returns>
        public static Item parseTitle(string str)
        {
            JObject jsonTitle = JObject.Parse(str);
            string json_id = jsonTitle[DBKeys.Title.ID].ToString();

            int id = 0;
            string name = jsonTitle[DBKeys.Title.NAME].ToString();

            if (int.TryParse(json_id, out id) && name != null)
            {
                return new Item(id, name);
            }

            return null;
        }

        /// <summary>
        /// Parse a subtitle JSONObject to a subtitle class object
        /// </summary>
        /// <param name="str"></param>
        /// <returns>Returns NULL if JSONObject don't contains id and name of subtitle</returns>
        public static Subtitle parseSubtitle(string str)
        {
            JObject jsonSubtitle = JObject.Parse(str);
            string json_id = jsonSubtitle[DBKeys.Subtitle.ID].ToString();
            string json_title_id = jsonSubtitle[DBKeys.Subtitle.TITLE_ID].ToString();

            int id = 0;
            int title_id = 0;
            string name = jsonSubtitle[DBKeys.Subtitle.NAME].ToString();

            if (int.TryParse(json_id, out id) && int.TryParse(json_title_id, out title_id) && name != null)
            {
                return new Subtitle(id, title_id, name);
            }

            return null;
        }

        /// <summary>
        /// Call to DB API REST to get the Title list
        /// </summary>
        /// <returns>A list of Title objects</returns>
        public static List<Item> GetTitles()
        {
            List<Item> titles = new List<Item>();

            string request = Properties.Resources.base_url + "title/titles"; //Properties.Settings.Default.base_url + "/brand/list";

            string response = Functions.readRequest(request);

            JArray jsonArray = JArray.Parse(response);

            foreach (JObject item in jsonArray)
            {
                string str = item.ToString();
                Item newTitle = parseTitle(str);
                if (newTitle != null)
                {
                    titles.Add(newTitle);
                }
            }

            titles.Sort();

            return titles;
        }

        /// <summary>
        /// Call to DB API REST to get the Subtitle list
        /// </summary>
        /// <param name="title_id">The Title that contains the list of Subtitles</param>
        /// <returns>A List of subtitles</returns>
        public static List<Subtitle> GetSubtitles(int title_id)
        {
            List<Subtitle> subtitles = new List<Subtitle>();

            string request = Properties.Resources.base_url + "title/subtitles?title_id=" + title_id;

            string response = Functions.readRequest(request);

            JArray jsonArray = JArray.Parse(response);

            foreach (JObject item in jsonArray)
            {
                string str = item.ToString();
                Subtitle newSubtitle = parseSubtitle(str);
                if (newSubtitle != null)
                {
                    subtitles.Add(newSubtitle);
                }
            }

            subtitles.Sort();

            return subtitles;
        }
    }
}
