using DevExpress.Mvvm;
using DoclerWPF.Models;
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
  /// <summary>
  /// Manage user interaction from view
  /// </summary>
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

    private Quality quality;


    public Quality Quality
    {
      get { return quality; }
      set 
      { 
        quality = value;
        RefreshPage();
      }
    }

    private async Task RefreshPage()
    {
      Content = await HttpCommunication.LoadDataAsync(Content?.Data?.pagination?.currentPage ?? 1, Quality);
    }

    public AsyncCommand NextPageAsyncCommand { get; set; }

    public AsyncCommand PreviousPageAsyncCommand { get; set; }

    public MainViewModel()
    {
      NextPageAsyncCommand = new AsyncCommand(LoadNextPageAsync);
      PreviousPageAsyncCommand = new AsyncCommand(LoadPreviousPageAsync);

      Quality = Quality.all;
    }

    /// <summary>
    /// Gets <see cref="Content"/> with default values from web
    /// </summary>
    /// <returns></returns>
    public async Task InitAsync()
    {
      Content = await HttpCommunication.LoadDataAsync(1, Quality);
    }


    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    #region Pagination
    private async Task LoadPreviousPageAsync()
    {
      if (Content?.Data?.pagination?.currentPage >= 2)
      {
        Content = await HttpCommunication.LoadDataAsync(Content?.Data?.pagination?.currentPage - 1 ?? 1, Quality);
      }
    }

    private async Task LoadNextPageAsync()
    {
      if (Content?.Data?.pagination?.currentPage < Content?.Data?.pagination?.totalPages)
      {
        Content = await HttpCommunication.LoadDataAsync(Content?.Data?.pagination?.currentPage + 1 ?? 1, Quality);
      }
    }

    #endregion


    internal Image GetImageFromURL(string profileImage)
    {
      return HttpCommunication.GetImageFromURL(profileImage);
    }

    /// <summary>
    /// Gets the image from the defined url
    /// </summary>
    /// <param name="profileImage"></param>
    /// <returns></returns>
    internal BitmapFrame GetBitmapFromURL(string profileImage)
    {
      return HttpCommunication.GetImage(profileImage);
    }
  }
}
