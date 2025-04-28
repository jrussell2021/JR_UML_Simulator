using SDL2;
using System;

namespace UML_Simulator_SDL2
{
    internal class Program
    {

        static void AppLoop()
        {
            while (Window.Instance.running)
            {
                EventProcessor.Instance.PollEvents();
                
                SDL.SDL_SetRenderDrawColor(Window.Instance.renderer, 180, 180, 180, 255);

                SDL.SDL_RenderClear(Window.Instance.renderer);

                //Draw Elements
                ElementManager.Instance.DrawElements();

                //Draw Toolbar
                UserInterface.Toolbar.Instance.DrawToolbar();

                //Draw Pagebar
                UserInterface.Pagebar.Instance.DrawPagebar();

                //Draw Main menu
                if (UserInterface.MainMenu.Instance.isActive == true)
                {
                    UserInterface.MainMenu.Instance.DrawMainMenu();
                }

                SDL.SDL_RenderPresent(Window.Instance.renderer);
            }
        }

        static void Main(string[] args)
        {
            Window.Instance.CreateWindow();
            Window.Instance.InitFont();
            Window.Instance.sampleColour.r = 0;
            Window.Instance.sampleColour.g = 0;
            Window.Instance.sampleColour.b = 0;

            UserInterface.Toolbar.Instance.InitToolbar();
            UserInterface.MainMenu.Instance.InitMainMenu();
            AppLoop();

            for(int i = 0; i < ElementManager.Instance.elementList.Count; i++)
            {
                ElementManager.Instance.elementList[i]._textBox.DestroyTextBox();
            }
            SDL.SDL_DestroyRenderer(Window.Instance.renderer);
            SDL.SDL_DestroyWindow(Window.Instance.window);
            SDL.SDL_Quit();
        }
    }
}