﻿using PicView.Core.Localization;

namespace PicView.Tests.LanguageTests;

public static class ChineseSimplifiedUnitTest
{
#pragma warning disable xUnit2000
    [Fact]
    public static async Task CheckChineseSimplifiedLanguage()
    {
        var exists = await TranslationHelper.LoadLanguage("zh-CN");
        Assert.True(exists);

        Assert.Equal(TranslationHelper.Translation.About, "关于");
        Assert.Equal(TranslationHelper.Translation.ActionProgram, "动作程序");
        Assert.Equal(TranslationHelper.Translation.AddedToClipboard, "已复制");
        Assert.Equal(TranslationHelper.Translation.AdditionalFunctions, "额外功能");
        Assert.Equal(TranslationHelper.Translation.AdjustNavSpeed, "按住键时调整速度");
        Assert.Equal(TranslationHelper.Translation.AdjustTimingForSlideshow, "调整幻灯片切换时间");
        Assert.Equal(TranslationHelper.Translation.AdjustTimingForZoom, "调整缩放动画速度");
        Assert.Equal(TranslationHelper.Translation.AdjustZoomLevel, "调整缩放级别");
        Assert.Equal(TranslationHelper.Translation.AdvanceBy100Images, "前进 100 张图片");
        Assert.Equal(TranslationHelper.Translation.AdvanceBy10Images, "前进 10 张图片");
        Assert.Equal(TranslationHelper.Translation.AllowZoomOut, "在最大尺寸时避免缩小图像");
        Assert.Equal(TranslationHelper.Translation.Alt, "Alt");
        Assert.Equal(TranslationHelper.Translation.Altitude, "海拔");
        Assert.Equal(TranslationHelper.Translation.AperturePriority, "光圈优先");
        Assert.Equal(TranslationHelper.Translation.Appearance, "外观");
        Assert.Equal(TranslationHelper.Translation.ApplicationShortcuts, "应用程序快捷键");
        Assert.Equal(TranslationHelper.Translation.ApplicationStartup, "应用程序启动");
        Assert.Equal(TranslationHelper.Translation.Apply, "应用");
        Assert.Equal(TranslationHelper.Translation.Applying, "正在应用");
        Assert.Equal(TranslationHelper.Translation.Ascending, "上升");
        Assert.Equal(TranslationHelper.Translation.AspectRatio, "纵横比");
        Assert.Equal(TranslationHelper.Translation.Authors, "作者");
        Assert.Equal(TranslationHelper.Translation.Auto, "自动");
        Assert.Equal(TranslationHelper.Translation.AutoFitWindow, "自动适应窗口");
        Assert.Equal(TranslationHelper.Translation.BadArchive, "无法处理压缩文件");
        Assert.Equal(TranslationHelper.Translation.Base64Image, "Base64 图片");
        Assert.Equal(TranslationHelper.Translation.BatchResize, "批量调整尺寸");
        Assert.Equal(TranslationHelper.Translation.BitDepth, "位深");
        Assert.Equal(TranslationHelper.Translation.BlackAndWhite, "黑白");
        Assert.Equal(TranslationHelper.Translation.Blur, "模糊");
        Assert.Equal(TranslationHelper.Translation.BottomGalleryItemSize, "底部图库项目");
        Assert.Equal(TranslationHelper.Translation.BottomGalleryThumbnailStretch, "缩略图拉伸在底部图库");
        Assert.Equal(TranslationHelper.Translation.Brightness, "亮度");
        Assert.Equal(TranslationHelper.Translation.CameraMaker, "相机制造商");
        Assert.Equal(TranslationHelper.Translation.CameraModel, "相机型号");
        Assert.Equal(TranslationHelper.Translation.Cancel, "取消");
        Assert.Equal(TranslationHelper.Translation.Center, "居中");
        Assert.Equal(TranslationHelper.Translation.CenterWindow, "居中窗口");
        Assert.Equal(TranslationHelper.Translation.Centimeters, "cm");
        Assert.Equal(TranslationHelper.Translation.ChangeBackground, "切换背景");
        Assert.Equal(TranslationHelper.Translation.ChangeBackgroundTooltip, "在透明背景的图像中改变背景颜色");
        Assert.Equal(TranslationHelper.Translation.ChangeKeybindingText, "点击文本框以更改按键绑定。按 Esc 键取消绑定。");
        Assert.Equal(TranslationHelper.Translation.ChangeKeybindingTooltip, "点击更改键绑定");
        Assert.Equal(TranslationHelper.Translation.ChangingThemeRequiresRestart, "* 切换主题需要重新启动应用程序");
        Assert.Equal(TranslationHelper.Translation.CheckForUpdates, "检查更新");
        Assert.Equal(TranslationHelper.Translation.ClipboardImage, "剪贴板图片");
        Assert.Equal(TranslationHelper.Translation.Close, "关闭");
        Assert.Equal(TranslationHelper.Translation.CloseApp, "关闭本应用程序");
        Assert.Equal(TranslationHelper.Translation.CloseGallery, "关闭相册");
        Assert.Equal(TranslationHelper.Translation.CloseWindowPrompt, "您想关闭窗口吗？");
        Assert.Equal(TranslationHelper.Translation.CloudyWeather, "多云");
        Assert.Equal(TranslationHelper.Translation.ColorPickerTool, "取色工具");
        Assert.Equal(TranslationHelper.Translation.ColorPickerToolTooltip, "从图片中选取颜色");
        Assert.Equal(TranslationHelper.Translation.ColorRepresentation, "颜色表示");
        Assert.Equal(TranslationHelper.Translation.ColorTone, "色调");
        Assert.Equal(TranslationHelper.Translation.CompressedBitsPixel, "压缩位像素");
        Assert.Equal(TranslationHelper.Translation.Compression, "压缩");
        Assert.Equal(TranslationHelper.Translation.Contrast, "对比度");
        Assert.Equal(TranslationHelper.Translation.ConvertTo, "转换为");
        Assert.Equal(TranslationHelper.Translation.ConvertedToBase64, "已复制为 base64");
        Assert.Equal(TranslationHelper.Translation.CoolWhiteFluorescent, "冷白荧光");
        Assert.Equal(TranslationHelper.Translation.CopiedImage, "已将图像复制至剪贴板");
        Assert.Equal(TranslationHelper.Translation.Copy, "复制");
        Assert.Equal(TranslationHelper.Translation.CopyFile, "复制文件");
        Assert.Equal(TranslationHelper.Translation.CopyImage, "复制图像");
        Assert.Equal(TranslationHelper.Translation.CopyImageTooltip, "将图片复制至剪贴板");
        Assert.Equal(TranslationHelper.Translation.Copyright, "版权");
        Assert.Equal(TranslationHelper.Translation.Created, "创建时间");
        Assert.Equal(TranslationHelper.Translation.CreationTime, "创建时间");
        Assert.Equal(TranslationHelper.Translation.CreativeProgram, "创意程序");
        Assert.Equal(TranslationHelper.Translation.Credits, "鸣谢");
        Assert.Equal(TranslationHelper.Translation.Crop, "裁剪");
        Assert.Equal(TranslationHelper.Translation.CropMessage, "Esc 键关闭，Enter 键保存");
        Assert.Equal(TranslationHelper.Translation.CropPicture, "裁剪图片");
        Assert.Equal(TranslationHelper.Translation.Ctrl, "Ctrl");
        Assert.Equal(TranslationHelper.Translation.CtrlToZoom, "Ctrl + 鼠标滚轮缩放，鼠标滚轮导航");
        Assert.Equal(TranslationHelper.Translation.Cut, "剪切");
        Assert.Equal(TranslationHelper.Translation.DarkTheme, "深色主题");
        Assert.Equal(TranslationHelper.Translation.Date, "时间");
        Assert.Equal(TranslationHelper.Translation.DateTaken, "拍摄日期");
        Assert.Equal(TranslationHelper.Translation.DayWhiteFluorescent, "白天白光");
        Assert.Equal(TranslationHelper.Translation.Daylight, "白天");
        Assert.Equal(TranslationHelper.Translation.DaylightFluorescent, "白天荧光");
        Assert.Equal(TranslationHelper.Translation.Del, "Del");
        Assert.Equal(TranslationHelper.Translation.DeleteFile, "删除文件");
        Assert.Equal(TranslationHelper.Translation.DeleteFilePermanently, "您確定要永久刪除嗎");
        Assert.Equal(TranslationHelper.Translation.DeletedFile, "已删除文件");
        Assert.Equal(TranslationHelper.Translation.Descending, "下降");
        Assert.Equal(TranslationHelper.Translation.DigitalZoom, "数码变焦");
        Assert.Equal(TranslationHelper.Translation.DisableFadeInButtonsOnHover, "禁用鼠标悬停时的淡入按钮");
        Assert.Equal(TranslationHelper.Translation.DiskSize, "磁盘大小");
        Assert.Equal(TranslationHelper.Translation.DoubleClick, "双击");
        Assert.Equal(TranslationHelper.Translation.Down, "下");
        Assert.Equal(TranslationHelper.Translation.Dpi, "DPI");
        Assert.Equal(TranslationHelper.Translation.DragFileTo, "将文件拖拽至 Windows 资源管理器或者其他应用程序/浏览器");
        Assert.Equal(TranslationHelper.Translation.DragImage, "拖拽图片");
        Assert.Equal(TranslationHelper.Translation.DropToLoad, "拖放以打开图像");
        Assert.Equal(TranslationHelper.Translation.DuplicateFile, "复制文件");
        Assert.Equal(TranslationHelper.Translation.Effects, "特效");
        Assert.Equal(TranslationHelper.Translation.EffectsTooltip, "显示图像特效窗口");
        Assert.Equal(TranslationHelper.Translation.Enter, "Enter");
        Assert.Equal(TranslationHelper.Translation.Esc, "Esc");
        Assert.Equal(TranslationHelper.Translation.EscCloseTooltip, "关闭当前打开的窗口和菜单");
        Assert.Equal(TranslationHelper.Translation.ExifVersion, "Exif 版本");
        Assert.Equal(TranslationHelper.Translation.ExpandedGalleryItemSize, "展开的图库项目");
        Assert.Equal(TranslationHelper.Translation.ExposureBias, "曝光补偿");
        Assert.Equal(TranslationHelper.Translation.ExposureProgram, "曝光程序");
        Assert.Equal(TranslationHelper.Translation.ExposureTime, "曝光时间");
        Assert.Equal(TranslationHelper.Translation.FNumber, "F 数");
        Assert.Equal(TranslationHelper.Translation.File, "文件");
        Assert.Equal(TranslationHelper.Translation.FileCopy, "文件已复制");
        Assert.Equal(TranslationHelper.Translation.FileCopyPath, "复制文件路径");
        Assert.Equal(TranslationHelper.Translation.FileCopyPathMessage, "文件路径已复制");
        Assert.Equal(TranslationHelper.Translation.FileCutMessage, "文件已剪切");
        Assert.Equal(TranslationHelper.Translation.FileExtension, "文件拓展名");
        Assert.Equal(TranslationHelper.Translation.FileManagement, "文件管理");
        Assert.Equal(TranslationHelper.Translation.FileName, "文件名");
        Assert.Equal(TranslationHelper.Translation.FilePaste, "粘贴");
        Assert.Equal(TranslationHelper.Translation.FileProperties, "文件属性");
        Assert.Equal(TranslationHelper.Translation.FileSize, "文件大小");
        Assert.Equal(TranslationHelper.Translation.Files, "文件");
        Assert.Equal(TranslationHelper.Translation.Fill, "填充");
        Assert.Equal(TranslationHelper.Translation.FillHeight, "⇔ 适应高度");
        Assert.Equal(TranslationHelper.Translation.FillSquare, "填充正方形");
        Assert.Equal(TranslationHelper.Translation.FineWeather, "晴天");
        Assert.Equal(TranslationHelper.Translation.FirstImage, "第一张图片");
        Assert.Equal(TranslationHelper.Translation.Fit, "适应");
        Assert.Equal(TranslationHelper.Translation.FitToWindow, "适应窗口/图片");
        Assert.Equal(TranslationHelper.Translation.Flash, "闪光灯");
        Assert.Equal(TranslationHelper.Translation.FlashDidNotFire, "闪光未触发");
        Assert.Equal(TranslationHelper.Translation.FlashEnergy, "闪光能量");
        Assert.Equal(TranslationHelper.Translation.FlashFired, "闪光已触发");
        Assert.Equal(TranslationHelper.Translation.FlashMode, "闪光模式");
        Assert.Equal(TranslationHelper.Translation.Flip, "水平翻转");
        Assert.Equal(TranslationHelper.Translation.Flipped, "已水平翻转");
        Assert.Equal(TranslationHelper.Translation.Fluorescent, "荧光");
        Assert.Equal(TranslationHelper.Translation.FocalLength, "焦距");
        Assert.Equal(TranslationHelper.Translation.FocalLength35mm, "35mm 焦距");
        Assert.Equal(TranslationHelper.Translation.Folder, "文件夹");
        Assert.Equal(TranslationHelper.Translation.Forward, "向前");
        Assert.Equal(TranslationHelper.Translation.Fstop, "F 值");
        Assert.Equal(TranslationHelper.Translation.FullPath, "完整路径");
        Assert.Equal(TranslationHelper.Translation.Fullscreen, "全屏");
        Assert.Equal(TranslationHelper.Translation.GallerySettings, "图库设置");
        Assert.Equal(TranslationHelper.Translation.GalleryThumbnailStretch, "图库缩略图拉伸");
        Assert.Equal(TranslationHelper.Translation.GeneralSettings, "常规设置");
        Assert.Equal(TranslationHelper.Translation.GenerateThumbnails, "生成缩略图");
        Assert.Equal(TranslationHelper.Translation.GithubRepo, "Github 仓库");
        Assert.Equal(TranslationHelper.Translation.GlassTheme, "玻璃主题");
        Assert.Equal(TranslationHelper.Translation.GoBackBy100Images, "后退 100 张图片");
        Assert.Equal(TranslationHelper.Translation.GoBackBy10Images, "后退 10 张图片");
        Assert.Equal(TranslationHelper.Translation.GoToImageAtSpecifiedIndex, "跳转至指定图片");
        Assert.Equal(TranslationHelper.Translation.Hard, "硬");
        Assert.Equal(TranslationHelper.Translation.Height, "高度");
        Assert.Equal(TranslationHelper.Translation.HideBottomGallery, "收起底部图");
        Assert.Equal(TranslationHelper.Translation.HideBottomToolbar, "隐藏底部工具栏");
        Assert.Equal(TranslationHelper.Translation.HideUI, "隐藏界面");
        Assert.Equal(TranslationHelper.Translation.High, "高");
        Assert.Equal(TranslationHelper.Translation.HighQuality, "高质量");
        Assert.Equal(TranslationHelper.Translation.HighlightColor, "主题高亮色");
        Assert.Equal(TranslationHelper.Translation.ISOSpeed, "ISO 速度");
        Assert.Equal(TranslationHelper.Translation.IconsUsed, "图标来自：");
        Assert.Equal(TranslationHelper.Translation.Image, "图片");
        Assert.Equal(TranslationHelper.Translation.ImageAliasing, "图像混淆");
        Assert.Equal(TranslationHelper.Translation.ImageControl, "图片调整");
        Assert.Equal(TranslationHelper.Translation.ImageInfo, "图像信息");
        Assert.Equal(TranslationHelper.Translation.Inches, "in");
        Assert.Equal(TranslationHelper.Translation.InfoWindow, "信息窗口");
        Assert.Equal(TranslationHelper.Translation.InfoWindowTitle, "信息与快捷键");
        Assert.Equal(TranslationHelper.Translation.InterfaceConfiguration, "界面配置");
        Assert.Equal(TranslationHelper.Translation.Landscape, "横向");
        Assert.Equal(TranslationHelper.Translation.Language, "语言");
        Assert.Equal(TranslationHelper.Translation.LastAccessTime, "访问时间");
        Assert.Equal(TranslationHelper.Translation.LastImage, "最后一张图片");
        Assert.Equal(TranslationHelper.Translation.LastWriteTime, "修改时间");
        Assert.Equal(TranslationHelper.Translation.Latitude, "纬度");
        Assert.Equal(TranslationHelper.Translation.Left, "左");
        Assert.Equal(TranslationHelper.Translation.LensMaker, "镜头制造商");
        Assert.Equal(TranslationHelper.Translation.LensModel, "镜头型号");
        Assert.Equal(TranslationHelper.Translation.LightSource, "光源");
        Assert.Equal(TranslationHelper.Translation.LightTheme, "亮色主题");
        Assert.Equal(TranslationHelper.Translation.Lighting, "照明");
        Assert.Equal(TranslationHelper.Translation.Loading, "正在加载...");
        Assert.Equal(TranslationHelper.Translation.Longitude, "经度");
        Assert.Equal(TranslationHelper.Translation.Looping, "循环");
        Assert.Equal(TranslationHelper.Translation.LoopingDisabled, "已禁用循环");
        Assert.Equal(TranslationHelper.Translation.LoopingEnabled, "已启用循环");
        Assert.Equal(TranslationHelper.Translation.Lossless, "无损");
        Assert.Equal(TranslationHelper.Translation.Lossy, "有损");
        Assert.Equal(TranslationHelper.Translation.Low, "低");
        Assert.Equal(TranslationHelper.Translation.Manual, "手动");
        Assert.Equal(TranslationHelper.Translation.MaxAperture, "最大光圈");
        Assert.Equal(TranslationHelper.Translation.Maximize, "最大化");
        Assert.Equal(TranslationHelper.Translation.MegaPixels, "mp");
        Assert.Equal(TranslationHelper.Translation.Meter, "米");
        Assert.Equal(TranslationHelper.Translation.MeteringMode, "测光模式");
        Assert.Equal(TranslationHelper.Translation.Minimize, "最小化");
        Assert.Equal(TranslationHelper.Translation.MiscSettings, "其他设置");
        Assert.Equal(TranslationHelper.Translation.Modified, "修改时间");
        Assert.Equal(TranslationHelper.Translation.MouseDrag, "鼠标拖拽");
        Assert.Equal(TranslationHelper.Translation.MouseKeyBack, "鼠标拓展键 向后键");
        Assert.Equal(TranslationHelper.Translation.MouseKeyForward, "鼠标拓展键 向前键");
        Assert.Equal(TranslationHelper.Translation.MouseWheel, "鼠标滚轮");
        Assert.Equal(TranslationHelper.Translation.MoveWindow, "移动窗口");
        Assert.Equal(TranslationHelper.Translation.Navigation, "导航");
        Assert.Equal(TranslationHelper.Translation.NearestNeighbor, "最近邻");
        Assert.Equal(TranslationHelper.Translation.NegativeColors, "反色");
        Assert.Equal(TranslationHelper.Translation.NewWindow, "新窗口");
        Assert.Equal(TranslationHelper.Translation.NextFolder, "导航到下一个文件夹");
        Assert.Equal(TranslationHelper.Translation.NextImage, "下一张图片");
        Assert.Equal(TranslationHelper.Translation.NoChange, "无变化");
        Assert.Equal(TranslationHelper.Translation.NoConversion, "不转换");
        Assert.Equal(TranslationHelper.Translation.NoImage, "请打开图片");
        Assert.Equal(TranslationHelper.Translation.NoImages, "无图像");
        Assert.Equal(TranslationHelper.Translation.NoResize, "禁止缩放");
        Assert.Equal(TranslationHelper.Translation.None, "没有任何");
        Assert.Equal(TranslationHelper.Translation.Normal, "正常");
        Assert.Equal(TranslationHelper.Translation.NormalWindow, "正常窗口");
        Assert.Equal(TranslationHelper.Translation.NotDefined, "未定义");
        Assert.Equal(TranslationHelper.Translation.NumpadMinus, "数字键盘 -");
        Assert.Equal(TranslationHelper.Translation.NumpadPlus, "数字键盘 +");
        Assert.Equal(TranslationHelper.Translation.OldMovie, "老电影");
        Assert.Equal(TranslationHelper.Translation.Open, "打开");
        Assert.Equal(TranslationHelper.Translation.OpenFileDialog, "打开文件对话框");
        Assert.Equal(TranslationHelper.Translation.OpenInSameWindow, "在同一窗口中打开文件");
        Assert.Equal(TranslationHelper.Translation.OpenLastFile, "打开最后一个文件");
        Assert.Equal(TranslationHelper.Translation.OpenWith, "打开使用");
        Assert.Equal(TranslationHelper.Translation.OptimizeImage, "优化图像");
        Assert.Equal(TranslationHelper.Translation.Orientation, "方向");
        Assert.Equal(TranslationHelper.Translation.OutputFolder, "输出文件夹");
        Assert.Equal(TranslationHelper.Translation.Pan, "平移");
        Assert.Equal(TranslationHelper.Translation.PasswordArchive, "暂不支持带密码的压缩文件");
        Assert.Equal(TranslationHelper.Translation.PasteImageFromClipholder, "从剪贴板中粘贴图片");
        Assert.Equal(TranslationHelper.Translation.PencilSketch, "铅笔素描");
        Assert.Equal(TranslationHelper.Translation.PercentComplete, "% 已完成…");
        Assert.Equal(TranslationHelper.Translation.Percentage, "百分比");
        Assert.Equal(TranslationHelper.Translation.PermanentlyDelete, "永久删除");
        Assert.Equal(TranslationHelper.Translation.PhotometricInterpretation, "光度学解释");
        Assert.Equal(TranslationHelper.Translation.Pixels, "px");
        Assert.Equal(TranslationHelper.Translation.Portrait, "纵向");
        Assert.Equal(TranslationHelper.Translation.PressKey, "按键...");
        Assert.Equal(TranslationHelper.Translation.PrevFolder, "导航到上一个文件夹");
        Assert.Equal(TranslationHelper.Translation.PrevImage, "上一张图片");
        Assert.Equal(TranslationHelper.Translation.Print, "打印");
        Assert.Equal(TranslationHelper.Translation.PrintSizeCm, "打印大小（厘米）");
        Assert.Equal(TranslationHelper.Translation.PrintSizeIn, "打印大小（英尺）");
        Assert.Equal(TranslationHelper.Translation.Quality, "质量");
        Assert.Equal(TranslationHelper.Translation.Random, "随机");
        Assert.Equal(TranslationHelper.Translation.RecentFiles, "最近文件");
        Assert.Equal(TranslationHelper.Translation.RedEyeReduction, "减少红眼");
        Assert.Equal(TranslationHelper.Translation.Reload, "重新载入");
        Assert.Equal(TranslationHelper.Translation.RemoveStarRating, "移除评分");
        Assert.Equal(TranslationHelper.Translation.RenameFile, "重命名文件");
        Assert.Equal(TranslationHelper.Translation.Reset, "重置");
        Assert.Equal(TranslationHelper.Translation.ResetButtonText, "重置为默认设置");
        Assert.Equal(TranslationHelper.Translation.ResetZoom, "重设缩放");
        Assert.Equal(TranslationHelper.Translation.Resize, "调整尺寸");
        Assert.Equal(TranslationHelper.Translation.ResizeImage, "调整图像大小");
        Assert.Equal(TranslationHelper.Translation.Resolution, "分辨率");
        Assert.Equal(TranslationHelper.Translation.ResolutionUnit, "分辨率单位");
        Assert.Equal(TranslationHelper.Translation.RestartApp, "重新启动应用程序");
        Assert.Equal(TranslationHelper.Translation.RestoreDown, "退出全屏");
        Assert.Equal(TranslationHelper.Translation.Reverse, "逆转");
        Assert.Equal(TranslationHelper.Translation.Right, "右");
        Assert.Equal(TranslationHelper.Translation.RotateLeft, "向左旋转");
        Assert.Equal(TranslationHelper.Translation.RotateRight, "向右旋转");
        Assert.Equal(TranslationHelper.Translation.Rotated, "已旋转");
        Assert.Equal(TranslationHelper.Translation.Saturation, "饱和度");
        Assert.Equal(TranslationHelper.Translation.Save, "保存");
        Assert.Equal(TranslationHelper.Translation.SaveAs, "另存为");
        Assert.Equal(TranslationHelper.Translation.SavingFileFailed, "文件保存失败");
        Assert.Equal(TranslationHelper.Translation.ScrollAndRotate, "滚动和旋转");
        Assert.Equal(TranslationHelper.Translation.ScrollDirection, "滚动方向");
        Assert.Equal(TranslationHelper.Translation.ScrollDown, "向下滑动");
        Assert.Equal(TranslationHelper.Translation.ScrollToBottom, "滑动到底部");
        Assert.Equal(TranslationHelper.Translation.ScrollToTop, "滑动到顶部");
        Assert.Equal(TranslationHelper.Translation.ScrollToZoom, "鼠标滚轮缩放，Ctrl + 鼠标滚轮导航");
        Assert.Equal(TranslationHelper.Translation.ScrollUp, "向上滑动");
        Assert.Equal(TranslationHelper.Translation.Scrolling, "滚动");
        Assert.Equal(TranslationHelper.Translation.ScrollingDisabled, "已禁用滚动");
        Assert.Equal(TranslationHelper.Translation.ScrollingEnabled, "已启用滚动");
        Assert.Equal(TranslationHelper.Translation.SearchSubdirectory, "包含子目录中的图片");
        Assert.Equal(TranslationHelper.Translation.SecAbbreviation, "秒");
        Assert.Equal(TranslationHelper.Translation.SelectAll, "全选");
        Assert.Equal(TranslationHelper.Translation.SelectGalleryThumb, "选择画廊缩略图");
        Assert.Equal(TranslationHelper.Translation.SendCurrentImageToRecycleBin, "将当前图片移至回收站");
        Assert.Equal(TranslationHelper.Translation.SentFileToRecycleBin, "将文件移至回收站");
        Assert.Equal(TranslationHelper.Translation.SetAs, "设置为…");
        Assert.Equal(TranslationHelper.Translation.SetAsLockScreenImage, "设置为锁屏图像");
        Assert.Equal(TranslationHelper.Translation.SetAsWallpaper, "设置为背景");
        Assert.Equal(TranslationHelper.Translation.SetCurrentImageAsWallpaper, "将当前图像设置为壁纸：");
        Assert.Equal(TranslationHelper.Translation.SetStarRating, "设置星级评分");
        Assert.Equal(TranslationHelper.Translation.Settings, "设置");
        Assert.Equal(TranslationHelper.Translation.Shade, "阴影");
        Assert.Equal(TranslationHelper.Translation.Sharpness, "锐度");
        Assert.Equal(TranslationHelper.Translation.Shift, "Shift");
        Assert.Equal(TranslationHelper.Translation.ShowAllSettingsWindow, "显示设置窗口");
        Assert.Equal(TranslationHelper.Translation.ShowBottomGallery, "查看底部图");
        Assert.Equal(TranslationHelper.Translation.ShowBottomGalleryWhenUiIsHidden, "当用户界面隐藏时显示底部图库");
        Assert.Equal(TranslationHelper.Translation.ShowBottomToolbar, "查看底部工具栏");
        Assert.Equal(TranslationHelper.Translation.ShowConfirmationOnEsc, "按下 'Esc' 时显示确认对话框");
        Assert.Equal(TranslationHelper.Translation.ShowFadeInButtonsOnHover, "將鼠標懸停時顯示淡入按鈕");
        Assert.Equal(TranslationHelper.Translation.ShowFileSavingDialog, "查看文件保存对话框");
        Assert.Equal(TranslationHelper.Translation.ShowImageGallery, "显示图片相册");
        Assert.Equal(TranslationHelper.Translation.ShowImageInfo, "显示图像信息");
        Assert.Equal(TranslationHelper.Translation.ShowInFolder, "在文件夹中显示");
        Assert.Equal(TranslationHelper.Translation.ShowInfoWindow, "显示应用程序信息窗口");
        Assert.Equal(TranslationHelper.Translation.ShowResizeWindow, "显示调整尺寸窗口");
        Assert.Equal(TranslationHelper.Translation.ShowUI, "顯示介面");
        Assert.Equal(TranslationHelper.Translation.ShutterPriority, "快门优先");
        Assert.Equal(TranslationHelper.Translation.SideBySide, "并排");
        Assert.Equal(TranslationHelper.Translation.SideBySideTooltip, "并排显示图像");
        Assert.Equal(TranslationHelper.Translation.Size, "大小");
        Assert.Equal(TranslationHelper.Translation.SizeMp, "大小（兆像素）");
        Assert.Equal(TranslationHelper.Translation.SizeTooltip, "以像素或百分比输入所需的尺寸。");
        Assert.Equal(TranslationHelper.Translation.Slideshow, "幻灯片");
        Assert.Equal(TranslationHelper.Translation.Soft, "柔和");
        Assert.Equal(TranslationHelper.Translation.Software, "软件");
        Assert.Equal(TranslationHelper.Translation.SortFilesBy, "排序方式");
        Assert.Equal(TranslationHelper.Translation.SourceFolder, "源文件夹");
        Assert.Equal(TranslationHelper.Translation.Space, "Space");
        Assert.Equal(TranslationHelper.Translation.Square, "正方形");
        Assert.Equal(TranslationHelper.Translation.Start, "启动");
        Assert.Equal(TranslationHelper.Translation.StartSlideshow, "放映幻灯片");
        Assert.Equal(TranslationHelper.Translation.StayCentered, "保持窗口居中");
        Assert.Equal(TranslationHelper.Translation.StayTopMost, "窗口置顶");
        Assert.Equal(TranslationHelper.Translation.Stretch, "拉伸");
        Assert.Equal(TranslationHelper.Translation.StretchImage, "拉伸图像");
        Assert.Equal(TranslationHelper.Translation.StrobeReturnLightDetected, "检测到闪光灯返回光");
        Assert.Equal(TranslationHelper.Translation.StrobeReturnLightNotDetected, "未检测到闪光灯返回光");
        Assert.Equal(TranslationHelper.Translation.Subject, "主题");
        Assert.Equal(TranslationHelper.Translation.Theme, "主题");
        Assert.Equal(TranslationHelper.Translation.Thumbnail, "缩略图");
        Assert.Equal(TranslationHelper.Translation.Tile, "平铺");
        Assert.Equal(TranslationHelper.Translation.Title, "标题");
        Assert.Equal(TranslationHelper.Translation.ToggleBackgroundColor, "切换背景色");
        Assert.Equal(TranslationHelper.Translation.ToggleFullscreen, "切换全屏");
        Assert.Equal(TranslationHelper.Translation.ToggleLooping, "切换循环");
        Assert.Equal(TranslationHelper.Translation.ToggleScroll, "切换滚动");
        Assert.Equal(TranslationHelper.Translation.ToggleTaskbarProgress, "显示任务栏进度");
        Assert.Equal(TranslationHelper.Translation.UnableToRender, "无法渲染图像");
        Assert.Equal(TranslationHelper.Translation.Uncalibrated, "未校准");
        Assert.Equal(TranslationHelper.Translation.UnexpectedError, "发生未知错误");
        Assert.Equal(TranslationHelper.Translation.Unflip, "取消水平翻转");
        Assert.Equal(TranslationHelper.Translation.Uniform, "均匀");
        Assert.Equal(TranslationHelper.Translation.UniformToFill, "均匀填充");
        Assert.Equal(TranslationHelper.Translation.Unknown, "未知");
        Assert.Equal(TranslationHelper.Translation.UnsupportedFile, "不支持的文件格式");
        Assert.Equal(TranslationHelper.Translation.Up, "上");
        Assert.Equal(TranslationHelper.Translation.UsingMouse, "使用鼠标");
        Assert.Equal(TranslationHelper.Translation.UsingTouchpad, "使用触控板");
        Assert.Equal(TranslationHelper.Translation.Version, "版本：");
        Assert.Equal(TranslationHelper.Translation.ViewLicenseFile, "查看开源协议");
        Assert.Equal(TranslationHelper.Translation.WhiteBalance, "白平衡");
        Assert.Equal(TranslationHelper.Translation.WhiteFluorescent, "白光荧光");
        Assert.Equal(TranslationHelper.Translation.Width, "宽度");
        Assert.Equal(TranslationHelper.Translation.WidthAndHeight, "宽度和高度");
        Assert.Equal(TranslationHelper.Translation.WindowManagement, "窗口管理");
        Assert.Equal(TranslationHelper.Translation.WindowScaling, "窗口缩放");
        Assert.Equal(TranslationHelper.Translation.Zoom, "缩放");
        Assert.Equal(TranslationHelper.Translation.ZoomIn, "放大");
        Assert.Equal(TranslationHelper.Translation.ZoomOut, "缩小");
        Assert.Equal(TranslationHelper.Translation._1Star, "1星评分");
        Assert.Equal(TranslationHelper.Translation._2Star, "2星评分");
        Assert.Equal(TranslationHelper.Translation._3Star, "3星评分");
        Assert.Equal(TranslationHelper.Translation._4Star, "4星评分");
        Assert.Equal(TranslationHelper.Translation._5Star, "5星评分");
    }
}