using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;

namespace UML_Simulator_SDL2
{
    internal class ElementManager
    {
        static readonly ElementManager instance = new ElementManager();

        static ElementManager()
        {
        }

        private ElementManager()
        {
        }

        public static ElementManager Instance
        {
            get
            {
                return instance;
            }
        }

        public void AddElement()
        {
            int x, y = 0;
            SDL.SDL_GetMouseState(out x, out y);

            elementList.Add(new DiagramElement());
            elementList[elementList.Count - 1]._rect.x = x;
            elementList[elementList.Count - 1]._rect.y = y;
            elementList[elementList.Count - 1]._rect.w = 120;
            elementList[elementList.Count - 1]._rect.h = 80;

            //Text
            string temp = "Element " + (elementList.Count - 1);
            elementList[elementList.Count - 1]._textBox.SetTextSurface(Window.Instance.font, temp, Window.Instance.sampleColour);
            
            elementList[elementList.Count - 1]._textBox.SetText(Window.Instance.renderer);
            elementList[elementList.Count - 1]._textBox._rect.x = x + 10;
            elementList[elementList.Count - 1]._textBox._rect.y = y + 20;
            elementList[elementList.Count - 1]._textBox._rect.w = 100;
            elementList[elementList.Count - 1]._textBox._rect.h = 40;
            
        }

        public void DrawElements()
        {
            for (int i = 0; i < elementList.Count; i++)
            {
                SDL.SDL_SetRenderDrawColor(Window.Instance.renderer, 55, 55, 55, 255);
                if (elementList[i]._type == "link")
                {
                    SDL.SDL_RenderDrawLine(Window.Instance.renderer, elementList[i]._rect.x, elementList[i]._rect.y, elementList[i]._rect.w, elementList[i]._rect.h);
                }
                else
                {
                    SDL.SDL_SetRenderDrawColor(Window.Instance.renderer, 255, 255, 255, 255);
                    SDL.SDL_RenderFillRect(Window.Instance.renderer, ref elementList[i]._rect);
                    SDL.SDL_SetRenderDrawColor(Window.Instance.renderer, 55, 55, 55, 255);
                    SDL.SDL_RenderDrawRect(Window.Instance.renderer, ref elementList[i]._rect);
                    elementList[i]._textBox.DrawText(Window.Instance.renderer);
                }

            }
        }

        public void ClearElements()
        {
            elementList.Clear();
        }
        public List<DiagramElement> elementList = new List<DiagramElement>();

    }
}
