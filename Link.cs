using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;

namespace UML_Simulator_SDL2
{
    internal class Link : DiagramElement
    {
        int startx, starty, endx, endy;
        public Link()
        {
            _type = "link";
        }
        public void Create()
        {
            SDL.SDL_GetMouseState(out _rect.x, out _rect.y);

            SDL.SDL_PollEvent(out SDL.SDL_Event e);
            while (e.type != SDL.SDL_EventType.SDL_MOUSEBUTTONUP)
            {
                SDL.SDL_PollEvent(out e);
                switch (e.type)
                {
                    case SDL.SDL_EventType.SDL_MOUSEBUTTONUP:
                        SDL.SDL_GetMouseState(out _rect.w, out _rect.h);
                        break;
                }
            }

            ElementManager.Instance.elementList.Add(this);
        }
    }
    
}
