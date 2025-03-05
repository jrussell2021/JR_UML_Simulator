using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;

namespace UML_Simulator_SDL2.UserInterface
{
    internal class Toolbar
    {
        static readonly Toolbar instance = new Toolbar();

        static Toolbar()
        {
        }

        private Toolbar()
        {
        }

        public static Toolbar Instance
        {
            get
            {
                return instance;
            }
        }

        public int xpos = 10;
        public int mode = 0;
        public List<Button> buttonList = new List<Button>();

        public void InitToolbar()
        {
            //Add main menu button
            buttonList.Add(new MainMenuButton());
            buttonList[buttonList.Count - 1]._rect.x = xpos;
            buttonList[buttonList.Count - 1]._rect.y = 10;
            buttonList[buttonList.Count - 1]._rect.w = 120;
            buttonList[buttonList.Count - 1]._rect.h = 80;

            buttonList[buttonList.Count - 1]._textBox.SetTextSurface(Window.Instance.font, buttonList[buttonList.Count - 1].text, Window.Instance.sampleColour);

            buttonList[buttonList.Count - 1]._textBox.SetText(Window.Instance.renderer);
            buttonList[buttonList.Count - 1]._textBox._rect.x = xpos + 10;
            buttonList[buttonList.Count - 1]._textBox._rect.y = 30;
            buttonList[buttonList.Count - 1]._textBox._rect.w = 100;
            buttonList[buttonList.Count - 1]._textBox._rect.h = 40;
            xpos += 130;

        }
        
        public void UpdateToolbar(int selectedDiagram)
        {
            buttonList.Clear();
            xpos = 10;
            InitToolbar();
            if(selectedDiagram == 1)
            {
                //Add element button
                buttonList.Add(new AddElementButton());
                buttonList[buttonList.Count - 1]._rect.x = xpos;
                buttonList[buttonList.Count - 1]._rect.y = 10;
                buttonList[buttonList.Count - 1]._rect.w = 120;
                buttonList[buttonList.Count - 1]._rect.h = 80;

                buttonList[buttonList.Count - 1]._textBox.SetTextSurface(Window.Instance.font, buttonList[buttonList.Count - 1].text, Window.Instance.sampleColour);

                buttonList[buttonList.Count - 1]._textBox.SetText(Window.Instance.renderer);
                buttonList[buttonList.Count - 1]._textBox._rect.x = xpos + 10;
                buttonList[buttonList.Count - 1]._textBox._rect.y = 30;
                buttonList[buttonList.Count - 1]._textBox._rect.w = 100;
                buttonList[buttonList.Count - 1]._textBox._rect.h = 40;
                xpos += 130;

                //Add line button
                buttonList.Add(new AddLineButton());
                buttonList[buttonList.Count - 1]._rect.x = xpos;
                buttonList[buttonList.Count - 1]._rect.y = 10;
                buttonList[buttonList.Count - 1]._rect.w = 120;
                buttonList[buttonList.Count - 1]._rect.h = 80;

                buttonList[buttonList.Count - 1]._textBox.SetTextSurface(Window.Instance.font, buttonList[buttonList.Count - 1].text, Window.Instance.sampleColour);

                buttonList[buttonList.Count - 1]._textBox.SetText(Window.Instance.renderer);
                buttonList[buttonList.Count - 1]._textBox._rect.x = xpos + 10;
                buttonList[buttonList.Count - 1]._textBox._rect.y = 30;
                buttonList[buttonList.Count - 1]._textBox._rect.w = 100;
                buttonList[buttonList.Count - 1]._textBox._rect.h = 40;
                xpos += 130;

                //Add Move Element Button
                buttonList.Add(new MoveElementButton());
                buttonList[buttonList.Count - 1]._rect.x = xpos;
                buttonList[buttonList.Count - 1]._rect.y = 10;
                buttonList[buttonList.Count - 1]._rect.w = 120;
                buttonList[buttonList.Count - 1]._rect.h = 80;

                buttonList[buttonList.Count - 1]._textBox.SetTextSurface(Window.Instance.font, buttonList[buttonList.Count - 1].text, Window.Instance.sampleColour);

                buttonList[buttonList.Count - 1]._textBox.SetText(Window.Instance.renderer);
                buttonList[buttonList.Count - 1]._textBox._rect.x = xpos + 10;
                buttonList[buttonList.Count - 1]._textBox._rect.y = 30;
                buttonList[buttonList.Count - 1]._textBox._rect.w = 100;
                buttonList[buttonList.Count - 1]._textBox._rect.h = 40;
                xpos += 130;
            }
        }

        public void DrawToolbar()
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

        public bool CheckToolbarInteraction()
        {
            for(int i = 0; i < buttonList.Count; i++)
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
