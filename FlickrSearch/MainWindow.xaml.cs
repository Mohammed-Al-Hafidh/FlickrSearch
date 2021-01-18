using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlickrSearch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            spResult.Children.Clear();
            // Create the rectangle
            Rectangle rec = new Rectangle()
            {
                Width = 30,
                Height = 30,
                Fill = Brushes.Green,
                Stroke = Brushes.Red,
                StrokeThickness = 2,
            };
            rec.Margin = new Thickness(0, 10, 0, 0);
            //spMainStackPanel.Children.Add(lblName);
            //spMainStackPanel.Children.Add(rec);

            string searchItem = txtSearch.Text;

            string url = @"https://www.flickr.com/services/feeds/photos_public.gne?" +
                        "format=json&jsoncallback=?&tags=" + searchItem + "&tagmode=all";          
            string result = Get(url);
            result = jsonEscape(result);

            JObject jObject = JObject.Parse(result);
            JArray items  = (JArray)jObject["items"];

            foreach(var item in items)
            {
                string title = (string)item["title"];
                string media = (string)item["media"]["m"];
                string tags = (string)item["tags"];

                Label lblName = new Label()
                {
                    Content = title,
                };
                Image img = new Image();
                
                
                    
                //var fullFilePath = @"http://www.americanlayout.com/wp/wp-content/uploads/2012/08/C-To-Go-300x300.png";

                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(media, UriKind.Absolute);
                //img.Stretch = Stretch.Fill;
                bitmap.EndInit();
                
                img.Source = bitmap;


               
            
                Label lblTags = new Label()
                {
                    Content = tags,
                };
                
             

                spResult.Children.Add(lblName);
                spResult.Children.Add(img);
                spResult.Children.Add(lblTags);

            }

        }
        public string jsonEscape(string str)
        {
            //str = str.Replace("\\", String.Empty);
            //str = str.Replace("\n", String.Empty);
            //str = str.Replace("\r", String.Empty);
            //str = str.Replace("\t", String.Empty);
            str = str.Replace("(", String.Empty);
            str = str.Replace(")", String.Empty);


            return str;
            
        }

        public string Get(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
        


    }
//    public class Feed
//    {
//        public string Title { get; set; }
//        public string Link { get; set; }
//        public string Description { get; set; }
//        public string Modified { get; set; }
//        public string Generator { get; set; }
//        public Item[] Items { get; set; }
//    }
//    public class Item
//    {
//        public string Title { get; set; }
//        public string Link { get; set; }
//        public Media Medias { get; set; }
//        public string DateTaken { get; set; }
//        public string Description { get; set; }
//        public string Published { get; set; }
//        public string Author { get; set; }
//        public string AuthorId { get; set; }
//        public string Tags { get; set; }

//    }
//public class Media
//{
//    public string M { get; set; }
//}
    


}



