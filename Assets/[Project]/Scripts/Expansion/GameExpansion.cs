/*
 扩展函数
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public enum RenderingMode
{
    Opaque,
    Cutout,
    Fade,
    Transparent,
}
public static class GameExpansion
{
    public static void ResetTransform(this GameObject g, Transform p)
    {
        g?.transform.SetParent(p);
        g.transform.localPosition = Vector3.zero;
        g.transform.localRotation = Quaternion.identity;
        g.transform.localScale = Vector3.one;

    }

    public static void ResetTransform(this GameObject g)
    {
        g.transform.localPosition = Vector3.zero;
        g.transform.localRotation = Quaternion.identity;
        g.transform.localScale = Vector3.one;
    }

    public static void SetX(this Transform tr, float x, bool world = true)
    {
        if (world)
            tr.position = new Vector3(x, tr.position.y, tr.position.z);
        else
            tr.localPosition = new Vector3(x, tr.localPosition.y, tr.localPosition.z);
    }
    public static void SetY(this Transform tr, float y, bool world = true)
    {
        if (world)
            tr.position = new Vector3(tr.position.x, y, tr.position.z);
        else
            tr.localPosition = new Vector3(tr.localPosition.x, y, tr.localPosition.z);
    }
    public static void SetZ(this Transform tr, float z, bool world = true)
    {
        if (world)
            tr.position = new Vector3(tr.position.x, tr.position.y, z);
        else
            tr.localPosition = new Vector3(tr.localPosition.x, tr.localPosition.y, z);
    }


    public static void SetMaterialRenderingMode(this Material material, RenderingMode renderingMode)
    {
        switch (renderingMode)
        {
            case RenderingMode.Opaque:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt("_ZWrite", 1);
                material.DisableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = -1;
                break;
            case RenderingMode.Cutout:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt("_ZWrite", 1);
                material.EnableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 2450;
                break;
            case RenderingMode.Fade:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt("_ZWrite", 0);
                material.DisableKeyword("_ALPHATEST_ON");
                material.EnableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 3000;
                break;
            case RenderingMode.Transparent:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt("_ZWrite", 0);
                material.DisableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 3000;
                break;
        }
    }


    public static string ParaeString<T>(this List<T> list,char separator=',')
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < list.Count; i++)
        {
            if (i==list.Count-1)
            {
                sb.Append(list[i]);
            }
            else
            {
                sb.Append(list[i]);
                sb.Append(separator);
            }
        }
        return sb.ToString();
    }

}
