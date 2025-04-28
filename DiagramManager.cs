using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML_Simulator_SDL2.UserInterface;

namespace UML_Simulator_SDL2
{
    internal class DiagramManager
    {
        static readonly DiagramManager instance = new DiagramManager();

        static DiagramManager()
        {
        }

        private DiagramManager()
        {
        }

        public static DiagramManager Instance
        {
            get
            {
                return instance;
            }
        }

        public void CreateDiagram()
        {
            diagramList.Add(new Diagram());
            diagramList[diagramList.Count - 1]._type = MainMenu.Instance.selectedDiagram;
            
            SetActiveDiagram(diagramList[diagramList.Count - 1]);

        }

        public void SetActiveDiagram(Diagram pDiagram)
        {
            activeDiagram = pDiagram;
            MainMenu.Instance.selectedDiagram = pDiagram._type;
            ElementManager.Instance.elementList = activeDiagram.elementList;
        }
        public List<Diagram> diagramList = new List<Diagram>();
        public Diagram activeDiagram;
    }
}
