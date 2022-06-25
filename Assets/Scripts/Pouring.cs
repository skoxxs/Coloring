using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class Pouring : PaintTools
{
    private Image image;
    private Texture2D texture;
    private bool canPouring = true;
    public Pouring(Image _imag)
    {
        image = _imag;
        texture = image.sprite.texture;

    }

    public void Draw(int pixelX, int pixelY, Color color)
    {
        if (canPouring)
        {
            canPouring = false;
            LinePouring(pixelX, pixelY, color);
        }
        
    }

    public void LinePouring(int pixelX, int pixelY, Color newColor)
    {
        //переделаный алгоритм с другого языка

        Color oldColor = texture.GetPixel(pixelX, pixelY);

        if (newColor == oldColor) 
            return;
        Vector2 point = new Vector2(pixelX, pixelY);

        List<Vector2> stack = new List<Vector2>();
        stack.Add(point);

        int w = texture.width;
        int h = texture.height;

        stack.Add(point);
        int spanLeft = 0; 
        int spanRight = 0;

        int y1;

        while (stack.Count != 0)
        {
            point = stack[0];
            stack.Remove(point);// Удаляем закрашенную точку из стека
            y1 = (int)point.y;


            //% Находим границу слева
            while (y1 >= 1 && texture.GetPixel((int)point.x, y1) == oldColor)
            {
                y1 = y1 - 1;
            }
            y1 = y1 + 1;
            spanLeft = 0;
            spanRight = 0;
            //% Топаем по строке от левой границы вправо
            while (y1 < h && texture.GetPixel((int)point.x, y1) == oldColor)
            {
                texture.SetPixel((int)point.x, y1, newColor); //% Закрашиваем текущую точку
                if (spanLeft == 0 && point.x > 0 && texture.GetPixel((int)point.x - 1, y1) == oldColor)
                {
                    Vector2 newpoint = new Vector2();
                    newpoint.x = point.x - 1; 
                    newpoint.y = y1; 
                    stack.Add(newpoint);
                    spanLeft = 1;
                }
                else
                {
                    if (spanLeft == 1 && point.x > 0 && texture.GetPixel((int)point.x - 1, y1) != oldColor)
                    spanLeft = 0;
                }


                if (spanRight == 0 && point.x < w && texture.GetPixel((int)point.x + 1, y1) == oldColor)
                {
                    Vector2 newpoint = new Vector2();
                    newpoint.x = point.x + 1; 
                    newpoint.y = y1; 
                    stack.Add(newpoint); ;
                    spanRight = 1;
                }
                else
                {
                    if (spanRight == 1 && point.x < w && texture.GetPixel((int)point.x + 1, y1) != oldColor)
                    spanRight = 0;
                }

                y1 = y1 + 1;
            
            }
        }
    }

    public void DrawStop()
    {
        canPouring = true;
    }
}
