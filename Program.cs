using SDL2;
using System;

namespace UML_Simulator_SDL2
{
    internal class Program
    {
        static int mode = 0;

        static void CreateWindow()
        {
            SDL.SDL_Init(SDL.SDL_INIT_VIDEO);

            Window.Instance.window = SDL.SDL_CreateWindow("UML Simulator", SDL.SDL_WINDOWPOS_UNDEFINED, SDL.SDL_WINDOWPOS_UNDEFINED, 1280, 720, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);

            Window.Instance.renderer = SDL.SDL_CreateRenderer(Window.Instance.window, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);

            SDL_image.IMG_Init(SDL_image.IMG_InitFlags.IMG_INIT_PNG);
            SDL_ttf.TTF_Init();

        }

        static void PollEvents()
        {
            while (SDL.SDL_PollEvent(out SDL.SDL_Event e) == 1)
            {
                switch (e.type)
                {
                    case SDL.SDL_EventType.SDL_QUIT:
                        Window.Instance.running = false;
                        break;
                    case SDL.SDL_EventType.SDL_KEYDOWN:
                        switch (e.key.keysym.sym)
                        {
                            case SDL.SDL_Keycode.SDLK_0:
                                mode = 0;
                                break;
                            case SDL.SDL_Keycode.SDLK_1:
                                mode = 1;
                                break;
                        }
                        break;
                    case SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN:
                        if(mode == 0)
                        {
                            ElementManager.Instance.AddElement();
                        }
                        else if(mode == 1)
                        {
                            Link testLink = new Link();
                            testLink.Create();
                        }
                            
                        break;
                    case SDL.SDL_EventType.SDL_MOUSEBUTTONUP:
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

                for (int i = 0; i < ElementManager.Instance.elementList.Count; i++)
                {
                    SDL.SDL_SetRenderDrawColor(Window.Instance.renderer, 55, 55, 55, 255);
                    if (ElementManager.Instance.elementList[i]._type == "link")
                    {
                        SDL.SDL_RenderDrawLine(Window.Instance.renderer, ElementManager.Instance.elementList[i]._rect.x, ElementManager.Instance.elementList[i]._rect.y, ElementManager.Instance.elementList[i]._rect.w, ElementManager.Instance.elementList[i]._rect.h);
                    }
                    else
                    {
                        SDL.SDL_SetRenderDrawColor(Window.Instance.renderer, 255, 255, 255, 255);
                        SDL.SDL_RenderFillRect(Window.Instance.renderer, ref ElementManager.Instance.elementList[i]._rect);
                        SDL.SDL_SetRenderDrawColor(Window.Instance.renderer, 55, 55, 55, 255);
                        SDL.SDL_RenderDrawRect(Window.Instance.renderer, ref ElementManager.Instance.elementList[i]._rect);
                        ElementManager.Instance.elementList[i]._textBox.DrawText(Window.Instance.renderer);
                    }
                    
                }
                SDL.SDL_SetRenderDrawColor(Window.Instance.renderer, 0, 55, 55, 255);
                SDL.SDL_RenderPresent(Window.Instance.renderer);
            }
        }

        static void Main(string[] args)
        {
            CreateWindow();
            Console.WriteLine("Git ignore test");
            Window.Instance.InitFont();
            Window.Instance.sampleColour.r = 0;
            Window.Instance.sampleColour.g = 0;
            Window.Instance.sampleColour.b = 0;
            
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