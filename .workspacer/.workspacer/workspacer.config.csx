#r "C:\software\workspacer-stable-0.9.10\workspacer.Shared.dll"
#r "C:\software\workspacer-stable-0.9.10\plugins\workspacer.Bar\workspacer.Bar.dll"
#r "C:\software\workspacer-stable-0.9.10\plugins\workspacer.ActionMenu\workspacer.ActionMenu.dll"
#r "C:\software\workspacer-stable-0.9.10\plugins\workspacer.FocusIndicator\workspacer.FocusIndicator.dll"



using System;
using System.Linq;
using System.IO;
using workspacer;
using workspacer.Bar;
using workspacer.Bar.Widgets;
using workspacer.ActionMenu;
using workspacer.FocusIndicator;

Action<IConfigContext> doConfig = (IConfigContext context) =>
{
    context.AddBar(new BarPluginConfig()
    {
        BarTitle = "workspacer.Bar",
        BarHeight = 40,
        FontSize = 10,
        DefaultWidgetForeground = Color.White,
        DefaultWidgetBackground = Color.Black,
    });

    context.AddFocusIndicator(new FocusIndicatorPluginConfig()
    {
        BorderColor = Color.Lime,
        TimeToShow = 150,
    });

    var actionMenu = context.AddActionMenu(new ActionMenuPluginConfig()
    {
        Foreground = Color.Blue,
    });

    var sticky = new StickyWorkspaceContainer(context, StickyWorkspaceIndexMode.Local);
    context.WorkspaceContainer = sticky;
    // create workspaces
    sticky.CreateWorkspaces("1 Web", "2 Notes", "3 Code", "4 Media", "5 Extra", "6", "7", "8", "9 Explorer");

    context.WindowRouter.AddRoute((window) => window.Title.Contains("Total Commander") ? context.WorkspaceContainer["9 Explorer"] : null);

    context.WindowRouter.AddRoute((window) => window.Title.Contains("Visual Studio") ? context.WorkspaceContainer["3 Code"] : null);
    context.WindowRouter.AddRoute((window) => window.Title.Contains("Hyper") ? context.WorkspaceContainer["3 Code"] : null);

    context.WindowRouter.AddRoute((window) => window.Title.Contains("SMPlayer") ? context.WorkspaceContainer["4 Media"] : null);


    context.WindowRouter.AddRoute((window) => window.Title.Contains("Firefox") ? context.WorkspaceContainer["1 Web"] : null);
    context.WindowRouter.AddRoute((window) => window.Title.Contains("Chrome") ? context.WorkspaceContainer["1 Web"] : null);

    context.WindowRouter.AddRoute((window) => window.Title.Contains("OneNote") ? context.WorkspaceContainer["2 Notes"] : null);

    // filters, workspacer will ignore windows with this names (I recommend ignoring games and fullscreen applications)
    context.WindowRouter.AddFilter((window) => !window.Title.Contains("Search")); //Getsu Fuma Den
    context.WindowRouter.AddFilter((window) => !window.Title.Contains("Lacuna")); //Lacuna Noir
    context.WindowRouter.AddFilter((window) => !window.Title.Contains("Monster Train")); //Monster Train

    // keybinds
    KeyModifiers mod = KeyModifiers.Alt;

    // default keybindings: https://github.com/rickbutton/workspacer/blob/master/src/workspacer/Keybinds/KeybindManager.cs

    //unsuscribe defaults that conflict with other software or that I don't use
    context.Keybinds.Unsubscribe(mod, Keys.O); //I'm mapping Alt + O to open PowerToys Run
    context.Keybinds.Unsubscribe(mod | KeyModifiers.LShift, Keys.O); //not going to use
    context.Keybinds.Unsubscribe(mod | KeyModifiers.LShift, Keys.I); //not going to use

    //suscribe
    context.Keybinds.Subscribe(mod | KeyModifiers.LShift, Keys.G, () => System.Diagnostics.Process.Start("explorer.exe", Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)));
    //context.Keybinds.Subscribe(mod | KeyModifiers.LShift, Keys.Enter, () => System.Diagnostics.Process.Start(Environment.ExpandEnvironmentVariables(@"%ProgramFiles%\Alacritty\alacritty.exe"));
};

return doConfig;