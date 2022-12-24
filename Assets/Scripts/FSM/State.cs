using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeamUp.FSM
{
    public abstract class State
    {
        public abstract void Enter();
        public abstract void Exit();
        public abstract void Update(float deltaTime);
    }
}
