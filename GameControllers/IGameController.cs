using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace YoloDetection
{
    
    interface IGameController {
        void MoveTo(Vector vector);
        void MoveOffset(Vector vector);
        void MakeCommand(GameCommand command);
        Vector StringToVector(string str);
    }
}
