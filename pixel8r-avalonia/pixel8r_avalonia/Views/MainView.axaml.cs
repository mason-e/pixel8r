using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
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

    private void Crop_Selected(object? sender, SelectionChangedEventArgs e)
    {
        if (DataContext is MainViewModel vm)
        {
            if (CropPreview.IsChecked ?? false)
            {
                if (Crop.SelectedItem is ComboBoxItem crop)
                {
                    string[] aspectVals = crop.Content.ToString().Split(':');
                    int aspectWidth = Convert.ToInt32(aspectVals[0]);
                    int aspectHeight = Convert.ToInt32(aspectVals[1].Split(" ")[0]);
                    (vm.ResizeWidth, vm.ResizeHeight) = ResizeHelper.getCropDimensions(aspectWidth, aspectHeight);
                    if (!(vm.ResizeWidth == vm.ImageWidth && vm.ResizeHeight == vm.ImageHeight))
                    {
                        vm.PendingEdit = $"Crop will resize image to {vm.ResizeWidth}x{vm.ResizeHeight}. Click on image to crop. Click preview button or press ESC to cancel.";
                    }
                    else
                    {
                        vm.PendingEdit = "Image is already in desired aspect ratio.";
                    }
                }
            }
        }
    }

    private void CropPreview_Click(object? sender, RoutedEventArgs e)
    {
        if (DataContext is MainViewModel vm)
        {
            if (CropPreview.IsChecked ?? false)
            {
                // @TODO disable editing controls
                if (Crop.SelectedItem is ComboBoxItem crop)
                {
                    string[] aspectVals = crop.Content.ToString().Split(':');
                    int aspectWidth = Convert.ToInt32(aspectVals[0]);
                    int aspectHeight = Convert.ToInt32(aspectVals[1].Split(" ")[0]);
                    (vm.ResizeWidth, vm.ResizeHeight) = ResizeHelper.getCropDimensions(aspectWidth, aspectHeight);
                    if (!(vm.ResizeWidth == vm.ImageWidth && vm.ResizeHeight == vm.ImageHeight))
                    {
                        vm.PendingEdit = $"Crop will resize image to {vm.ResizeWidth}x{vm.ResizeHeight}. Click on image to crop. Click preview button or press ESC to cancel.";
                    }
                    else
                    {
                        vm.PendingEdit = "Image is already in desired aspect ratio.";
                    }
                }
            }
            else
            {
                // @TODO enable editing controls
                vm.PendingEdit = "";
                vm.ResizeShow = false;
            }
        }
    }

    private void MainImage_Mouseover(object? sender, Avalonia.Input.PointerEventArgs e)
    {
        if (DataContext is MainViewModel vm)
        {
            if (CropPreview.IsChecked ?? false)
            {
                if (vm.ResizeWidth == vm.ImageWidth)
                {
                    vm.ResizeLeft = vm.ImageLeft;
                    vm.ResizeTop = vm.ImageTop + (int)e.GetPosition(MainImage).Y - vm.ResizeHeight / 2;
                }
                if (vm.ResizeHeight == vm.ImageHeight)
                {
                    vm.ResizeTop = vm.ImageTop;
                    vm.ResizeLeft = vm.ImageLeft + (int)e.GetPosition(MainImage).X - vm.ResizeWidth / 2;
                }
                vm.ResizeShow = true;
            }
        }
    }

    private void MainImage_Mouseleave(object? sender, PointerEventArgs e)
    {
        if (DataContext is MainViewModel vm)
        {
            vm.ResizeShow = false;
        }
    }

    private void MainImage_Click(object? sender, PointerPressedEventArgs e)
    {
        if (DataContext is MainViewModel vm)
        {
            if (CropPreview.IsChecked ?? false)
            {
                try
                {
                    Bitmap cropped = BitmapHelper.cropBitmap(
                        MainImage.Source as Bitmap,
                        vm.ResizeLeft - vm.ImageLeft,
                        vm.ResizeTop - vm.ImageTop,
                        vm.ResizeWidth,
                        vm.ResizeHeight
                    );
                    SetImageProperties(cropped);
                    CropPreview.IsChecked = false;
                    vm.ResizeShow = false;
                    vm.PendingEdit = "";
                }
                catch (OutOfMemoryException ex)
                {
                    vm.PendingEdit = "Crop attempt failed! Make sure that the crop boundary is not extending outside the image.";
                }
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

    private void KeyDownHandler(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Escape)
        {
            if (DataContext is MainViewModel vm)
            {
                if (CropPreview.IsChecked ?? false)
                {
                    CropPreview.IsChecked = false;
                    vm.PendingEdit = "";
                    vm.ResizeShow = false;
                }
            }
        }
    }

    private void SetImageFromFile()
    {
        if (DataContext is MainViewModel vm)
        {
            Bitmap image = BitmapHelper.generateBitmap(vm.FilePath);
            SetImageProperties(image);
        }
    }

    private void SetImageProperties(Bitmap image)
    {
        if (DataContext is MainViewModel vm)
        {
            vm.ImageWidth = (int)image.Size.Width;
            MainImage.Width = vm.ImageWidth;
            GlobalVars.ImageWidth = vm.ImageWidth;
            vm.ImageHeight = (int)image.Size.Height;
            MainImage.Height = vm.ImageHeight;
            GlobalVars.ImageHeight = vm.ImageHeight;
            vm.ImageDimensions = $"{vm.ImageWidth} x {vm.ImageHeight}";
            vm.ImageLeft = (vm.ImageMaxWidth - vm.ImageWidth) / 2;
            vm.ImageTop = (vm.ImageMaxHeight - vm.ImageHeight) / 2;
            MainImage.Source = image;
        }
    }
}
