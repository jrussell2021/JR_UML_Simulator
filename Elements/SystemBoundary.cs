using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;

namespace UML_Simulator_SDL2.Elements
{
    internal class SystemBoundary : DiagramElement
    {
        public void Create()
        {
            _type = "sysbound";

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

            _rect.w = _rect.w - _rect.x;
            _rect.h = _rect.h - _rect.y;
            //Text
            string temp = "System Boundary";
            _textBox.SetTextSurface(Window.Instance.font, temp, Window.Instance.sampleColour);

            _textBox.SetText(Window.Instance.renderer);
            _textBox._rect.x = _rect.x;
            _textBox._rect.y = _rect.y + _rect.h;
            _textBox._rect.w = 100;
            _textBox._rect.h = 40;

            ElementManager.Instance.elementList.Add(this);

        }
    }
}
