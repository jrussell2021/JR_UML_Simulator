using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;

namespace UML_Simulator_SDL2
{
    public class TextBox
    {
        public void SetTextSurface(IntPtr font, string text, SDL.SDL_Color colour)
        {
            textSurface = SDL_ttf.TTF_RenderText_Solid(font, text, colour);
        }

        public void SetText(IntPtr renderer)
        {
            text = SDL.SDL_CreateTextureFromSurface(renderer, textSurface);
        }

        public void DrawText(IntPtr renderer)
        {
            SDL.SDL_RenderCopy(renderer, text, (IntPtr)null, ref _rect);
        }

        public void DestroyTextBox()
        {
            SDL.SDL_FreeSurface(textSurface);
            SDL.SDL_DestroyTexture(text);
        }
        public SDL.SDL_Rect _rect;
        public IntPtr textSurface;
        public IntPtr text;
    }
}
