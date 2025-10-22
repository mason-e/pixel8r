using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using Avalonia.ReactiveUI;
using pixel8r_avalonia.ViewModels;
using ReactiveUI;

namespace pixel8r_avalonia.Views;

public partial class MainView : ReactiveUserControl<MainViewModel>
{
    public MainView()
    {
        InitializeComponent();

        this.WhenActivated(d =>
        {
            d(ViewModel!.OpenFileInteraction.RegisterHandler(this.OpenFileHandler));
        });
    }

    private async Task OpenFileHandler(IInteractionContext<string?, string?> context)
    {
        var topLevel = TopLevel.GetTopLevel(this);

        var storageFiles = await topLevel!.StorageProvider
            .OpenFilePickerAsync(
                        new FilePickerOpenOptions()
                        {
                            AllowMultiple = false,
                            Title = "Open Image File",
                            FileTypeFilter = [FilePickerFileTypes.ImageAll]
                        });

        context.SetOutput(storageFiles.First().TryGetLocalPath());
    }
}