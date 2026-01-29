# pixel8r

pixel8r is a graphical image editor that was created with the goal of producing stylized images resembling the style of video game consoles. It has a host of pre-defined, one-click options for cropping, resizing, pixelating, tinting, and changing palette. It is not meant for precise editing.

This README pertains more to the technical requirements of the repository. For instructions on using the program, refer to the [Manual](manual.md).

## Development

### Setup

Current target .NET SDK major version: 9.0.

Follow instructions at https://docs.avaloniaui.net/docs/get-started/ to prepare the dependencies. This page also includes a link to instructions for installing the .NET SDK, which vary depending on your OS, distro and chosen install method.

The IDE and extension don't really matter, although I find Visual Studio makes things easier.

At the "Installing Avalonia Templates" step, I've had some of these fail, but if the core ones work it's enough for this project.

### Building/Running

In Visual Studio, select the proper targets (more info in subsections below) and use the appropriate green play button to run or debug.

In any terminal, navigate to the directory of the program and execute `dotnet run` to run without debugging.

For VSCode, follow instructions at https://learn.microsoft.com/en-us/dotnet/core/tutorials/debugging-with-visual-studio-code to debug. 

Debugging with any other editors would be editor-specific and those aren't covered here since I have not used them.

#### Desktop

`pixel8r.Desktop` is the build target/directory to navigate to when running or debugging the main desktop app. This should work on Linux or Windows.

#### Browser

Browser is included in the source for now, but is semi-broken, so be warned. At a minimum, it will likely need extra Web Assembly dependencies:

https://docs.avaloniaui.net/docs/guides/platforms/how-to-use-web-assembly

Without Web Assembly, the browser may build and appear to work but only show an Avalonia splash screen. Even with this, issues remain with accessing the file system, which is necessary to select a picture and to load palettes.

`pixel8r.Browser` is the build target/directory to navigate to.

I've also noticed when trying out the browser mode that dotnet(.exe) processes are prone to remaining around even when it seems like they were closed. These can be terminated with Task Manager (or Linux equivalent).

#### Tests

`pixel8rtests` for the target directory. These can be run or debugged just like the Desktop project. In addition, Visual Studio has a nice graphical menu by going to Test > Test Explorer.

The "performance" tests have an Ignore line that needs to be uncommented if you want to run these. There is currently no real instrumentation around these, it's just to get an idea of execution time for each algorithm.