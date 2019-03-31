using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace predictcheque
{
    static class Class1
    {

        static void Main()
        {

            string imageFilePath = @"D:\as.jpg";

            string containstring = MakePredictionRequest(imageFilePath).Result;

            var oMycustomclassname = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObject>(containstring);

            var indianbank = oMycustomclassname.predictions[1].probability;

            var notindianbank = oMycustomclassname.predictions[6].probability;

            if (indianbank > notindianbank) {

                Console.WriteLine("Indian Bank");
            }
            else {

                Console.WriteLine("Not Indian Bank");
            }

            var chequepro = oMycustomclassname.predictions[0].probability;
            var tagname = oMycustomclassname.predictions[0].tagName;

            Console.WriteLine(tagname+" "+chequepro);


            Thread.Sleep(100000);
        }



       static byte[] GetImageAsByteArray(string imageFilePath)
        {
            FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            return binaryReader.ReadBytes((int)fileStream.Length);
        }

        static async Task<string>  MakePredictionRequest(string imageFilePath)
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Prediction-Key", "226f12ea329149718db1847640d07a04");

            string url = "https://southcentralus.api.cognitive.microsoft.com/customvision/v2.0/Prediction/a536dc35-0205-47ef-a66d-8ee6b12e2491/image";

            HttpResponseMessage response;

            byte[] byteData = GetImageAsByteArray(imageFilePath);

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                response = await client.PostAsync(url, content);

                //string containstring = await response.Content.ReadAsStringAsync();

                string contentSt;

                return (contentSt = await response.Content.ReadAsStringAsync());

                //  Thread.Sleep(5000);

                // System.IO.File.WriteAllText(@"C:\live\azure.json", cons);


                // str3 = oMycustomclassname.predictions[0].tagName;


            }

          
        }

    }
}
