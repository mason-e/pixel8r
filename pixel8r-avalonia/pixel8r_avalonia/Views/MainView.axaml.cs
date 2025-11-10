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

        if (DataContext is MainViewModel vm)
        {
            vm.FilePath = result[0].TryGetLocalPath();
        }
        SetImageFromFile();
    }

    private void Reload_Click(object? sender, RoutedEventArgs e)
    {
        SetImageFromFile();
    }

    private void DiscretePalette_Selected(object? sender, SelectionChangedEventArgs e)
    {
        
        if (DataContext is MainViewModel vm)
        {
            if (DiscretePalette.SelectedItem is ComboBoxItem selectedItem)
            {
                PalettePreviewImage.Source = BitmapHelper.DrawPalette(selectedItem.Content.ToString());
            }
        }
    }

    private void SwapDiscrete_Click(object? sender, RoutedEventArgs e)
    {
        if (DataContext is MainViewModel vm)
        {
            if (DiscretePalette.SelectedItem is ComboBoxItem palette)
            {
                if (SwapAlgorithm.SelectedItem is ComboBoxItem algorithm)
                {
                    MainImage.Source = BitmapHelper.paletteSwapPredefined(
                        MainImage.Source as Bitmap,
                        palette.Content.ToString(),
                        algorithm.Content.ToString()
                    );
                }

            }
        }
    }

    private void SwapProgrammatic_Click(object? sender, RoutedEventArgs e)
    {
        if (DataContext is MainViewModel vm)
        {
            if (ProgrammaticPalette.SelectedItem is ComboBoxItem palette)
            {
                MainImage.Source = BitmapHelper.paletteSwapProgrammatic(
                    MainImage.Source as Bitmap,
                    palette.Content.ToString()
                );
            }
        }
    }

    private void Tint_Click(object? sender, RoutedEventArgs e)
    {
        if (DataContext is MainViewModel vm)
        {
            if (Tint.SelectedItem is ComboBoxItem tint)
            {
                MainImage.Source = BitmapHelper.tint(
                    MainImage.Source as Bitmap,
                    tint.Content.ToString()
                );
            }
        }
    }

    private void Pixelate_Click(object? sender, RoutedEventArgs e)
    {
        if (DataContext is MainViewModel vm)
        {
            MainImage.Source = BitmapHelper.pixelate(
                MainImage.Source as Bitmap
            );
        }
    }

    private void Scanlines_Click(object? sender, RoutedEventArgs e)
    {
        if (DataContext is MainViewModel vm)
        {
            MainImage.Source = BitmapHelper.scanlines(
                MainImage.Source as Bitmap
            );
        }
    }

    private void SetImageFromFile()
    {
        if (DataContext is MainViewModel vm)
        {
            Bitmap image = BitmapHelper.generateBitmap(vm.FilePath);
            vm.ImageWidth = (int)image.Size.Width;
            MainImage.Width = vm.ImageWidth;
            GlobalVars.ImageWidth = vm.ImageWidth;
            vm.ImageHeight = (int)image.Size.Height;
            MainImage.Height = vm.ImageHeight;
            GlobalVars.ImageHeight = vm.ImageHeight;
            vm.ImageDimensions = $"{vm.ImageWidth} x {vm.ImageHeight}";
            // make border disappear when an image is loaded
            ImagePreview.BorderThickness = new Thickness(0);
            MainImage.Source = image;
        }
    }
}
