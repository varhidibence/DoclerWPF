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
using DoclerWPF.Models;

namespace DoclerWPF.Services
{
  public static class HttpCommunication
  {

    private static string uri = "https://pt.pctlwm.com/";
    private static string path = "api/video-promotion/v1/list?psid=varhidibence&pstool=421_1&accessKey=19706d152c8486f9b435c7ae1bd05643&ms_notrack=1&program=revs&campaign_id=&type=&site=jasmin&sexualOrientation=straight&forcedPerformers=&limit=25&primaryColor=%238AC437&labelColor=%23212121&clientIp=10.111.111.84";


    /// <summary>
    /// Sending HTTP request to the API and serialize the response into a <see cref="Response"/>
    /// </summary>
    /// <param name="pageIndex">page filter</param>
    /// <param name="quality">quality filter</param>
    /// <returns>The response</returns>
    public static async Task<Response> LoadDataAsync(int pageIndex = 1, Quality quality = Quality.all)
    {
      // https://pt.pctlwm.com/api/video-promotion/v1/list?
      // psid=varhidibence&pstool=421_1&accessKey=19706d152c8486f9b435c7ae1bd05643&ms_notrack=1
      // &program=revs&campaign_id=&type=&site=jasmin&sexualOrientation=straight
      // &forcedPerformers=&limit=25&primaryColor=%238AC437&labelColor=%23212121&clientIp=10.111.111.84
      using (HttpClient client = new HttpClient())
      {
        try
        {
          UriBuilder uriBuilder = new UriBuilder(uri + path);
          Uri uriFiltered = GetUriWithPageIndex(uriBuilder, pageIndex);
          uriFiltered = GetUriWithQuality(uriBuilder, quality);

          client.DefaultRequestHeaders.Accept.Clear();
          client.DefaultRequestHeaders.Accept.Add(
              new MediaTypeWithQualityHeaderValue("application/json"));


          HttpResponseMessage response = await client.GetAsync(uriFiltered);

          string responseBody = await response.Content.ReadAsStringAsync();

          Response responseContent = JsonConvert.DeserializeObject<Response>(responseBody);

          return responseContent;
        }
        catch (Exception e)
        {
          Console.WriteLine(e.StackTrace);
          return null;
        }
        
      }
    }

    /// <summary>
    /// Add quality filtering to the uri
    /// </summary>
    /// <param name="uri">base uri</param>
    /// <param name="quality"></param>
    /// <returns>uri with quality filter</returns>
    private static Uri GetUriWithQuality(UriBuilder uri, Quality quality)
    {
      if (quality != Quality.all)
      {
        NameValueCollection queryParams = HttpUtility.ParseQueryString(uri.Query);
        queryParams.Add("quality", quality.ToString());
        uri.Query = queryParams.ToString();
      }

      return uri.Uri;
    }

    private static Uri GetUriWithPageIndex(UriBuilder uri, int pageIndex)
    {
      NameValueCollection queryParams = HttpUtility.ParseQueryString(uri.Query);
      queryParams.Add("pageIndex", pageIndex.ToString());
      uri.Query = queryParams.ToString();
      return uri.Uri;
    }

    public static Response LoadData(int pageIndex)
    {
      using (HttpClient client = new HttpClient())
      {
        try
        {
          UriBuilder uriBuilder = new UriBuilder(uri + path);
          Uri uriWithPage = GetUriWithPageIndex(uriBuilder, pageIndex);

          client.DefaultRequestHeaders.Accept.Clear();
          client.DefaultRequestHeaders.Accept.Add(
              new MediaTypeWithQualityHeaderValue("application/json"));

          HttpResponseMessage response = client.GetAsync(uriWithPage).Result;
          string responseBody = response.Content.ReadAsStringAsync().Result;

          Response responseContent = JsonConvert.DeserializeObject<Response>(responseBody);

          return responseContent;
        }
        catch (Exception e)
        {
          Console.WriteLine(e.StackTrace);
          return null;
        }

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
        Console.WriteLine(ex.StackTrace);
      }

      return image;
    }

    internal static BitmapFrame GetImage(string resourceName)
    {
      try
      {
        Uri uri = new Uri("https:" + resourceName, UriKind.Absolute);
        return BitmapFrame.Create(uri);
      }
      catch (Exception e)
      {
        Console.WriteLine(e.StackTrace);
        return null;
      }

    }


  }
  }
