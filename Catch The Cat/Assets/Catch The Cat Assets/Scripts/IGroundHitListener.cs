using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    public interface IGroundHitListener
    {
        void OnGroundHit(GameObject fallingObject);
    }
}
