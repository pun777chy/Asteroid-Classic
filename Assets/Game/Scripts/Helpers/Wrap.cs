using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Helpers
{
    public class Wrap : MonoBehaviour
    {
       // Update is called once per frame
        void Update()
        {
            Vector3 viewPortPosition = Camera.main.WorldToViewportPoint(transform.position);
            Vector3 moveAdjustment = Vector3.zero;
            if(viewPortPosition.x < 0)
            {
                moveAdjustment.x += 1;
            }
            else if (viewPortPosition.x > 1)
            {
                moveAdjustment.x -= 1;
            }
            else if (viewPortPosition.y < 0)
            {
                moveAdjustment.y += 1;
            }
            else if (viewPortPosition.y > 1)
            {
                moveAdjustment.y -= 1;
            }

            transform.position = Camera.main.ViewportToWorldPoint(viewPortPosition + moveAdjustment);
        }
    }
}
