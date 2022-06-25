using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaintManager : MonoBehaviour
{
    [SerializeField] private Image image;
    private Color SelectColor;
    private Pouring pouring;
    private Brush brush;
    private Texture2D texture;
    private PaintTools paintTool;

    void Start()
    {
        texture = image.sprite.texture;
        brush = new Brush(image);
        pouring = new Pouring(image);
        SetColor(1);
        paintTool = brush;

    }

    public void Draw(float x, float y)
    {
        if (x > image.rectTransform.rect.width )
        {
            return;
        }
      
        x = (float)texture.width  / image.rectTransform.rect.width  * x;
        y = (float)texture.height / image.rectTransform.rect.height * y;

        paintTool.Draw((int)x,(int)y,SelectColor);
        image.sprite.texture.Apply();
       
    }

    public void DrawStop()
    {
        paintTool.DrawStop();
    }

    public void SelectBrush()
    {
        paintTool = brush;
    }
    public void SelectPouring()
    {
        paintTool = pouring;
    }


    public void SetColor(int colorNum)
    {
        switch (colorNum)
        {
            case 1:
                SelectColor = Color.red;
                break;

            case 2:
                SelectColor = Color.blue;
                break;

            case 3:
                SelectColor = Color.black;
                break;
            case 4:
                SelectColor =Color.green;
                break;
            case 5:
                SelectColor = new Color(0.8f, 0.3f, 0.1f);
                break;
            case 6:
                SelectColor = Color.yellow;
                break;
            case 7:
                SelectColor = Color.white;
                break;

            default:
                SelectColor = Color.white;
                break;
        }
        
    }
}
