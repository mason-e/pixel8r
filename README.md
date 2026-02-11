# pixel8r

pixel8r is a graphical image editor that was created with the goal of producing stylized images inspired by classic video game consoles. It has a host of pre-defined, simple options for cropping, resizing, pixelating, tinting, and changing palette. It is not meant for precise editing.

This README pertains more to the requirements of developing or running the program. For specific details on what the program can do, refer to the [Manual](manual.md).

## Running

Executables are currently built manually with Visual Studio's Publish option and included in the Releases. The executable should run portably as long as the proper dependencies in the zip are kept with it. 

### Supported Operating Systems

I've published builds for all available runtimes I could, but the ones I've tested are limited by the PCs I have access to.

#### Tested
- win-x86
- win-x64
- linux-x64
- linux-arm
- linux-arm64

#### Untested
- win-arm64
- osx-x64
- osx-arm64

#### Unpublished
`win-arm` fails due to an issue I haven't resolved, and likely will not resolve unless there is a demand for this runtime.

### Other Constraints

pixel8r is meant for desktop environments with a resolution of at least 1080p, as it does not currently support dynamic resolution scaling.

On Linux, it may be necessary to run via a `./pixel8r.Desktop` command to get it to work.

### Palettes

The program comes with a folder of default palettes. Palettes can be added as desired by creating a `.hex` file where each line is a color in hex format without a `#`, e.g. `fa64c7`. 

The website [Lospec](https://lospec.com/palette-list/) contains a ton of user created palettes with a download option for `.hex`.

## Developing

### Setup

Follow the relevant instructions for your platform and desired method to install [.NET Framework 9.0](https://dotnet.microsoft.com/en-us/download/dotnet/9.0).

The UI framework used by this project, Avalonia. Has some additional [optional instructions](https://docs.avaloniaui.net/docs/get-started/) for a better developer experience. In these instructions, at the "Installing Avalonia Templates" step, I've had some of these fail, but if the core ones work it's enough for this project.

### Building/Running

In Visual Studio, select the proper targets (more info in subsections below) and use the appropriate green play button to run or debug.

Alternatively, in any terminal, navigate to the directory of the program and execute `dotnet run` to run without debugging.

For VSCode, follow [these instructions](https://learn.microsoft.com/en-us/dotnet/core/tutorials/debugging-with-visual-studio-code) to debug. 

Debugging with any other editors would be editor-specific and those aren't covered here since I have not used them.

#### Desktop

`pixel8r.Desktop` is the build target/directory to navigate to when running or debugging the main desktop app. This should work on Linux or Windows.

#### Browser

Browser is included in the source for now, but is semi-broken, so be warned. At a minimum, it will likely need extra Web Assembly dependencies:

https://docs.avaloniaui.net/docs/guides/platforms/how-to-use-web-assembly

Without Web Assembly, the browser may build and appear to work but only show an Avalonia splash screen. Even with this, issues remain with accessing the file system, which is necessary to select a picture and to load palettes.

`pixel8r.Browser` is the build target/directory to navigate to.

I've also noticed when trying out the browser mode that dotnet(.exe) processes are prone to remaining around even when it seems like they were closed. These can be terminated with Task Manager or equivalent.

#### Tests

`pixel8rtests` for the target directory. These can be run or debugged just like the Desktop project. In addition, Visual Studio has a nice graphical menu by going to Test > Test Explorer.

The "performance" tests have an Ignore line that needs to be uncommented if you want to run these. There is currently no real instrumentation around these, it's just to get an idea of execution time for each algorithm.