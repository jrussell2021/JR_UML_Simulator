using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;

namespace UML_Simulator_SDL2.UserInterface
{
    internal class MainMenu
    {
        static readonly MainMenu instance = new MainMenu();

        static MainMenu()
        {
        }

        private MainMenu()
        {
        }

        public static MainMenu Instance
        {
            get
            {
                return instance;
            }
        }

        public void InitMainMenu()
        {
            rect.x = 6;
            rect.y = 6;
            rect.w = Window.Instance.width - 12;
            rect.h = Window.Instance.height / 2;

            //Add close button
            buttonList.Add(new MainMenuButton());
            buttonList[buttonList.Count - 1]._rect.x = xpos;
            buttonList[buttonList.Count - 1]._rect.y = ypos;
            buttonList[buttonList.Count - 1]._rect.w = 120;
            buttonList[buttonList.Count - 1]._rect.h = 80;

            buttonList[buttonList.Count - 1]._textBox.SetTextSurface(Window.Instance.font, buttonList[buttonList.Count - 1].text, Window.Instance.sampleColour);

            buttonList[buttonList.Count - 1]._textBox.SetText(Window.Instance.renderer);
            buttonList[buttonList.Count - 1]._textBox._rect.x = xpos + 10;
            buttonList[buttonList.Count - 1]._textBox._rect.y = ypos + 20;
            buttonList[buttonList.Count - 1]._textBox._rect.w = 100;
            buttonList[buttonList.Count - 1]._textBox._rect.h = 40;
            xpos += 130;

            //Add test diagram button
            buttonList.Add(new TestDiagramButton());
            buttonList[buttonList.Count - 1]._rect.x = xpos;
            buttonList[buttonList.Count - 1]._rect.y = ypos;
            buttonList[buttonList.Count - 1]._rect.w = 120;
            buttonList[buttonList.Count - 1]._rect.h = 80;

            buttonList[buttonList.Count - 1]._textBox.SetTextSurface(Window.Instance.font, buttonList[buttonList.Count - 1].text, Window.Instance.sampleColour);

            buttonList[buttonList.Count - 1]._textBox.SetText(Window.Instance.renderer);
            buttonList[buttonList.Count - 1]._textBox._rect.x = xpos + 10;
            buttonList[buttonList.Count - 1]._textBox._rect.y = ypos + 20;
            buttonList[buttonList.Count - 1]._textBox._rect.w = 100;
            buttonList[buttonList.Count - 1]._textBox._rect.h = 40;
            xpos += 130;
        }

        public void DrawMainMenu()
        {
            SDL.SDL_SetRenderDrawColor(Window.Instance.renderer, 255, 255, 255, 255);
            SDL.SDL_RenderFillRect(Window.Instance.renderer, ref rect);

            for(int i = 0; i < buttonList.Count; i++)
            {
                SDL.SDL_SetRenderDrawColor(Window.Instance.renderer, 180, 180, 180, 255);
                SDL.SDL_RenderFillRect(Window.Instance.renderer, ref buttonList[i]._rect);
                SDL.SDL_SetRenderDrawColor(Window.Instance.renderer, 55, 55, 55, 255);
                SDL.SDL_RenderDrawRect(Window.Instance.renderer, ref buttonList[i]._rect);
                buttonList[i]._textBox.DrawText(Window.Instance.renderer);
            }
        }
        public bool CheckMainMenuInteraction()
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

        public SDL.SDL_Rect rect;
        public int xpos = 16, ypos = 16;
        public bool isActive = false;
        public int selectedDiagram = 0;
        public List<Button> buttonList = new List<Button>();
    }
}
