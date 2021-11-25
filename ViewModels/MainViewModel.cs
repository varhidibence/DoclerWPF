using DoclerWPF.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using WpfApp1.Models;

namespace DoclerWPF.ViewModels
{
  public class MainViewModel
  {
    public Response Content { get; set; }

    public MainViewModel()
    {
      //Content = HttpCommunication.LoadDataAsync().Result;
      Content = HttpCommunication.LoadData();
    }

    public async Task InitAsync()
    {
      // Content = await HttpCommunication.LoadDataAsync();
    }


    internal Image GetImageFromURL(string profileImage)
    {
      return HttpCommunication.GetImageFromURL(profileImage);
    }

    internal BitmapFrame GetBitmapFromURL(string profileImage)
    {
      return HttpCommunication.GetImage(profileImage);
    }
  }
}
