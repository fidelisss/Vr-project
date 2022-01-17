using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Field
{
    //Name,
    Mass, 
    Volume, 
    Density, 
    Radius
}

public class ItemInfoUI : MonoBehaviour
{
    [SerializeField] private Text[] _texts;
    [SerializeField] private Field[] _fields;
    [SerializeField] private Item _item;
    [SerializeField] private Transform _canvasTransform;
    public Vector3 direction = Vector3.forward;

    private void Awake()
    {
        // RotateUI();
        // AlignCanvas();
        FillFields();
    }

    private void Update()
    {
        // transform.LookAt(transform.position + direction);
        RotateUI();
    }

    private void AlignCanvas()
    {
        float r = _item.radius;
        _canvasTransform.localPosition = new Vector3(0, r, r) * _item.transform.localScale.x + Vector3.up * 0.05f;
    }

    private void RotateUI()
    {
        // transform.localRotation = Quaternion.Inverse(_item.transform.localRotation);
        // transform.LookAt(Camera.main.transform);
    }

    private void FillFields()
    {
        for (int i = 0; i < _texts.Length; i++)
        {
            switch (_fields[i])
            {
                //case Field.Name:
                //    _texts[i].text = _item.itemName.ToString();
                //    break;
                case Field.Mass:
                    _texts[i].text = Convertor.Convert(_item.mass.ToString()) + " kg";
                    break;
                case Field.Volume:
                    _texts[i].text = Convertor.Convert(_item.volume.ToString()) + " m\xB3";
                    break;
                case Field.Density:
                    _texts[i].text = Convertor.Convert(_item.density.ToString()) + " kg/m\xB3";
                    break;
                case Field.Radius:
                    _texts[i].text = Convertor.Convert(_item.radius.ToString()) + " m";
                    break;
                //case Field.Angle:
                //    _texts[i].text = Convertor.Convert(_item.Angle.ToString()) + " C°";
                //    break;
            }
        }
    }
}
