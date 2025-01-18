﻿using PicView.Core.Localization;

namespace PicView.Tests.LanguageTests;
public static class EnglishUnitTest
{
#pragma warning disable xUnit2000
    [Fact]
    public static async Task CheckEnglishLanguage()
    {
        var exists = await TranslationHelper.LoadLanguage("en");
        Assert.True(exists);
        Assert.Equal(TranslationHelper.Translation.About, "About");
        Assert.Equal(TranslationHelper.Translation.ActionProgram, "Action program");
        Assert.Equal(TranslationHelper.Translation.AddedToClipboard, "Added to clipboard");
        Assert.Equal(TranslationHelper.Translation.AdditionalFunctions, "Additional functions");
        Assert.Equal(TranslationHelper.Translation.AdjustNavSpeed, "Adjust speed when key is held down");
        Assert.Equal(TranslationHelper.Translation.AdjustTimingForSlideshow, "Adjust timing for slideshow");
        Assert.Equal(TranslationHelper.Translation.AdjustTimingForZoom, "Adjust zooming speed");
        Assert.Equal(TranslationHelper.Translation.AdjustZoomLevel, "Adjust zoom level");
        Assert.Equal(TranslationHelper.Translation.AdvanceBy100Images, "Advance by 100 Images");
        Assert.Equal(TranslationHelper.Translation.AdvanceBy10Images, "Advance by 10 Images");
        Assert.Equal(TranslationHelper.Translation.AllowZoomOut,
            "Avoid zooming out when it is already at maximum size");
        Assert.Equal(TranslationHelper.Translation.Alt, "Alt");
        Assert.Equal(TranslationHelper.Translation.Altitude, "Altitude");
        Assert.Equal(TranslationHelper.Translation.AperturePriority, "Aperture priority");
        Assert.Equal(TranslationHelper.Translation.Appearance, "Appearance");
        Assert.Equal(TranslationHelper.Translation.ApplicationShortcuts, "Application Shortcuts");
        Assert.Equal(TranslationHelper.Translation.ApplicationStartup, "Application startup");
        Assert.Equal(TranslationHelper.Translation.Apply, "Apply");
        Assert.Equal(TranslationHelper.Translation.Applying, "Applying");
        Assert.Equal(TranslationHelper.Translation.Ascending, "Ascending");
        Assert.Equal(TranslationHelper.Translation.AspectRatio, "Aspect ratio");
        Assert.Equal(TranslationHelper.Translation.Authors, "Authors");
        Assert.Equal(TranslationHelper.Translation.Auto, "Auto");
        Assert.Equal(TranslationHelper.Translation.AutoFitWindow, "Auto fit window");
        Assert.Equal(TranslationHelper.Translation.BadArchive, "Archive could not be processed");
        Assert.Equal(TranslationHelper.Translation.BandedSwirl, "Banded Swirl");
        Assert.Equal(TranslationHelper.Translation.Bands, "Bands");
        Assert.Equal(TranslationHelper.Translation.Base64Image, "Base64 image");
        Assert.Equal(TranslationHelper.Translation.BatchResize, "Batch Resize");
        Assert.Equal(TranslationHelper.Translation.BitDepth, "Bit depth");
        Assert.Equal(TranslationHelper.Translation.BlackAndWhite, "Black & White");
        Assert.Equal(TranslationHelper.Translation.Bloom, "Bloom");
        Assert.Equal(TranslationHelper.Translation.Blur, "Blur");
        Assert.Equal(TranslationHelper.Translation.BottomGalleryItemSize, "Size of thumbnails in the bottom gallery");
        Assert.Equal(TranslationHelper.Translation.BottomGalleryThumbnailStretch,
            "Thumbnail stretch in the bottom gallery");
        Assert.Equal(TranslationHelper.Translation.Brightness, "Brightness");
        Assert.Equal(TranslationHelper.Translation.CameraMaker, "Camera maker");
        Assert.Equal(TranslationHelper.Translation.CameraModel, "Camera model");
        Assert.Equal(TranslationHelper.Translation.Cancel, "Cancel");
        Assert.Equal(TranslationHelper.Translation.Center, "Center");
        Assert.Equal(TranslationHelper.Translation.CenterWindow, "Center window");
        Assert.Equal(TranslationHelper.Translation.Centimeters, "centimeters");
        Assert.Equal(TranslationHelper.Translation.ChangeBackground, "Change background");
        Assert.Equal(TranslationHelper.Translation.ChangeBackgroundTooltip,
            "Change between background color for images with transparent background");
        Assert.Equal(TranslationHelper.Translation.ChangeKeybindingText,
            "Click on a textbox to change keybinding. Pressing Esc unbinds key.");
        Assert.Equal(TranslationHelper.Translation.ChangeKeybindingTooltip, "Click to change keybinding");
        Assert.Equal(TranslationHelper.Translation.ChangingThemeRequiresRestart, "* Changing theme requires restart");
        Assert.Equal(TranslationHelper.Translation.CheckForUpdates, "Check for updates");
        Assert.Equal(TranslationHelper.Translation.ClipboardImage, "Clipboard image");
        Assert.Equal(TranslationHelper.Translation.Close, "Close");
        Assert.Equal(TranslationHelper.Translation.CloseApp, "Close the whole application");
        Assert.Equal(TranslationHelper.Translation.CloseGallery, "Close gallery");
        Assert.Equal(TranslationHelper.Translation.CloseWindowPrompt, "Do you wish to close the window?");
        Assert.Equal(TranslationHelper.Translation.CloudyWeather, "Cloudy weather");
        Assert.Equal(TranslationHelper.Translation.ColorPickerTool, "Color Picker Tool");
        Assert.Equal(TranslationHelper.Translation.ColorPickerToolTooltip, "Pick color from image");
        Assert.Equal(TranslationHelper.Translation.ColorRepresentation, "Color representation");
        Assert.Equal(TranslationHelper.Translation.ColorTone, "Color Tone");
        Assert.Equal(TranslationHelper.Translation.CompressedBitsPixel, "Compressed bits per pixel");
        Assert.Equal(TranslationHelper.Translation.Compression, "Compression");
        Assert.Equal(TranslationHelper.Translation.Contrast, "Contrast");
        Assert.Equal(TranslationHelper.Translation.ConvertTo, "Convert to");
        Assert.Equal(TranslationHelper.Translation.ConvertedToBase64, "Converted to base64");
        Assert.Equal(TranslationHelper.Translation.CoolWhiteFluorescent, "Cool white fluorescent");
        Assert.Equal(TranslationHelper.Translation.CopiedImage, "Copied image to clipboard");
        Assert.Equal(TranslationHelper.Translation.Copy, "Copy");
        Assert.Equal(TranslationHelper.Translation.CopyFile, "Copy file");
        Assert.Equal(TranslationHelper.Translation.CopyImage, "Copy image");
        Assert.Equal(TranslationHelper.Translation.CopyImageTooltip, "Copy as Windows clipboard image");
        Assert.Equal(TranslationHelper.Translation.Copyright, "Copyright");
        Assert.Equal(TranslationHelper.Translation.Created, "Created");
        Assert.Equal(TranslationHelper.Translation.CreationTime, "Creation time");
        Assert.Equal(TranslationHelper.Translation.CreativeProgram, "Creative program");
        Assert.Equal(TranslationHelper.Translation.Credits, "Credits");
        Assert.Equal(TranslationHelper.Translation.Crop, "Crop");
        Assert.Equal(TranslationHelper.Translation.CropMessage, "Press Esc to close, Enter to save");
        Assert.Equal(TranslationHelper.Translation.CropPicture, "Crop Picture");
        Assert.Equal(TranslationHelper.Translation.Ctrl, "Ctrl");
        Assert.Equal(TranslationHelper.Translation.CtrlToZoom, "Ctrl to zoom, scroll to navigate");
        Assert.Equal(TranslationHelper.Translation.Cut, "Cut");
        Assert.Equal(TranslationHelper.Translation.DarkTheme, "Dark theme");
        Assert.Equal(TranslationHelper.Translation.Date, "Date");
        Assert.Equal(TranslationHelper.Translation.DateTaken, "Date taken");
        Assert.Equal(TranslationHelper.Translation.DayWhiteFluorescent, "Day white fluorescent");
        Assert.Equal(TranslationHelper.Translation.Daylight, "Daylight");
        Assert.Equal(TranslationHelper.Translation.DaylightFluorescent, "Daylight fluorescent");
        Assert.Equal(TranslationHelper.Translation.Del, "Del");
        Assert.Equal(TranslationHelper.Translation.DeleteFile, "Delete file");
        Assert.Equal(TranslationHelper.Translation.DeleteFilePermanently,
            "Are you sure you want to permanently delete");
        Assert.Equal(TranslationHelper.Translation.DeletedFile, "Deleted file");
        Assert.Equal(TranslationHelper.Translation.Descending, "Descending");
        Assert.Equal(TranslationHelper.Translation.DigitalZoom, "Digital zoom");
        Assert.Equal(TranslationHelper.Translation.DirectionalBlur, "Directional Blur");
        Assert.Equal(TranslationHelper.Translation.DisableFadeInButtonsOnHover,
            "Disable fade-in buttons on mouse hover");
        Assert.Equal(TranslationHelper.Translation.DiskSize, "Disk size");
        Assert.Equal(TranslationHelper.Translation.DoubleClick, "Double Click");
        Assert.Equal(TranslationHelper.Translation.Down, "Down");
        Assert.Equal(TranslationHelper.Translation.Dpi, "DPI");
        Assert.Equal(TranslationHelper.Translation.DragFileTo,
            "Drag file to Windows Explorer or another application/browser");
        Assert.Equal(TranslationHelper.Translation.DragImage, "Drag image");
        Assert.Equal(TranslationHelper.Translation.DropToLoad, "Drop to load image");
        Assert.Equal(TranslationHelper.Translation.DuplicateFile, "Duplicate file");
        Assert.Equal(TranslationHelper.Translation.Effects, "Effects");
        Assert.Equal(TranslationHelper.Translation.EffectsTooltip, "Show image effects window");
        Assert.Equal(TranslationHelper.Translation.Embossed, "Embossed");
        Assert.Equal(TranslationHelper.Translation.Enter, "Enter");
        Assert.Equal(TranslationHelper.Translation.Esc, "Esc");
        Assert.Equal(TranslationHelper.Translation.EscCloseTooltip, "Closes currently opened window/menu");
        Assert.Equal(TranslationHelper.Translation.ExifVersion, "Exif version");
        Assert.Equal(TranslationHelper.Translation.ExpandedGalleryItemSize, "Size of thumbnails");
        Assert.Equal(TranslationHelper.Translation.ExposureBias, "Exposure bias");
        Assert.Equal(TranslationHelper.Translation.ExposureProgram, "Exposure program");
        Assert.Equal(TranslationHelper.Translation.ExposureTime, "Exposure time");
        Assert.Equal(TranslationHelper.Translation.FNumber, "F number");
        Assert.Equal(TranslationHelper.Translation.File, "file");
        Assert.Equal(TranslationHelper.Translation.FileCopy, "File added to clipboard");
        Assert.Equal(TranslationHelper.Translation.FileCopyPath, "Copy file path");
        Assert.Equal(TranslationHelper.Translation.FileCopyPathMessage, "File path added to clipboard");
        Assert.Equal(TranslationHelper.Translation.FileCutMessage, "File added to move clipboard");
        Assert.Equal(TranslationHelper.Translation.FileExtension, "File extension");
        Assert.Equal(TranslationHelper.Translation.FileManagement, "File management");
        Assert.Equal(TranslationHelper.Translation.FileName, "File name");
        Assert.Equal(TranslationHelper.Translation.FilePaste, "Paste");
        Assert.Equal(TranslationHelper.Translation.FileProperties, "File properties");
        Assert.Equal(TranslationHelper.Translation.FileSize, "File size");
        Assert.Equal(TranslationHelper.Translation.Files, "files");
        Assert.Equal(TranslationHelper.Translation.Fill, "Fill");
        Assert.Equal(TranslationHelper.Translation.FillHeight, "⇔ Fill height");
        Assert.Equal(TranslationHelper.Translation.FillSquare, "Fill square");
        Assert.Equal(TranslationHelper.Translation.FineWeather, "Fine weather");
        Assert.Equal(TranslationHelper.Translation.FirstImage, "First image");
        Assert.Equal(TranslationHelper.Translation.Fit, "Fit");
        Assert.Equal(TranslationHelper.Translation.FitToWindow, "Fit to window/image");
        Assert.Equal(TranslationHelper.Translation.Flash, "Flash");
        Assert.Equal(TranslationHelper.Translation.FlashDidNotFire, "Flash did not fire");
        Assert.Equal(TranslationHelper.Translation.FlashEnergy, "Flash energy");
        Assert.Equal(TranslationHelper.Translation.FlashFired, "Flash fired");
        Assert.Equal(TranslationHelper.Translation.FlashMode, "Flash mode");
        Assert.Equal(TranslationHelper.Translation.Flip, "Flip horizontally");
        Assert.Equal(TranslationHelper.Translation.Flipped, "Flipped horizontally");
        Assert.Equal(TranslationHelper.Translation.Fluorescent, "Fluorescent");
        Assert.Equal(TranslationHelper.Translation.FocalLength, "Focal length");
        Assert.Equal(TranslationHelper.Translation.FocalLength35mm, "Focal length 35mm");
        Assert.Equal(TranslationHelper.Translation.Folder, "Folder");
        Assert.Equal(TranslationHelper.Translation.Forward, "Forward");
        Assert.Equal(TranslationHelper.Translation.FrostyOutline, "Frosty Outline");
        Assert.Equal(TranslationHelper.Translation.Fstop, "F-stop");
        Assert.Equal(TranslationHelper.Translation.FullPath, "Full path");
        Assert.Equal(TranslationHelper.Translation.Fullscreen, "Fullscreen");
        Assert.Equal(TranslationHelper.Translation.GallerySettings, "Gallery settings");
        Assert.Equal(TranslationHelper.Translation.GalleryThumbnailStretch, "Gallery thumbnail stretch");
        Assert.Equal(TranslationHelper.Translation.GeneralSettings, "General Settings");
        Assert.Equal(TranslationHelper.Translation.GenerateThumbnails, "Generate thumbnails");
        Assert.Equal(TranslationHelper.Translation.GithubRepo, "Github repository");
        Assert.Equal(TranslationHelper.Translation.GlassTheme, "Glass Theme");
        Assert.Equal(TranslationHelper.Translation.GlassTile, "Glass Tile");
        Assert.Equal(TranslationHelper.Translation.Gloom, "Gloom");
        Assert.Equal(TranslationHelper.Translation.GoBackBy100Images, "Go Back by 100 Images");
        Assert.Equal(TranslationHelper.Translation.GoBackBy10Images, "Go Back by 10 Images");
        Assert.Equal(TranslationHelper.Translation.GoToImageAtSpecifiedIndex, "Go to image at specified index");
        Assert.Equal(TranslationHelper.Translation.Hard, "Hard");
        Assert.Equal(TranslationHelper.Translation.Height, "Height");
        Assert.Equal(TranslationHelper.Translation.HideBottomGallery, "Hide bottom gallery");
        Assert.Equal(TranslationHelper.Translation.HideBottomToolbar, "Hide Bottom Toolbar");
        Assert.Equal(TranslationHelper.Translation.HideUI, "Hide interface");
        Assert.Equal(TranslationHelper.Translation.High, "High");
        Assert.Equal(TranslationHelper.Translation.HighQuality, "High quality");
        Assert.Equal(TranslationHelper.Translation.HighlightColor, "Highlight color");
        Assert.Equal(TranslationHelper.Translation.ISOSpeed, "ISO speed");
        Assert.Equal(TranslationHelper.Translation.IconsUsed, "Icons used:");
        Assert.Equal(TranslationHelper.Translation.Image, "Image");
        Assert.Equal(TranslationHelper.Translation.ImageAliasing, "Image aliasing");
        Assert.Equal(TranslationHelper.Translation.ImageControl, "Image control");
        Assert.Equal(TranslationHelper.Translation.ImageInfo, "Image Info");
        Assert.Equal(TranslationHelper.Translation.Inches, "inches");
        Assert.Equal(TranslationHelper.Translation.InfoWindow, "Info window");
        Assert.Equal(TranslationHelper.Translation.InfoWindowTitle, "Info and shortcuts");
        Assert.Equal(TranslationHelper.Translation.InterfaceConfiguration, "Interface configuration");
        Assert.Equal(TranslationHelper.Translation.Landscape, "Landscape");
        Assert.Equal(TranslationHelper.Translation.Language, "Language");
        Assert.Equal(TranslationHelper.Translation.LastAccessTime, "Last access time");
        Assert.Equal(TranslationHelper.Translation.LastImage, "Last image");
        Assert.Equal(TranslationHelper.Translation.LastWriteTime, "Last write time");
        Assert.Equal(TranslationHelper.Translation.Latitude, "Latitude");
        Assert.Equal(TranslationHelper.Translation.Left, "Left");
        Assert.Equal(TranslationHelper.Translation.LensMaker, "Lens maker");
        Assert.Equal(TranslationHelper.Translation.LensModel, "Lens model");
        Assert.Equal(TranslationHelper.Translation.LightSource, "Light source");
        Assert.Equal(TranslationHelper.Translation.LightTheme, "Light theme");
        Assert.Equal(TranslationHelper.Translation.Lighting, "Lighting");
        Assert.Equal(TranslationHelper.Translation.Loading, "Loading...");
        Assert.Equal(TranslationHelper.Translation.Longitude, "Longitude");
        Assert.Equal(TranslationHelper.Translation.Looping, "Looping");
        Assert.Equal(TranslationHelper.Translation.LoopingDisabled, "Looping disabled");
        Assert.Equal(TranslationHelper.Translation.LoopingEnabled, "Looping enabled");
        Assert.Equal(TranslationHelper.Translation.Lossless, "Lossless");
        Assert.Equal(TranslationHelper.Translation.Lossy, "Lossy");
        Assert.Equal(TranslationHelper.Translation.Low, "Low");
        Assert.Equal(TranslationHelper.Translation.Manual, "Manual");
        Assert.Equal(TranslationHelper.Translation.MaxAperture, "Max aperture");
        Assert.Equal(TranslationHelper.Translation.Maximize, "Maximize");
        Assert.Equal(TranslationHelper.Translation.MegaPixels, "megapixels");
        Assert.Equal(TranslationHelper.Translation.Meter, "Meter");
        Assert.Equal(TranslationHelper.Translation.MeteringMode, "Metering mode");
        Assert.Equal(TranslationHelper.Translation.Minimize, "Minimize");
        Assert.Equal(TranslationHelper.Translation.MiscSettings, "Misc settings");
        Assert.Equal(TranslationHelper.Translation.Modified, "Modified");
        Assert.Equal(TranslationHelper.Translation.Monochrome, "Monochrome");
        Assert.Equal(TranslationHelper.Translation.MouseDrag, "Mouse drag");
        Assert.Equal(TranslationHelper.Translation.MouseKeyBack, "Mouse key back");
        Assert.Equal(TranslationHelper.Translation.MouseKeyForward, "Mouse key forward");
        Assert.Equal(TranslationHelper.Translation.MouseWheel, "Mouse wheel");
        Assert.Equal(TranslationHelper.Translation.MoveWindow, "Move window");
        Assert.Equal(TranslationHelper.Translation.Navigation, "Navigation");
        Assert.Equal(TranslationHelper.Translation.NearestNeighbor, "Nearest neighbor");
        Assert.Equal(TranslationHelper.Translation.NegativeColors, "Negative Colors");
        Assert.Equal(TranslationHelper.Translation.NewWindow, "New window");
        Assert.Equal(TranslationHelper.Translation.NextFolder, "Navigate to next folder");
        Assert.Equal(TranslationHelper.Translation.NextImage, "Next image");
        Assert.Equal(TranslationHelper.Translation.NoChange, "No changes");
        Assert.Equal(TranslationHelper.Translation.NoConversion, "No conversion");
        Assert.Equal(TranslationHelper.Translation.NoImage, "No image loaded");
        Assert.Equal(TranslationHelper.Translation.NoImages, "No Images");
        Assert.Equal(TranslationHelper.Translation.NoResize, "No resize");
        Assert.Equal(TranslationHelper.Translation.None, "None");
        Assert.Equal(TranslationHelper.Translation.Normal, "Normal");
        Assert.Equal(TranslationHelper.Translation.NormalWindow, "Normal window");
        Assert.Equal(TranslationHelper.Translation.NotDefined, "Not defined");
        Assert.Equal(TranslationHelper.Translation.NumpadMinus, "Numpad -");
        Assert.Equal(TranslationHelper.Translation.NumpadPlus, "Numpad +");
        Assert.Equal(TranslationHelper.Translation.OldMovie, "Old Movie");
        Assert.Equal(TranslationHelper.Translation.Open, "Open");
        Assert.Equal(TranslationHelper.Translation.OpenFileDialog, "Select a file");
        Assert.Equal(TranslationHelper.Translation.OpenInSameWindow, "Open files in the same window");
        Assert.Equal(TranslationHelper.Translation.OpenLastFile, "Open last file");
        Assert.Equal(TranslationHelper.Translation.OpenWith, "Open with...");
        Assert.Equal(TranslationHelper.Translation.OptimizeImage, "Optimize Image");
        Assert.Equal(TranslationHelper.Translation.Orientation, "Orientation");
        Assert.Equal(TranslationHelper.Translation.OutputFolder, "Output folder");
        Assert.Equal(TranslationHelper.Translation.Pan, "Pan");
        Assert.Equal(TranslationHelper.Translation.PaperFold, "Paper Fold");
        Assert.Equal(TranslationHelper.Translation.PasswordArchive, "Password protected archive not supported");
        Assert.Equal(TranslationHelper.Translation.PasteImageFromClipholder, "Paste image from clip-holder");
        Assert.Equal(TranslationHelper.Translation.PencilSketch, "Pencil Sketch");
        Assert.Equal(TranslationHelper.Translation.PercentComplete, "% complete...");
        Assert.Equal(TranslationHelper.Translation.Percentage, "Percentage");
        Assert.Equal(TranslationHelper.Translation.PermanentlyDelete, "Permanently delete");
        Assert.Equal(TranslationHelper.Translation.PhotometricInterpretation, "Photometric interpretation");
        Assert.Equal(TranslationHelper.Translation.Pivot, "Pivot");
        Assert.Equal(TranslationHelper.Translation.Pixelate, "Pixelate");
        Assert.Equal(TranslationHelper.Translation.Pixels, "pixels");
        Assert.Equal(TranslationHelper.Translation.Portrait, "Portrait");
        Assert.Equal(TranslationHelper.Translation.PressKey, "Press key...");
        Assert.Equal(TranslationHelper.Translation.PrevFolder, "Navigate to previous folder");
        Assert.Equal(TranslationHelper.Translation.PrevImage, "Previous image");
        Assert.Equal(TranslationHelper.Translation.Print, "Print");
        Assert.Equal(TranslationHelper.Translation.PrintSizeCm, "Print size (cm)");
        Assert.Equal(TranslationHelper.Translation.PrintSizeIn, "Print size (in)");
        Assert.Equal(TranslationHelper.Translation.Quality, "Quality");
        Assert.Equal(TranslationHelper.Translation.Random, "Random");
        Assert.Equal(TranslationHelper.Translation.RecentFiles, "Recent files");
        Assert.Equal(TranslationHelper.Translation.RedEyeReduction, "Red eye reduction");
        Assert.Equal(TranslationHelper.Translation.Reload, "Reload");
        Assert.Equal(TranslationHelper.Translation.RemoveStarRating, "Remove rating");
        Assert.Equal(TranslationHelper.Translation.RenameFile, "Rename file");
        Assert.Equal(TranslationHelper.Translation.Reset, "Reset");
        Assert.Equal(TranslationHelper.Translation.ResetButtonText, "Reset to default");
        Assert.Equal(TranslationHelper.Translation.ResetZoom, "Reset zoom");
        Assert.Equal(TranslationHelper.Translation.Resize, "Resize");
        Assert.Equal(TranslationHelper.Translation.ResizeImage, "Resize Image");
        Assert.Equal(TranslationHelper.Translation.Resolution, "Resolution");
        Assert.Equal(TranslationHelper.Translation.ResolutionUnit, "Resolution unit");
        Assert.Equal(TranslationHelper.Translation.RestartApp, "Restart the application");
        Assert.Equal(TranslationHelper.Translation.RestoreDown, "Restore Down");
        Assert.Equal(TranslationHelper.Translation.Reverse, "Reverse");
        Assert.Equal(TranslationHelper.Translation.Right, "Right");
        Assert.Equal(TranslationHelper.Translation.Ripple, "Ripple");
        Assert.Equal(TranslationHelper.Translation.RippleAlt, "Ripple Alt");
        Assert.Equal(TranslationHelper.Translation.RotateLeft, "Rotate left");
        Assert.Equal(TranslationHelper.Translation.RotateRight, "Rotate right");
        Assert.Equal(TranslationHelper.Translation.Rotated, "Rotated");
        Assert.Equal(TranslationHelper.Translation.Saturation, "Saturation");
        Assert.Equal(TranslationHelper.Translation.Save, "Save");
        Assert.Equal(TranslationHelper.Translation.SaveAs, "Save as");
        Assert.Equal(TranslationHelper.Translation.SavingFileFailed, "Saving file failed");
        Assert.Equal(TranslationHelper.Translation.ScrollAndRotate, "Scroll and rotate");
        Assert.Equal(TranslationHelper.Translation.ScrollDirection, "Scroll direction");
        Assert.Equal(TranslationHelper.Translation.ScrollDown, "Scroll down");
        Assert.Equal(TranslationHelper.Translation.ScrollToBottom, "Scroll to bottom");
        Assert.Equal(TranslationHelper.Translation.ScrollToTop, "Scroll to top");
        Assert.Equal(TranslationHelper.Translation.ScrollToZoom, "Zoom with mousewheel, navigate with Ctrl");
        Assert.Equal(TranslationHelper.Translation.ScrollUp, "Scroll up");
        Assert.Equal(TranslationHelper.Translation.Scrolling, "Scrolling");
        Assert.Equal(TranslationHelper.Translation.ScrollingDisabled, "Scrolling disabled");
        Assert.Equal(TranslationHelper.Translation.ScrollingEnabled, "Scrolling enabled");
        Assert.Equal(TranslationHelper.Translation.SearchSubdirectory, "Search subdirectories");
        Assert.Equal(TranslationHelper.Translation.SecAbbreviation, "Sec.");
        Assert.Equal(TranslationHelper.Translation.SelectAll, "Select All");
        Assert.Equal(TranslationHelper.Translation.SelectGalleryThumb, "Select gallery thumbnail");
        Assert.Equal(TranslationHelper.Translation.SendCurrentImageToRecycleBin,
            "Send current image to the recycle bin");
        Assert.Equal(TranslationHelper.Translation.SentFileToRecycleBin, "Sent file to the recycle bin");
        Assert.Equal(TranslationHelper.Translation.SetAs, "Set as...");
        Assert.Equal(TranslationHelper.Translation.SetAsLockScreenImage, "Set as lock screen image");
        Assert.Equal(TranslationHelper.Translation.SetAsWallpaper, "Set as wallpaper");
        Assert.Equal(TranslationHelper.Translation.SetCurrentImageAsWallpaper, "Set current image as wallpaper:");
        Assert.Equal(TranslationHelper.Translation.SetStarRating, "Set star rating");
        Assert.Equal(TranslationHelper.Translation.Settings, "Settings");
        Assert.Equal(TranslationHelper.Translation.Shade, "Shade");
        Assert.Equal(TranslationHelper.Translation.Sharpness, "Sharpness");
        Assert.Equal(TranslationHelper.Translation.Shift, "Shift");
        Assert.Equal(TranslationHelper.Translation.ShowAllSettingsWindow, "Show all settings window");
        Assert.Equal(TranslationHelper.Translation.ShowBottomGallery, "Show bottom gallery");
        Assert.Equal(TranslationHelper.Translation.ShowBottomGalleryWhenUiIsHidden,
            "Show bottom gallery when UI is hidden");
        Assert.Equal(TranslationHelper.Translation.ShowBottomToolbar, "Show bottom toolbar");
        Assert.Equal(TranslationHelper.Translation.ShowConfirmationOnEsc,
            "Show confirmation dialog when pressing 'Esc'");
        Assert.Equal(TranslationHelper.Translation.ShowFadeInButtonsOnHover, "Show fade-in buttons on mouse hover");
        Assert.Equal(TranslationHelper.Translation.ShowFileSavingDialog, "Show file saving dialog");
        Assert.Equal(TranslationHelper.Translation.ShowImageGallery, "Show image gallery");
        Assert.Equal(TranslationHelper.Translation.ShowImageInfo, "Show Image Info");
        Assert.Equal(TranslationHelper.Translation.ShowInFolder, "Show in folder");
        Assert.Equal(TranslationHelper.Translation.ShowInfoWindow, "Show info window");
        Assert.Equal(TranslationHelper.Translation.ShowResizeWindow, "Show the Resize Window");
        Assert.Equal(TranslationHelper.Translation.ShowUI, "Show UI");
        Assert.Equal(TranslationHelper.Translation.ShutterPriority, "Shutter priority");
        Assert.Equal(TranslationHelper.Translation.SideBySide, "Side by side");
        Assert.Equal(TranslationHelper.Translation.SideBySideTooltip, "Show images side by side");
        Assert.Equal(TranslationHelper.Translation.Size, "Size");
        Assert.Equal(TranslationHelper.Translation.SizeMp, "Size (mp)");
        Assert.Equal(TranslationHelper.Translation.SizeTooltip, "Enter desired size in pixels or percentage.");
        Assert.Equal(TranslationHelper.Translation.Sketch, "Sketch");
        Assert.Equal(TranslationHelper.Translation.Slideshow, "Slideshow");
        Assert.Equal(TranslationHelper.Translation.SmoothMagnify, "Smooth Magnify");
        Assert.Equal(TranslationHelper.Translation.Soft, "Soft");
        Assert.Equal(TranslationHelper.Translation.Software, "Software");
        Assert.Equal(TranslationHelper.Translation.SortFilesBy, "Sort files by");
        Assert.Equal(TranslationHelper.Translation.SourceFolder, "Source folder");
        Assert.Equal(TranslationHelper.Translation.Space, "Space");
        Assert.Equal(TranslationHelper.Translation.Square, "Square");
        Assert.Equal(TranslationHelper.Translation.Start, "Start");
        Assert.Equal(TranslationHelper.Translation.StartSlideshow, "Start slideshow");
        Assert.Equal(TranslationHelper.Translation.StayCentered, "Keep window centered");
        Assert.Equal(TranslationHelper.Translation.StayTopMost, "Stay on top of other windows");
        Assert.Equal(TranslationHelper.Translation.Stretch, "Stretch");
        Assert.Equal(TranslationHelper.Translation.StretchImage, "Stretch image");
        Assert.Equal(TranslationHelper.Translation.StrobeReturnLightDetected, "Strobe return light detected");
        Assert.Equal(TranslationHelper.Translation.StrobeReturnLightNotDetected, "Strobe return light not detected");
        Assert.Equal(TranslationHelper.Translation.Subject, "Subject");
        Assert.Equal(TranslationHelper.Translation.Swirl, "Swirl");
        Assert.Equal(TranslationHelper.Translation.TelescopicBlur, "Telescopic Blur");
        Assert.Equal(TranslationHelper.Translation.Theme, "Theme");
        Assert.Equal(TranslationHelper.Translation.Thumbnail, "Thumb");
        Assert.Equal(TranslationHelper.Translation.Tile, "Tile");
        Assert.Equal(TranslationHelper.Translation.Title, "Title");
        Assert.Equal(TranslationHelper.Translation.ToggleBackgroundColor, "Toggle background color");
        Assert.Equal(TranslationHelper.Translation.ToggleFullscreen, "Toggle fullscreen");
        Assert.Equal(TranslationHelper.Translation.ToggleLooping, "Toggle looping");
        Assert.Equal(TranslationHelper.Translation.ToggleScroll, "Toggle scroll");
        Assert.Equal(TranslationHelper.Translation.ToggleTaskbarProgress, "Display taskbar progress");
        Assert.Equal(TranslationHelper.Translation.ToneMapping, "ToneMapping");
        Assert.Equal(TranslationHelper.Translation.UnableToRender, "Unable to render image");
        Assert.Equal(TranslationHelper.Translation.Uncalibrated, "Uncalibrated");
        Assert.Equal(TranslationHelper.Translation.Underwater, "Underwater");
        Assert.Equal(TranslationHelper.Translation.UnexpectedError, "An unknown error occured");
        Assert.Equal(TranslationHelper.Translation.Unflip, "Unflip");
        Assert.Equal(TranslationHelper.Translation.Uniform, "Uniform");
        Assert.Equal(TranslationHelper.Translation.UniformToFill, "Uniform to fill");
        Assert.Equal(TranslationHelper.Translation.Unknown, "Unknown");
        Assert.Equal(TranslationHelper.Translation.UnsupportedFile, "Unsupported file");
        Assert.Equal(TranslationHelper.Translation.Up, "Up");
        Assert.Equal(TranslationHelper.Translation.UsingMouse, "Using mouse");
        Assert.Equal(TranslationHelper.Translation.UsingTouchpad, "Using touchpad");
        Assert.Equal(TranslationHelper.Translation.Version, "Version:");
        Assert.Equal(TranslationHelper.Translation.ViewLicenseFile, "View license file");
        Assert.Equal(TranslationHelper.Translation.WaveWarper, "Wave Warper");
        Assert.Equal(TranslationHelper.Translation.WhiteBalance, "White balance");
        Assert.Equal(TranslationHelper.Translation.WhiteFluorescent, "White fluorescent");
        Assert.Equal(TranslationHelper.Translation.Width, "Width");
        Assert.Equal(TranslationHelper.Translation.WidthAndHeight, "Width and height");
        Assert.Equal(TranslationHelper.Translation.WindowManagement, "Window management");
        Assert.Equal(TranslationHelper.Translation.WindowScaling, "Window scaling");
        Assert.Equal(TranslationHelper.Translation.Zoom, "Zoom");
        Assert.Equal(TranslationHelper.Translation.ZoomIn, "Zoom in");
        Assert.Equal(TranslationHelper.Translation.ZoomOut, "Zoom out");
        Assert.Equal(TranslationHelper.Translation._1Star, "1 star rating");
        Assert.Equal(TranslationHelper.Translation._2Star, "2 star rating");
        Assert.Equal(TranslationHelper.Translation._3Star, "3 star rating");
        Assert.Equal(TranslationHelper.Translation._4Star, "4 star rating");
        Assert.Equal(TranslationHelper.Translation._5Star, "5 star rating");
    }
#pragma warning restore xUnit2000
}
