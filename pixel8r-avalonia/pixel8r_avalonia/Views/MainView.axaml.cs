using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using Avalonia.ReactiveUI;
using pixel8r_avalonia.Helpers;
using pixel8r_avalonia.ViewModels;

namespace pixel8r_avalonia.Views;

public partial class MainView : ReactiveUserControl<MainViewModel>
{
    public MainView()
    {
        InitializeComponent();
    }

    private async void Open_Click(object? sender, RoutedEventArgs e)
    {
        var window = this.VisualRoot as Window;
        if (window == null)
            return;

        var options = new FilePickerOpenOptions
        {
            Title = "Open image",
            AllowMultiple = false,
            FileTypeFilter = new List<FilePickerFileType>
            {
                new FilePickerFileType("Image")
                {
                    Patterns = new[] { "*.png", "*.jpg", "*.jpeg", "*.bmp", "*.gif" }
                }
            }
        };

        var result = await window.StorageProvider.OpenFilePickerAsync(options);
        if (result == null || result.Count == 0)
            return;

        var file = result[0];
        SetImageFromFile(file);
    }

    private void SetImageFromFile(IStorageFile file)
    {
        Bitmap image = BitmapHelper.generateBitmap(file.TryGetLocalPath());
        if (DataContext is MainViewModel vm)
        {
            vm.FilePath = file.TryGetLocalPath();
            vm.ImageWidth = (int)image.Size.Width;
            MainImage.Width = vm.ImageWidth;
            vm.ImageHeight = (int)image.Size.Height;
            MainImage.Height = vm.ImageHeight;
            vm.ImageDimensions = $"{vm.ImageWidth} x {vm.ImageHeight}";
        }
        // make border disappear when an image is loaded
        ImagePreview.BorderThickness = new Thickness(0);
        MainImage.Source = image;
    }
}
