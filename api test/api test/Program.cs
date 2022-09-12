using System;
using System.Text;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using apicon;
using System.Net.Http.Headers;

namespace program
{
    public class Post
    {
        public string apTimestamp { get; set; }
        public string apref { get; set; }
        public string apbarcode { get; set; }
      
    }

    class program 
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter Youre Username");
            string user = Console.ReadLine();
            Console.WriteLine("Enter Youre Password");
            string password = Console.ReadLine();
            var test = DateTime.Now;
            string str = test.ToString();
            Console.WriteLine("Type in the barcode Value String");
            string barcode = Console.ReadLine();
            Console.WriteLine("Type In the Barcode ID");
            string barcoderef = Console.ReadLine();
           
            var newPost = new Post();
            newPost.apTimestamp = str;
            newPost.apref = barcoderef;
            newPost.apbarcode = barcode;  
           

            using (var client = new HttpClient()) 
            {
              
                var postendpoint = new Uri("https://appnostic.dbflex.net/secure/api/v2/61847/Komati%20Barcode%20Scans/create.json");
                //var postendpoint = new Uri("https://appnostic.dbflex.net/secure/api/v2/61847/90225A9B19414979BE70DCEDFBCE6E6C/Komati%20Barcode%20Scans/create.json");

                /*
                 {
                  "ap Timestamp": "2022/08/25 10:02:22",
                 "ap ref": "3",
                 "ap barcode": "1.000000-Pear-Allandale Phase 1-B6 - Box"
                    }
                 */
                var newpostjson = JsonConvert.SerializeObject(newPost);
                var byteArray = Encoding.ASCII.GetBytes($"{user}:{password}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                client.DefaultRequestHeaders.Accept.Clear();
                var payload = new StringContent(newpostjson, Encoding.UTF8,"application/Json" );
                var postresult = client.PostAsync(postendpoint, payload).Result.Content.ReadAsStringAsync().Result;
                Console.WriteLine(newpostjson);
                Console.WriteLine(payload);
               Console.WriteLine(postresult);
;            }

        }

    }

}