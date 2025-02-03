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
    }
}
