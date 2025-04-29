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
            text = "Activity Diagram";
        }

        public override void OnClick()
        {
            //Start new test diagram
            SDL.SDL_ShowSimpleMessageBox(0x0, "Create Activity Diagram", "Creating new activity diagram. The current diagram cannot be saved.", Window.Instance.window);
            MainMenu.Instance.selectedDiagram = 1;
            Toolbar.Instance.UpdateToolbar(MainMenu.Instance.selectedDiagram);
            //ElementManager.Instance.ClearElements();
            MainMenu.Instance.isActive = !MainMenu.Instance.isActive;


            //Create diagram
            DiagramManager.Instance.CreateDiagram();

            //Create page
            Pagebar.Instance.AddPage(DiagramManager.Instance.activeDiagram);
            Pagebar.Instance.buttonList[Pagebar.Instance.buttonList.Count - 1].OnClick();
        }

    }

    class UseCaseDiagramButton : Button
    {
        public UseCaseDiagramButton()
        {
            text = "Use Case Diagram";
        }

        public override void OnClick()
        {
            //Start new test diagram
            SDL.SDL_ShowSimpleMessageBox(0x0, "Create Use Case Diagram", "Creating new use case diagram. The current diagram cannot be saved.", Window.Instance.window);
            MainMenu.Instance.selectedDiagram = 2;
            Toolbar.Instance.UpdateToolbar(MainMenu.Instance.selectedDiagram);
            ElementManager.Instance.ClearElements();
            MainMenu.Instance.isActive = !MainMenu.Instance.isActive;

            //Create diagram
            DiagramManager.Instance.CreateDiagram();

            //Create page
            Pagebar.Instance.AddPage(DiagramManager.Instance.activeDiagram);
            Pagebar.Instance.buttonList[Pagebar.Instance.buttonList.Count - 1].OnClick();
        }

    }

    class AddElementButton : Button
    {
        public AddElementButton()
        {
            text = "Add Action";
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

    class MoveElementButton : Button
    {
        public MoveElementButton()
        {
            text = "Move Element";
        }

        public override void OnClick()
        {
            Toolbar.Instance.mode = 2;
        }

    }

    class EditTextButton : Button
    {
        public EditTextButton()
        {
            text = "Edit Text";
        }

        public override void OnClick()
        {
            Toolbar.Instance.mode = 3;
        }

    }

    class AddCircleButton : Button
    {
        public AddCircleButton()
        {
            text = "Add Start/End";
        }

        public override void OnClick()
        {
            Toolbar.Instance.mode = 4;
        }

    }

    class AddDecisionButton : Button
    {
        public AddDecisionButton()
        {
            text = "Add Decision";
        }

        public override void OnClick()
        {
            Toolbar.Instance.mode = 5;
        }

    }

    class AddForkButton : Button
    {
        public AddForkButton()
        {
            text = "Add Fork";
        }

        public override void OnClick()
        {
            Toolbar.Instance.mode = 6;
        }

    }

    class AddActorButton : Button
    {
        public AddActorButton()
        {
            text = "Add Actor";
        }

        public override void OnClick()
        {
            Toolbar.Instance.mode = 7;
        }

    }
    class AddUseCaseButton : Button
    {
        public AddUseCaseButton()
        {
            text = "Add Use Case";
        }

        public override void OnClick()
        {
            Toolbar.Instance.mode = 8;
        }

    }

    class AddSystemBoundaryButton : Button
    {
        public AddSystemBoundaryButton()
        {
            text = "Add System Boundary";
        }

        public override void OnClick()
        {
            Toolbar.Instance.mode = 9;
        }

    }
    class ExitButton : Button
    {
        public ExitButton()
        {
            text = "Exit App";
        }

        public override void OnClick()
        {
            Window.Instance.running = false;
        }

    }

    class PageButton : Button
    {
        public PageButton()
        {
            text = "Page";
        }

        Diagram targetDiagram = new Diagram();
        public void SetDiagram(Diagram pDiagram)
        {
            text = "Page";
            targetDiagram = pDiagram;
        }
        public override void OnClick()
        {
            DiagramManager.Instance.SetActiveDiagram(targetDiagram);
            Toolbar.Instance.UpdateToolbar(MainMenu.Instance.selectedDiagram);
        }

    }
}
