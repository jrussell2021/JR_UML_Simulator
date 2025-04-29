using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;
namespace UML_Simulator_SDL2.UserInterface
{
    internal class Pagebar
    {
        static readonly Pagebar instance = new Pagebar();

        static Pagebar()
        {
        }

        private Pagebar()
        {
        }

        public static Pagebar Instance
        {
            get
            {
                return instance;
            }
        }

        public int xpos = 10;
        public List<Button> buttonList = new List<Button>();

        public void AddPage(Diagram pDiagram)
        {
            PageButton tempButton = new PageButton();
            tempButton.SetDiagram(pDiagram);
            buttonList.Add(tempButton);

            buttonList[buttonList.Count - 1]._rect.x = xpos;
            buttonList[buttonList.Count - 1]._rect.y = Window.Instance.height - 100;
            buttonList[buttonList.Count - 1]._rect.w = 120;
            buttonList[buttonList.Count - 1]._rect.h = 80;

            buttonList[buttonList.Count - 1]._textBox.SetTextSurface(Window.Instance.font, buttonList[buttonList.Count - 1].text, Window.Instance.sampleColour);

            buttonList[buttonList.Count - 1]._textBox.SetText(Window.Instance.renderer);
            buttonList[buttonList.Count - 1]._textBox._rect.x = xpos + 10;
            buttonList[buttonList.Count - 1]._textBox._rect.y = Window.Instance.height - 80;
            buttonList[buttonList.Count - 1]._textBox._rect.w = 100;
            buttonList[buttonList.Count - 1]._textBox._rect.h = 40;
            xpos += 130;
        }
        public void DrawPagebar()
        {
            for (int i = 0; i < buttonList.Count; i++)
            {
                SDL.SDL_SetRenderDrawColor(Window.Instance.renderer, 255, 255, 255, 255);
                SDL.SDL_RenderFillRect(Window.Instance.renderer, ref buttonList[i]._rect);
                SDL.SDL_SetRenderDrawColor(Window.Instance.renderer, 55, 55, 55, 255);
                SDL.SDL_RenderDrawRect(Window.Instance.renderer, ref buttonList[i]._rect);
                buttonList[i]._textBox.DrawText(Window.Instance.renderer);
            }
        }

        public bool CheckPagebarInteraction()
        {
            for (int i = 0; i < buttonList.Count; i++)
            {
                if (buttonList[i].IsMouseInBounds() == true)
                {
                    buttonList[i].OnClick();
                    return true;
                }
            }

            return false;
        }
    }
}
