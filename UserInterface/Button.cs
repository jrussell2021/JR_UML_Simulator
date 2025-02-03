using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;

namespace UML_Simulator_SDL2.UserInterface
{
    abstract class Button
    {
        public SDL.SDL_Rect _rect;
        public string text;
        public TextBox _textBox = new();

        public abstract void OnClick();
    }

    class AddElementButton : Button
    {
        public AddElementButton()
        {
            text = "Add Element";
        }

        public override void OnClick()
        {
            Toolbar.Instance.mode = 0;
        }

    }

    class AddLineButton : Button
    {
        public AddLineButton()
        {
            text = "Add Line";
        }

        public override void OnClick()
        {
            Toolbar.Instance.mode = 1;
        }

    }
}
