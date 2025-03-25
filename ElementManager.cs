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

            elementList[elementList.Count - 1].CreateNodes();
        }

        public void DrawDiagonalSquare(SDL.SDL_Rect rect)
        {
            Node tempNode;
            Node[] points = new Node[4];
            //top
            points[0] = new Node(rect.x + (rect.w / 2), rect.y);
            //right
            points[1] = new Node(rect.x + rect.w, rect.y + (rect.h / 2));
            //bottom
            points[2] = new Node(rect.x + (rect.w / 2), rect.y + rect.h);
            //left
            points[3] = new Node(rect.x, rect.y + (rect.h / 2));

            SDL.SDL_RenderDrawLine(Window.Instance.renderer, points[0].x, points[0].y, points[1].x, points[1].y);
            SDL.SDL_RenderDrawLine(Window.Instance.renderer, points[1].x, points[1].y, points[2].x, points[2].y);
            SDL.SDL_RenderDrawLine(Window.Instance.renderer, points[2].x, points[2].y, points[3].x, points[3].y);
            SDL.SDL_RenderDrawLine(Window.Instance.renderer, points[3].x, points[3].y, points[0].x, points[0].y);
        }

        public void DrawCircle(int circleX, int circleY, int radius)
        {
            int diameter = radius * 2;

            int x = radius - 1;
            int y = 0;
            int tx = 1;
            int ty = 1;
            int error = tx - diameter;

            while (x >= y)
            {
                SDL.SDL_RenderDrawPoint(Window.Instance.renderer, circleX + x, circleY - y);
                SDL.SDL_RenderDrawPoint(Window.Instance.renderer, circleX + x, circleY + y);
                SDL.SDL_RenderDrawPoint(Window.Instance.renderer, circleX - x, circleY - y);
                SDL.SDL_RenderDrawPoint(Window.Instance.renderer, circleX - x, circleY + y);
                SDL.SDL_RenderDrawPoint(Window.Instance.renderer, circleX + y, circleY - x);
                SDL.SDL_RenderDrawPoint(Window.Instance.renderer, circleX + y, circleY + x);
                SDL.SDL_RenderDrawPoint(Window.Instance.renderer, circleX - y, circleY - x);
                SDL.SDL_RenderDrawPoint(Window.Instance.renderer, circleX - y, circleY + x);

                
                if (error <= 0)
                {
                    ++y;
                    error += ty;
                    ty += 2;
                }

                if (error > 0)
                {
                    --x;
                    tx += 2;
                    error += (tx - diameter);
                }
                
            }
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
                else if (elementList[i]._type == "start")
                {
                    DrawCircle(elementList[i]._rect.x + (elementList[i]._rect.w / 2), elementList[i]._rect.y + (elementList[i]._rect.h / 2), elementList[i]._rect.h / 2);
                    SDL.SDL_SetRenderDrawColor(Window.Instance.renderer, 255, 255, 255, 255);
                    elementList[i]._textBox.DrawText(Window.Instance.renderer);
                }
                else if (elementList[i]._type == "dec")
                {
                    DrawDiagonalSquare(elementList[i]._rect);
                    SDL.SDL_SetRenderDrawColor(Window.Instance.renderer, 255, 255, 255, 255);
                    elementList[i]._textBox.DrawText(Window.Instance.renderer);
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

        public bool MoveElements()
        {
            for (int d = 0; d < elementList.Count; d++)
            {
                if (elementList[d].IsMouseInBounds() == true)
                {
                    elementList[d].MoveElement();
                    return true;
                }
            }
            return false;
        }

        public void ScrollElements(int scrollValueX, int scrollValueY)
        {
            for (int i = 0; i < elementList.Count; i++)
            {
                if (elementList[i]._type == "link")
                {
                    elementList[i]._rect.x += scrollValueX;
                    elementList[i]._rect.y += scrollValueY;
                    elementList[i]._rect.w += scrollValueX;
                    elementList[i]._rect.h += scrollValueY;
                }
                else
                {
                    elementList[i]._rect.x += scrollValueX;
                    elementList[i]._rect.y += scrollValueY;
                    elementList[i]._textBox._rect.x += scrollValueX;
                    elementList[i]._textBox._rect.y += scrollValueY;
                }

                //Nodes
                for (int p = 0; p < elementList[i]._nodeList.Count; p++)
                {
                    elementList[i]._nodeList[p].x += scrollValueX;
                    elementList[i]._nodeList[p].y += scrollValueY;
                }

            }
        }

        public void EditTextBox()
        {
            for(int i = 0; i < elementList.Count; i++)
            {
                if (elementList[i]._textBox.IsMouseInBounds())
                {
                    string updatedText = EventProcessor.Instance.GetInput();
                    elementList[i]._textBox.SetTextSurface(Window.Instance.font, updatedText, Window.Instance.sampleColour);
                    elementList[i]._textBox.SetText(Window.Instance.renderer);
                }
            }
        }

        public void ClearElements()
        {
            for(int x = 0; x < elementList.Count; x++)
            {
                elementList[x]._textBox.DestroyTextBox();
                
            }
            elementList.Clear();
        }
        public List<DiagramElement> elementList = new List<DiagramElement>();

    }
}
