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
            textSurface = SDL_ttf.TTF_RenderText_Blended_Wrapped(font, text, colour, 400);
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

        public SDL.SDL_Rect _rect;
        public IntPtr textSurface;
        public IntPtr text;
    }
}
