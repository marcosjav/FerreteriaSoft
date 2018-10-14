using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FerreteriaNorte.Resources.Utils
{
    class Functions
    {
        /// <summary>
        /// Check if the passed string is a numeric type
        /// </summary>
        /// <param name="value">String to evaluate</param>
        /// <returns>True if can parse into numeric</returns>
        public static Boolean IsNumeric(string value)
        {
            int result;
            return int.TryParse(value, out result);
        }

        /// <summary>
        /// Check if a string is a numeric type, and return it
        /// </summary>
        /// <param name="value">String to evaluate and, if can, convert</param>
        /// <returns>Returns a Tuple with format isNumeric,value</returns>
        public static Tuple<bool, int> GetIsNumeric(string value)
        {
            int result = 0;
            return Tuple.Create(int.TryParse(value, out result), result);
        }

        /// <summary>
        /// Convert an image to base64 string
        /// </summary>
        /// <param name="image">The image to convert</param>
        /// <param name="format">The type of the image</param>
        /// <returns>The base64 string containing the image</returns>
        public static string ImageToBase64(Image image, ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

        /// <summary>
        /// Convert a base64 image to a System.Drawing.Image type
        /// </summary>
        /// <param name="base64String">String containing the base64 to convert</param>
        /// <returns></returns>
        public static Image Base64ToImage(string base64String)
        {
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0,
              imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }

        /// <summary>
        /// Read the response of request to a specific url.
        /// It's util to send GET request
        /// </summary>
        /// <param name="url">url to request</param>
        /// <returns>the response of the url</returns>
        public static String readRequest(String url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            String str = "";

            // Set some reasonable limits on resources used by this request
            request.MaximumAutomaticRedirections = 4;
            request.MaximumResponseHeadersLength = 4;
            // Set credentials to use for this request.
            request.Credentials = CredentialCache.DefaultCredentials;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            str += "Content length is " + response.ContentLength;
            str += "Content type is " + response.ContentType;

            // Get the stream associated with the response.
            Stream receiveStream = response.GetResponseStream();

            // Pipes the stream to a higher level stream reader with the required encoding format. 
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);

            str += "Response stream received.";
            String rta = readStream.ReadToEnd();
            str += rta;
            response.Close();
            readStream.Close();

            //mensaje("RESPUESTA", str);

            return rta;
        }

        /// <summary>
        /// Send POST data to a specific URL then return the HTTP status and response data
        /// </summary>
        /// <param name="url">Destination URL</param>
        /// <param name="postData">String to send by POST request</param>
        /// <returns>Return the HTTP status and response data</returns>
        public static Tuple<HttpStatusCode, string> sendPost(string url, string postData)
        {
            // Create a request using a URL that can receive a post.   
            WebRequest request = WebRequest.Create(url);
            // Set the Method property of the request to POST.  
            request.Method = "POST";
            // Create POST data and convert it to a byte array.  
            //string postData = "This is a test that posts this string to a Web server.";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            // Set the ContentType property of the WebRequest.  
            request.ContentType = "application/x-www-form-urlencoded";
            // Set the ContentLength property of the WebRequest.  
            request.ContentLength = byteArray.Length;
            // Get the request stream.  
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.  
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.  
            dataStream.Close();
            // Get the response.  
            WebResponse response = request.GetResponse();
            // Display the status.  
            //Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            HttpStatusCode status = ((HttpWebResponse)response).StatusCode;
            // Get the stream containing content returned by the server.  
            dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.  
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.  
            string responseFromServer = reader.ReadToEnd();
            // Display the content.  
            // Console.WriteLine(responseFromServer);
            // Clean up the streams.  
            reader.Close();
            dataStream.Close();
            response.Close();

            return Tuple.Create(status, responseFromServer);
        }
    }
}
