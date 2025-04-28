using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;
using UML_Simulator_SDL2.Elements;
using UML_Simulator_SDL2.UserInterface;

namespace UML_Simulator_SDL2
{
    internal class Link : DiagramElement
    {
        int startx, starty, endx, endy;

        public Link()
        {
            _type = "link";
        }
        
        public void ClampStart(DiagramElement targetElement)
        {
            Node shortestNode = targetElement._nodeList[0];
            double diffx, diffy;
            double sqrDistance, actDistance;
            
            //
            for(int i = 0; i < targetElement._nodeList.Count; i++)
            {
                //Calc diff
                diffx = targetElement._nodeList[i].x - _rect.x;
                diffy = targetElement._nodeList[i].y - _rect.y;
                sqrDistance = (diffx * diffx) + (diffy * diffy);
                actDistance = Math.Sqrt(sqrDistance);

                //Turn negative values into positive values.
                actDistance = actDistance * actDistance;
                actDistance = Math.Sqrt(actDistance);

                //Set node distance
                targetElement._nodeList[i].distance = actDistance;

                //Set shortest node
                if(actDistance < shortestNode.distance)
                {
                    shortestNode = targetElement._nodeList[i];
                }
            }

            _rect.x = shortestNode.x;
            _rect.y = shortestNode.y;
        }

        public void ClampEnd(DiagramElement targetElement)
        {
            Node shortestNode = targetElement._nodeList[0];
            double diffx, diffy;
            double sqrDistance, actDistance;

            //
            for (int i = 0; i < targetElement._nodeList.Count; i++)
            {
                //Calc diff
                diffx = targetElement._nodeList[i].x - _rect.w;
                diffy = targetElement._nodeList[i].y - _rect.h;
                sqrDistance = (diffx * diffx) + (diffy * diffy);
                actDistance = Math.Sqrt(sqrDistance);

                //Turn negative values into positive values.
                actDistance = actDistance * actDistance;
                actDistance = Math.Sqrt(actDistance);

                //Set node distance
                targetElement._nodeList[i].distance = actDistance;

                //Set shortest node
                if (actDistance < shortestNode.distance)
                {
                    shortestNode = targetElement._nodeList[i];
                }
            }

            _rect.w = shortestNode.x;
            _rect.h = shortestNode.y;
        }

        public void Create()
        {
            SDL.SDL_GetMouseState(out _rect.x, out _rect.y);
            //Add start link to element
            for (int x = 0; x < ElementManager.Instance.elementList.Count; x++)
            {
                if (ElementManager.Instance.elementList[x]._type != "link" && ElementManager.Instance.elementList[x]._type != "sysbound" && ElementManager.Instance.elementList[x].IsMouseInBounds() == true)
                {
                    ElementManager.Instance.elementList[x]._linkStartList.Add(this);
                    ClampStart(ElementManager.Instance.elementList[x]);
                }

            }

            SDL.SDL_PollEvent(out SDL.SDL_Event e);
            while (e.type != SDL.SDL_EventType.SDL_MOUSEBUTTONUP)
            {
                SDL.SDL_PollEvent(out e);
                switch (e.type)
                {
                    case SDL.SDL_EventType.SDL_MOUSEBUTTONUP:
                        SDL.SDL_GetMouseState(out _rect.w, out _rect.h);
                        break;
                }
            }

            ElementManager.Instance.elementList.Add(this);

            //Add end link to element
            for (int x = 0; x < ElementManager.Instance.elementList.Count; x++) 
            {
                if (ElementManager.Instance.elementList[x]._type != "link" && ElementManager.Instance.elementList[x]._type != "sysbound" && ElementManager.Instance.elementList[x].IsMouseInBounds() == true) 
                {
                    ElementManager.Instance.elementList[x]._linkEndList.Add(this);
                    ClampEnd(ElementManager.Instance.elementList[x]);
                }

            }
        }
    }
    
}
