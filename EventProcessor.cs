using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
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

        public string GetInput()
        {
            IntPtr myPointer;
            bool isDone = false;
            string text = "";
            int value;
            Console.WriteLine("Input test");
            
            while (!isDone)
            {
                if(SDL.SDL_PollEvent(out SDL.SDL_Event e) > 0)
                {
                    switch (e.type)
                    {
                        case SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN:
                            isDone = true;
                            break;
                        case SDL.SDL_EventType.SDL_TEXTINPUT:
                            unsafe
                            {
                                myPointer = (IntPtr)e.text.text;
                                value = Marshal.ReadByte(myPointer);
                                text += (char)value;
                            }
                            break;
                    }
                }
            }

            return text;
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
                                else if (UserInterface.Toolbar.Instance.mode == 3)
                                {
                                    ElementManager.Instance.EditTextBox();
                                    
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
