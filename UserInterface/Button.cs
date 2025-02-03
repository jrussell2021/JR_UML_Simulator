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
        public bool IsMouseInBounds()
        {
            int x, y = 0;
            SDL.SDL_GetMouseState(out x, out y);

            if(x >= _rect.x && y >= _rect.y && x <= (_rect.x + _rect.w) && y <= (_rect.y + _rect.h))
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
    }

    class MainMenuButton : Button
    {
        public MainMenuButton()
        {
            text = "Main Menu";
        }

        public override void OnClick()
        {
            MainMenu.Instance.isActive = !MainMenu.Instance.isActive;
        }

    }

    class TestDiagramButton : Button
    {
        public TestDiagramButton()
        {
            text = "Test Diagram";
        }

        public override void OnClick()
        {
            //Start new test diagram
            SDL.SDL_ShowSimpleMessageBox(0x0, "Create Test Diagram", "Creating new test diagram. The current diagram cannot be saved.", Window.Instance.window);
            MainMenu.Instance.selectedDiagram = 1;
            Toolbar.Instance.UpdateToolbar(MainMenu.Instance.selectedDiagram);
            ElementManager.Instance.ClearElements();
            MainMenu.Instance.isActive = !MainMenu.Instance.isActive;
        }

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
