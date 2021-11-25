using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Collections.Specialized;
using System.Web;

namespace DoclerWPF.Services
{
  public static class HttpCommunication
  {

    private static string uri = "https://pt.pctlwm.com/";
    private static string path = "api/video-promotion/v1/list?psid=varhidibence&pstool=421_1&accessKey=19706d152c8486f9b435c7ae1bd05643&ms_notrack=1&program=revs&campaign_id=&type=&site=jasmin&sexualOrientation=straight&forcedPerformers=&limit=25&primaryColor=%238AC437&labelColor=%23212121&clientIp=10.111.111.84";


    public static async Task<Response> LoadDataAsync(int pageIndex)
    {
      // https://pt.pctlwm.com/api/video-promotion/v1/list?
      // psid=varhidibence&pstool=421_1&accessKey=19706d152c8486f9b435c7ae1bd05643&ms_notrack=1
      // &program=revs&campaign_id=&type=&site=jasmin&sexualOrientation=straight
      // &forcedPerformers=&limit=25&primaryColor=%238AC437&labelColor=%23212121&clientIp=10.111.111.84
      using (HttpClient client = new HttpClient())
      {
        Uri uriWithPage = GetUriWithPageIndex(pageIndex);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        
        HttpResponseMessage response = await client.GetAsync(uriWithPage);

        string responseBody = await response.Content.ReadAsStringAsync();

        Response responseContent = JsonConvert.DeserializeObject<Response>(responseBody);

        return responseContent;
        
      }
    }

    private static Uri GetUriWithPageIndex(int pageIndex)
    {
      UriBuilder uriBuilder = new UriBuilder(uri + path);

      NameValueCollection queryParams = HttpUtility.ParseQueryString(uriBuilder.Query);
      queryParams.Add("pageIndex", pageIndex.ToString());
      uriBuilder.Query = queryParams.ToString();
      return uriBuilder.Uri;
    }

    public static Response LoadData(int pageIndex)
    {
      using (HttpClient client = new HttpClient())
      {
        Uri uriWithPage = GetUriWithPageIndex(pageIndex);

        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage response = client.GetAsync(uriWithPage).Result;
        string responseBody = response.Content.ReadAsStringAsync().Result;

        Response responseContent = JsonConvert.DeserializeObject<Response>(responseBody);

        return responseContent;

      }
    }

    internal static Image GetImageFromURL(string profileImage)
    {
      System.Drawing.Image image = null;
      string fullname = "https:" + profileImage;

      try
      {
        System.Net.HttpWebRequest webRequest = System.Net.FileWebRequest.CreateHttp(fullname);
        webRequest.AllowWriteStreamBuffering = true;
        webRequest.Timeout = 30000;

        System.Net.WebResponse webResponse = webRequest.GetResponse();

        System.IO.Stream stream = webResponse.GetResponseStream();

        image = System.Drawing.Image.FromStream(stream);

        webResponse.Close();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
      }

      return image;
    }

    internal static BitmapFrame GetImage(string resourceName)
    {
      Uri uri = new Uri("https:" + resourceName, UriKind.Absolute);
      return BitmapFrame.Create(uri);
    }


  }
  }
