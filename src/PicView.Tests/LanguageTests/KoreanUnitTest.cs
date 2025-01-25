﻿using PicView.Core.Localization;

namespace PicView.Tests.LanguageTests;

public static class KoreanUnitTest
{
#pragma warning disable xUnit2000
    [Fact]
    public static async Task CheckKoreanLanguage()
    {
        var exists = await TranslationHelper.LoadLanguage("ko");
        Assert.True(exists);

        Assert.Equal(TranslationHelper.Translation.About, "정보");
        Assert.Equal(TranslationHelper.Translation.ActionProgram, "실행 프로그램");
        Assert.Equal(TranslationHelper.Translation.AddedToClipboard, "클립보드에 추가됨");
        Assert.Equal(TranslationHelper.Translation.AdditionalFunctions, "부가 기능");
        Assert.Equal(TranslationHelper.Translation.AdjustNavSpeed, "키를 누르고 있을 때 속도 조정");
        Assert.Equal(TranslationHelper.Translation.AdjustTimingForSlideshow, "슬라이드쇼 시간 조정");
        Assert.Equal(TranslationHelper.Translation.AdjustTimingForZoom, "확대/축소 속도 조정");
        Assert.Equal(TranslationHelper.Translation.AdjustZoomLevel, "확대/축소 수준 조정");
        Assert.Equal(TranslationHelper.Translation.AdvanceBy100Images, "이미지 100개 앞으로 이동");
        Assert.Equal(TranslationHelper.Translation.AdvanceBy10Images, "이미지 10개 앞으로 이동");
        Assert.Equal(TranslationHelper.Translation.AllowZoomOut, "이미지가 이미 최대 크기일 때는 축소하지 않음");
        Assert.Equal(TranslationHelper.Translation.Alt, "Alt");
        Assert.Equal(TranslationHelper.Translation.Altitude, "고도");
        Assert.Equal(TranslationHelper.Translation.AperturePriority, "조리개 우선");
        Assert.Equal(TranslationHelper.Translation.Appearance, "모양");
        Assert.Equal(TranslationHelper.Translation.ApplicationShortcuts, "응용 프로그램 단축키");
        Assert.Equal(TranslationHelper.Translation.ApplicationStartup, "응용 프로그램 시작");
        Assert.Equal(TranslationHelper.Translation.Apply, "적용");
        Assert.Equal(TranslationHelper.Translation.Applying, "적용");
        Assert.Equal(TranslationHelper.Translation.Ascending, "오름차순");
        Assert.Equal(TranslationHelper.Translation.AspectRatio, "가로 세로 비율");
        Assert.Equal(TranslationHelper.Translation.Authors, "저자");
        Assert.Equal(TranslationHelper.Translation.Auto, "자동");
        Assert.Equal(TranslationHelper.Translation.AutoFitWindow, "자동 맞춤 창");
        Assert.Equal(TranslationHelper.Translation.BadArchive, "압축파일을 처리할 수 없습니다");
        Assert.Equal(TranslationHelper.Translation.Base64Image, "Base64 이미지");
        Assert.Equal(TranslationHelper.Translation.BatchResize, "일괄 크기 조정");
        Assert.Equal(TranslationHelper.Translation.BitDepth, "비트 깊이");
        Assert.Equal(TranslationHelper.Translation.BlackAndWhite, "흑백");
        Assert.Equal(TranslationHelper.Translation.Blur, "흐림");
        Assert.Equal(TranslationHelper.Translation.BottomGalleryItemSize, "하단 갤러리의 썸네일 크기");
        Assert.Equal(TranslationHelper.Translation.BottomGalleryThumbnailStretch, "하단 갤러리의 슬라이드쇼 크기");
        Assert.Equal(TranslationHelper.Translation.Brightness, "밝기");
        Assert.Equal(TranslationHelper.Translation.CameraMaker, "카메라 제조사");
        Assert.Equal(TranslationHelper.Translation.CameraModel, "카메라 모델");
        Assert.Equal(TranslationHelper.Translation.Cancel, "취소");
        Assert.Equal(TranslationHelper.Translation.Center, "가운데");
        Assert.Equal(TranslationHelper.Translation.CenterWindow, "창 가운데");
        Assert.Equal(TranslationHelper.Translation.Centimeters, "센티미터");
        Assert.Equal(TranslationHelper.Translation.ChangeBackground, "배경 변경");
        Assert.Equal(TranslationHelper.Translation.ChangeBackgroundTooltip, "배경이 투명한 이미지의 배경색 변경");
        Assert.Equal(TranslationHelper.Translation.ChangeKeybindingText,
            "키 바인딩을 변경하려면 텍스트 상자를 클릭합니다. Esc 키를 누르면 키 바인딩이 해제됩니다.");
        Assert.Equal(TranslationHelper.Translation.ChangeKeybindingTooltip, "키 바인딩을 변경하려면 클릭");
        Assert.Equal(TranslationHelper.Translation.ChangingThemeRequiresRestart, "* 테마 변경 시 재시작 필요");
        Assert.Equal(TranslationHelper.Translation.CheckForUpdates, "업데이트 확인");
        Assert.Equal(TranslationHelper.Translation.ClipboardImage, "클립보드 이미지");
        Assert.Equal(TranslationHelper.Translation.Close, "닫기");
        Assert.Equal(TranslationHelper.Translation.CloseApp, "전체 응용 프로그램 닫기");
        Assert.Equal(TranslationHelper.Translation.CloseGallery, "갤러리 닫기");
        Assert.Equal(TranslationHelper.Translation.CloseWindowPrompt, "창을 닫으시겠습니까?");
        Assert.Equal(TranslationHelper.Translation.CloudyWeather, "흐린 날씨");
        Assert.Equal(TranslationHelper.Translation.ColorPickerTool, "색 선택 도구");
        Assert.Equal(TranslationHelper.Translation.ColorPickerToolTooltip, "이미지에서 색 선택");
        Assert.Equal(TranslationHelper.Translation.ColorRepresentation, "색상 표현");
        Assert.Equal(TranslationHelper.Translation.ColorTone, "색조");
        Assert.Equal(TranslationHelper.Translation.CompressedBitsPixel, "픽셀당 압축 비트");
        Assert.Equal(TranslationHelper.Translation.Compression, "압축");
        Assert.Equal(TranslationHelper.Translation.Contrast, "대비");
        Assert.Equal(TranslationHelper.Translation.ConvertTo, "변환");
        Assert.Equal(TranslationHelper.Translation.ConvertedToBase64, "base64로 변환");
        Assert.Equal(TranslationHelper.Translation.CoolWhiteFluorescent, "차가운 흰색 형광등");
        Assert.Equal(TranslationHelper.Translation.CopiedImage, "클립보드에 이미지 복사");
        Assert.Equal(TranslationHelper.Translation.Copy, "복사");
        Assert.Equal(TranslationHelper.Translation.CopyFile, "파일 복사");
        Assert.Equal(TranslationHelper.Translation.CopyImage, "이미지 복사");
        Assert.Equal(TranslationHelper.Translation.CopyImageTooltip, "Windows 클립보드 이미지로 복사");
        Assert.Equal(TranslationHelper.Translation.Copyright, "저작권");
        Assert.Equal(TranslationHelper.Translation.Created, "생성 날짜");
        Assert.Equal(TranslationHelper.Translation.CreationTime, "생성 날짜");
        Assert.Equal(TranslationHelper.Translation.CreativeProgram, "창작 프로그램");
        Assert.Equal(TranslationHelper.Translation.Credits, "크레딧");
        Assert.Equal(TranslationHelper.Translation.Crop, "자르기");
        Assert.Equal(TranslationHelper.Translation.CropMessage, "닫으려면 Esc를, 저장하려면 Enter를 누르세요");
        Assert.Equal(TranslationHelper.Translation.CropPicture, "사진 자르기");
        Assert.Equal(TranslationHelper.Translation.Ctrl, "Ctrl");
        Assert.Equal(TranslationHelper.Translation.CtrlToZoom, "확대/축소하려면 Ctrl, 탐색하려면 스크롤");
        Assert.Equal(TranslationHelper.Translation.Cut, "잘라내기");
        Assert.Equal(TranslationHelper.Translation.DarkTheme, "어두운 테마");
        Assert.Equal(TranslationHelper.Translation.Date, "날짜");
        Assert.Equal(TranslationHelper.Translation.DateTaken, "촬영 날짜");
        Assert.Equal(TranslationHelper.Translation.DayWhiteFluorescent, "일광 백색 형광등");
        Assert.Equal(TranslationHelper.Translation.Daylight, "일광");
        Assert.Equal(TranslationHelper.Translation.DaylightFluorescent, "일광 형광등");
        Assert.Equal(TranslationHelper.Translation.Del, "Del");
        Assert.Equal(TranslationHelper.Translation.DeleteFile, "파일 삭제");
        Assert.Equal(TranslationHelper.Translation.DeleteFilePermanently, "영구적으로 삭제하시겠습니까");
        Assert.Equal(TranslationHelper.Translation.DeletedFile, "삭제된 파일");
        Assert.Equal(TranslationHelper.Translation.Descending, "내림차순");
        Assert.Equal(TranslationHelper.Translation.DigitalZoom, "디지털 줌");
        Assert.Equal(TranslationHelper.Translation.DisableFadeInButtonsOnHover, "마우스 오버 시 페이드인 버튼 비활성화");
        Assert.Equal(TranslationHelper.Translation.DiskSize, "디스크 크기");
        Assert.Equal(TranslationHelper.Translation.DoubleClick, "더블 클릭");
        Assert.Equal(TranslationHelper.Translation.Down, "아래로");
        Assert.Equal(TranslationHelper.Translation.Dpi, "DPI");
        Assert.Equal(TranslationHelper.Translation.DragFileTo, "파일을 Windows 탐색기 또는 다른 응용 프로그램/브라우저로 끌어다 놓기");
        Assert.Equal(TranslationHelper.Translation.DragImage, "이미지 끌기");
        Assert.Equal(TranslationHelper.Translation.DropToLoad, "이미지를 불러오려면 끌기");
        Assert.Equal(TranslationHelper.Translation.DuplicateFile, "파일 복제");
        Assert.Equal(TranslationHelper.Translation.Effects, "효과");
        Assert.Equal(TranslationHelper.Translation.EffectsTooltip, "이미지 효과 창 표시");
        Assert.Equal(TranslationHelper.Translation.Enter, "Enter");
        Assert.Equal(TranslationHelper.Translation.Esc, "Esc");
        Assert.Equal(TranslationHelper.Translation.EscCloseTooltip, "현재 열려 있는 창/메뉴 닫기");
        Assert.Equal(TranslationHelper.Translation.ExifVersion, "Exif 버전");
        Assert.Equal(TranslationHelper.Translation.ExpandedGalleryItemSize, "썸네일 크기");
        Assert.Equal(TranslationHelper.Translation.ExposureBias, "노출 보정");
        Assert.Equal(TranslationHelper.Translation.ExposureProgram, "노출 프로그램");
        Assert.Equal(TranslationHelper.Translation.ExposureTime, "노출 시간");
        Assert.Equal(TranslationHelper.Translation.FNumber, "F 번호");
        Assert.Equal(TranslationHelper.Translation.File, "파일");
        Assert.Equal(TranslationHelper.Translation.FileCopy, "클립보드에 파일 추가");
        Assert.Equal(TranslationHelper.Translation.FileCopyPath, "파일 경로 복사");
        Assert.Equal(TranslationHelper.Translation.FileCopyPathMessage, "클립보드에 파일 경로 추가");
        Assert.Equal(TranslationHelper.Translation.FileCutMessage, "클립보드 이동을 위해 파일 추가");
        Assert.Equal(TranslationHelper.Translation.FileExtension, "파일 확장자");
        Assert.Equal(TranslationHelper.Translation.FileManagement, "파일 관리");
        Assert.Equal(TranslationHelper.Translation.FileName, "파일 이름");
        Assert.Equal(TranslationHelper.Translation.FilePaste, "붙여넣기");
        Assert.Equal(TranslationHelper.Translation.FileProperties, "파일 속성");
        Assert.Equal(TranslationHelper.Translation.FileSize, "파일 크기");
        Assert.Equal(TranslationHelper.Translation.Files, "파일");
        Assert.Equal(TranslationHelper.Translation.Fill, "채우기");
        Assert.Equal(TranslationHelper.Translation.FillHeight, "⇔ 높이 채우기");
        Assert.Equal(TranslationHelper.Translation.FillSquare, "정사각형 채우기");
        Assert.Equal(TranslationHelper.Translation.FineWeather, "맑은 날씨");
        Assert.Equal(TranslationHelper.Translation.FirstImage, "첫 번째 이미지");
        Assert.Equal(TranslationHelper.Translation.Fit, "맟춤");
        Assert.Equal(TranslationHelper.Translation.FitToWindow, "창/이미지에 맞춤");
        Assert.Equal(TranslationHelper.Translation.Flash, "플래시");
        Assert.Equal(TranslationHelper.Translation.FlashDidNotFire, "플래시 발광 안 함");
        Assert.Equal(TranslationHelper.Translation.FlashEnergy, "플래시 에너지");
        Assert.Equal(TranslationHelper.Translation.FlashFired, "플래시 발광");
        Assert.Equal(TranslationHelper.Translation.FlashMode, "플래시 모드");
        Assert.Equal(TranslationHelper.Translation.Flip, "수평 뒤집기");
        Assert.Equal(TranslationHelper.Translation.Flipped, "수평으로 뒤집음");
        Assert.Equal(TranslationHelper.Translation.Fluorescent, "형광등");
        Assert.Equal(TranslationHelper.Translation.FocalLength, "초점 거리");
        Assert.Equal(TranslationHelper.Translation.FocalLength35mm, "35mm 초점 거리");
        Assert.Equal(TranslationHelper.Translation.Folder, "폴더");
        Assert.Equal(TranslationHelper.Translation.Forward, "앞으로");
        Assert.Equal(TranslationHelper.Translation.Fstop, "F 값");
        Assert.Equal(TranslationHelper.Translation.FullPath, "전체 경로");
        Assert.Equal(TranslationHelper.Translation.Fullscreen, "전체 화면");
        Assert.Equal(TranslationHelper.Translation.GallerySettings, "갤러리 설정");
        Assert.Equal(TranslationHelper.Translation.GalleryThumbnailStretch, "썸네일 스크립트");
        Assert.Equal(TranslationHelper.Translation.GeneralSettings, "일반 설정");
        Assert.Equal(TranslationHelper.Translation.GenerateThumbnails, "썸네일 생성");
        Assert.Equal(TranslationHelper.Translation.GithubRepo, "Github 저장소");
        Assert.Equal(TranslationHelper.Translation.GlassTheme, "유리 테마");
        Assert.Equal(TranslationHelper.Translation.GoBackBy100Images, "이미지 100개 뒤로 이동");
        Assert.Equal(TranslationHelper.Translation.GoBackBy10Images, "이미지 10개 뒤로 이동");
        Assert.Equal(TranslationHelper.Translation.GoToImageAtSpecifiedIndex, "지정된 인덱스에서 이미지로 이동");
        Assert.Equal(TranslationHelper.Translation.Hard, "단단함");
        Assert.Equal(TranslationHelper.Translation.Height, "높이");
        Assert.Equal(TranslationHelper.Translation.HideBottomGallery, "하단 갤러리 숨기기");
        Assert.Equal(TranslationHelper.Translation.HideBottomToolbar, "하단 도구 모음 숨기기");
        Assert.Equal(TranslationHelper.Translation.HideUI, "UI 숨기기");
        Assert.Equal(TranslationHelper.Translation.High, "높음");
        Assert.Equal(TranslationHelper.Translation.HighQuality, "높은 품질");
        Assert.Equal(TranslationHelper.Translation.HighlightColor, "강조 색상");
        Assert.Equal(TranslationHelper.Translation.ISOSpeed, "ISO 속도");
        Assert.Equal(TranslationHelper.Translation.IconsUsed, "사용된 아이콘:");
        Assert.Equal(TranslationHelper.Translation.Image, "이미지");
        Assert.Equal(TranslationHelper.Translation.ImageAliasing, "이미지 앨리어싱");
        Assert.Equal(TranslationHelper.Translation.ImageControl, "이미지 제어");
        Assert.Equal(TranslationHelper.Translation.ImageInfo, "이미지 정보");
        Assert.Equal(TranslationHelper.Translation.Inches, "인치");
        Assert.Equal(TranslationHelper.Translation.InfoWindow, "정보 창");
        Assert.Equal(TranslationHelper.Translation.InfoWindowTitle, "정보 및 한국어 번역: 비너스걸");
        Assert.Equal(TranslationHelper.Translation.InterfaceConfiguration, "인터페이스 구성");
        Assert.Equal(TranslationHelper.Translation.Landscape, "가로");
        Assert.Equal(TranslationHelper.Translation.Language, "언어");
        Assert.Equal(TranslationHelper.Translation.LastAccessTime, "최근 접근 날짜");
        Assert.Equal(TranslationHelper.Translation.LastImage, "마지막 이미지");
        Assert.Equal(TranslationHelper.Translation.LastWriteTime, "최근 쓰기 날짜");
        Assert.Equal(TranslationHelper.Translation.Latitude, "위도");
        Assert.Equal(TranslationHelper.Translation.Left, "왼쪽");
        Assert.Equal(TranslationHelper.Translation.LensMaker, "렌즈 제조사");
        Assert.Equal(TranslationHelper.Translation.LensModel, "렌즈 모델");
        Assert.Equal(TranslationHelper.Translation.LightSource, "광원");
        Assert.Equal(TranslationHelper.Translation.LightTheme, "밝은 테마");
        Assert.Equal(TranslationHelper.Translation.Lighting, "조명");
        Assert.Equal(TranslationHelper.Translation.Loading, "불러오는 중...");
        Assert.Equal(TranslationHelper.Translation.Longitude, "경도");
        Assert.Equal(TranslationHelper.Translation.Looping, "순환");
        Assert.Equal(TranslationHelper.Translation.LoopingDisabled, "순환 사용 안 함");
        Assert.Equal(TranslationHelper.Translation.LoopingEnabled, "순환 사용함");
        Assert.Equal(TranslationHelper.Translation.Lossless, "무손실");
        Assert.Equal(TranslationHelper.Translation.Lossy, "손실");
        Assert.Equal(TranslationHelper.Translation.Low, "낮음");
        Assert.Equal(TranslationHelper.Translation.Manual, "수동");
        Assert.Equal(TranslationHelper.Translation.MaxAperture, "최대 조리개");
        Assert.Equal(TranslationHelper.Translation.Maximize, "최대화");
        Assert.Equal(TranslationHelper.Translation.MegaPixels, "메가픽셀");
        Assert.Equal(TranslationHelper.Translation.Meter, "미터");
        Assert.Equal(TranslationHelper.Translation.MeteringMode, "미터링 모드");
        Assert.Equal(TranslationHelper.Translation.Minimize, "최소화");
        Assert.Equal(TranslationHelper.Translation.MiscSettings, "기타 설정");
        Assert.Equal(TranslationHelper.Translation.Modified, "수정 날짜");
        Assert.Equal(TranslationHelper.Translation.MouseDrag, "마우스 끌기");
        Assert.Equal(TranslationHelper.Translation.MouseKeyBack, "마우스 키 뒤로");
        Assert.Equal(TranslationHelper.Translation.MouseKeyForward, "마우스 키 앞으로");
        Assert.Equal(TranslationHelper.Translation.MouseWheel, "마우스 휠");
        Assert.Equal(TranslationHelper.Translation.MoveWindow, "창 이동");
        Assert.Equal(TranslationHelper.Translation.Navigation, "탐색");
        Assert.Equal(TranslationHelper.Translation.NearestNeighbor, "가장 가까운 이웃");
        Assert.Equal(TranslationHelper.Translation.NegativeColors, "네거티브 컬러");
        Assert.Equal(TranslationHelper.Translation.NewWindow, "새 창");
        Assert.Equal(TranslationHelper.Translation.NextFolder, "다음 폴더로 이동");
        Assert.Equal(TranslationHelper.Translation.NextImage, "다음 이미지");
        Assert.Equal(TranslationHelper.Translation.NoChange, "변경 없음");
        Assert.Equal(TranslationHelper.Translation.NoConversion, "변환 안 함");
        Assert.Equal(TranslationHelper.Translation.NoImage, "불러온 이미지가 없습니다");
        Assert.Equal(TranslationHelper.Translation.NoImages, "이미지 없음");
        Assert.Equal(TranslationHelper.Translation.NoResize, "크기 조정 안 함");
        Assert.Equal(TranslationHelper.Translation.None, "없음");
        Assert.Equal(TranslationHelper.Translation.Normal, "일반");
        Assert.Equal(TranslationHelper.Translation.NormalWindow, "일반 창");
        Assert.Equal(TranslationHelper.Translation.NotDefined, "정의되지 않음");
        Assert.Equal(TranslationHelper.Translation.NumpadMinus, "숫자 패드 -");
        Assert.Equal(TranslationHelper.Translation.NumpadPlus, "숫자 패드 +");
        Assert.Equal(TranslationHelper.Translation.OldMovie, "오래된 영화");
        Assert.Equal(TranslationHelper.Translation.Open, "열기");
        Assert.Equal(TranslationHelper.Translation.OpenFileDialog, "파일 열기 대화 상자");
        Assert.Equal(TranslationHelper.Translation.OpenInSameWindow, "동일한 창에서 파일 열기");
        Assert.Equal(TranslationHelper.Translation.OpenLastFile, "마지막 파일 열기");
        Assert.Equal(TranslationHelper.Translation.OpenWith, "열기...");
        Assert.Equal(TranslationHelper.Translation.OptimizeImage, "이미지 최적화");
        Assert.Equal(TranslationHelper.Translation.Orientation, "방향");
        Assert.Equal(TranslationHelper.Translation.OutputFolder, "출력 폴더");
        Assert.Equal(TranslationHelper.Translation.Pan, "팬");
        Assert.Equal(TranslationHelper.Translation.PasswordArchive, "암호로 보호된 압축파일은 지원되지 않습니다");
        Assert.Equal(TranslationHelper.Translation.PasteImageFromClipholder, "클립 홀더에서 이미지 붙여넣기");
        Assert.Equal(TranslationHelper.Translation.PencilSketch, "연필 스케치");
        Assert.Equal(TranslationHelper.Translation.PercentComplete, "% 완료...");
        Assert.Equal(TranslationHelper.Translation.Percentage, "백분율");
        Assert.Equal(TranslationHelper.Translation.PermanentlyDelete, "영구 삭제");
        Assert.Equal(TranslationHelper.Translation.PhotometricInterpretation, "측광 해석");
        Assert.Equal(TranslationHelper.Translation.Pixels, "픽셀");
        Assert.Equal(TranslationHelper.Translation.Portrait, "세로");
        Assert.Equal(TranslationHelper.Translation.PressKey, "키를 누르세요...");
        Assert.Equal(TranslationHelper.Translation.PrevFolder, "이전 폴더로 이동");
        Assert.Equal(TranslationHelper.Translation.PrevImage, "이전 이미지");
        Assert.Equal(TranslationHelper.Translation.Print, "인쇄");
        Assert.Equal(TranslationHelper.Translation.PrintSizeCm, "인쇄 크기 (cm)");
        Assert.Equal(TranslationHelper.Translation.PrintSizeIn, "인쇄 크기 (in)");
        Assert.Equal(TranslationHelper.Translation.Quality, "품질");
        Assert.Equal(TranslationHelper.Translation.Random, "무작위");
        Assert.Equal(TranslationHelper.Translation.RecentFiles, "최근 파일");
        Assert.Equal(TranslationHelper.Translation.RedEyeReduction, "적목 감소");
        Assert.Equal(TranslationHelper.Translation.Reload, "다시 불러오기");
        Assert.Equal(TranslationHelper.Translation.RemoveStarRating, "등급 제거");
        Assert.Equal(TranslationHelper.Translation.RenameFile, "파일 이름 바꾸기");
        Assert.Equal(TranslationHelper.Translation.Reset, "재설정");
        Assert.Equal(TranslationHelper.Translation.ResetButtonText, "기본값으로 재설정");
        Assert.Equal(TranslationHelper.Translation.ResetZoom, "확대/축소 재설정");
        Assert.Equal(TranslationHelper.Translation.Resize, "크기 조정");
        Assert.Equal(TranslationHelper.Translation.ResizeImage, "이미지 크기 조정");
        Assert.Equal(TranslationHelper.Translation.Resolution, "해상도");
        Assert.Equal(TranslationHelper.Translation.ResolutionUnit, "해상도 단위");
        Assert.Equal(TranslationHelper.Translation.RestartApp, "응용 프로그램 다시 시작");
        Assert.Equal(TranslationHelper.Translation.RestoreDown, "복원 다운");
        Assert.Equal(TranslationHelper.Translation.Reverse, "역방향");
        Assert.Equal(TranslationHelper.Translation.Right, "오른쪽");
        Assert.Equal(TranslationHelper.Translation.RotateLeft, "왼쪽으로 회전");
        Assert.Equal(TranslationHelper.Translation.RotateRight, "오른쪽으로 회전");
        Assert.Equal(TranslationHelper.Translation.Rotated, "회전됨");
        Assert.Equal(TranslationHelper.Translation.Saturation, "채도");
        Assert.Equal(TranslationHelper.Translation.Save, "저장");
        Assert.Equal(TranslationHelper.Translation.SaveAs, "다른 이름으로 저장");
        Assert.Equal(TranslationHelper.Translation.SavingFileFailed, "파일 저장 실패");
        Assert.Equal(TranslationHelper.Translation.ScrollAndRotate, "스크롤 및 회전");
        Assert.Equal(TranslationHelper.Translation.ScrollDirection, "스크롤 방향");
        Assert.Equal(TranslationHelper.Translation.ScrollDown, "아래로 스크롤");
        Assert.Equal(TranslationHelper.Translation.ScrollToBottom, "맨 아래로 스크롤");
        Assert.Equal(TranslationHelper.Translation.ScrollToTop, "맨 위로 스크롤");
        Assert.Equal(TranslationHelper.Translation.ScrollToZoom, "마우스 휠로 확대/축소, Ctrl로 탐색");
        Assert.Equal(TranslationHelper.Translation.ScrollUp, "위로 스크롤");
        Assert.Equal(TranslationHelper.Translation.Scrolling, "스크롤");
        Assert.Equal(TranslationHelper.Translation.ScrollingDisabled, "스크롤 사용 안 함");
        Assert.Equal(TranslationHelper.Translation.ScrollingEnabled, "스크롤 사용함");
        Assert.Equal(TranslationHelper.Translation.SearchSubdirectory, "하위 디렉터리 검색");
        Assert.Equal(TranslationHelper.Translation.SecAbbreviation, "초");
        Assert.Equal(TranslationHelper.Translation.SelectAll, "전체 선택");
        Assert.Equal(TranslationHelper.Translation.SelectGalleryThumb, "갤러리 썸네일 선택");
        Assert.Equal(TranslationHelper.Translation.SendCurrentImageToRecycleBin, "현재 이미지를 휴지통으로 보내기");
        Assert.Equal(TranslationHelper.Translation.SentFileToRecycleBin, "파일을 휴지통으로 보냈습니다");
        Assert.Equal(TranslationHelper.Translation.SetAs, "다음으로 설정...");
        Assert.Equal(TranslationHelper.Translation.SetAsLockScreenImage, "잠금 화면 이미지로 설정");
        Assert.Equal(TranslationHelper.Translation.SetAsWallpaper, "배경 화면으로 설정");
        Assert.Equal(TranslationHelper.Translation.SetCurrentImageAsWallpaper, "현재 이미지를 배경 화면으로 설정:");
        Assert.Equal(TranslationHelper.Translation.SetStarRating, "별 등급 설정");
        Assert.Equal(TranslationHelper.Translation.Settings, "설정");
        Assert.Equal(TranslationHelper.Translation.Shade, "그늘");
        Assert.Equal(TranslationHelper.Translation.Sharpness, "선명도");
        Assert.Equal(TranslationHelper.Translation.Shift, "Shift");
        Assert.Equal(TranslationHelper.Translation.ShowAllSettingsWindow, "모든 설정 창 표시");
        Assert.Equal(TranslationHelper.Translation.ShowBottomGallery, "하단 갤러리 표시");
        Assert.Equal(TranslationHelper.Translation.ShowBottomGalleryWhenUiIsHidden, "UI를 숨겼을 때 하단 갤러리 표시");
        Assert.Equal(TranslationHelper.Translation.ShowBottomToolbar, "하단 도구 모음 표시");
        Assert.Equal(TranslationHelper.Translation.ShowConfirmationOnEsc, "'Esc'를 누를 때 확인 대화 상자를 표시");
        Assert.Equal(TranslationHelper.Translation.ShowFadeInButtonsOnHover, "마우스 오버 시 페이드인 버튼 표시");
        Assert.Equal(TranslationHelper.Translation.ShowFileSavingDialog, "파일 저장 대화상자 표시");
        Assert.Equal(TranslationHelper.Translation.ShowImageGallery, "이미지 갤러리 표시");
        Assert.Equal(TranslationHelper.Translation.ShowImageInfo, "이미지 정보 표시");
        Assert.Equal(TranslationHelper.Translation.ShowInFolder, "폴더에서 표시");
        Assert.Equal(TranslationHelper.Translation.ShowInfoWindow, "정보 창 표시");
        Assert.Equal(TranslationHelper.Translation.ShowResizeWindow, "크기 조정 창 표시");
        Assert.Equal(TranslationHelper.Translation.ShowUI, "UI 표시");
        Assert.Equal(TranslationHelper.Translation.ShutterPriority, "셔터 우선");
        Assert.Equal(TranslationHelper.Translation.SideBySide, "나란히");
        Assert.Equal(TranslationHelper.Translation.SideBySideTooltip, "이미지를 나란히 표시");
        Assert.Equal(TranslationHelper.Translation.Size, "크기");
        Assert.Equal(TranslationHelper.Translation.SizeMp, "크기 (mp)");
        Assert.Equal(TranslationHelper.Translation.SizeTooltip, "원하는 크기를 픽셀 또는 백분율로 입력하세요.");
        Assert.Equal(TranslationHelper.Translation.Slideshow, "슬라이드쇼");
        Assert.Equal(TranslationHelper.Translation.Soft, "부드럽게");
        Assert.Equal(TranslationHelper.Translation.Software, "소프트웨어");
        Assert.Equal(TranslationHelper.Translation.SortFilesBy, "파일 정렬");
        Assert.Equal(TranslationHelper.Translation.SourceFolder, "원본 폴더");
        Assert.Equal(TranslationHelper.Translation.Space, "공간");
        Assert.Equal(TranslationHelper.Translation.Square, "정사각형");
        Assert.Equal(TranslationHelper.Translation.Start, "시작");
        Assert.Equal(TranslationHelper.Translation.StartSlideshow, "슬라이드쇼 시작");
        Assert.Equal(TranslationHelper.Translation.StayCentered, "창 중앙 유지");
        Assert.Equal(TranslationHelper.Translation.StayTopMost, "다른 창 위에 유지");
        Assert.Equal(TranslationHelper.Translation.Stretch, "늘이기");
        Assert.Equal(TranslationHelper.Translation.StretchImage, "이미지 늘리기");
        Assert.Equal(TranslationHelper.Translation.StrobeReturnLightDetected, "스트로브 반사 빛 감지됨");
        Assert.Equal(TranslationHelper.Translation.StrobeReturnLightNotDetected, "스트로브 반사 빛 감지 안 됨");
        Assert.Equal(TranslationHelper.Translation.Subject, "주제");
        Assert.Equal(TranslationHelper.Translation.Theme, "테마");
        Assert.Equal(TranslationHelper.Translation.Thumbnail, "썸네일");
        Assert.Equal(TranslationHelper.Translation.Tile, "타일");
        Assert.Equal(TranslationHelper.Translation.Title, "제목");
        Assert.Equal(TranslationHelper.Translation.ToggleBackgroundColor, "배경색 전환");
        Assert.Equal(TranslationHelper.Translation.ToggleFullscreen, "전체 화면 전환");
        Assert.Equal(TranslationHelper.Translation.ToggleLooping, "순환 전환");
        Assert.Equal(TranslationHelper.Translation.ToggleScroll, "스크롤 전환");
        Assert.Equal(TranslationHelper.Translation.ToggleTaskbarProgress, "작업 표시줄 진행률 표시");
        Assert.Equal(TranslationHelper.Translation.UnableToRender, "이미지를 렌더링할 수 없습니다");
        Assert.Equal(TranslationHelper.Translation.Uncalibrated, "보정되지 않음");
        Assert.Equal(TranslationHelper.Translation.UnexpectedError, "알 수 없는 오류 발생");
        Assert.Equal(TranslationHelper.Translation.Unflip, "뒤집기 취소");
        Assert.Equal(TranslationHelper.Translation.Uniform, "균일하게");
        Assert.Equal(TranslationHelper.Translation.UniformToFill, "균일하게 채우기");
        Assert.Equal(TranslationHelper.Translation.Unknown, "알 수 없음");
        Assert.Equal(TranslationHelper.Translation.UnsupportedFile, "지원되지 않는 파일");
        Assert.Equal(TranslationHelper.Translation.Up, "위로");
        Assert.Equal(TranslationHelper.Translation.UsingMouse, "마우스 사용");
        Assert.Equal(TranslationHelper.Translation.UsingTouchpad, "터치패드 사용");
        Assert.Equal(TranslationHelper.Translation.Version, "버전:");
        Assert.Equal(TranslationHelper.Translation.ViewLicenseFile, "라이선스 파일 보기");
        Assert.Equal(TranslationHelper.Translation.WhiteBalance, "화이트 밸런스");
        Assert.Equal(TranslationHelper.Translation.WhiteFluorescent, "백색 형광등");
        Assert.Equal(TranslationHelper.Translation.Width, "너비");
        Assert.Equal(TranslationHelper.Translation.WidthAndHeight, "너비 및 높이");
        Assert.Equal(TranslationHelper.Translation.WindowManagement, "창 관리");
        Assert.Equal(TranslationHelper.Translation.WindowScaling, "창 비율");
        Assert.Equal(TranslationHelper.Translation.Zoom, "확대/축소");
        Assert.Equal(TranslationHelper.Translation.ZoomIn, "확대");
        Assert.Equal(TranslationHelper.Translation.ZoomOut, "축소");
        Assert.Equal(TranslationHelper.Translation._1Star, "별 1 등급");
        Assert.Equal(TranslationHelper.Translation._2Star, "별 2 등급");
        Assert.Equal(TranslationHelper.Translation._3Star, "별 3 등급");
        Assert.Equal(TranslationHelper.Translation._4Star, "별 4 등급");
        Assert.Equal(TranslationHelper.Translation._5Star, "별 5 등급");
    }
}