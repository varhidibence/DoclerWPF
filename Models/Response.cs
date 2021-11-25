using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
  public class Response : INotifyPropertyChanged
  {
    private bool success;
    private string status;
    private Data data;

    public bool Success 
    { 
      get => success;
      set
      {
        success = value;
        OnPropertyChanged();
      }
    }

    public string Status { 
      get => status; 
      set { 
        status = value;
        OnPropertyChanged();
      }
    }

    public Data Data 
    { 
      get => data;
      set
      {
        data = value;
        OnPropertyChanged();
      }

    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
  }
}
