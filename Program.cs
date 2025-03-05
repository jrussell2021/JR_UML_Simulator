using SDL2;
using System;

namespace UML_Simulator_SDL2
{
    internal class Program
    {

        static void PollEvents()
        {
            while (SDL.SDL_PollEvent(out SDL.SDL_Event e) == 1)
            {
                switch (e.type)
                {
                    case SDL.SDL_EventType.SDL_QUIT:
                        Window.Instance.running = false;
                        break;
                    case SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN:
                        if (!UserInterface.MainMenu.Instance.isActive || UserInterface.MainMenu.Instance.CheckMainMenuInteraction() == false)
                        {
                            if (UserInterface.Toolbar.Instance.CheckToolbarInteraction() == false)
                            {
                                if (UserInterface.Toolbar.Instance.mode == 0)
                                {
                                    ElementManager.Instance.AddElement();
                                }
                                else if (UserInterface.Toolbar.Instance.mode == 1)
                                {
                                    Link testLink = new Link();
                                    testLink.Create();
                                }
                                else if (UserInterface.Toolbar.Instance.mode == 2)
                                {
                                    ElementManager.Instance.MoveElements();
                                }
 
                            }
                        }
                            
                        break;
                    case SDL.SDL_EventType.SDL_MOUSEBUTTONUP:
                        break;
                    case SDL.SDL_EventType.SDL_KEYDOWN:
                        if(e.key.keysym.sym == SDL.SDL_Keycode.SDLK_RIGHT)
                        {
                            Window.Instance.screenPosX += 4;
                            ElementManager.Instance.ScrollElements(-4, 0);
                        }
                        else if(e.key.keysym.sym == SDL.SDL_Keycode.SDLK_LEFT)
                        {
                            Window.Instance.screenPosX -= 4;
                            ElementManager.Instance.ScrollElements(4, 0);
                        }
                        else if (e.key.keysym.sym == SDL.SDL_Keycode.SDLK_UP)
                        {
                            Window.Instance.screenPosY -= 4;
                            ElementManager.Instance.ScrollElements(0, 4);
                        }
                        else if (e.key.keysym.sym == SDL.SDL_Keycode.SDLK_DOWN)
                        {
                            Window.Instance.screenPosY += 4;
                            ElementManager.Instance.ScrollElements(0, -4);
                        }
                        break;
                }
            }
        }
        static void AppLoop()
        {
            while (Window.Instance.running)
            {
                PollEvents();
                
                SDL.SDL_SetRenderDrawColor(Window.Instance.renderer, 180, 180, 180, 255);

                SDL.SDL_RenderClear(Window.Instance.renderer);

                //Draw Toolbar
                UserInterface.Toolbar.Instance.DrawToolbar();

                //Draw Elements
                ElementManager.Instance.DrawElements();

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