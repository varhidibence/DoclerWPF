using DoclerWPF.Services;
using DoclerWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
using WpfApp1.Models;

namespace DoclerWPF
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    //public MainViewModel MainViewModel { get; set; }

    public MainWindow()
    {
      InitializeComponent();

    }

  private async void Window_Loaded(object sender, RoutedEventArgs e)
  {
     // MainViewModel = new MainViewModel();

      //await MainViewModel.InitAsync();
    }

    private void GridControl_CustomUnboundColumnData(object sender, DevExpress.Xpf.Grid.GridColumnDataEventArgs e)
    {
      if (e.Column.FieldName == "PreviewImage" && e.IsGetData)
      {
        GridView view = sender as GridView;
        var mainViewModel = this.DataContext as MainViewModel;

        System.Drawing.Image image = null;

        Video a = mainViewModel.Content.Data.videos.ElementAtOrDefault(e.ListSourceRowIndex);

        // e.Value = MainViewModel.GetImageFromURL(a.previewImages.ElementAtOrDefault(0)); // does not work
        e.Value = mainViewModel.GetBitmapFromURL(a.previewImages.ElementAtOrDefault(0));

      } 
    }
  }
}
