using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;

namespace UML_Simulator_SDL2
{
    internal class DiagramElement
    {
        public SDL.SDL_Rect _rect;
        public string _type;
        public string text;
        public TextBox _textBox = new();

        public bool IsMouseInBounds()
        {
            int x, y = 0;
            SDL.SDL_GetMouseState(out x, out y);

            if (x >= _rect.x && y >= _rect.y && x <= (_rect.x + _rect.w) && y <= (_rect.y + _rect.h))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public void MoveElement()
        {
            SDL.SDL_PollEvent(out SDL.SDL_Event e);
            while (e.type != SDL.SDL_EventType.SDL_MOUSEBUTTONUP)
            {
                SDL.SDL_PollEvent(out e);
                switch (e.type)
                {
                    case SDL.SDL_EventType.SDL_MOUSEBUTTONUP:
                        int diffx, diffy;
                        int newx, newy;
                        SDL.SDL_GetMouseState(out newx, out newy);
                        diffx = newx - _rect.x;
                        diffy = newy - _rect.y;
                        _rect.x += diffx;
                        _rect.y += diffy;

                        _textBox._rect.x += diffx;
                        _textBox._rect.y += diffy;
                        for(int i = 0; i < _linkEndList.Count; i++)
                        {
                            _linkEndList[i]._rect.w += diffx;
                            _linkEndList[i]._rect.h += diffy;
                        }
                        break;
                }
            }
        }

        public List<DiagramElement> _linkStartList = new List<DiagramElement>();
        public List<DiagramElement> _linkEndList = new List<DiagramElement>();
    }
}
