using DoclerWPF.Models;
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
    public MainViewModel MainViewModel { get; set; }

    public MainWindow()
    {
      InitializeComponent();
    }

  private async void Window_Loaded(object sender, RoutedEventArgs e)
  {
      MainViewModel = new MainViewModel();
      this.DataContext = MainViewModel;
      await MainViewModel.InitAsync();
    }

    /// <summary>
    /// Takes out the url of the first preview image from each video on the view,
    /// and trying to gets the image from web
    /// and load to the view
    /// </summary>
    /// <param name="sender">the grid</param>
    /// <param name="e"></param>
    private void GridControl_CustomUnboundColumnData(object sender, DevExpress.Xpf.Grid.GridColumnDataEventArgs e)
    {
      if (e.Column.FieldName == "PreviewImage" && e.IsGetData)
      {
        Video a = MainViewModel.Content.Data.videos.ElementAtOrDefault(e.ListSourceRowIndex);

        // e.Value = MainViewModel.GetImageFromURL(a.previewImages.ElementAtOrDefault(0)); // does not work
        e.Value = MainViewModel.GetBitmapFromURL(a.previewImages.ElementAtOrDefault(0));

      } 
    }

    private void ComboBoxEdit_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
    {
      try
      {
       
        Enum.TryParse<Quality>(e.NewValue.ToString(), out Quality quality);
        MainViewModel.Quality = quality;
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString() + ex.StackTrace);
        
      }
    }
  }
}
