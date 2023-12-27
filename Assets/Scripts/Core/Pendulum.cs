using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum 
{
    private PendulumView _view;
    public Pendulum(PendulumView view)
    {
        _view = view;
    }

    public void ThrowCircle()
    {
        _view.ThrowCircle();
    }

    public void DestroyCircles()
    {
        _view.DestroyCreated();
    }
    
}
