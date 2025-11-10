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

    private int _imageLeft;
    public int ImageLeft
    {
        get => _imageLeft;
        set => this.RaiseAndSetIfChanged(ref _imageLeft, value);
    }

    private int _imageTop;
    public int ImageTop
    {
        get => _imageTop;
        set => this.RaiseAndSetIfChanged(ref _imageTop, value);
    }

    private int _resizeWidth;
    public int ResizeWidth
    {
        get => _resizeWidth;
        set => this.RaiseAndSetIfChanged(ref _resizeWidth, value);
    }

    private int _resizeHeight;
    public int ResizeHeight
    {
        get => _resizeHeight;
        set => this.RaiseAndSetIfChanged(ref _resizeHeight, value);
    }

    private int _resizeLeft;
    public int ResizeLeft
    {
        get => _resizeLeft;
        set => this.RaiseAndSetIfChanged(ref _resizeLeft, value);
    }

    private int _resizeTop;
    public int ResizeTop
    {
        get => _resizeTop;
        set => this.RaiseAndSetIfChanged(ref _resizeTop, value);
    }

    private bool _resizeShow = false;
    public bool ResizeShow
    {
        get => _resizeShow;
        set => this.RaiseAndSetIfChanged(ref _resizeShow, value);
    }
}
