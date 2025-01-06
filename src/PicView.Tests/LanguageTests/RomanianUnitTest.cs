﻿using PicView.Core.Localization;

namespace PicView.Tests.LanguageTests;

public static class RomanianUnitTest
{
#pragma warning disable xUnit2000
    [Fact]
    public static async Task CheckRomanianLanguage()
    {
        var exists = await TranslationHelper.LoadLanguage("ro");
        Assert.True(exists);

        Assert.Equal(TranslationHelper.Translation.About, "Despre");
        Assert.Equal(TranslationHelper.Translation.ActionProgram, "Program de acțiune");
        Assert.Equal(TranslationHelper.Translation.AddedToClipboard, "adăugat în memoria temporară");
        Assert.Equal(TranslationHelper.Translation.AdditionalFunctions, "Funcții suplimentare");
        Assert.Equal(TranslationHelper.Translation.AdjustNavSpeed,
            "Ajustați viteza atunci când tasta este ținută apăsată");
        Assert.Equal(TranslationHelper.Translation.AdjustTimingForSlideshow, "Ajustare viteză de transfocare");
        Assert.Equal(TranslationHelper.Translation.AdjustTimingForZoom, "Adjust zooming speed");
        Assert.Equal(TranslationHelper.Translation.AdjustZoomLevel, "Ajustare nivel de transfocare");
        Assert.Equal(TranslationHelper.Translation.AdvanceBy100Images, "Avansează cu 100 imagini");
        Assert.Equal(TranslationHelper.Translation.AdvanceBy10Images, "Avansează cu 10 imagini");
        Assert.Equal(TranslationHelper.Translation.AllowZoomOut,
            "Evitați micșorarea imaginii când aceasta este deja la dimensiunea maximă");
        Assert.Equal(TranslationHelper.Translation.Alt, "Alt");
        Assert.Equal(TranslationHelper.Translation.Altitude, "Altitudine");
        Assert.Equal(TranslationHelper.Translation.AperturePriority, "Prioritate diafragmă");
        Assert.Equal(TranslationHelper.Translation.Appearance, "Aspect");
        Assert.Equal(TranslationHelper.Translation.ApplicationShortcuts, "Comenzi rapide ale aplicației");
        Assert.Equal(TranslationHelper.Translation.ApplicationStartup, "Pornirea aplicației");
        Assert.Equal(TranslationHelper.Translation.Apply, "Aplicare");
        Assert.Equal(TranslationHelper.Translation.Applying, "Se aplică");
        Assert.Equal(TranslationHelper.Translation.Ascending, "Crescător");
        Assert.Equal(TranslationHelper.Translation.AspectRatio, "Raport aspect");
        Assert.Equal(TranslationHelper.Translation.Authors, "Autori");
        Assert.Equal(TranslationHelper.Translation.Auto, "Automat");
        Assert.Equal(TranslationHelper.Translation.AutoFitWindow, "Potrivire automată fereastră");
        Assert.Equal(TranslationHelper.Translation.BadArchive, "Arhiva nu a putut fi procesată");
        Assert.Equal(TranslationHelper.Translation.BandedSwirl, "Vârtej cu benzi");
        Assert.Equal(TranslationHelper.Translation.Bands, "Benzi");
        Assert.Equal(TranslationHelper.Translation.Base64Image, "Imagine Base64");
        Assert.Equal(TranslationHelper.Translation.BatchResize, "Redimensionare lot");
        Assert.Equal(TranslationHelper.Translation.BitDepth, "Adâncimea biților");
        Assert.Equal(TranslationHelper.Translation.BlackAndWhite, "Negru și alb");
        Assert.Equal(TranslationHelper.Translation.Bloom, "Strălucire");
        Assert.Equal(TranslationHelper.Translation.Blur, "Estompare");
        Assert.Equal(TranslationHelper.Translation.BottomGalleryItemSize, "Elemente galerie inferioară");
        Assert.Equal(TranslationHelper.Translation.BottomGalleryThumbnailStretch,
            "Întinderea miniaturilor galeriei de jos");
        Assert.Equal(TranslationHelper.Translation.Brightness, "Luminozitate");
        Assert.Equal(TranslationHelper.Translation.CameraMaker, "Producătorul camerei");
        Assert.Equal(TranslationHelper.Translation.CameraModel, "Modelul camerei");
        Assert.Equal(TranslationHelper.Translation.Cancel, "Anulare");
        Assert.Equal(TranslationHelper.Translation.Center, "Centrare");
        Assert.Equal(TranslationHelper.Translation.CenterWindow, "Centrare fereastră");
        Assert.Equal(TranslationHelper.Translation.Centimeters, "centimetri");
        Assert.Equal(TranslationHelper.Translation.ChangeBackground, "Modificare fundal");
        Assert.Equal(TranslationHelper.Translation.ChangeBackgroundTooltip,
            "Schimbarea culorii de fundal pentru imaginile cu fundal transparent");
        Assert.Equal(TranslationHelper.Translation.ChangeKeybindingText,
            "Faceți clic pe o casetă de text pentru a schimba combinația de taste. Apăsarea tastei Esc anulează combinația de taste.");
        Assert.Equal(TranslationHelper.Translation.ChangeKeybindingTooltip, "Click pentru a schimba atribuirea tastei");
        Assert.Equal(TranslationHelper.Translation.ChangingThemeRequiresRestart,
            "* Modificarea temei necesită repornire");
        Assert.Equal(TranslationHelper.Translation.CheckForUpdates, "Caută actualizări");
        Assert.Equal(TranslationHelper.Translation.ClipboardImage, "Imagine din memoria temporară");
        Assert.Equal(TranslationHelper.Translation.Close, "Închidere");
        Assert.Equal(TranslationHelper.Translation.CloseApp, "Închide întreaga aplicație");
        Assert.Equal(TranslationHelper.Translation.CloseGallery, "Închide galeria");
        Assert.Equal(TranslationHelper.Translation.CloseWindowPrompt, "Doriți să închideți fereastra?");
        Assert.Equal(TranslationHelper.Translation.CloudyWeather, "Vreme înnorată");
        Assert.Equal(TranslationHelper.Translation.ColorPickerTool, "Selector de culoare");
        Assert.Equal(TranslationHelper.Translation.ColorPickerToolTooltip, "Alegere culoare din imagine");
        Assert.Equal(TranslationHelper.Translation.ColorRepresentation, "Reprezentare color");
        Assert.Equal(TranslationHelper.Translation.ColorTone, "Ton de culoare");
        Assert.Equal(TranslationHelper.Translation.CompressedBitsPixel, "Biți compresați pe pixel");
        Assert.Equal(TranslationHelper.Translation.Compression, "Compresie");
        Assert.Equal(TranslationHelper.Translation.Contrast, "Contrast");
        Assert.Equal(TranslationHelper.Translation.ConvertTo, "Conversie în");
        Assert.Equal(TranslationHelper.Translation.ConvertedToBase64, "CConvertit în base64");
        Assert.Equal(TranslationHelper.Translation.CoolWhiteFluorescent, "Fluorescent alb rece");
        Assert.Equal(TranslationHelper.Translation.CopiedImage, "Imagine copiată în memoria temporară");
        Assert.Equal(TranslationHelper.Translation.Copy, "Copiere");
        Assert.Equal(TranslationHelper.Translation.CopyFile, "Copiere fișier");
        Assert.Equal(TranslationHelper.Translation.CopyImage, "Copiere imagine");
        Assert.Equal(TranslationHelper.Translation.CopyImageTooltip, "Copiere ca imagine din memoria temporară");
        Assert.Equal(TranslationHelper.Translation.Copyright, "Drepturi de autor");
        Assert.Equal(TranslationHelper.Translation.Created, "Creată");
        Assert.Equal(TranslationHelper.Translation.CreationTime, "Data creării");
        Assert.Equal(TranslationHelper.Translation.CreativeProgram, "Program creativ");
        Assert.Equal(TranslationHelper.Translation.Credits, "Contribuții");
        Assert.Equal(TranslationHelper.Translation.Crop, "Decupare");
        Assert.Equal(TranslationHelper.Translation.CropMessage, "Apasă Esc pentru a închide, Enter pentru a salva");
        Assert.Equal(TranslationHelper.Translation.CropPicture, "Decupare imagine");
        Assert.Equal(TranslationHelper.Translation.Ctrl, "Ctrl");
        Assert.Equal(TranslationHelper.Translation.CtrlToZoom, "Ctrl pentru a transfoca, defilare pentru a naviga");
        Assert.Equal(TranslationHelper.Translation.Cut, "Decupează");
        Assert.Equal(TranslationHelper.Translation.DarkTheme, "Temă întunecată");
        Assert.Equal(TranslationHelper.Translation.Date, "Dată");
        Assert.Equal(TranslationHelper.Translation.DateTaken, "Dată capturată");
        Assert.Equal(TranslationHelper.Translation.DayWhiteFluorescent, "Fluorescent alb de zi");
        Assert.Equal(TranslationHelper.Translation.Daylight, "Lumină de zi");
        Assert.Equal(TranslationHelper.Translation.DaylightFluorescent, "Fluorescent de zi");
        Assert.Equal(TranslationHelper.Translation.Del, "Del");
        Assert.Equal(TranslationHelper.Translation.DeleteFile, "Ștergere fișier");
        Assert.Equal(TranslationHelper.Translation.DeleteFilePermanently, "Ești sigur că vrei să ștergi definitiv");
        Assert.Equal(TranslationHelper.Translation.DeletedFile, "Fișier șters");
        Assert.Equal(TranslationHelper.Translation.Descending, "Descrescător");
        Assert.Equal(TranslationHelper.Translation.DigitalZoom, "Transfocare digitală");
        Assert.Equal(TranslationHelper.Translation.DirectionalBlur, "Estompare direcțională");
        Assert.Equal(TranslationHelper.Translation.DisableFadeInButtonsOnHover,
            "Dezactivează butoanele fade-in la trecerea mouse-ului");
        Assert.Equal(TranslationHelper.Translation.DiskSize, "Mărime disc");
        Assert.Equal(TranslationHelper.Translation.DoubleClick, "Dublu clic");
        Assert.Equal(TranslationHelper.Translation.Down, "Jos");
        Assert.Equal(TranslationHelper.Translation.Dpi, "DPI");
        Assert.Equal(TranslationHelper.Translation.DragFileTo,
            "Glisează fișierul către Windows Explorer sau altă aplicație/navigator");
        Assert.Equal(TranslationHelper.Translation.DragImage, "Glisare imagine");
        Assert.Equal(TranslationHelper.Translation.DropToLoad, "Fixează pentru a încărca imaginea");
        Assert.Equal(TranslationHelper.Translation.DuplicateFile, "Duplică fișierul");
        Assert.Equal(TranslationHelper.Translation.Effects, "Efecte");
        Assert.Equal(TranslationHelper.Translation.EffectsTooltip, "Arată fereastra cu efectele imaginii");
        Assert.Equal(TranslationHelper.Translation.Embossed, "În relief");
        Assert.Equal(TranslationHelper.Translation.Enter, "Enter");
        Assert.Equal(TranslationHelper.Translation.Esc, "Esc");
        Assert.Equal(TranslationHelper.Translation.EscCloseTooltip, "Închide fereastra/meniul deschis în prezent");
        Assert.Equal(TranslationHelper.Translation.ExifVersion, "Versiune Exif");
        Assert.Equal(TranslationHelper.Translation.ExpandedGalleryItemSize, "Elemente galerie extinse");
        Assert.Equal(TranslationHelper.Translation.ExposureBias, "Compensare expunere");
        Assert.Equal(TranslationHelper.Translation.ExposureProgram, "Program de expunere");
        Assert.Equal(TranslationHelper.Translation.ExposureTime, "Timp de expunere");
        Assert.Equal(TranslationHelper.Translation.FNumber, "Număr F");
        Assert.Equal(TranslationHelper.Translation.File, "fișier");
        Assert.Equal(TranslationHelper.Translation.FileCopy, "Fișier adăugat în memoria temporară");
        Assert.Equal(TranslationHelper.Translation.FileCopyPath, "Copiere cale fișier");
        Assert.Equal(TranslationHelper.Translation.FileCopyPathMessage,
            "Calea fișierului a fost adăugată în memoria temporară");
        Assert.Equal(TranslationHelper.Translation.FileCutMessage,
            "Fișier adăugat în memoria temporară în vederea mutării");
        Assert.Equal(TranslationHelper.Translation.FileExtension, "Extensia fișierului");
        Assert.Equal(TranslationHelper.Translation.FileManagement, "Gestionarea fișierelor");
        Assert.Equal(TranslationHelper.Translation.FileName, "Denumirea fișierului");
        Assert.Equal(TranslationHelper.Translation.FilePaste, "Lipi");
        Assert.Equal(TranslationHelper.Translation.FileProperties, "Proprietățile fișierului");
        Assert.Equal(TranslationHelper.Translation.FileSize, "Dimensiunea fișierului");
        Assert.Equal(TranslationHelper.Translation.Files, "fișiere");
        Assert.Equal(TranslationHelper.Translation.Fill, "Umplere");
        Assert.Equal(TranslationHelper.Translation.FillHeight, "⇔ Umplere înălțime");
        Assert.Equal(TranslationHelper.Translation.FillSquare, "UmplePătrat");
        Assert.Equal(TranslationHelper.Translation.FineWeather, "Vreme frumoasă");
        Assert.Equal(TranslationHelper.Translation.FirstImage, "Prima imagine");
        Assert.Equal(TranslationHelper.Translation.Fit, "Potrivire");
        Assert.Equal(TranslationHelper.Translation.FitToWindow, "Potrivire la fereastră/imagine");
        Assert.Equal(TranslationHelper.Translation.Flash, "Bliț");
        Assert.Equal(TranslationHelper.Translation.FlashDidNotFire, "Blițul nu a fost declanșat");
        Assert.Equal(TranslationHelper.Translation.FlashEnergy, "Energie bliț");
        Assert.Equal(TranslationHelper.Translation.FlashFired, "Bliț declanșat");
        Assert.Equal(TranslationHelper.Translation.FlashMode, "Modul bliț");
        Assert.Equal(TranslationHelper.Translation.Flip, "Răsturnare orizontală");
        Assert.Equal(TranslationHelper.Translation.Flipped, "Răsturnată");
        Assert.Equal(TranslationHelper.Translation.Fluorescent, "Fluorescent");
        Assert.Equal(TranslationHelper.Translation.FocalLength, "Distanță focală");
        Assert.Equal(TranslationHelper.Translation.FocalLength35mm, "Distanță focală 35mm");
        Assert.Equal(TranslationHelper.Translation.Folder, "Dosar");
        Assert.Equal(TranslationHelper.Translation.Forward, "Înainte");
        Assert.Equal(TranslationHelper.Translation.FrostyOutline, "Conturare glacială");
        Assert.Equal(TranslationHelper.Translation.Fstop, "F-stop");
        Assert.Equal(TranslationHelper.Translation.FullPath, "Cale completă");
        Assert.Equal(TranslationHelper.Translation.Fullscreen, "Ecran complet");
        Assert.Equal(TranslationHelper.Translation.GallerySettings, "Setări de galerie");
        Assert.Equal(TranslationHelper.Translation.GalleryThumbnailStretch, "Extinde miniaturile de gallerie");
        Assert.Equal(TranslationHelper.Translation.GeneralSettings, "Setări generale");
        Assert.Equal(TranslationHelper.Translation.GenerateThumbnails, "Generare miniaturi");
        Assert.Equal(TranslationHelper.Translation.GithubRepo, "Depozit GitHub");
        Assert.Equal(TranslationHelper.Translation.GlassTheme, "Temă de sticlă");
        Assert.Equal(TranslationHelper.Translation.GlassTile, "Dale din sticlă");
        Assert.Equal(TranslationHelper.Translation.Gloom, "Întunecare");
        Assert.Equal(TranslationHelper.Translation.GoBackBy100Images, "Mergi înapoi cu 100 imagini");
        Assert.Equal(TranslationHelper.Translation.GoBackBy10Images, "Mergi înapoi cu 10 imagini");
        Assert.Equal(TranslationHelper.Translation.GoToImageAtSpecifiedIndex, "Mergi la imagine la indexul specificat");
        Assert.Equal(TranslationHelper.Translation.Hard, "Tare");
        Assert.Equal(TranslationHelper.Translation.Height, "Înălțime");
        Assert.Equal(TranslationHelper.Translation.HideBottomGallery, "Ascunde galeria de jos");
        Assert.Equal(TranslationHelper.Translation.HideBottomToolbar, "Ascunde bara de unelte inferioară");
        Assert.Equal(TranslationHelper.Translation.HideUI, "Ascunde interfața");
        Assert.Equal(TranslationHelper.Translation.High, "Înalt");
        Assert.Equal(TranslationHelper.Translation.HighQuality, "Calitate înaltă");
        Assert.Equal(TranslationHelper.Translation.HighlightColor, "Culoare de evidențiere");
        Assert.Equal(TranslationHelper.Translation.ISOSpeed, "Viteză ISO");
        Assert.Equal(TranslationHelper.Translation.IconsUsed, "Pictograme utilizate:");
        Assert.Equal(TranslationHelper.Translation.Image, "Imagine");
        Assert.Equal(TranslationHelper.Translation.ImageAliasing, "Aliasare imagine");
        Assert.Equal(TranslationHelper.Translation.ImageControl, "Controlul imaginii");
        Assert.Equal(TranslationHelper.Translation.ImageInfo, "Informații imagine");
        Assert.Equal(TranslationHelper.Translation.Inches, "țoli");
        Assert.Equal(TranslationHelper.Translation.InfoWindow, "Fereastră cu informații");
        Assert.Equal(TranslationHelper.Translation.InfoWindowTitle, "Informații și comenzi rapide");
        Assert.Equal(TranslationHelper.Translation.InterfaceConfiguration, "Configurare interfață");
        Assert.Equal(TranslationHelper.Translation.Landscape, "Peisaj");
        Assert.Equal(TranslationHelper.Translation.Language, "Limbă");
        Assert.Equal(TranslationHelper.Translation.LastAccessTime, "Data ultimei accesări");
        Assert.Equal(TranslationHelper.Translation.LastImage, "Ultima imagine");
        Assert.Equal(TranslationHelper.Translation.LastWriteTime, "Data ultimei scrieri");
        Assert.Equal(TranslationHelper.Translation.Latitude, "Latitudine");
        Assert.Equal(TranslationHelper.Translation.Left, "Stânga");
        Assert.Equal(TranslationHelper.Translation.LensMaker, "Producător obiectiv");
        Assert.Equal(TranslationHelper.Translation.LensModel, "Model obiectiv");
        Assert.Equal(TranslationHelper.Translation.LightSource, "Sursă de lumină");
        Assert.Equal(TranslationHelper.Translation.LightTheme, "Temă luminoasă");
        Assert.Equal(TranslationHelper.Translation.Lighting, "Iluminare");
        Assert.Equal(TranslationHelper.Translation.Loading, "Se încarcă...");
        Assert.Equal(TranslationHelper.Translation.Longitude, "Longitudine");
        Assert.Equal(TranslationHelper.Translation.Looping, "Repetare");
        Assert.Equal(TranslationHelper.Translation.LoopingDisabled, "Repetare dezactivată");
        Assert.Equal(TranslationHelper.Translation.LoopingEnabled, "Repetare activată");
        Assert.Equal(TranslationHelper.Translation.Lossless, "Fără pierderi");
        Assert.Equal(TranslationHelper.Translation.Lossy, "Cu pierderi");
        Assert.Equal(TranslationHelper.Translation.Low, "Scăzut");
        Assert.Equal(TranslationHelper.Translation.Manual, "Manual");
        Assert.Equal(TranslationHelper.Translation.MaxAperture, "Diafragmă maximă");
        Assert.Equal(TranslationHelper.Translation.Maximize, "Maximizare");
        Assert.Equal(TranslationHelper.Translation.MegaPixels, "megapixeli");
        Assert.Equal(TranslationHelper.Translation.Meter, "Metru");
        Assert.Equal(TranslationHelper.Translation.MeteringMode, "Mod de măsurare");
        Assert.Equal(TranslationHelper.Translation.Minimize, "Minimizare");
        Assert.Equal(TranslationHelper.Translation.MiscSettings, "Alte setări");
        Assert.Equal(TranslationHelper.Translation.Modified, "Modificată");
        Assert.Equal(TranslationHelper.Translation.Monochrome, "Monocrom");
        Assert.Equal(TranslationHelper.Translation.MouseDrag, "Glisare cu mausul");
        Assert.Equal(TranslationHelper.Translation.MouseKeyBack, "Tastă mausă înapoi");
        Assert.Equal(TranslationHelper.Translation.MouseKeyForward, "Tastă maus înainte");
        Assert.Equal(TranslationHelper.Translation.MouseWheel, "Rotiță maus");
        Assert.Equal(TranslationHelper.Translation.MoveWindow, "Mută fereastra");
        Assert.Equal(TranslationHelper.Translation.Navigation, "Navigare");
        Assert.Equal(TranslationHelper.Translation.NearestNeighbor, "Cel mai apropiat vecin");
        Assert.Equal(TranslationHelper.Translation.NegativeColors, "Culori negative");
        Assert.Equal(TranslationHelper.Translation.NewWindow, "Fereastră nouă");
        Assert.Equal(TranslationHelper.Translation.NextFolder, "Navighează la următorul folder");
        Assert.Equal(TranslationHelper.Translation.NextImage, "Imaginea următoare");
        Assert.Equal(TranslationHelper.Translation.NoChange, "Nicio schimbare");
        Assert.Equal(TranslationHelper.Translation.NoConversion, "Fără conversie");
        Assert.Equal(TranslationHelper.Translation.NoImage, "Nicio imagine încărcată");
        Assert.Equal(TranslationHelper.Translation.NoImages, "Fără imagini");
        Assert.Equal(TranslationHelper.Translation.NoResize, "Fără redimensionare");
        Assert.Equal(TranslationHelper.Translation.None, "Nici unul");
        Assert.Equal(TranslationHelper.Translation.Normal, "Normal");
        Assert.Equal(TranslationHelper.Translation.NormalWindow, "Fereastră normală");
        Assert.Equal(TranslationHelper.Translation.NotDefined, "Nedefinit");
        Assert.Equal(TranslationHelper.Translation.NumpadMinus, "Tastă numerică -");
        Assert.Equal(TranslationHelper.Translation.NumpadPlus, "Tastă numerică +");
        Assert.Equal(TranslationHelper.Translation.OldMovie, "Film vechi");
        Assert.Equal(TranslationHelper.Translation.Open, "Deschidere");
        Assert.Equal(TranslationHelper.Translation.OpenFileDialog, "Selectează un fișier");
        Assert.Equal(TranslationHelper.Translation.OpenInSameWindow, "Deschideți fișierele în aceeași fereastră");
        Assert.Equal(TranslationHelper.Translation.OpenLastFile, "Deschide ultimul fișier");
        Assert.Equal(TranslationHelper.Translation.OpenWith, "Deschidere cu....");
        Assert.Equal(TranslationHelper.Translation.OptimizeImage, "Optimizare imagine");
        Assert.Equal(TranslationHelper.Translation.Orientation, "Orientare");
        Assert.Equal(TranslationHelper.Translation.OutputFolder, "Dosar de ieșire");
        Assert.Equal(TranslationHelper.Translation.Pan, "Panoramare");
        Assert.Equal(TranslationHelper.Translation.PaperFold, "Pliant de hârtie");
        Assert.Equal(TranslationHelper.Translation.PasswordArchive, "Arhiva protejată cu parolă nu este acceptată");
        Assert.Equal(TranslationHelper.Translation.PasteImageFromClipholder, "Lipire imagine din memoria temporară");
        Assert.Equal(TranslationHelper.Translation.PencilSketch, "Schiță cu creionul");
        Assert.Equal(TranslationHelper.Translation.PercentComplete, "% finalizat...");
        Assert.Equal(TranslationHelper.Translation.Percentage, "Procentaj");
        Assert.Equal(TranslationHelper.Translation.PermanentlyDelete, "Șterge definitiv");
        Assert.Equal(TranslationHelper.Translation.PhotometricInterpretation, "Interpretare fotometrică");
        Assert.Equal(TranslationHelper.Translation.Pivot, "Pivot");
        Assert.Equal(TranslationHelper.Translation.Pixelate, "Pixelare");
        Assert.Equal(TranslationHelper.Translation.Pixels, "pixeli");
        Assert.Equal(TranslationHelper.Translation.Portrait, "Portret");
        Assert.Equal(TranslationHelper.Translation.PressKey, "Apasă tasta...");
        Assert.Equal(TranslationHelper.Translation.PrevFolder, "Navighează la folderul anterior");
        Assert.Equal(TranslationHelper.Translation.PrevImage, "Imaginea anterioară");
        Assert.Equal(TranslationHelper.Translation.Print, "Imprimare");
        Assert.Equal(TranslationHelper.Translation.PrintSizeCm, "Dimensiune imprimare (cm)");
        Assert.Equal(TranslationHelper.Translation.PrintSizeIn, "Dimensiune imprimare (in)");
        Assert.Equal(TranslationHelper.Translation.Quality, "Calitate");
        Assert.Equal(TranslationHelper.Translation.Random, "Aleatoriu");
        Assert.Equal(TranslationHelper.Translation.RecentFiles, "Fișiere recente");
        Assert.Equal(TranslationHelper.Translation.RedEyeReduction, "Reducere ochi roșii");
        Assert.Equal(TranslationHelper.Translation.Reload, "Reîncărcare");
        Assert.Equal(TranslationHelper.Translation.RemoveStarRating, "Eliminare evaluare");
        Assert.Equal(TranslationHelper.Translation.RenameFile, "Redenumire fișier");
        Assert.Equal(TranslationHelper.Translation.Reset, "Resetează");
        Assert.Equal(TranslationHelper.Translation.ResetButtonText, "Resetare la implicit");
        Assert.Equal(TranslationHelper.Translation.ResetZoom, "Resetare transfocare");
        Assert.Equal(TranslationHelper.Translation.Resize, "Redimensionare");
        Assert.Equal(TranslationHelper.Translation.ResizeImage, "Redimensionare imagine");
        Assert.Equal(TranslationHelper.Translation.Resolution, "Rezoluție");
        Assert.Equal(TranslationHelper.Translation.ResolutionUnit, "Unitate de rezoluție");
        Assert.Equal(TranslationHelper.Translation.RestartApp, "Repornește aplicația");
        Assert.Equal(TranslationHelper.Translation.RestoreDown, "Restabilire în jos");
        Assert.Equal(TranslationHelper.Translation.Reverse, "Inversă");
        Assert.Equal(TranslationHelper.Translation.Right, "Dreapta");
        Assert.Equal(TranslationHelper.Translation.Ripple, "Undă");
        Assert.Equal(TranslationHelper.Translation.RippleAlt, "Undă alternativă");
        Assert.Equal(TranslationHelper.Translation.RotateLeft, "Rotire la stânga");
        Assert.Equal(TranslationHelper.Translation.RotateRight, "Rotire la dreapta");
        Assert.Equal(TranslationHelper.Translation.Rotated, "Rotită");
        Assert.Equal(TranslationHelper.Translation.Saturation, "Saturație");
        Assert.Equal(TranslationHelper.Translation.Save, "Salvare");
        Assert.Equal(TranslationHelper.Translation.SaveAs, "Salvează ca");
        Assert.Equal(TranslationHelper.Translation.SavingFileFailed, "Salvarea fișierului a eșuat");
        Assert.Equal(TranslationHelper.Translation.ScrollAndRotate, "Derulați și rotiți");
        Assert.Equal(TranslationHelper.Translation.ScrollDirection, "Direcție de defilare");
        Assert.Equal(TranslationHelper.Translation.ScrollDown, "Derulează în jos");
        Assert.Equal(TranslationHelper.Translation.ScrollToBottom, "Derulează în partea de jos");
        Assert.Equal(TranslationHelper.Translation.ScrollToTop, "Derulează în partea de sus");
        Assert.Equal(TranslationHelper.Translation.ScrollToZoom, "Transfocare cu rotița mausului, navigare cu Ctrl");
        Assert.Equal(TranslationHelper.Translation.ScrollUp, "Derulează în sus");
        Assert.Equal(TranslationHelper.Translation.Scrolling, "Defilare");
        Assert.Equal(TranslationHelper.Translation.ScrollingDisabled, "Defilare dezactivată");
        Assert.Equal(TranslationHelper.Translation.ScrollingEnabled, "Defilare activată");
        Assert.Equal(TranslationHelper.Translation.SearchSubdirectory, "Căutare subdirectoare");
        Assert.Equal(TranslationHelper.Translation.SecAbbreviation, "Sec.");
        Assert.Equal(TranslationHelper.Translation.SelectAll, "Selectează tot");
        Assert.Equal(TranslationHelper.Translation.SelectGalleryThumb, "Selectați miniatura galeriei");
        Assert.Equal(TranslationHelper.Translation.SendCurrentImageToRecycleBin,
            "Trimite imaginea curentă la coșul de reciclare");
        Assert.Equal(TranslationHelper.Translation.SentFileToRecycleBin, "Fișier trimis la coșul de reciclare");
        Assert.Equal(TranslationHelper.Translation.SetAs, "Setare ca....");
        Assert.Equal(TranslationHelper.Translation.SetAsLockScreenImage, "Setare ca imagine pentru ecranul de blocare");
        Assert.Equal(TranslationHelper.Translation.SetAsWallpaper, "Setare ca fundal");
        Assert.Equal(TranslationHelper.Translation.SetCurrentImageAsWallpaper, "Setează imaginea curentă ca fundal:");
        Assert.Equal(TranslationHelper.Translation.SetStarRating, "Setare evaluare cu stele");
        Assert.Equal(TranslationHelper.Translation.Settings, "Setări");
        Assert.Equal(TranslationHelper.Translation.Shade, "Umbrit");
        Assert.Equal(TranslationHelper.Translation.Sharpness, "Claritate");
        Assert.Equal(TranslationHelper.Translation.Shift, "Shift");
        Assert.Equal(TranslationHelper.Translation.ShowAllSettingsWindow, "Arată fereastra cu toate setările");
        Assert.Equal(TranslationHelper.Translation.ShowBottomGallery, "Afișează galeria de jos");
        Assert.Equal(TranslationHelper.Translation.ShowBottomGalleryWhenUiIsHidden,
            "Afișează galeria de jos când interfața utilizatorului este ascunsă");
        Assert.Equal(TranslationHelper.Translation.ShowBottomToolbar, "Afișează bara de instrumente de jos");
        Assert.Equal(TranslationHelper.Translation.ShowConfirmationOnEsc,
            "Afișează dialogul de confirmare când apăsați 'Esc'");
        Assert.Equal(TranslationHelper.Translation.ShowFadeInButtonsOnHover,
            "Afișează butoanele fade-in la trecerea mouse-ului");
        Assert.Equal(TranslationHelper.Translation.ShowFileSavingDialog, "Afișează dialogul de salvare fișier");
        Assert.Equal(TranslationHelper.Translation.ShowImageGallery, "Arată galeria de imagini");
        Assert.Equal(TranslationHelper.Translation.ShowImageInfo, "Arată informațiile imaginii");
        Assert.Equal(TranslationHelper.Translation.ShowInFolder, "Arată în dosar");
        Assert.Equal(TranslationHelper.Translation.ShowInfoWindow, "Arată fereastra cu informații");
        Assert.Equal(TranslationHelper.Translation.ShowResizeWindow, "Arată fereastra de redimensionare");
        Assert.Equal(TranslationHelper.Translation.ShutterPriority, "Prioritate obturator");
        Assert.Equal(TranslationHelper.Translation.SideBySide, "Alăturat");
        Assert.Equal(TranslationHelper.Translation.SideBySideTooltip, "Afișați imagini una lângă alta");
        Assert.Equal(TranslationHelper.Translation.Size, "Dimensiune");
        Assert.Equal(TranslationHelper.Translation.SizeMp, "Dimensiune (mp)");
        Assert.Equal(TranslationHelper.Translation.SizeTooltip, "Introdu dimensiunea dorită în pixeli sau în procent.");
        Assert.Equal(TranslationHelper.Translation.Sketch, "Schiță");
        Assert.Equal(TranslationHelper.Translation.Slideshow, "Prezentare");
        Assert.Equal(TranslationHelper.Translation.SmoothMagnify, "Amplificare lină");
        Assert.Equal(TranslationHelper.Translation.Soft, "Moale");
        Assert.Equal(TranslationHelper.Translation.Software, "Software");
        Assert.Equal(TranslationHelper.Translation.SortFilesBy, "Sortare fișiere după");
        Assert.Equal(TranslationHelper.Translation.SourceFolder, "Dosar sursă");
        Assert.Equal(TranslationHelper.Translation.Space, "Spațiu");
        Assert.Equal(TranslationHelper.Translation.Square, "Pătrat");
        Assert.Equal(TranslationHelper.Translation.Start, "Pornire");
        Assert.Equal(TranslationHelper.Translation.StartSlideshow, "Pornire prezentare");
        Assert.Equal(TranslationHelper.Translation.StayCentered, "Păstrează fereastra centrată");
        Assert.Equal(TranslationHelper.Translation.StayTopMost, "Rămâi deasupra celorlalte ferestre");
        Assert.Equal(TranslationHelper.Translation.Stretch, "Întindere");
        Assert.Equal(TranslationHelper.Translation.StretchImage, "Întinde imaginea");
        Assert.Equal(TranslationHelper.Translation.StrobeReturnLightDetected,
            "Lumină de întoarcere a blițului detectată");
        Assert.Equal(TranslationHelper.Translation.StrobeReturnLightNotDetected,
            "Lumină de întoarcere a blițului nedetectată");
        Assert.Equal(TranslationHelper.Translation.Subject, "Subiect");
        Assert.Equal(TranslationHelper.Translation.Swirl, "Vârtej");
        Assert.Equal(TranslationHelper.Translation.TelescopicBlur, "Estompare telescopică");
        Assert.Equal(TranslationHelper.Translation.Theme, "Temă");
        Assert.Equal(TranslationHelper.Translation.Thumbnail, "Miniatură");
        Assert.Equal(TranslationHelper.Translation.Tile, "Împărțire");
        Assert.Equal(TranslationHelper.Translation.Title, "Titlu");
        Assert.Equal(TranslationHelper.Translation.ToggleBackgroundColor, "\"Comută culoarea de fundal");
        Assert.Equal(TranslationHelper.Translation.ToggleFullscreen, "Comutare la ecran complet");
        Assert.Equal(TranslationHelper.Translation.ToggleLooping, "Comutare repetare");
        Assert.Equal(TranslationHelper.Translation.ToggleScroll, "Comutare defilare");
        Assert.Equal(TranslationHelper.Translation.ToggleTaskbarProgress, "Afișează progresul în bara de activități");
        Assert.Equal(TranslationHelper.Translation.ToneMapping, "Mapare de ton");
        Assert.Equal(TranslationHelper.Translation.UnableToRender, "Nu se poate reda imaginea");
        Assert.Equal(TranslationHelper.Translation.Uncalibrated, "Necalibrat");
        Assert.Equal(TranslationHelper.Translation.Underwater, "Subacvatic");
        Assert.Equal(TranslationHelper.Translation.UnexpectedError, "A apărut o eroare necunoscută");
        Assert.Equal(TranslationHelper.Translation.Unflip, "Anulare răsturnare");
        Assert.Equal(TranslationHelper.Translation.Uniform, "Uniform");
        Assert.Equal(TranslationHelper.Translation.UniformToFill, "UniformPentruUmplere");
        Assert.Equal(TranslationHelper.Translation.Unknown, "Necunoscut");
        Assert.Equal(TranslationHelper.Translation.UnsupportedFile, "Fișier neacceptat");
        Assert.Equal(TranslationHelper.Translation.Up, "Sus");
        Assert.Equal(TranslationHelper.Translation.UsingMouse, "Utilizarea mouse-ului");
        Assert.Equal(TranslationHelper.Translation.UsingTouchpad, "Utilizarea touchpad-ului");
        Assert.Equal(TranslationHelper.Translation.Version, "Versiune:");
        Assert.Equal(TranslationHelper.Translation.ViewLicenseFile, "Vizualizare fișier de licență");
        Assert.Equal(TranslationHelper.Translation.WaveWarper, "Deformare cu unde");
        Assert.Equal(TranslationHelper.Translation.WhiteBalance, "Balans de alb");
        Assert.Equal(TranslationHelper.Translation.WhiteFluorescent, "Fluorescent alb");
        Assert.Equal(TranslationHelper.Translation.Width, "Lățime");
        Assert.Equal(TranslationHelper.Translation.WidthAndHeight, "Lățime și înălțime");
        Assert.Equal(TranslationHelper.Translation.WindowManagement, "Gestionarea ferestrei");
        Assert.Equal(TranslationHelper.Translation.WindowScaling, "Scalare fereastră");
        Assert.Equal(TranslationHelper.Translation.Zoom, "Transfocare");
        Assert.Equal(TranslationHelper.Translation.ZoomIn, "Apropiere");
        Assert.Equal(TranslationHelper.Translation.ZoomOut, "Depărtare");
        Assert.Equal(TranslationHelper.Translation._1Star, "Evaluare cu 1 stea");
        Assert.Equal(TranslationHelper.Translation._2Star, "Evaluare cu 2 stele");
        Assert.Equal(TranslationHelper.Translation._3Star, "Evaluare cu 3 stele");
        Assert.Equal(TranslationHelper.Translation._4Star, "Evaluare cu 4 stele");
        Assert.Equal(TranslationHelper.Translation._5Star, "Evaluare cu 5 stele");
    }
}