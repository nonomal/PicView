﻿namespace PicView.Avalonia.MacOS;

public static class MacOsKeybindings
{
    public const string DefaultKeybindings = """
                                              {
                                                "D": "Next",
                                                "Right": "Next",
                                                "Cmd+Right": "Last",
                                                "Cmd+D": "Last",
                                                "Cmd+Left": "First",
                                                "Cmd+A": "First",
                                                "Shift+D": "NextFolder",
                                                "Shift+Right": "NextFolder",
                                                "Shift+A": "PrevFolder",
                                                "Shift+Left": "PrevFolder",
                                                "A": "Prev",
                                                "Left": "Prev",
                                                "W": "Up",
                                                "Up": "Up",
                                                "S": "Down",
                                                "Down": "Down",
                                                "PageUp": "ScrollUp",
                                                "PageDown": "ScrollDown",
                                                "Add": "ZoomIn",
                                                "OemPlus": "ZoomIn",
                                                "OemMinus": "ZoomOut",
                                                "Subtract": "ZoomOut",
                                                "Scroll": "ToggleScroll",
                                                "Home": "ScrollToTop",
                                                "End": "ScrollToBottom",
                                                "G": "ToggleGallery",
                                                "F": "Flip",
                                                "J": "ResizeImage",
                                                "L": "ToggleLooping",
                                                "C": "Crop",
                                                "E": "GalleryClick",
                                                "Enter": "GalleryClick",
                                                "I": "ImageInfoWindow",
                                                "F6": "EffectsWindow",
                                                "F1": "AboutWindow",
                                                "F3": "OpenInExplorer",
                                                "F4": "SettingsWindow",
                                                "F5": "Slideshow",
                                                "F11": "Fullscreen",
                                                "F12": "Fullscreen",
                                                "B": "ChangeBackground",
                                                "Space": "Center",
                                                "K": "KeybindingsWindow",
                                                "D0": "Set0Star",
                                                "D1": "Set1Star",
                                                "D2": "Set2Star",
                                                "D3": "Set3Star",
                                                "D4": "Set4Star",
                                                "D5": "Set5Star",
                                                "Cmd+O": "Open",
                                                "Cmd+E": "OpenWith",
                                                "Cmd+R": "Reload",
                                                "Cmd+S": "Save",
                                                "Cmd+Shift+S": "SaveAs",
                                                "F2": "Rename",
                                                "Cmd+C": "CopyFile",
                                                "Cmd+Alt+V": "CopyFilePath",
                                                "Cmd+Shift+C": "CopyImage",
                                                "Cmd+X": "CutFile",
                                                "Cmd+V": "Paste",
                                                "Cmd+P": "Print",
                                                "Alt+Z": "ToggleInterface",
                                                "Delete": "DeleteFile"
                                              }
                                              """;
}
