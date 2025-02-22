﻿using PicView.Core.Localization;

namespace PicView.Tests.LanguageTests;

public class SpanishUnitTest
{
#pragma warning disable xUnit2000
    [Fact]
    public static async Task CheckSpanishLanguage()
    {
        var exists = await TranslationHelper.LoadLanguage("es");
        Assert.True(exists);
        Assert.Equal(TranslationHelper.Translation.About, "Acerca de");
        Assert.Equal(TranslationHelper.Translation.ActionProgram, "Programa de acción");
        Assert.Equal(TranslationHelper.Translation.AddedToClipboard, "agregada al portapapeles");
        Assert.Equal(TranslationHelper.Translation.AdditionalFunctions, "Funciones adicionales");
        Assert.Equal(TranslationHelper.Translation.AdjustNavSpeed,
            "Ajustar velocidad cuando la tecla se mantiene presionada");
        Assert.Equal(TranslationHelper.Translation.AdjustTimingForSlideshow, "Ajustar velocidad de presentación");
        Assert.Equal(TranslationHelper.Translation.AdjustTimingForZoom, "Ajustar velocidad de acercado/alejado");
        Assert.Equal(TranslationHelper.Translation.AdjustZoomLevel, "Ajustar nivel de zoom");
        Assert.Equal(TranslationHelper.Translation.AdvanceBy100Images, "Avanzar 100 imágenes");
        Assert.Equal(TranslationHelper.Translation.AdvanceBy10Images, "Avanzar 10 imágenes");
        Assert.Equal(TranslationHelper.Translation.AllowZoomOut,
            "Evite hacer zoom out en la imagen cuando ya esté en tamaño máximo");
        Assert.Equal(TranslationHelper.Translation.Alt, "Alt");
        Assert.Equal(TranslationHelper.Translation.Altitude, "Altitud");
        Assert.Equal(TranslationHelper.Translation.AperturePriority, "Prioridad de apertura");
        Assert.Equal(TranslationHelper.Translation.Appearance, "Opciones de interfaz");
        Assert.Equal(TranslationHelper.Translation.ApplicationShortcuts, "Atajos de la aplicación");
        Assert.Equal(TranslationHelper.Translation.ApplicationStartup, "Inicio de la aplicación");
        Assert.Equal(TranslationHelper.Translation.Apply, "Aplicar");
        Assert.Equal(TranslationHelper.Translation.Applying, "Aplicando");
        Assert.Equal(TranslationHelper.Translation.Ascending, "Ascendante");
        Assert.Equal(TranslationHelper.Translation.AspectRatio, "Dimensiones");
        Assert.Equal(TranslationHelper.Translation.Authors, "Autores");
        Assert.Equal(TranslationHelper.Translation.Auto, "Automático");
        Assert.Equal(TranslationHelper.Translation.AutoFitWindow, "Auto ajustar ventana");
        Assert.Equal(TranslationHelper.Translation.BadArchive, "No se ha podido procesar el archivo");
        Assert.Equal(TranslationHelper.Translation.Base64Image, "Imagen en Base64");
        Assert.Equal(TranslationHelper.Translation.BatchResize, "Cambio de tamaño por lotes");
        Assert.Equal(TranslationHelper.Translation.BitDepth, "Profundidad de bits");
        Assert.Equal(TranslationHelper.Translation.BlackAndWhite, "Blanco y Negro");
        Assert.Equal(TranslationHelper.Translation.Blur, "Desenfoque");
        Assert.Equal(TranslationHelper.Translation.BottomGalleryItemSize, "Elementos de la galería inferior");
        Assert.Equal(TranslationHelper.Translation.BottomGalleryThumbnailStretch,
            "Estiramiento de miniatura de la galería inferior");
        Assert.Equal(TranslationHelper.Translation.Brightness, "Brillo");
        Assert.Equal(TranslationHelper.Translation.CameraMaker, "Fabricante de la cámara");
        Assert.Equal(TranslationHelper.Translation.CameraModel, "Modelo de la cámara");
        Assert.Equal(TranslationHelper.Translation.Cancel, "Cancelar");
        Assert.Equal(TranslationHelper.Translation.Center, "Centro");
        Assert.Equal(TranslationHelper.Translation.CenterWindow, "Centrar ventana");
        Assert.Equal(TranslationHelper.Translation.Centimeters, "centímetros");
        Assert.Equal(TranslationHelper.Translation.ChangeBackground, "Cambiar fondo");
        Assert.Equal(TranslationHelper.Translation.ChangeBackgroundTooltip,
            "Cambiar entre color de fondo para imágenes de fondo transparente");
        Assert.Equal(TranslationHelper.Translation.ChangeKeybindingText,
            "Haz clic en un cuadro de texto para cambiar la asignación de teclas. Presiona Esc para desvincular la tecla.");
        Assert.Equal(TranslationHelper.Translation.ChangeKeybindingTooltip,
            "Haz clic para cambiar la asignación de teclas");
        Assert.Equal(TranslationHelper.Translation.ChangingThemeRequiresRestart,
            "* Cambiar el tema requiere reabrir el programa");
        Assert.Equal(TranslationHelper.Translation.CheckForUpdates, "Compruebe las actualizaciones");
        Assert.Equal(TranslationHelper.Translation.ClipboardImage, "Imagen de Portapapeles");
        Assert.Equal(TranslationHelper.Translation.Close, "Cerrar");
        Assert.Equal(TranslationHelper.Translation.CloseApp, "Cierra la aplicación por completo");
        Assert.Equal(TranslationHelper.Translation.CloseGallery, "Cerrar galería");
        Assert.Equal(TranslationHelper.Translation.CloseWindowPrompt, "¿Desea cerrar la ventana?");
        Assert.Equal(TranslationHelper.Translation.CloudyWeather, "Tiempo nublado");
        Assert.Equal(TranslationHelper.Translation.ColorPickerTool, "Cuentagotas");
        Assert.Equal(TranslationHelper.Translation.ColorPickerToolTooltip, "Elegir color desde imagen");
        Assert.Equal(TranslationHelper.Translation.ColorRepresentation, "Representación de color");
        Assert.Equal(TranslationHelper.Translation.ColorTone, "Tono de Color");
        Assert.Equal(TranslationHelper.Translation.CompressedBitsPixel, "Bits por píxel comprimidos");
        Assert.Equal(TranslationHelper.Translation.Compression, "Compresión");
        Assert.Equal(TranslationHelper.Translation.Contrast, "Contraste");
        Assert.Equal(TranslationHelper.Translation.ConvertTo, "Convertir a");
        Assert.Equal(TranslationHelper.Translation.ConvertedToBase64, "Convertido a base64");
        Assert.Equal(TranslationHelper.Translation.CoolWhiteFluorescent, "Fluorescente blanco frío");
        Assert.Equal(TranslationHelper.Translation.CopiedImage, "Copiar imagen al portapapeles");
        Assert.Equal(TranslationHelper.Translation.Copy, "Copiar");
        Assert.Equal(TranslationHelper.Translation.CopyFile, "Copiar archivo");
        Assert.Equal(TranslationHelper.Translation.CopyImage, "Copiar imagen");
        Assert.Equal(TranslationHelper.Translation.CopyImageTooltip, "Copiar como imagen del portapapeles de Windows");
        Assert.Equal(TranslationHelper.Translation.Copyright, "Derechos de autor");
        Assert.Equal(TranslationHelper.Translation.Created, "Creado");
        Assert.Equal(TranslationHelper.Translation.CreationTime, "Fecha de creación");
        Assert.Equal(TranslationHelper.Translation.CreativeProgram, "Programa creativo");
        Assert.Equal(TranslationHelper.Translation.Credits, "Créditos");
        Assert.Equal(TranslationHelper.Translation.Crop, "Recortar");
        Assert.Equal(TranslationHelper.Translation.CropMessage, "Presiona Esc para cerrar, Enter para guardar");
        Assert.Equal(TranslationHelper.Translation.CropPicture, "Recortar Imagen");
        Assert.Equal(TranslationHelper.Translation.Ctrl, "Ctrl");
        Assert.Equal(TranslationHelper.Translation.CtrlToZoom, "Ctrl para zoom, rueda para navegar");
        Assert.Equal(TranslationHelper.Translation.Cut, "Cortar");
        Assert.Equal(TranslationHelper.Translation.DarkTheme, "Tema oscuro");
        Assert.Equal(TranslationHelper.Translation.Date, "Fecha");
        Assert.Equal(TranslationHelper.Translation.DateTaken, "Fecha de toma");
        Assert.Equal(TranslationHelper.Translation.DayWhiteFluorescent, "Fluorescente blanco de luz del día");
        Assert.Equal(TranslationHelper.Translation.Daylight, "Luz del día");
        Assert.Equal(TranslationHelper.Translation.DaylightFluorescent, "Fluorescente de luz del día");
        Assert.Equal(TranslationHelper.Translation.Del, "Supr");
        Assert.Equal(TranslationHelper.Translation.DeleteFile, "Eliminar archivo");
        Assert.Equal(TranslationHelper.Translation.DeleteFilePermanently,
            "¿Estás seguro de que deseas eliminar permanentemente");
        Assert.Equal(TranslationHelper.Translation.DeletedFile, "Archivo eliminado");
        Assert.Equal(TranslationHelper.Translation.Descending, "Descendante");
        Assert.Equal(TranslationHelper.Translation.DigitalZoom, "Zoom digital");
        Assert.Equal(TranslationHelper.Translation.DisableFadeInButtonsOnHover,
            "Desactivar botones de desvanecimiento al pasar el ratón");
        Assert.Equal(TranslationHelper.Translation.DiskSize, "Tamaño en disco");
        Assert.Equal(TranslationHelper.Translation.DoubleClick, "Doble Click");
        Assert.Equal(TranslationHelper.Translation.Down, "Abajo");
        Assert.Equal(TranslationHelper.Translation.Dpi, "DPI");
        Assert.Equal(TranslationHelper.Translation.DragFileTo,
            "Arrastra el archivo al Explorador de Windows o a otra aplicación/navegador");
        Assert.Equal(TranslationHelper.Translation.DragImage, "Arrastrar imagen");
        Assert.Equal(TranslationHelper.Translation.DropToLoad, "Arrastra para cargar una imagen");
        Assert.Equal(TranslationHelper.Translation.DuplicateFile, "Duplicar archivo");
        Assert.Equal(TranslationHelper.Translation.Effects, "Efectos de Sombra");
        Assert.Equal(TranslationHelper.Translation.EffectsTooltip, "Mostrar ventana de efectos");
        Assert.Equal(TranslationHelper.Translation.Enter, "Enter");
        Assert.Equal(TranslationHelper.Translation.Esc, "Esc");
        Assert.Equal(TranslationHelper.Translation.EscCloseTooltip, "Cierra el menú/ventana actualmente abierto");
        Assert.Equal(TranslationHelper.Translation.ExifVersion, "Versión Exif");
        Assert.Equal(TranslationHelper.Translation.ExpandedGalleryItemSize, "Elementos de la galería expandida");
        Assert.Equal(TranslationHelper.Translation.ExposureBias, "Compensación de exposición");
        Assert.Equal(TranslationHelper.Translation.ExposureProgram, "Programa de exposición");
        Assert.Equal(TranslationHelper.Translation.ExposureTime, "Tiempo de exposición");
        Assert.Equal(TranslationHelper.Translation.FNumber, "Número F");
        Assert.Equal(TranslationHelper.Translation.File, "archivo");
        Assert.Equal(TranslationHelper.Translation.FileCopy, "Archivo agregado al portapapeles");
        Assert.Equal(TranslationHelper.Translation.FileCopyPath, "Copiar carpeta del archivo");
        Assert.Equal(TranslationHelper.Translation.FileCopyPathMessage, "Carpeta del archivo agregada al portapapeles");
        Assert.Equal(TranslationHelper.Translation.FileCutMessage, "Archivo agregada para mover al portapapeles");
        Assert.Equal(TranslationHelper.Translation.FileExtension, "Extensión de archivo");
        Assert.Equal(TranslationHelper.Translation.FileManagement, "Gestión de archivos");
        Assert.Equal(TranslationHelper.Translation.FileName, "Nombre de archivo");
        Assert.Equal(TranslationHelper.Translation.FilePaste, "Pegar");
        Assert.Equal(TranslationHelper.Translation.FileProperties, "Propiedades del archivo");
        Assert.Equal(TranslationHelper.Translation.FileSize, "Tamaño de archivo");
        Assert.Equal(TranslationHelper.Translation.Files, "archivos");
        Assert.Equal(TranslationHelper.Translation.Fill, "Rellenar");
        Assert.Equal(TranslationHelper.Translation.FillHeight, "⇔ Rellenar altura");
        Assert.Equal(TranslationHelper.Translation.FillSquare, "LlenarCuadrado");
        Assert.Equal(TranslationHelper.Translation.FineWeather, "Buen tiempo");
        Assert.Equal(TranslationHelper.Translation.FirstImage, "Primera imagen");
        Assert.Equal(TranslationHelper.Translation.Fit, "Ajustar");
        Assert.Equal(TranslationHelper.Translation.FitToWindow, "Ajustar a ventana/imagen");
        Assert.Equal(TranslationHelper.Translation.Flash, "Flash");
        Assert.Equal(TranslationHelper.Translation.FlashDidNotFire, "Flash no disparado");
        Assert.Equal(TranslationHelper.Translation.FlashEnergy, "Energía de flash");
        Assert.Equal(TranslationHelper.Translation.FlashFired, "Flash disparado");
        Assert.Equal(TranslationHelper.Translation.FlashMode, "Modo de flash");
        Assert.Equal(TranslationHelper.Translation.Flip, "Voltear horizontalmente");
        Assert.Equal(TranslationHelper.Translation.Flipped, "Volteado");
        Assert.Equal(TranslationHelper.Translation.Fluorescent, "Fluorescente");
        Assert.Equal(TranslationHelper.Translation.FocalLength, "Longitud focal");
        Assert.Equal(TranslationHelper.Translation.FocalLength35mm, "Longitud focal 35mm");
        Assert.Equal(TranslationHelper.Translation.Folder, "Carpeta");
        Assert.Equal(TranslationHelper.Translation.Forward, "Hacia adelante");
        Assert.Equal(TranslationHelper.Translation.Fstop, "F-stop");
        Assert.Equal(TranslationHelper.Translation.FullPath, "Nombre completo de la carpeta");
        Assert.Equal(TranslationHelper.Translation.Fullscreen, "Pantalla Completa");
        Assert.Equal(TranslationHelper.Translation.GallerySettings, "Configuración de la Galería");
        Assert.Equal(TranslationHelper.Translation.GalleryThumbnailStretch,
            "Estiramiento de la miniatura de la galería");
        Assert.Equal(TranslationHelper.Translation.GeneralSettings, "Opciones Generales");
        Assert.Equal(TranslationHelper.Translation.GenerateThumbnails, "Generar miniaturas");
        Assert.Equal(TranslationHelper.Translation.GithubRepo, "Repositorio de Github");
        Assert.Equal(TranslationHelper.Translation.GlassTheme, "Tema de vidrio");
        Assert.Equal(TranslationHelper.Translation.GoBackBy100Images, "Retroceder 100 imágenes");
        Assert.Equal(TranslationHelper.Translation.GoBackBy10Images, "Retroceder 10 imágenes");
        Assert.Equal(TranslationHelper.Translation.GoToImageAtSpecifiedIndex, "Ir a imagen en índice especificado");
        Assert.Equal(TranslationHelper.Translation.Hard, "Duro");
        Assert.Equal(TranslationHelper.Translation.Height, "Alto");
        Assert.Equal(TranslationHelper.Translation.HideBottomGallery, "Ocultar galería inferior");
        Assert.Equal(TranslationHelper.Translation.HideBottomToolbar, "Ocultar barra de herramientas inferior");
        Assert.Equal(TranslationHelper.Translation.HideUI, "Ocultar interfaz");
        Assert.Equal(TranslationHelper.Translation.High, "Alto");
        Assert.Equal(TranslationHelper.Translation.HighQuality, "Alta calidad");
        Assert.Equal(TranslationHelper.Translation.HighlightColor, "Color de resaltado");
        Assert.Equal(TranslationHelper.Translation.ISOSpeed, "Velocidad ISO");
        Assert.Equal(TranslationHelper.Translation.IconsUsed, "Íconos usados:");
        Assert.Equal(TranslationHelper.Translation.Image, "Imagen");
        Assert.Equal(TranslationHelper.Translation.ImageAliasing, "Alias de imagen");
        Assert.Equal(TranslationHelper.Translation.ImageControl, "Control de Imagen");
        Assert.Equal(TranslationHelper.Translation.ImageInfo, "Información de Imagen");
        Assert.Equal(TranslationHelper.Translation.Inches, "pulgadas");
        Assert.Equal(TranslationHelper.Translation.InfoWindow, "Ventana de información");
        Assert.Equal(TranslationHelper.Translation.InfoWindowTitle, "Información y atajos");
        Assert.Equal(TranslationHelper.Translation.InterfaceConfiguration, "Configuración de interfaz");
        Assert.Equal(TranslationHelper.Translation.Landscape, "Horizontal");
        Assert.Equal(TranslationHelper.Translation.Language, "Lenguaje");
        Assert.Equal(TranslationHelper.Translation.LastAccessTime, "Fecha de último acceso");
        Assert.Equal(TranslationHelper.Translation.LastImage, "Última imagen");
        Assert.Equal(TranslationHelper.Translation.LastWriteTime, "Fecha de última modificación");
        Assert.Equal(TranslationHelper.Translation.Latitude, "Latitud");
        Assert.Equal(TranslationHelper.Translation.Left, "Izquierda");
        Assert.Equal(TranslationHelper.Translation.LensMaker, "Fabricante de lente");
        Assert.Equal(TranslationHelper.Translation.LensModel, "Modelo de lente");
        Assert.Equal(TranslationHelper.Translation.LightSource, "Fuente de luz");
        Assert.Equal(TranslationHelper.Translation.LightTheme, "Tema claro");
        Assert.Equal(TranslationHelper.Translation.Lighting, "Iluminación");
        Assert.Equal(TranslationHelper.Translation.Loading, "Cargando...");
        Assert.Equal(TranslationHelper.Translation.Longitude, "Longitud");
        Assert.Equal(TranslationHelper.Translation.Looping, "Bucle");
        Assert.Equal(TranslationHelper.Translation.LoopingDisabled, "Bucle desactivado");
        Assert.Equal(TranslationHelper.Translation.LoopingEnabled, "Bucle activado");
        Assert.Equal(TranslationHelper.Translation.Lossless, "Lossless");
        Assert.Equal(TranslationHelper.Translation.Lossy, "Lossy");
        Assert.Equal(TranslationHelper.Translation.Low, "Bajo");
        Assert.Equal(TranslationHelper.Translation.Manual, "Manual");
        Assert.Equal(TranslationHelper.Translation.MaxAperture, "Apertura máxima");
        Assert.Equal(TranslationHelper.Translation.Maximize, "Maximizar");
        Assert.Equal(TranslationHelper.Translation.MegaPixels, "megapixeles");
        Assert.Equal(TranslationHelper.Translation.Meter, "Medidor");
        Assert.Equal(TranslationHelper.Translation.MeteringMode, "Modo de medición");
        Assert.Equal(TranslationHelper.Translation.Minimize, "Minimizar");
        Assert.Equal(TranslationHelper.Translation.MiscSettings, "Otras opciones");
        Assert.Equal(TranslationHelper.Translation.Modified, "Modificado");
        Assert.Equal(TranslationHelper.Translation.MouseDrag, "Arrastrar con el ratón");
        Assert.Equal(TranslationHelper.Translation.MouseKeyBack, "Tecla de retroceder del ratón");
        Assert.Equal(TranslationHelper.Translation.MouseKeyForward, "Rueda del ratón hacia atrás");
        Assert.Equal(TranslationHelper.Translation.MouseWheel, "Rueda del ratón");
        Assert.Equal(TranslationHelper.Translation.MoveWindow, "Mover ventana");
        Assert.Equal(TranslationHelper.Translation.Navigation, "Navegación");
        Assert.Equal(TranslationHelper.Translation.NearestNeighbor, "Vecino más cercano");
        Assert.Equal(TranslationHelper.Translation.NegativeColors, "Negativo");
        Assert.Equal(TranslationHelper.Translation.NewWindow, "Nueva Ventana");
        Assert.Equal(TranslationHelper.Translation.NextFolder, "Navegar a la siguiente carpeta");
        Assert.Equal(TranslationHelper.Translation.NextImage, "Imagen siguiente");
        Assert.Equal(TranslationHelper.Translation.NoChange, "Sin cambios");
        Assert.Equal(TranslationHelper.Translation.NoConversion, "Sin conversión");
        Assert.Equal(TranslationHelper.Translation.NoImage, "No se ha cargado imagen");
        Assert.Equal(TranslationHelper.Translation.NoImages, "No hay imágenes");
        Assert.Equal(TranslationHelper.Translation.NoResize, "No resize");
        Assert.Equal(TranslationHelper.Translation.None, "Ninguna");
        Assert.Equal(TranslationHelper.Translation.Normal, "Normal");
        Assert.Equal(TranslationHelper.Translation.NormalWindow, "Ventana normal");
        Assert.Equal(TranslationHelper.Translation.NotDefined, "No definido");
        Assert.Equal(TranslationHelper.Translation.NumpadMinus, "Teclado numérico -");
        Assert.Equal(TranslationHelper.Translation.NumpadPlus, "Teclado numérico +");
        Assert.Equal(TranslationHelper.Translation.OldMovie, "Filme Antiguo");
        Assert.Equal(TranslationHelper.Translation.Open, "Abrir");
        Assert.Equal(TranslationHelper.Translation.OpenFileDialog, "Abrir selector de archivo");
        Assert.Equal(TranslationHelper.Translation.OpenInSameWindow, "Abrir archivos en la misma ventana");
        Assert.Equal(TranslationHelper.Translation.OpenLastFile, "Abrir ultimo archivo");
        Assert.Equal(TranslationHelper.Translation.OpenWith, "Abrir con...");
        Assert.Equal(TranslationHelper.Translation.OptimizeImage, "Optimizar la imagen");
        Assert.Equal(TranslationHelper.Translation.Orientation, "Orientación");
        Assert.Equal(TranslationHelper.Translation.OutputFolder, "Carpeta de salida");
        Assert.Equal(TranslationHelper.Translation.Pan, "Ajustar Tamaño");
        Assert.Equal(TranslationHelper.Translation.PasswordArchive, "Archivo protegido por contraseña no soportado");
        Assert.Equal(TranslationHelper.Translation.PasteImageFromClipholder, "Pegar imagen desde portapapeles");
        Assert.Equal(TranslationHelper.Translation.PencilSketch, "Dibujo a lápiz");
        Assert.Equal(TranslationHelper.Translation.PercentComplete, "% completado...");
        Assert.Equal(TranslationHelper.Translation.Percentage, "Porcentaje");
        Assert.Equal(TranslationHelper.Translation.PermanentlyDelete, "Eliminar permanentemente");
        Assert.Equal(TranslationHelper.Translation.PhotometricInterpretation, "Interpretación fotométrica");
        Assert.Equal(TranslationHelper.Translation.Pixels, "pixeles");
        Assert.Equal(TranslationHelper.Translation.Portrait, "Vertical");
        Assert.Equal(TranslationHelper.Translation.PressKey, "Presiona tecla...");
        Assert.Equal(TranslationHelper.Translation.PrevFolder, "Navegar a la carpeta anterior");
        Assert.Equal(TranslationHelper.Translation.PrevImage, "Imagen anterior");
        Assert.Equal(TranslationHelper.Translation.Print, "Imprimir");
        Assert.Equal(TranslationHelper.Translation.PrintSizeCm, "Tamaño de impresión (cm)");
        Assert.Equal(TranslationHelper.Translation.PrintSizeIn, "Tamaño de impresión (in)");
        Assert.Equal(TranslationHelper.Translation.Quality, "Calidad");
        Assert.Equal(TranslationHelper.Translation.Random, "Aleatorio");
        Assert.Equal(TranslationHelper.Translation.RecentFiles, "Archivos recientes");
        Assert.Equal(TranslationHelper.Translation.RedEyeReduction, "Reducción de ojos rojos");
        Assert.Equal(TranslationHelper.Translation.Reload, "Recargar");
        Assert.Equal(TranslationHelper.Translation.RemoveStarRating, "Eliminar clasificación");
        Assert.Equal(TranslationHelper.Translation.RenameFile, "Renombrar archivo");
        Assert.Equal(TranslationHelper.Translation.Reset, "Restablecer");
        Assert.Equal(TranslationHelper.Translation.ResetButtonText, "Restablecer a predeterminado");
        Assert.Equal(TranslationHelper.Translation.ResetZoom, "Reset zoom");
        Assert.Equal(TranslationHelper.Translation.Resize, "Cambiar de tamaño");
        Assert.Equal(TranslationHelper.Translation.ResizeImage, "Cambiar el tamaño de la imagen");
        Assert.Equal(TranslationHelper.Translation.Resolution, "Resolución");
        Assert.Equal(TranslationHelper.Translation.ResolutionUnit, "Unidad de resolución");
        Assert.Equal(TranslationHelper.Translation.RestartApp, "Reiniciar la aplicación");
        Assert.Equal(TranslationHelper.Translation.RestoreDown, "Restaurar Abajo");
        Assert.Equal(TranslationHelper.Translation.Reverse, "Marcha atrás");
        Assert.Equal(TranslationHelper.Translation.Right, "Derecha");
        Assert.Equal(TranslationHelper.Translation.RotateLeft, "Rotar a la izquierda");
        Assert.Equal(TranslationHelper.Translation.RotateRight, "Rotar a la derecha");
        Assert.Equal(TranslationHelper.Translation.Rotated, "Rotado");
        Assert.Equal(TranslationHelper.Translation.Saturation, "Saturación");
        Assert.Equal(TranslationHelper.Translation.Save, "Guardar");
        Assert.Equal(TranslationHelper.Translation.SaveAs, "Guardar como");
        Assert.Equal(TranslationHelper.Translation.SavingFileFailed, "Guardando archivo fallido");
        Assert.Equal(TranslationHelper.Translation.ScrollAndRotate, "Desplazar y rotar");
        Assert.Equal(TranslationHelper.Translation.ScrollDirection, "Dirección de desplazamiento");
        Assert.Equal(TranslationHelper.Translation.ScrollDown, "Desplazar hacia abajo");
        Assert.Equal(TranslationHelper.Translation.ScrollToBottom, "Desplazar hacia abajo al fondo");
        Assert.Equal(TranslationHelper.Translation.ScrollToTop, "Desplazar hacia arriba al tope");
        Assert.Equal(TranslationHelper.Translation.ScrollToZoom, "Rueda para zoom, Ctrl para navegar");
        Assert.Equal(TranslationHelper.Translation.ScrollUp, "Desplazar hacia arriba");
        Assert.Equal(TranslationHelper.Translation.Scrolling, "Desplazar");
        Assert.Equal(TranslationHelper.Translation.ScrollingDisabled, "Rueda del mouse desactivada");
        Assert.Equal(TranslationHelper.Translation.ScrollingEnabled, "Rueda del mouse activada");
        Assert.Equal(TranslationHelper.Translation.SearchSubdirectory, "Buscar subdirectorios");
        Assert.Equal(TranslationHelper.Translation.SecAbbreviation, "Seg.");
        Assert.Equal(TranslationHelper.Translation.SelectAll, "Seleccionar todo");
        Assert.Equal(TranslationHelper.Translation.SelectGalleryThumb, "Seleccionar miniatura de la galería");
        Assert.Equal(TranslationHelper.Translation.SendCurrentImageToRecycleBin,
            "Enviar imagen actual a papelera de reciclaje");
        Assert.Equal(TranslationHelper.Translation.SentFileToRecycleBin, "Enviar archivo a papelera de reciclaje");
        Assert.Equal(TranslationHelper.Translation.SetAs, "Establecer como...");
        Assert.Equal(TranslationHelper.Translation.SetAsLockScreenImage, "Establecer como imagen de bloqueo");
        Assert.Equal(TranslationHelper.Translation.SetAsWallpaper, "Establecer como fondo de pantalla");
        Assert.Equal(TranslationHelper.Translation.SetCurrentImageAsWallpaper,
            "Establecer imagen actual como fondo de pantalla:");
        Assert.Equal(TranslationHelper.Translation.SetStarRating, "Establecer clasificación con estrellas");
        Assert.Equal(TranslationHelper.Translation.Settings, "Opciones");
        Assert.Equal(TranslationHelper.Translation.Shade, "Sombra");
        Assert.Equal(TranslationHelper.Translation.Sharpness, "Nitidez");
        Assert.Equal(TranslationHelper.Translation.Shift, "Shift");
        Assert.Equal(TranslationHelper.Translation.ShowAllSettingsWindow, "Ventana de mostrar todas las opciones");
        Assert.Equal(TranslationHelper.Translation.ShowBottomGallery, "Mostrar galería inferior");
        Assert.Equal(TranslationHelper.Translation.ShowBottomGalleryWhenUiIsHidden,
            "Mostrar galería inferior cuando la interfaz de usuario está oculta");
        Assert.Equal(TranslationHelper.Translation.ShowBottomToolbar, "Mostrar barra de herramientas inferior");
        Assert.Equal(TranslationHelper.Translation.ShowConfirmationOnEsc,
            "Mostrar cuadro de confirmación al presionar 'Esc'");
        Assert.Equal(TranslationHelper.Translation.ShowFadeInButtonsOnHover,
            "Mostrar botones de desvanecimiento al pasar el ratón");
        Assert.Equal(TranslationHelper.Translation.ShowFileSavingDialog,
            "Mostrar el cuadro de diálogo de guardar archivo");
        Assert.Equal(TranslationHelper.Translation.ShowImageGallery, "Mostrar galería de imáagenes");
        Assert.Equal(TranslationHelper.Translation.ShowImageInfo, "Mostrar Información de Imagen");
        Assert.Equal(TranslationHelper.Translation.ShowInFolder, "Mostrar en carpeta");
        Assert.Equal(TranslationHelper.Translation.ShowInfoWindow, "Mostrar ventana de información");
        Assert.Equal(TranslationHelper.Translation.ShowResizeWindow, "Mostrar la ventana de cambio de tamaño");
        Assert.Equal(TranslationHelper.Translation.ShowUI, "Mostrar interfaz");
        Assert.Equal(TranslationHelper.Translation.ShutterPriority, "Prioridad de obturador");
        Assert.Equal(TranslationHelper.Translation.SideBySide, "Vista lateral");
        Assert.Equal(TranslationHelper.Translation.SideBySideTooltip, "Mostrar las imagenes uno al lado del otro");
        Assert.Equal(TranslationHelper.Translation.Size, "Tamaño");
        Assert.Equal(TranslationHelper.Translation.SizeMp, "Tamaño (mp)");
        Assert.Equal(TranslationHelper.Translation.SizeTooltip, "Ingrese el tamaño deseado en píxeles o porcentaje.");
        Assert.Equal(TranslationHelper.Translation.Slideshow, "Presentación");
        Assert.Equal(TranslationHelper.Translation.Soft, "Suave");
        Assert.Equal(TranslationHelper.Translation.Software, "Software");
        Assert.Equal(TranslationHelper.Translation.SortFilesBy, "Ordenar archivos por");
        Assert.Equal(TranslationHelper.Translation.SourceFolder, "Carpeta de origen");
        Assert.Equal(TranslationHelper.Translation.Space, "Espacio");
        Assert.Equal(TranslationHelper.Translation.Square, "Cuadrado");
        Assert.Equal(TranslationHelper.Translation.Start, "Comienzo");
        Assert.Equal(TranslationHelper.Translation.StartSlideshow, "Iniciar presentación");
        Assert.Equal(TranslationHelper.Translation.StayCentered, "Mantener ventana centrada");
        Assert.Equal(TranslationHelper.Translation.StayTopMost, "Mostrar sobre otras ventanas");
        Assert.Equal(TranslationHelper.Translation.Stretch, "Estirar");
        Assert.Equal(TranslationHelper.Translation.StretchImage, "Estirar imagen");
        Assert.Equal(TranslationHelper.Translation.StrobeReturnLightDetected, "Luz de retorno de flash detectada");
        Assert.Equal(TranslationHelper.Translation.StrobeReturnLightNotDetected,
            "Luz de retorno de flash no detectada");
        Assert.Equal(TranslationHelper.Translation.Subject, "Asunto");
        Assert.Equal(TranslationHelper.Translation.Theme, "Tema");
        Assert.Equal(TranslationHelper.Translation.Thumbnail, "Miniatura");
        Assert.Equal(TranslationHelper.Translation.Tile, "Mosaico");
        Assert.Equal(TranslationHelper.Translation.Title, "Título");
        Assert.Equal(TranslationHelper.Translation.ToggleBackgroundColor, "Alternar color de fondo");
        Assert.Equal(TranslationHelper.Translation.ToggleFullscreen, "Alternar pantalla completa");
        Assert.Equal(TranslationHelper.Translation.ToggleLooping, "Alternar bucles");
        Assert.Equal(TranslationHelper.Translation.ToggleScroll, "Alternar desplazamiento");
        Assert.Equal(TranslationHelper.Translation.ToggleTaskbarProgress, "Mostrar el progreso en la barra de tareas");
        Assert.Equal(TranslationHelper.Translation.UnableToRender, "No se puede renderizar la imagen");
        Assert.Equal(TranslationHelper.Translation.Uncalibrated, "Sin calibrar");
        Assert.Equal(TranslationHelper.Translation.UnexpectedError, "Ha ocurrido un error desconocido");
        Assert.Equal(TranslationHelper.Translation.Unflip, "Deshacer volteado");
        Assert.Equal(TranslationHelper.Translation.Uniform, "Uniforme");
        Assert.Equal(TranslationHelper.Translation.UniformToFill, "UniformeParaRellenar");
        Assert.Equal(TranslationHelper.Translation.Unknown, "Desconocido");
        Assert.Equal(TranslationHelper.Translation.UnsupportedFile, "Archivo no compatible");
        Assert.Equal(TranslationHelper.Translation.Up, "Arriba");
        Assert.Equal(TranslationHelper.Translation.UsingMouse, "Usar ratón");
        Assert.Equal(TranslationHelper.Translation.UsingTouchpad, "Usar panel táctil");
        Assert.Equal(TranslationHelper.Translation.Version, "Versión:");
        Assert.Equal(TranslationHelper.Translation.ViewLicenseFile, "Ver archivo de licencia");
        Assert.Equal(TranslationHelper.Translation.WhiteBalance, "Balance de blancos");
        Assert.Equal(TranslationHelper.Translation.WhiteFluorescent, "Fluorescente blanco");
        Assert.Equal(TranslationHelper.Translation.Width, "Ancho");
        Assert.Equal(TranslationHelper.Translation.WidthAndHeight, "Ancho y alto");
        Assert.Equal(TranslationHelper.Translation.WindowManagement, "Gestión de ventana");
        Assert.Equal(TranslationHelper.Translation.WindowScaling, "Escalado de ventana");
        Assert.Equal(TranslationHelper.Translation.Zoom, "Zoom");
        Assert.Equal(TranslationHelper.Translation.ZoomIn, "Acercar");
        Assert.Equal(TranslationHelper.Translation.ZoomOut, "Alejar");
        Assert.Equal(TranslationHelper.Translation._1Star, "Clasificación de 1 estrella");
        Assert.Equal(TranslationHelper.Translation._2Star, "Clasificación de 2 estrella");
        Assert.Equal(TranslationHelper.Translation._3Star, "Clasificación de 3 estrella");
        Assert.Equal(TranslationHelper.Translation._4Star, "Clasificación de 4 estrella");
        Assert.Equal(TranslationHelper.Translation._5Star, "Clasificación de 5 estrella");
    }
}
