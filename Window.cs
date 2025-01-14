using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;

namespace UML_Simulator_SDL2
{
    internal class Window
    {
        static readonly Window instance = new Window();

        static Window()
        {
        }

        private Window()
        {
        }

        public static Window Instance
        {
            get
            {
                return instance;
            }
        }
        public void InitFont()
        {
            font = SDL_ttf.TTF_OpenFont("open-sans.regular.ttf", 80);

            sampleColour = new();
        }

        public IntPtr renderer;
        public IntPtr window;
        public bool running = true;

        public IntPtr font;

        public SDL.SDL_Color sampleColour;

    }
}
