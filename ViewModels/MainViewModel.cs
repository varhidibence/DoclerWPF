using DevExpress.Mvvm;
using DoclerWPF.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using WpfApp1.Models;

namespace DoclerWPF.ViewModels
{
  public class MainViewModel : INotifyPropertyChanged
  {
    private Response content;

    public Response Content 
    { get => content;
      set 
      { 
        content = value;
        OnPropertyChanged();
      }
    }

    public AsyncCommand NextPageAsyncCommand { get; set; }

    public MainViewModel()
    {
      NextPageAsyncCommand = new AsyncCommand(LoadNextPageAsync);
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    private async Task LoadNextPageAsync()
    {
      if (Content?.Data?.pagination?.currentPage < Content?.Data?.pagination?.totalPages)
      {
        Content = await HttpCommunication.LoadDataAsync(Content?.Data?.pagination?.currentPage + 1 ?? 1);
      }
    }

    public async Task InitAsync()
    {
      Content = await HttpCommunication.LoadDataAsync(1);
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
