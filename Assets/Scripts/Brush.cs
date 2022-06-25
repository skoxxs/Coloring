using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Brush : PaintTools
{
    private Image image;
    private bool[,] maskBrush;
    private int oldX = -1;
    private int oldY = -1;
    private float maxStepTool = 5f;
    private int twoRadius = 21;
    public Brush(Image _imag)
    {
        image = _imag;
        maskBrush = new bool[twoRadius, twoRadius];
        for (int i = 0; i < twoRadius; i++)
            for (int j = 0; j < twoRadius; j++)
                maskBrush[i,j] = false;
        FillMaskBrush();

    }
    
    public void Draw(int x, int y, Color SelectColor)
    {
        if (oldX < 0 || oldY < 0)
        {
            oldX = x;
            oldY = y;
            brushDraw(x, y, SelectColor);
            return;
        }
        else
        {
            if ((oldX - x) * (oldX - x) + (oldY - y) * (oldY - y) < maxStepTool * maxStepTool)
            {
                brushDraw(x, y, SelectColor);
            }
            else
            {
                int count = (int)(Mathf.Sqrt((oldX - x) * (oldX - x) + (oldY - y) * (oldY - y)) / maxStepTool);
                
                float secX = ((float)(x - oldX)) / (float)count;
                float secY = ((float)(y - oldY)) / (float)count;
                

                for (int i = 0; i <= count; i++)
                {
                    brushDraw((int)(oldX + secX * i), (int)(oldY + secY * i), SelectColor);
                }
                
            }
        }
        oldX = x;
        oldY = y;
    }

    private int startPositionX;
    private int startPositionY;
    private void brushDraw(int pixelX, int pixelY, Color color)
    {
        startPositionX = pixelX - twoRadius / 2;
        startPositionY = pixelY - twoRadius / 2;
        for (int i = 0; i < twoRadius; i++)
            for (int j = 0; j < twoRadius; j++)
            {
                if (maskBrush[i, j])
                {
                    image.sprite.texture.SetPixel(startPositionX + i, startPositionY + j, color);
                }
            }
    }
    private void FillMaskBrush()
    {
        //алгоритм с другого языка рисуюший круг + закраска аналог x^2 + y^2= r^2
        int pixelX = 10;
        int pixelY = 10;
        
            // R - радиус, X1, Y1 - координаты центра
            int R = (twoRadius-1)/2;
            int x = 0;
            int y = R;
            int delta = 1 - 2 * R;
            int error = 0;
            while (y >= x)
            {
                maskBrush[pixelX + x, pixelY + y] = true;
                maskBrush[pixelX + x, pixelY - y] = true;
                for (int i = pixelY - y; i < pixelY + y; i++)
                {
                    maskBrush[pixelX + x, i] = true;
                }
                maskBrush[pixelX - x, pixelY + y] = true;
                maskBrush[pixelX - x, pixelY - y] = true;
                for (int i = pixelY - y; i < pixelY + y; i++)
                {
                    maskBrush[pixelX - x, i] = true;
                }
                maskBrush[pixelX + y, pixelY + x] = true;
                maskBrush[pixelX + y, pixelY - x] = true;
                for (int i = pixelY - x; i < pixelY + x; i++)
                {
                    maskBrush[pixelX + y, i] = true;
                }
                maskBrush[pixelX - y, pixelY + x] = true;
                maskBrush[pixelX - y, pixelY - x] = true;
                for (int i = pixelY - x; i < pixelY + x; i++)
                {
                    maskBrush[pixelX - y, i] = true;
                }
                error = 2 * (delta + y) - 1;
                if ((delta < 0) && (error <= 0))
                {
                    delta += 2 * ++x + 1;
                    continue;
                }
                if ((delta > 0) && (error > 0))
                {
                    delta -= 2 * --y + 1;
                    continue;
                }
                delta += 2 * (++x - --y);
            }
            
        }

    public void DrawStop()
    {
        oldX = oldY = -1;
    }
}
