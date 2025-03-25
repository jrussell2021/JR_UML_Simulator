using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;

namespace UML_Simulator_SDL2
{
    internal class EventProcessor
    {
        static readonly EventProcessor instance = new EventProcessor();

        static EventProcessor()
        {
        }

        private EventProcessor()
        {
        }

        public static EventProcessor Instance
        {
            get
            {
                return instance;
            }
        }

        public void PollEvents()
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
                        if (e.key.keysym.sym == SDL.SDL_Keycode.SDLK_RIGHT)
                        {
                            Window.Instance.screenPosX += 4;
                            ElementManager.Instance.ScrollElements(-4, 0);
                        }
                        else if (e.key.keysym.sym == SDL.SDL_Keycode.SDLK_LEFT)
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
    }
}
