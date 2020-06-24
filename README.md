# Command Core

A simple command line parsing library that helps creating MVC like command line applications.

There are many command line parsing libraries out there but most of them are unnecessarily complicated. They require so much boilerplate code; whereas, Command Core library uses an already well-understood design pattern: MVC. It is easy to start with and reason about it.

## How to use it?

Let's start with a simple command:

```bash
helloworld.exe add --name tarik --lastname guney
```

The following command needs these classes `AddVerb<AddOptions>`, `AddOptions`, `AddVerbView`.

- `AddVerb<AddOptions>` is the equivalent of a controller in MVC. It inherits the `Verb` abstract class. You specify the type of the options class as a generic parameter.
- `AddOptions` is the model class which includes all of the possible options with attributes like `[Required]`, `[DefaultValue("test")]`, `[Alias("-v")]` for its properties. You can also customize the name of the properties with `[DisplayName("--add-person")]` if you want to use a different option name.
- `AddVerbView` is the class that helps you customize the output that is written to the console or any other place. It inherits from `VerbView` class.


To activate this library, you need to add the following code to the `Main` function in `Program.cs` file:

```c#
public static int Main(string[] args)
{
    return CommandCore.Parse(args);
}
```

That's all, and it will be all. The whole idea of this library is to simplify the console application command line argument parsing.

## Developed

by Tarik Guney with .NET Core 3.x.
