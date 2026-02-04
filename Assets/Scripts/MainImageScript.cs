using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainImageScript : MonoBehaviour
{
    [SerializeField] private GameObject image_unknown;
    [SerializeField] private MemoryController memory_controller;

    public void OnMouseDown()
    {
        if (image_unknown.activeSelf && memory_controller.canOpen)
        {
            image_unknown.SetActive(false);
            memory_controller.imageOpened(this);
        }
    }

    private int _spriteId;
    public int spriteId
    {
        get { return _spriteId; }
    }

    public void ChangeSprite(int id, Sprite image)
    {
        _spriteId = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }

    public void Close()
    {
        image_unknown.SetActive(true); //esconder imagem
    }
}
