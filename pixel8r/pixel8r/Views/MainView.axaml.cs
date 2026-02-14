using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using Avalonia.ReactiveUI;
using Avalonia.Skia.Helpers;
using pixel8r.Helpers;
using pixel8r.ViewModels;
using SkiaSharp;

namespace pixel8r.Views;

public partial class MainView : ReactiveUserControl<MainViewModel>
{
    public MainView()
    {
        InitializeComponent();
    }

    public void MainView_Loaded(object sender, RoutedEventArgs e)
    {
        if (DataContext is MainViewModel vm)
        {
            if (DiscretePalette.SelectedItem is string selectedPalette)
            {
                PaletteLoader.setCurrentPalette(selectedPalette);
                PalettePreviewImage.Source = BitmapHelper.DrawPalette();
            }
        }
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

    private async void Save_Click(object? sender, RoutedEventArgs e)
    {
        var window = this.VisualRoot as Window;
        if (window == null)
            return;

        var options = new FilePickerSaveOptions
        {
            Title = "Save image",
            FileTypeChoices = new List<FilePickerFileType>
            {
                new FilePickerFileType("Image")
                {
                    Patterns = new[] { "*.png", "*.jpg", "*.jpeg", "*.bmp", "*.gif" }
                }
            }
        };

        var result = await window.StorageProvider.SaveFilePickerAsync(options);
        if (result == null)
            return;
        using (var memory = new System.IO.MemoryStream())
        {
            Bitmap bmp = MainImage.Source as Bitmap;
            bmp.Save(memory);
            memory.Position = 0;
            SKImage saveBmp = SKImage.FromEncodedData(memory);
            ImageSavingHelper.SaveImage(saveBmp, result.TryGetLocalPath());
        }
    }

    private void Undo_Click(object? sender, RoutedEventArgs e)
    {
        if (DataContext is MainViewModel vm)
        {
            if (vm.PreviousImage != null)
            {
                SetImageProperties(vm.PreviousImage);
                vm.AllowUndo = false;
            }
        }
    }

    private void Reload_Click(object? sender, RoutedEventArgs e)
    {
        SetImageFromFile();
    }

    private void DiscretePalette_Selected(object? sender, SelectionChangedEventArgs e)
    {
        if (DataContext is MainViewModel vm)
        {
            if (DiscretePalette.SelectedItem is string selectedPalette)
            {
                PaletteLoader.setCurrentPalette(selectedPalette);
                PalettePreviewImage.Source = BitmapHelper.DrawPalette();
            }
        }
    }

    private void SwapDiscrete_Click(object? sender, RoutedEventArgs e)
    {
        if (DataContext is MainViewModel vm)
        {
            if (SwapAlgorithm.SelectedItem is ComboBoxItem algorithm)
            {
                vm.PreviousImage = MainImage.Source as Bitmap;
                vm.AllowUndo = true;
                MainImage.Source = BitmapHelper.paletteSwapPredefined(
                    MainImage.Source as Bitmap,
                    algorithm.Content.ToString(),
                    FastMode.IsChecked ?? false
                );
            }
        }
    }

    private void Transpose_Click(object? sender, RoutedEventArgs e)
    {
        if (DataContext is MainViewModel vm)
        {
            if (Transpose.SelectedItem is ComboBoxItem selection)
            {
                vm.PreviousImage = MainImage.Source as Bitmap;
                vm.AllowUndo = true;
                MainImage.Source = BitmapHelper.transpose(
                    MainImage.Source as Bitmap,
                    selection.Content.ToString()
                );
            }
        }
    }

    private void ReduceFidelity_Click(object? sender, RoutedEventArgs e)
    {
        if (DataContext is MainViewModel vm)
        {
            if (ReduceBits.SelectedItem is ComboBoxItem selection)
            {
                vm.PreviousImage = MainImage.Source as Bitmap;
                vm.AllowUndo = true;
                MainImage.Source = BitmapHelper.reduceFidelity(
                    MainImage.Source as Bitmap,
                    selection.Content.ToString()
                );
            }
        }
    }

    private void Saturate_Click(object? sender, RoutedEventArgs e)
    {
        if (DataContext is MainViewModel vm)
        {
            if (SaturateSlider.Value is double percent)
            {
                vm.PreviousImage = MainImage.Source as Bitmap;
                vm.AllowUndo = true;
                MainImage.Source = BitmapHelper.saturate(
                    MainImage.Source as Bitmap,
                    (int)percent
                );
            }
        }
    }

    private void Tint_Click(object? sender, RoutedEventArgs e)
    {
        if (DataContext is MainViewModel vm)
        {
            if (Tint.SelectedItem is ComboBoxItem selection)
            {
                if (selection.Content.ToString().Contains("scale"))
                {
                    vm.PreviousImage = MainImage.Source as Bitmap;
                    vm.AllowUndo = true;
                    MainImage.Source = BitmapHelper.tintScale(
                        MainImage.Source as Bitmap,
                        selection.Content.ToString()
                    );
                }
                else
                {
                    if (TintSlider.Value is double value)
                    {
                        vm.PreviousImage = MainImage.Source as Bitmap;
                        vm.AllowUndo = true;
                        MainImage.Source = BitmapHelper.tint(
                            MainImage.Source as Bitmap,
                            selection.Content.ToString(),
                            (int)value,
                            TintSoftHard.IsChecked ?? false
                        );
                    }
                }
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
                vm.AllowEdit = false;
                vm.AllowUndo = false;
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
                vm.AllowEdit = true;
                vm.AllowUndo = (vm.PreviousImage != null);
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
                Bitmap previous = MainImage.Source as Bitmap;
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
                    vm.AllowEdit = true;
                    vm.ResizeShow = false;
                    vm.PendingEdit = "";
                    vm.PreviousImage = previous;
                    vm.AllowUndo = true;
                }
                catch (OutOfMemoryException ex)
                {
                    vm.PendingEdit = "Crop attempt failed! Make sure that the crop boundary is not extending outside the image.";
                }
            }
        }
    }

    private void ResizeSlider_Change(object? sender, RangeBaseValueChangedEventArgs e)
    {
        if (DataContext is MainViewModel vm)
        {
            if (ResizeSlider.Value is double sliderValue)
            {
                (vm.ResizeWidth, vm.ResizeHeight) = ResizeHelper.getResizeDimensions(
                    (int)sliderValue
                );
                ResizeTarget.Content = $"{vm.ResizeWidth} x {vm.ResizeHeight}";
                vm.ResizeLeft = (vm.ImageMaxWidth - vm.ResizeWidth) / 2;
                vm.ResizeTop = (vm.ImageMaxHeight - vm.ResizeHeight) / 2;
                vm.ResizeShow = true;
            }
        }
    }

    private void ResizeSlider_Release(object? sender, PointerCaptureLostEventArgs e)
    {
        if (DataContext is MainViewModel vm)
        {
            vm.ResizeShow = false;
        }
    }

    private void Resize_Click(object? sender, RoutedEventArgs e)
    {
        if (DataContext is MainViewModel vm)
        {
            if (ResizeSlider.Value is double sliderValue)
            {
                vm.PreviousImage = MainImage.Source as Bitmap;
                vm.AllowUndo = true;
                Bitmap resized = BitmapHelper.resize(
                    MainImage.Source as Bitmap,
                    100 / (float)sliderValue
                );
                SetImageProperties(resized);
                vm.ResizeShow = false;
            }
        }
    }

    private void Pixelate_Click(object? sender, RoutedEventArgs e)
    {
        if (DataContext is MainViewModel vm)
        {
            vm.PreviousImage = MainImage.Source as Bitmap;
            vm.AllowUndo = true;
            MainImage.Source = BitmapHelper.pixelate(
                MainImage.Source as Bitmap
            );
        }
    }

    private void Scanlines_Click(object? sender, RoutedEventArgs e)
    {
        if (DataContext is MainViewModel vm)
        {
            vm.PreviousImage = MainImage.Source as Bitmap;
            vm.AllowUndo = true;
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
                    vm.AllowEdit = true;
                    vm.AllowUndo = (vm.PreviousImage != null);
                    vm.PendingEdit = "";
                    vm.ResizeShow = false;
                }
            }
        }
    }

    private void SetResizeOptions()
    {
        (int resizeLowerLimit, int resizeUpperLimit) = ResizeHelper.getResizeBounds();
        ResizeSlider.Minimum = resizeLowerLimit;
        ResizeSlider.Maximum = resizeUpperLimit;
        ResizeSlider.Value = resizeLowerLimit > 100 ? resizeLowerLimit : 100;
        ResizeSlider.LargeChange = (resizeUpperLimit - resizeLowerLimit) / 20;
    }

    private void SetImageFromFile()
    {
        if (DataContext is MainViewModel vm)
        {
            Bitmap image = BitmapHelper.generateBitmap(vm.FilePath);
            SetImageProperties(image);
            vm.AllowEdit = true;
            CropPreview.IsEnabled = true;
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
            SetResizeOptions();
            SaturateSlider.Value = 0;
            TintSlider.Value = 0;
            TintSoftHard.IsChecked = false;
            MainImage.Source = image;
        }
    }
}
