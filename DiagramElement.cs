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

        public List<DiagramElement> linkList = new List<DiagramElement>();
    }
}
