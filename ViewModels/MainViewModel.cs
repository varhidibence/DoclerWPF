using DoclerWPF.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;

namespace DoclerWPF.ViewModels
{
  public class MainViewModel
  {
    public Response Content { get; set; }

    public MainViewModel()
    {
      Content = new Response();
    }

    public async Task InitAsync()
    {
      Content = await HttpCommunication.LoadDataAsync();
    }
  }
}
