using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;

namespace DoclerWPF.Services
{
  public static class HttpCommunication
  {

    private static string uri = "https://pt.pctlwm.com/";
    private static string path = "api/video-promotion/v1/list?psid=varhidibence&pstool=421_1&accessKey=19706d152c8486f9b435c7ae1bd05643&ms_notrack=1&program=revs&campaign_id=&type=&site=jasmin&sexualOrientation=straight&forcedPerformers=&limit=25&primaryColor=%238AC437&labelColor=%23212121&clientIp=10.111.111.84";


    public static async Task<Response> LoadDataAsync()
    {
      // https://pt.pctlwm.com/api/video-promotion/v1/list?psid=varhidibence&pstool=421_1&accessKey=19706d152c8486f9b435c7ae1bd05643&ms_notrack=1&program=revs&campaign_id=&type=&site=jasmin&sexualOrientation=straight&forcedPerformers=&limit=25&primaryColor=%238AC437&labelColor=%23212121&clientIp=10.111.111.84
      using (HttpClient client = new HttpClient())
      {
        client.BaseAddress = new Uri(uri);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage response = await client.GetAsync(path);

        string responseBody = await response.Content.ReadAsStringAsync();

        Response responseContent = JsonConvert.DeserializeObject<Response>(responseBody);

        return responseContent;
        
      }
    }

    public static Response LoadData()
    {
      using (HttpClient client = new HttpClient())
      {
        client.BaseAddress = new Uri(uri);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        HttpResponseMessage response = client.GetAsync(path).Result;

        string responseBody = response.Content.ReadAsStringAsync().Result;

        Response responseContent = JsonConvert.DeserializeObject<Response>(responseBody);

        return responseContent;

      }
    }



  }
  }
