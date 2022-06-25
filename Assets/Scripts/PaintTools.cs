using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PaintTools 
{
    public void Draw(int pixelX, int pixelY, Color color);
    public void DrawStop();
}
