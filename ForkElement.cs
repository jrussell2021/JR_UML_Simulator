using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;

namespace UML_Simulator_SDL2
{
    internal class ForkElement : DecisionElement
    {
        public void Create()
        {
            int x, y;
            SDL.SDL_GetMouseState(out x, out y);
            _type = "fork";

            _rect.x = x;
            _rect.y = y;
            _rect.w = 320;
            _rect.h = 20;

            //Text
            string temp = "    Fork    ";
            _textBox.SetTextSurface(Window.Instance.font, temp, Window.Instance.sampleColour);

            _textBox.SetText(Window.Instance.renderer);
            _textBox._rect.x = x + 110;
            _textBox._rect.y = y + 10;
            _textBox._rect.w = 100;
            _textBox._rect.h = 40;

            ElementManager.Instance.elementList.Add(this);

            CreateNodes();
        }
    }
}
