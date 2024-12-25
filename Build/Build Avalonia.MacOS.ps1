param (
    [Parameter()]
    [string]$Platform,
    
    [Parameter()]
    [string]$outputPath,
	
	[Parameter()]
    [string]$appVersion
)

# Define the core project path relative to the script's location
$coreProjectPath = Join-Path -Path $PSScriptRoot -ChildPath "../src/PicView.Core/PicView.Core.csproj"

# Load the .csproj file as XML
[xml]$coreCsproj = Get-Content $coreProjectPath

# Define the package reference to replace
$packageRefX64 = "Magick.NET-Q8-x64"
$packageRefArm64 = "Magick.NET-Q8-arm64"

# Find the Magick.NET package reference and update it based on the platform
$packageNodes = $coreCsproj.Project.ItemGroup.PackageReference | Where-Object { $_.Include -eq $packageRefX64 -or $_.Include -eq $packageRefArm64 }
if ($packageNodes) {
    foreach ($packageNode in $packageNodes) {
        if ($Platform -eq "arm64") {
            $packageNode.Include = $packageRefArm64
        } else {
            $packageNode.Include = $packageRefX64
        }
    }
}

# Save the updated .csproj file
$coreCsproj.Save($coreProjectPath)

# Define the project path for the actual build target
$avaloniaProjectPath = Join-Path -Path $PSScriptRoot -ChildPath "../src/PicView.Avalonia.MacOS/PicView.Avalonia.MacOS.csproj"

# Create temporary build output directory
$tempBuildPath = Join-Path -Path $outputPath -ChildPath "temp"
New-Item -ItemType Directory -Force -Path $tempBuildPath

# Run dotnet publish for the Avalonia project
dotnet publish $avaloniaProjectPath `
    --runtime "osx-$Platform" `
    --self-contained true `
    --configuration Release `
    -p:PublishSingleFile=false `
    --output $tempBuildPath

# Create .app bundle structure
$appBundlePath = Join-Path -Path $outputPath -ChildPath "PicView.app"
$contentsPath = Join-Path -Path $appBundlePath -ChildPath "Contents"
$macOSPath = Join-Path -Path $contentsPath -ChildPath "MacOS"
$resourcesPath = Join-Path -Path $contentsPath -ChildPath "Resources"

# Create directory structure
New-Item -ItemType Directory -Force -Path $macOSPath
New-Item -ItemType Directory -Force -Path $resourcesPath

# Create Info.plist
$infoPlistContent = @"
<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE plist PUBLIC "-//Apple//DTD PLIST 1.0//EN" "http://www.apple.com/DTDs/PropertyList-1.0.dtd">
<plist version="1.0">
<dict>
    <key>CFBundleName</key>
    <string>PicView</string>
    <key>CFBundleDisplayName</key>
    <string>PicView</string>
    <key>CFBundleIdentifier</key>
    <string>com.ruben2776.picview</string>
    <key>CFBundleVersion</key>
    <string>${appVersion}</string>
    <key>CFBundlePackageType</key>
    <string>APPL</string>
    <key>CFBundleSignature</key>
    <string>picview.org</string>
    <key>CFBundleExecutable</key>
    <string>PicView.Avalonia.MacOS</string>
    <key>CFBundleIconFile</key>
    <string>AppIcon.icns</string>
    <key>CFBundleShortVersionString</key>
    <string>${appVersion}</string>
    <key>LSMinimumSystemVersion</key>
    <string>10.15</string>
    <key>NSHighResolutionCapable</key>
    <true/>
</dict>
</plist>
"@

# Save Info.plist
$infoPlistPath = Join-Path -Path $contentsPath -ChildPath "Info.plist"
$infoPlistContent | Out-File -FilePath $infoPlistPath -Encoding UTF8

# Copy build output to MacOS directory
Copy-Item -Path "$tempBuildPath/*" -Destination $macOSPath -Recurse

# Copy icon if it exists (you'll need to create this)
$iconSource = Join-Path -Path $PSScriptRoot -ChildPath "../src/PicView.Avalonia.MacOS/Assets/AppIcon.icns"
if (Test-Path $iconSource) {
    Copy-Item -Path $iconSource -Destination $resourcesPath
}

# Remove PDB files
Get-ChildItem -Path $macOSPath -Filter "*.pdb" -Recurse | Remove-Item -Force

# Remove temporary build directory
Remove-Item -Path $tempBuildPath -Recurse -Force

# Set executable permissions on the main binary
if ($IsLinux -or $IsMacOS) {
    $mainBinary = Join-Path -Path $macOSPath -ChildPath "PicView.Avalonia.MacOS"
    chmod +x $mainBinary
}