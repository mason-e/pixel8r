using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;

namespace pixel8r_avalonia.ViewModels;

public class MainViewModel : ViewModelBase
{
    private string _filePath = "No file loaded. Select a file with the open button to enable more editing controls.";
    public string FilePath
    {
        get => _filePath;
        set => this.RaiseAndSetIfChanged(ref _filePath, value);
    }

    private string? _pendingEdit;
    public string? PendingEdit
    {
        get => _pendingEdit;
        set => this.RaiseAndSetIfChanged(ref _pendingEdit, value);
    }

    private string? _imageDimensions;
    public string? ImageDimensions
    {
        get => _imageDimensions;
        set => this.RaiseAndSetIfChanged(ref _imageDimensions, value);
    }

    public int ImageMaxWidth
    {
        get => 1280;
    }

    public int ImageMaxHeight
    {
        get => 720;
    }

    private int _imageWidth;
    public int ImageWidth
    {
        get => _imageWidth;
        set => this.RaiseAndSetIfChanged(ref _imageWidth, value);
    }

    private int _imageHeight;
    public int ImageHeight
    {
        get => _imageHeight;
        set => this.RaiseAndSetIfChanged(ref _imageHeight, value); 
    }
}
