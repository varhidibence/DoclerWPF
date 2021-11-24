using DoclerWPF.Services;
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
    public Response Response { get; set; }

    public MainWindow()
    {
      InitializeComponent();

    }

    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
      Response = await HttpCommunication.LoadDataAsync();
    }
  }
}
