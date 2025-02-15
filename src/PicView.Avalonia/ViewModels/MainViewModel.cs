﻿using System.Diagnostics;
using System.Reactive;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.Media;
using PicView.Avalonia.Clipboard;
using PicView.Avalonia.Converters;
using PicView.Avalonia.Gallery;
using PicView.Avalonia.ImageEffects;
using PicView.Avalonia.ImageHandling;
using PicView.Avalonia.ImageTransformations;
using PicView.Avalonia.Interfaces;
using PicView.Avalonia.Navigation;
using PicView.Avalonia.UI;
using PicView.Avalonia.Wallpaper;
using PicView.Avalonia.WindowBehavior;
using PicView.Core.Calculations;
using PicView.Core.Config;
using PicView.Core.FileHandling;
using PicView.Core.Gallery;
using PicView.Core.Localization;
using PicView.Core.ProcessHandling;
using ReactiveUI;
using ImageViewer = PicView.Avalonia.Views.ImageViewer;

namespace PicView.Avalonia.ViewModels;

public class MainViewModel : ViewModelBase
{
    public readonly IPlatformSpecificService? PlatformService;
    
    #region Image

    public object? ImageSource
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public object? SecondaryImageSource
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public ImageType ImageType
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public double ImageWidth
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public double ImageHeight
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public double SecondaryImageWidth
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public Brush? ImageBackground
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public bool IsShowingSideBySide
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public double ScrollViewerWidth
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    } = double.NaN;

    public double ScrollViewerHeight
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    } = double.NaN;
    
    public double AspectRatio
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }
    
    public ImageEffectConfig? EffectConfig
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    #endregion

    #region Gallery

    public Thickness GalleryMargin
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public bool IsGalleryShown
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public bool IsBottomGalleryShownInHiddenUI
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public GalleryMode GalleryMode
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    } = GalleryMode.Closed;

    public Stretch GalleryStretch
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public int SelectedGalleryItemIndex
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public VerticalAlignment GalleryVerticalAlignment
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    } = VerticalAlignment.Bottom;

    public Orientation GalleryOrientation
    {
        set => this.RaiseAndSetIfChanged(ref field, value);
        get;
    }

    public bool IsGalleryCloseIconVisible
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public double GalleryWidth
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public double GalleryHeight
    {
        get
        {
            if (!SettingsHelper.Settings.Gallery.IsBottomGalleryShown)
            {
                return 0;
            }

            if (SettingsHelper.Settings.WindowProperties.Fullscreen)
            {
                return SettingsHelper.Settings.Gallery.IsBottomGalleryShown ? GetBottomGalleryItemHeight + SizeDefaults.ScrollbarSize : 0;
            }
            if (!SettingsHelper.Settings.Gallery.ShowBottomGalleryInHiddenUI && !IsUIShown)
            {
                return 0;
            }
            return GetBottomGalleryItemHeight + SizeDefaults.ScrollbarSize;
        }
    }

    public double GetGalleryItemWidth
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    } = double.NaN;

    public double GetGalleryItemHeight
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public double GetFullGalleryItemHeight
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public double GetBottomGalleryItemHeight
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public double MaxFullGalleryItemHeight
    {
        get => GalleryDefaults.MaxFullGalleryItemHeight;
    }
    
    public double MinFullGalleryItemHeight
    {
        get => GalleryDefaults.MinFullGalleryItemHeight;
    }
    public double MaxBottomGalleryItemHeight
    {
        get => GalleryDefaults.MaxBottomGalleryItemHeight;
    }
    
    public double MinBottomGalleryItemHeight
    {
        get => GalleryDefaults.MinBottomGalleryItemHeight;
    }

    public Thickness GalleryItemMargin
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    #region Gallery Stretch IsChecked

    public bool IsUniformBottomChecked
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public bool IsUniformFullChecked
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public bool IsUniformMenuChecked
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public bool IsUniformToFillBottomChecked
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public bool IsUniformToFillFullChecked
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public bool IsUniformToFillMenuChecked
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public bool IsFillBottomChecked
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public bool IsFillFullChecked
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public bool IsFillMenuChecked
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public bool IsNoneBottomChecked
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public bool IsNoneFullChecked
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public bool IsNoneMenuChecked
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public bool IsSquareBottomChecked
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public bool IsSquareFullChecked
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public bool IsSquareMenuChecked
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public bool IsFillSquareBottomChecked
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public bool IsFillSquareFullChecked
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public bool IsFillSquareMenuChecked
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    #endregion

    #endregion Gallery

    #region Commands

    public ReactiveCommand<Unit, Unit>? ExitCommand { get; }
    public ReactiveCommand<Unit, Unit>? MinimizeCommand { get; }
    public ReactiveCommand<Unit, Unit>? MaximizeCommand { get; }
    
    public ReactiveCommand<Unit, Unit>? RestoreCommand { get; }
    public ReactiveCommand<Unit, Unit>? ToggleFullscreenCommand { get; }
    public ReactiveCommand<Unit, Unit>? NextCommand { get; }
    public ReactiveCommand<Unit, Unit>? NextButtonCommand { get; }
    public ReactiveCommand<Unit, Unit>? NextArrowButtonCommand { get; }
    public ReactiveCommand<Unit, Unit>? PreviousCommand { get; }
    public ReactiveCommand<Unit, Unit>? PreviousButtonCommand { get; }
    public ReactiveCommand<Unit, Unit>? PreviousArrowButtonCommand { get; }
    public ReactiveCommand<Unit, Unit>? NextFolderCommand { get; }
    public ReactiveCommand<Unit, Unit>? PreviousFolderCommand { get; }
    public ReactiveCommand<Unit, Unit>? FirstCommand { get; }
    public ReactiveCommand<Unit, Unit>? LastCommand { get; }
    public ReactiveCommand<Unit, Unit>? Skip10Command { get; }
    public ReactiveCommand<Unit, Unit>? Prev10Command { get; }
    public ReactiveCommand<Unit, Unit>? Skip100Command { get; }
    public ReactiveCommand<Unit, Unit>? Prev100Command { get; }
    public ReactiveCommand<Unit, Unit>? OpenFileCommand { get; }
    public ReactiveCommand<Unit, Unit>? SaveFileCommand { get; }
    public ReactiveCommand<Unit, Unit>? SaveFileAsCommand { get; }
    public ReactiveCommand<Unit, Unit>? OpenLastFileCommand { get; }
    public ReactiveCommand<Unit, Unit>? PasteCommand { get; }
    public ReactiveCommand<string, Unit>? CopyFileCommand { get; }
    public ReactiveCommand<string, Unit>? CopyBase64Command { get; }
    public ReactiveCommand<string, Unit>? CopyFilePathCommand { get; }
    public ReactiveCommand<string, Unit>? FilePropertiesCommand { get; }
    public ReactiveCommand<Unit, Unit>? CopyImageCommand { get; }
    public ReactiveCommand<string, Unit>? CutCommand { get; }
    public ReactiveCommand<Unit, Unit>? ReloadCommand { get; }
    public ReactiveCommand<string, Unit>? PrintCommand { get; }
    public ReactiveCommand<string, Unit>? DeleteFileCommand { get; }
    public ReactiveCommand<string, Unit>? RecycleFileCommand { get; }
    public ReactiveCommand<Unit, Unit>? CloseMenuCommand { get; }
    public ReactiveCommand<Unit, Unit>? ToggleFileMenuCommand { get; }
    public ReactiveCommand<Unit, Unit>? ToggleImageMenuCommand { get; }
    public ReactiveCommand<Unit, Unit>? ToggleSettingsMenuCommand { get; }
    public ReactiveCommand<Unit, Unit>? ToggleToolsMenuCommand { get; }
    public ReactiveCommand<string, Unit>? LocateOnDiskCommand { get; }
    public ReactiveCommand<string, Unit>? OpenWithCommand { get; }
    public ReactiveCommand<Unit, Unit>? RenameCommand { get; }
    public ReactiveCommand<Unit, Unit>? NewWindowCommand { get; }
    public ReactiveCommand<string, Unit>? DuplicateFileCommand { get; }
    public ReactiveCommand<Unit, Unit>? ToggleLoopingCommand { get; }
    public ReactiveCommand<Unit, Unit>? RotateLeftCommand { get; }
    public ReactiveCommand<Unit, Unit>? RotateLeftButtonCommand { get; }
    public ReactiveCommand<Unit, Unit>? RotateRightCommand { get; }
    public ReactiveCommand<Unit, Unit>? RotateRightButtonCommand { get; }
    public ReactiveCommand<Unit, Unit>? RotateRightWindowBorderButtonCommand { get; }
    public ReactiveCommand<Unit, Unit>? FlipCommand { get; }
    public ReactiveCommand<Unit, Unit>? StretchCommand { get; }
    public ReactiveCommand<Unit, Unit>? CropCommand { get; }
    public ReactiveCommand<Unit, Unit>? ChangeAutoFitCommand { get; }
    public ReactiveCommand<Unit, Unit>? ChangeTopMostCommand { get; }
    public ReactiveCommand<Unit, Unit>? ChangeCtrlZoomCommand { get; }
    public ReactiveCommand<Unit, Unit>? ToggleUsingTouchpadCommand { get; }
    public ReactiveCommand<Unit, Unit>? ToggleUICommand { get; }
    public ReactiveCommand<Unit, Unit>? ChangeBackgroundCommand { get; }
    public ReactiveCommand<Unit, Unit>? ToggleBottomNavBarCommand { get; }
    public ReactiveCommand<Unit, Unit>? ToggleBottomGalleryShownInHiddenUICommand { get; }
    
    public ReactiveCommand<Unit, Unit>? ToggleFadeInButtonsOnHoverCommand { get; }
    public ReactiveCommand<Unit, Unit>? ToggleTaskbarProgressCommand { get; }
    public ReactiveCommand<Unit, Unit>? ShowExifWindowCommand { get; }
    public ReactiveCommand<Unit, Unit>? ShowAboutWindowCommand { get; }
    public ReactiveCommand<Unit, Unit>? ShowSettingsWindowCommand { get; }
    public ReactiveCommand<Unit, Unit>? ShowKeybindingsWindowCommand { get; }
    public ReactiveCommand<Unit, Unit>? ShowBatchResizeWindowCommand { get; }
    public ReactiveCommand<Unit, Unit>? ShowSingleImageResizeWindowCommand { get; }
    public ReactiveCommand<Unit, Unit>? ShowEffectsWindowCommand { get; }
    public ReactiveCommand<Unit, Unit>? SetExifRating0Command { get; }
    public ReactiveCommand<Unit, Unit>? SetExifRating1Command { get; }
    public ReactiveCommand<Unit, Unit>? SetExifRating2Command { get; }
    public ReactiveCommand<Unit, Unit>? SetExifRating3Command { get; }
    public ReactiveCommand<Unit, Unit>? SetExifRating4Command { get; }
    public ReactiveCommand<Unit, Unit>? SetExifRating5Command { get; }
    public ReactiveCommand<Unit, Unit>? OpenGoogleLinkCommand { get; }
    public ReactiveCommand<Unit, Unit>? OpenBingLinkCommand { get; }

    public ReactiveCommand<Unit, Unit>? OptimizeImageCommand { get; }
    public ReactiveCommand<int, Unit>? ResizeCommand { get; }
    public ReactiveCommand<int, Unit>? ConvertCommand { get; }

    public ReactiveCommand<Unit, Unit>? SortFilesByNameCommand { get; }
    public ReactiveCommand<Unit, Unit>? SortFilesBySizeCommand { get; }
    public ReactiveCommand<Unit, Unit>? SortFilesByExtensionCommand { get; }
    public ReactiveCommand<Unit, Unit>? SortFilesByCreationTimeCommand { get; }
    public ReactiveCommand<Unit, Unit>? SortFilesByLastAccessTimeCommand { get; }
    public ReactiveCommand<Unit, Unit>? SortFilesRandomlyCommand { get; }
    public ReactiveCommand<Unit, Unit>? SortFilesAscendingCommand { get; }
    public ReactiveCommand<Unit, Unit>? SortFilesDescendingCommand { get; }

    public ReactiveCommand<Unit, Unit>? ToggleGalleryCommand { get; }
    public ReactiveCommand<Unit, Unit>? ToggleBottomGalleryCommand { get; }
    public ReactiveCommand<Unit, Unit>? CloseGalleryCommand { get; }

    public ReactiveCommand<Unit, Unit>? ToggleScrollCommand { get; }

    public ReactiveCommand<Unit, Unit>? ToggleSubdirectoriesCommand { get; }

    public ReactiveCommand<Unit, Unit>? ColorPickerCommand { get; }

    public ReactiveCommand<int, Unit>? SlideshowCommand { get; }
    
    public ReactiveCommand<string, Unit>? SetAsWallpaperCommand { get; }
    public ReactiveCommand<string, Unit>? SetAsWallpaperFilledCommand { get; }
    public ReactiveCommand<string, Unit>? SetAsWallpaperStretchedCommand { get; }
    public ReactiveCommand<string, Unit>? SetAsWallpaperTiledCommand { get; }
    public ReactiveCommand<string, Unit>? SetAsWallpaperCenteredCommand { get; }
    
    public ReactiveCommand<string, Unit>? SetAsLockScreenCommand { get; }
    
    public ReactiveCommand<string, Unit>? GalleryItemStretchCommand { get; }
    
    public ReactiveCommand<Unit, Unit>? ResetSettingsCommand { get; }
    
    public ReactiveCommand<Unit, Unit>? ShowSideBySideCommand { get; }
    
    public ReactiveCommand<Unit, Unit>? RestartCommand { get; }

    #endregion Commands

    #region Fields

    #region Booleans

    public bool IsAvoidingZoomingOut
    {
        get;
        set
        {
            SettingsHelper.Settings.Zoom.AvoidZoomingOut = value;
            this.RaiseAndSetIfChanged(ref field, value);
        }
    }

    public IImage? ChangeCtrlZoomImage
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public bool IsLoading
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public bool IsUIShown
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public bool IsTopToolbarShown
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public bool IsBottomToolbarShown
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public bool IsBottomToolbarShownSetting
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    } 

    public bool IsShowingTaskbarProgress
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public bool IsFullscreen
    {
        get;
        set
        {
            this.RaiseAndSetIfChanged(ref field, value);
            ShouldRestore = IsFullscreen || IsMaximized;
        }
    }
    
    public bool IsMaximized
    {
        get;
        set
        {
            this.RaiseAndSetIfChanged(ref field, value);
            ShouldRestore = IsFullscreen || IsMaximized;
        }
    }
    
    public bool ShouldRestore
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public bool IsTopMost
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public bool IsIncludingSubdirectories
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    } 

    public bool IsScrollingEnabled
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public bool IsStretched
    {
        get;
        set
        {
            this.RaiseAndSetIfChanged(ref field, value);
            SettingsHelper.Settings.ImageScaling.StretchImage = value;
            WindowResizing.SetSize(this);
        }
    } 

    public bool IsLooping
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public bool IsAutoFit
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public bool IsStayingCentered
    {
        get;
        set
        {
            this.RaiseAndSetIfChanged(ref field, value);
            SettingsHelper.Settings.WindowProperties.KeepCentered = value;
        }
    }

    public bool IsOpeningInSameWindow
    {
        get;
        set
        {
            this.RaiseAndSetIfChanged(ref field, value);
            SettingsHelper.Settings.UIProperties.OpenInSameWindow = value;
        }
    }
    
    public bool IsShowingConfirmationOnEsc
    {
        get;
        set
        {
            this.RaiseAndSetIfChanged(ref field, value);
            SettingsHelper.Settings.UIProperties.ShowConfirmationOnEsc = value;
        }
    }

    public bool IsEditableTitlebarOpen
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public bool IsUsingTouchpad
    {
        get;
        set
        {
            this.RaiseAndSetIfChanged(ref field, value);
            SettingsHelper.Settings.Zoom.IsUsingTouchPad = value;
        }
    }

    #endregion Booleans
    
    public Thickness TopScreenMargin
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public Thickness BottomScreenMargin
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }
    
    public CornerRadius BottomCornerRadius
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }
    
    public int BackgroundChoice
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public double WindowMinSize
    {
        get
        {
            return SizeDefaults.WindowMinSize;
        }
    }

    public double TitlebarHeight
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    } 

    public double BottombarHeight
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
        
    }

    // Used to flip the flip button
    public int ScaleX
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    } = 1;

    public UserControl? CurrentView
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public ImageViewer? ImageViewer;

    public uint EXIFRating
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public int GetIndex
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public double GetSlideshowSpeed
    {
        get;
        set
        {
            var roundedValue = Math.Round(value, 2);
            this.RaiseAndSetIfChanged(ref field, roundedValue);
            SettingsHelper.Settings.UIProperties.SlideShowTimer = roundedValue;
        }
    } 

    public double GetNavSpeed
    {
        get => Math.Round(field, 2);
        set 
        {
            this.RaiseAndSetIfChanged(ref field, value);
            SettingsHelper.Settings.UIProperties.NavSpeed = value;
        }
    }

    public double GetZoomSpeed
    {
        get;
        set
        {
            var roundedValue = Math.Round(value, 2);
            this.RaiseAndSetIfChanged(ref field, roundedValue);
            SettingsHelper.Settings.Zoom.ZoomSpeed = roundedValue;
        }
    } 

    #region strings

    public string? GetIsShowingUITranslation
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string? GetIsShowingBottomToolbarTranslation
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string? GetIsShowingFadingUIButtonsTranslation
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string? GetIsUsingTouchpadTranslation
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string? GetIsFlippedTranslation
    {
        get => ScaleX == -1 ? UnFlip : Flip;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string? GetIsShowingBottomGalleryTranslation
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string? GetIsLoopingTranslation
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string? GetIsScrollingTranslation
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string? GetIsCtrlZoomTranslation
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string? GetPrintSizeInch
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string? GetPrintSizeCm
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string? GetSizeMp
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string? GetResolution
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string? GetBitDepth
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string? GetAspectRatio
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string? GetLatitude
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string? GetLongitude
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string? GetAltitude
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string? GoogleLink
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string? BingLink
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string? GetAuthors
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string? GetDateTaken
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string? GetCopyright
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string? GetTitle
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string? GetSubject
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string? GetSoftware
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string? GetResolutionUnit
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string? GetColorRepresentation
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string? GetCompression
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string? GetCompressedBitsPixel
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string? GetCameraMaker
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string? GetCameraModel
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string? GetExposureProgram
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string? GetExposureTime
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string? GetExposureBias
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string? GetFNumber
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string? GetMaxAperture
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string? GetDigitalZoom
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string? GetFocalLength35Mm
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string? GetFocalLength
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string? GetISOSpeed
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string? GetMeteringMode
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string? GetContrast
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string? GetSaturation
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string? GetSharpness
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string? GetWhiteBalance
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string? GetFlashMode
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string? GetFlashEnergy
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string? GetLightSource
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string? GetBrightness
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string? GetPhotometricInterpretation
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string? GetOrientation
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string? GetExifVersion
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string? GetLensModel
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string? GetLensMaker
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    #endregion strings

    #region Window Properties

    public string? Title
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    } = "Loading...";

    public string? TitleTooltip
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    } = "Loading...";

    public string? WindowTitle
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    } = "PicView";

    public SizeToContent SizeToContent
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public bool CanResize
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    #endregion Window Properties

    #region Size

    public double TitleMaxWidth
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    #endregion Size

    #region Zoom

    public double RotationAngle
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public double ZoomValue
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public ScrollBarVisibility ToggleScrollBarVisibility
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    #endregion Zoom

    #region Menus

    public bool IsFileMenuVisible
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public bool IsImageMenuVisible
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public bool IsSettingsMenuVisible
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public bool IsToolsMenuVisible
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    #endregion Menus

    #endregion Fields

    #region Services

    public ImageIterator? ImageIterator;

    #endregion Services

    #region Methods

    #region Sorting Order

    public FileListHelper.SortFilesBy SortOrder
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public bool IsAscending
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    #endregion Sorting Order

    private async Task ResizeImageByPercentage(int percentage)
    {
        SetTitleHelper.SetLoadingTitle(this);
        var success = await ConversionHelper.ResizeImageByPercentage(FileInfo, percentage);
        if (success)
        {
            if (ImageIterator is not null)
            {
                await ImageIterator.QuickReload().ConfigureAwait(false);
            }
        }
        else
        {
            SetTitleHelper.SetTitle(this);
        }
    }

    private async Task ConvertFileExtension(int index)
    {
        if (FileInfo is null)
        {
            return;
        }

        var newPath = await ConversionHelper.ConvertTask(FileInfo, index);
        if (!string.IsNullOrWhiteSpace(newPath))
        {
            await NavigationManager.LoadPicFromStringAsync(newPath, this);
        }
    }
    
    private async Task CopyFileTask(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return;
        }
        if (PlatformService is null)
        {
            return;
        }
        await ClipboardHelper.CopyFileToClipboard(path, this);
    }
    
    private async Task CopyFilePathTask(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return;
        }
        if (PlatformService is null)
        {
            return;
        }
        await ClipboardHelper.CopyTextToClipboard(path);
    }
    
    private async Task CopyBase64Task(string path)
    {
        if (PlatformService is null)
        {
            return;
        }
        await ClipboardHelper.CopyBase64ToClipboard(path, this);
    }
    
    private async Task CutFileTask(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return;
        }
        if (PlatformService is null)
        {
            return;
        }
        await ClipboardHelper.CutFile(path, this);
    }
    
    private async Task DeleteFileTask(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return;
        }
        await Task.Run(() =>
        {
            try
            {
                var index = ImageIterator?.ImagePaths.IndexOf(path);
                if (!index.HasValue)
                {
                    return;
                }

                var errorMsg = FileDeletionHelper.DeleteFileWithErrorMsg(path, recycle: false);
                if (!string.IsNullOrWhiteSpace(errorMsg))
                {
                    _ = TooltipHelper.ShowTooltipMessageAsync(errorMsg);
                }
                else
                {
                    ImageIterator?.RemoveItemFromPreLoader(index.Value);
                }
            }
            catch (Exception e)
            {
                _ = TooltipHelper.ShowTooltipMessageAsync(e);
#if DEBUG
                Console.WriteLine(e);
#endif
            }
        });
        
    }
    
    private async Task RecycleFileTask(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return;
        }
        await Task.Run(() =>
        {
            FileDeletionHelper.DeleteFileWithErrorMsg(path, recycle: true);
        });
    }
    
    private async Task DuplicateFileTask(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return;
        }

        IsLoading = true;
        if (path == FileInfo.FullName)
        {
            await FunctionsHelper.DuplicateFile();
        }
        else
        {
            var duplicatedPath = await FileHelper.DuplicateAndReturnFileNameAsync(path);
            if (!string.IsNullOrWhiteSpace(duplicatedPath))
            {
                await ClipboardHelper.CopyAnimation();
            }
        }
        IsLoading = false;
    }
    
    private async Task ShowFilePropertiesTask(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return;
        }
        if (PlatformService is null)
        {
            return;
        }
        await Task.Run(() =>
        {
            PlatformService.ShowFileProperties(path);
        });
    }

    private async Task PrintTask(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return;
        }
        if (PlatformService is null)
        {
            return;
        }
        await Task.Run(() =>
        {
            PlatformService?.Print(path);
        });
    }
    
    private async Task OpenWithTask(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return;
        }
        if (PlatformService is null)
        {
            return;
        }
        await Task.Run(() =>
        {
            PlatformService?.OpenWith(path);
        });
    }
    
    private async Task LocateOnDiskTask(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return;
        }
        if (PlatformService is null)
        {
            return;
        }
        await Task.Run(() =>
        {
            PlatformService?.LocateOnDisk(path);
        });
    }
    
    public async Task SetAsWallpaperTask(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return;
        }
        await SetAsWallpaperTask(path, WallpaperStyle.Fit).ConfigureAwait(false);
    }
    
    public async Task SetAsWallpaperFilledTask(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return;
        }
        await SetAsWallpaperTask(path, WallpaperStyle.Fill).ConfigureAwait(false);
    }
    
    public async Task SetAsWallpaperTiledTask(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return;
        }
        await SetAsWallpaperTask(path, WallpaperStyle.Tile).ConfigureAwait(false);
    }
    
    public async Task SetAsWallpaperStretchedTask(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return;
        }
        await SetAsWallpaperTask(path, WallpaperStyle.Stretch).ConfigureAwait(false);
    }
    
    public async Task SetAsWallpaperCenteredTask(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return;
        }
        await SetAsWallpaperTask(path, WallpaperStyle.Center).ConfigureAwait(false);
    }
    
    public async Task SetAsWallpaperTask(string path, WallpaperStyle style)
    {

        if (PlatformService is null)
        {
            return;
        }
        
        IsLoading = true;
        try
        {
            var file = await ImageHelper.ConvertToCommonSupportedFormatAsync(path, this).ConfigureAwait(false);

            PlatformService?.SetAsWallpaper(file, WallpaperManager.GetWallpaperStyle(style));
        }
        catch (Exception e)
        {
            await TooltipHelper.ShowTooltipMessageAsync(e.Message, true);
#if DEBUG
            Console.WriteLine(e);   
#endif
        }
        finally
        {
            IsLoading = false;
        }
    }
    
    private async Task SetAsLockScreenTask(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return;
        }
        if (PlatformService is null)
        {
            return;
        }
        
        IsLoading = true;

        try
        {
            var file = await ImageHelper.ConvertToCommonSupportedFormatAsync(path, this).ConfigureAwait(false);

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    Verb = "runas",
                    UseShellExecute = true,
                    FileName = "PicView.exe",
                    Arguments = "lockscreen," + file,
                    WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory
                }
            };
            process.Start();
            await TooltipHelper.ShowTooltipMessageAsync(TranslationHelper.Translation.Applying, true);
            await process.WaitForExitAsync();
        }
        catch (Exception e)
        {
            await TooltipHelper.ShowTooltipMessageAsync(e.Message, true);
#if DEBUG
         Console.WriteLine(e);   
#endif
        }
        finally
        {
            IsLoading = false;
        }
    }

    public void SetGalleryItemStretch(string value)
    {
        if (value.Equals("Square", StringComparison.OrdinalIgnoreCase))
        {
            if (GalleryFunctions.IsFullGalleryOpen)
            {
                GalleryStretchMode.ChangeFullGalleryStretchSquare(this);
            }
            else
            {
                GalleryStretchMode.ChangeBottomGalleryStretchSquare(this);
            }
            return;
        }
        
        if (value.Equals("FillSquare", StringComparison.OrdinalIgnoreCase))
        {
            if (GalleryFunctions.IsFullGalleryOpen)
            {
                GalleryStretchMode.ChangeFullGalleryStretchSquareFill(this);
            }
            else
            {
                GalleryStretchMode.ChangeBottomGalleryStretchSquareFill(this);
            }
            return;
        }

        if (Enum.TryParse<Stretch>(value, out var stretch))
        {
            if (GalleryFunctions.IsFullGalleryOpen)
            {
                GalleryStretchMode.ChangeFullGalleryItemStretch(this, stretch);
            }
            else
            {
                GalleryStretchMode.ChangeBottomGalleryItemStretch(this, stretch);
            }
        }
    }

    public async Task StartSlideShowTask(int milliseconds)
    {
        if (milliseconds <= 0)
        {
            await Avalonia.Navigation.Slideshow.StartSlideshow(this);
        }
        else
        {
            await Avalonia.Navigation.Slideshow.StartSlideshow(this, milliseconds);
        }
    }

    #endregion Methods

    public MainViewModel(IPlatformSpecificService? platformSpecificService)
    {
        FunctionsHelper.Vm = this;
        PlatformService = platformSpecificService;

        #region Window commands

        ExitCommand = ReactiveCommand.CreateFromTask(WindowFunctions.Close);
        MinimizeCommand = ReactiveCommand.CreateFromTask(WindowFunctions.Minimize);
        MaximizeCommand = ReactiveCommand.CreateFromTask(WindowFunctions.MaximizeRestore);
        RestoreCommand = ReactiveCommand.Create(WindowFunctions.Restore);
        ToggleFullscreenCommand = ReactiveCommand.CreateFromTask(FunctionsHelper.ToggleFullscreen);
        NewWindowCommand = ReactiveCommand.Create(ProcessHelper.StartNewProcess);

        ShowExifWindowCommand = ReactiveCommand.Create(platformSpecificService.ShowExifWindow);
        ShowSettingsWindowCommand = ReactiveCommand.Create(platformSpecificService.ShowSettingsWindow);
        ShowKeybindingsWindowCommand = ReactiveCommand.Create(platformSpecificService.ShowKeybindingsWindow);
        ShowAboutWindowCommand = ReactiveCommand.Create(platformSpecificService.ShowAboutWindow);
        ShowBatchResizeWindowCommand = ReactiveCommand.Create(platformSpecificService.ShowBatchResizeWindow);
        ShowSingleImageResizeWindowCommand = ReactiveCommand.Create(platformSpecificService.ShowSingleImageResizeWindow);
        ShowEffectsWindowCommand = ReactiveCommand.Create(platformSpecificService.ShowEffectsWindow);
        #endregion Window commands

        #region Navigation Commands

        NextCommand = ReactiveCommand.Create(() =>
        {
            Task.Run(FunctionsHelper.Next);
        });
        
        NextButtonCommand = ReactiveCommand.Create(() =>
        {
            var button = UIHelper.GetBottomBar?.NextButton;
            if (button != null)
            {
                button.Interval =
                    (int)TimeSpan.FromSeconds(SettingsHelper.Settings.UIProperties.NavSpeed).TotalMilliseconds;
            }

            Task.Run(() =>
               NavigationManager.NavigateAndPositionCursor(next: true, arrow: false, vm: this)
            );
        });
        
        NextArrowButtonCommand = ReactiveCommand.Create( () =>
        {
            var button = UIHelper.GetMainView?.ClickArrowRight?.PolyButton;
            if (button != null)
            {
                button.Interval =
                    (int)TimeSpan.FromSeconds(SettingsHelper.Settings.UIProperties.NavSpeed).TotalMilliseconds;
            }

            Task.Run(() =>
                NavigationManager.NavigateAndPositionCursor(next:true, arrow: true, vm: this)
            );
        });
        
        NextFolderCommand = ReactiveCommand.Create(() =>
        {
            Task.Run(FunctionsHelper.NextFolder);
        });
        
        PreviousCommand = ReactiveCommand.Create(() =>
        {
            Task.Run(FunctionsHelper.Prev);
        });
        
        PreviousButtonCommand = ReactiveCommand.Create( () =>
        {
            var button = UIHelper.GetBottomBar?.PreviousButton;
            if (button != null)
            {
                button.Interval =
                    (int)TimeSpan.FromSeconds(SettingsHelper.Settings.UIProperties.NavSpeed).TotalMilliseconds;
            }

            Task.Run(() =>
                NavigationManager.NavigateAndPositionCursor(next:false, arrow: false, vm: this)
            );
        });
        
        PreviousArrowButtonCommand = ReactiveCommand.Create( () =>
        {
            var button = UIHelper.GetMainView?.ClickArrowLeft?.PolyButton;
            if (button != null)
            {
                button.Interval =
                    (int)TimeSpan.FromSeconds(SettingsHelper.Settings.UIProperties.NavSpeed).TotalMilliseconds;
            }

            Task.Run(() =>
                NavigationManager.NavigateAndPositionCursor(next:false, arrow: true, vm: this)
            );
        });
        
        PreviousFolderCommand = ReactiveCommand.Create(() =>
        {
            Task.Run(FunctionsHelper.PrevFolder);
        });
        
        Skip10Command = ReactiveCommand.CreateFromTask(FunctionsHelper.Next10);

        Skip100Command = ReactiveCommand.CreateFromTask(FunctionsHelper.Next100);
        
        Prev10Command = ReactiveCommand.CreateFromTask(FunctionsHelper.Prev10);

        Prev100Command = ReactiveCommand.CreateFromTask(FunctionsHelper.Prev100);

        FirstCommand = ReactiveCommand.CreateFromTask(FunctionsHelper.First);

        LastCommand = ReactiveCommand.CreateFromTask(FunctionsHelper.Last);

        ReloadCommand = ReactiveCommand.CreateFromTask(FunctionsHelper.Reload);

        #endregion Navigation Commands

        #region Sort Commands

        SortFilesByNameCommand = ReactiveCommand.CreateFromTask(FunctionsHelper.SortFilesByName);

        SortFilesByCreationTimeCommand = ReactiveCommand.CreateFromTask(FunctionsHelper.SortFilesByCreationTime);

        SortFilesByLastAccessTimeCommand = ReactiveCommand.CreateFromTask(FunctionsHelper.SortFilesByLastAccessTime);

        SortFilesBySizeCommand = ReactiveCommand.CreateFromTask(FunctionsHelper.SortFilesBySize);

        SortFilesByExtensionCommand = ReactiveCommand.CreateFromTask(FunctionsHelper.SortFilesByExtension);

        SortFilesRandomlyCommand = ReactiveCommand.CreateFromTask(FunctionsHelper.SortFilesRandomly);

        SortFilesAscendingCommand = ReactiveCommand.CreateFromTask(FunctionsHelper.SortFilesAscending);

        SortFilesDescendingCommand = ReactiveCommand.CreateFromTask(FunctionsHelper.SortFilesDescending);

        #endregion Sort Commands

        #region Menus

        CloseMenuCommand = ReactiveCommand.CreateFromTask(FunctionsHelper.CloseMenus);

        ToggleFileMenuCommand = ReactiveCommand.CreateFromTask(FunctionsHelper.ToggleFileMenu);

        ToggleImageMenuCommand = ReactiveCommand.CreateFromTask(FunctionsHelper.ToggleImageMenu);

        ToggleSettingsMenuCommand = ReactiveCommand.CreateFromTask(FunctionsHelper.ToggleSettingsMenu);

        ToggleToolsMenuCommand = ReactiveCommand.CreateFromTask(FunctionsHelper.ToggleToolsMenu);

        #endregion Menus

        #region Image commands

        RotateLeftCommand = ReactiveCommand.CreateFromTask(FunctionsHelper.RotateLeft);
        RotateLeftButtonCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            await Rotation.RotateLeft(this, Rotation.RotationButton.RotateLeftButton);
        });

        RotateRightCommand = ReactiveCommand.CreateFromTask(FunctionsHelper.RotateRight);
        RotateRightButtonCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            await Rotation.RotateRight(this, Rotation.RotationButton.RotateRightButton);
        });
        
        RotateRightWindowBorderButtonCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            await Rotation.RotateRight(this, Rotation.RotationButton.WindowBorderButton);
        });

        FlipCommand = ReactiveCommand.CreateFromTask(FunctionsHelper.Flip);

        StretchCommand = ReactiveCommand.CreateFromTask(FunctionsHelper.Stretch);

        CropCommand = ReactiveCommand.CreateFromTask(FunctionsHelper.Crop);

        ToggleScrollCommand = ReactiveCommand.CreateFromTask(FunctionsHelper.ToggleScroll);

        OptimizeImageCommand = ReactiveCommand.CreateFromTask(FunctionsHelper.OptimizeImage);
        
        ChangeBackgroundCommand = ReactiveCommand.CreateFromTask(FunctionsHelper.ChangeBackground);
        
        ShowSideBySideCommand = ReactiveCommand.CreateFromTask(FunctionsHelper.SideBySide);

        #endregion Image commands

        #region File commands
        
        OpenFileCommand = ReactiveCommand.Create( () =>
        {
            Task.Run(FunctionsHelper.Open);
        });
        
        OpenLastFileCommand = ReactiveCommand.Create( () =>
        {
            Task.Run(FunctionsHelper.OpenLastFile);
        });

        SaveFileCommand = ReactiveCommand.CreateFromTask(FunctionsHelper.Save);

        SaveFileAsCommand = ReactiveCommand.CreateFromTask(FunctionsHelper.SaveAs);

        CopyFileCommand = ReactiveCommand.CreateFromTask<string>(CopyFileTask);

        CopyFilePathCommand = ReactiveCommand.CreateFromTask<string>(CopyFilePathTask);
        
        FilePropertiesCommand = ReactiveCommand.CreateFromTask<string>(ShowFilePropertiesTask);

        CopyImageCommand = ReactiveCommand.CreateFromTask(FunctionsHelper.CopyImage);
        
        CopyBase64Command = ReactiveCommand.CreateFromTask<string>(CopyBase64Task);

        CutCommand = ReactiveCommand.CreateFromTask<string>(CutFileTask);
        
        PasteCommand = ReactiveCommand.Create( () =>
        {
            Task.Run(FunctionsHelper.Paste);
        });

        OpenWithCommand = ReactiveCommand.CreateFromTask<string>(OpenWithTask);

        RenameCommand = ReactiveCommand.CreateFromTask(FunctionsHelper.Rename);

        ResizeCommand = ReactiveCommand.CreateFromTask<int>(ResizeImageByPercentage);
        ConvertCommand = ReactiveCommand.CreateFromTask<int>(ConvertFileExtension);

        DuplicateFileCommand = ReactiveCommand.CreateFromTask<string>(DuplicateFileTask);

        PrintCommand = ReactiveCommand.CreateFromTask<string>(PrintTask);    

        DeleteFileCommand = ReactiveCommand.CreateFromTask<string>(DeleteFileTask);

        RecycleFileCommand = ReactiveCommand.CreateFromTask<string>(RecycleFileTask);

        LocateOnDiskCommand = ReactiveCommand.CreateFromTask<string>(LocateOnDiskTask);
        
        SetAsWallpaperCommand = ReactiveCommand.CreateFromTask<string>(SetAsWallpaperTask);
        SetAsWallpaperTiledCommand = ReactiveCommand.CreateFromTask<string>(SetAsWallpaperTiledTask);
        SetAsWallpaperStretchedCommand = ReactiveCommand.CreateFromTask<string>(SetAsWallpaperStretchedTask);
        SetAsWallpaperCenteredCommand = ReactiveCommand.CreateFromTask<string>(SetAsWallpaperCenteredTask);
        SetAsWallpaperFilledCommand = ReactiveCommand.CreateFromTask<string>(SetAsWallpaperFilledTask);
        
        SetAsLockScreenCommand = ReactiveCommand.CreateFromTask<string>(SetAsLockScreenTask);

        #endregion File commands

        #region EXIF commands

        SetExifRating0Command = ReactiveCommand.CreateFromTask(FunctionsHelper.Set0Star);
        SetExifRating1Command = ReactiveCommand.CreateFromTask(FunctionsHelper.Set1Star);
        SetExifRating2Command = ReactiveCommand.CreateFromTask(FunctionsHelper.Set2Star);
        SetExifRating3Command = ReactiveCommand.CreateFromTask(FunctionsHelper.Set3Star);
        SetExifRating4Command = ReactiveCommand.CreateFromTask(FunctionsHelper.Set4Star);
        SetExifRating5Command = ReactiveCommand.CreateFromTask(FunctionsHelper.Set5Star);

        OpenGoogleLinkCommand = ReactiveCommand.CreateFromTask(FunctionsHelper.OpenGoogleMaps);
        OpenBingLinkCommand = ReactiveCommand.CreateFromTask(FunctionsHelper.OpenBingMaps);

        #endregion EXIF commands

        #region Gallery Commands

        ToggleGalleryCommand = ReactiveCommand.CreateFromTask(FunctionsHelper.ToggleGallery);

        ToggleBottomGalleryCommand = ReactiveCommand.CreateFromTask(FunctionsHelper.OpenCloseBottomGallery);
        
        CloseGalleryCommand = ReactiveCommand.CreateFromTask(FunctionsHelper.CloseGallery);
        
        GalleryItemStretchCommand = ReactiveCommand.Create<string>(SetGalleryItemStretch);

        #endregion Gallery Commands

        #region UI Commands

        ToggleUICommand = ReactiveCommand.CreateFromTask(FunctionsHelper.ToggleInterface);

        ToggleBottomNavBarCommand = ReactiveCommand.CreateFromTask(FunctionsHelper.ToggleBottomToolbar);
        
        ToggleBottomGalleryShownInHiddenUICommand = ReactiveCommand.CreateFromTask(async() =>
        {
            await HideInterfaceLogic.ToggleBottomGalleryShownInHiddenUI(this);
        });
        
        ToggleFadeInButtonsOnHoverCommand = ReactiveCommand.CreateFromTask(async() =>
        {
            await HideInterfaceLogic.ToggleFadeInButtonsOnHover(this);
        });

        ChangeCtrlZoomCommand = ReactiveCommand.CreateFromTask(FunctionsHelper.ChangeCtrlZoom);
        
        ColorPickerCommand = ReactiveCommand.CreateFromTask(FunctionsHelper.ColorPicker);
        
        SlideshowCommand = ReactiveCommand.CreateFromTask<int>(StartSlideShowTask);
        
        ToggleTaskbarProgressCommand = ReactiveCommand.CreateFromTask(FunctionsHelper.ToggleTaskbarProgress);

        #endregion UI Commands

        #region Settings commands

        ChangeAutoFitCommand = ReactiveCommand.CreateFromTask(FunctionsHelper.AutoFitWindow);

        ChangeTopMostCommand = ReactiveCommand.CreateFromTask(FunctionsHelper.SetTopMost);

        ToggleSubdirectoriesCommand = ReactiveCommand.CreateFromTask(FunctionsHelper.ToggleSubdirectories);

        ToggleLoopingCommand = ReactiveCommand.CreateFromTask(FunctionsHelper.ToggleLooping);
        
        ResetSettingsCommand = ReactiveCommand.CreateFromTask(FunctionsHelper.ResetSettings);
        
        RestartCommand = ReactiveCommand.CreateFromTask(FunctionsHelper.Restart);
        
        ToggleUsingTouchpadCommand = ReactiveCommand.CreateFromTask(FunctionsHelper.ToggleUsingTouchpad);

        #endregion Settings commands
    }

    public MainViewModel()
    {
        // Only use for unit test
    }
}
