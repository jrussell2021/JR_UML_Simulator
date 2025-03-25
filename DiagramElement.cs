using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;

namespace UML_Simulator_SDL2
{
    internal class DiagramElement
    {
        public SDL.SDL_Rect _rect;
        public string _type;
        public string text;
        public TextBox _textBox = new();

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

        public void MoveElement()
        {
            if(_type == "link")
            {
                return;
            }
            SDL.SDL_PollEvent(out SDL.SDL_Event e);
            while (e.type != SDL.SDL_EventType.SDL_MOUSEBUTTONUP)
            {
                SDL.SDL_PollEvent(out e);
                switch (e.type)
                {
                    case SDL.SDL_EventType.SDL_MOUSEBUTTONUP:
                        int diffx, diffy;
                        int newx, newy;
                        SDL.SDL_GetMouseState(out newx, out newy);
                        diffx = newx - _rect.x;
                        diffy = newy - _rect.y;
                        _rect.x += diffx;
                        _rect.y += diffy;

                        _textBox._rect.x += diffx;
                        _textBox._rect.y += diffy;

                        //start list
                        for (int i = 0; i < _linkStartList.Count; i++)
                        {
                            _linkStartList[i]._rect.x += diffx;
                            _linkStartList[i]._rect.y += diffy;
                        }
                        //end list
                        for (int i = 0; i < _linkEndList.Count; i++)
                        {
                            _linkEndList[i]._rect.w += diffx;
                            _linkEndList[i]._rect.h += diffy;
                        }
                        //Nodes
                        for(int i = 0; i < _nodeList.Count; i++)
                        {
                            _nodeList[i].x += diffx;
                            _nodeList[i].y += diffy;
                        }
                        break;
                }
            }
        }

        public void CreateNodes()
        {
            //Top Left
            _nodeList.Add(new Node(_rect.x, _rect.y));
            //Top Right
            _nodeList.Add(new Node(_rect.x + _rect.w, _rect.y));
            //Bottom Left
            _nodeList.Add(new Node(_rect.x, _rect.y + _rect.h));
            //Bottom Right
            _nodeList.Add(new Node(_rect.x + _rect.w, _rect.y + _rect.h));

            //Middle Top
            _nodeList.Add(new Node(_rect.x + (_rect.w / 2), _rect.y));

            //Middle Bottom
            _nodeList.Add(new Node(_rect.x + (_rect.w / 2), _rect.y + _rect.h));

            //Middle Left
            _nodeList.Add(new Node(_rect.x, _rect.y + (_rect.h / 2)));

            //Middle Right
            _nodeList.Add(new Node(_rect.x + _rect.w, _rect.y + (_rect.h / 2)));

        }

        public void MoveNodes(int shiftX, int shiftY)
        {
            for(int i = 0; i < _nodeList.Count; i++)
            {
                _nodeList[i].x += shiftX;
                _nodeList[i].y += shiftY;
            }
        }
        public List<DiagramElement> _linkStartList = new List<DiagramElement>();
        public List<DiagramElement> _linkEndList = new List<DiagramElement>();
        public List<Node> _nodeList = new List<Node>();
    }
}
