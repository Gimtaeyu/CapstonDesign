using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundOffset : MonoBehaviour
{
    public static BackgroundOffset instance;
    [SerializeField] private float _amountSpeed;
    private float _scrollSpeed;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private Material _material;
    [SerializeField] private Vector2 _newOffset;
    GameObject player;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        _scrollSpeed = _amountSpeed;
        _material = _spriteRenderer.material;
    }

    private void Update()
    {
        if(player.transform.position.x < -3.5 || player.transform.position.x > 3.5)
        {
            _scrollSpeed = 0.05f;
        }
        else
        {
            _scrollSpeed = 0.03f;
        }
    }

    public void Leftmove()
    {
        _newOffset = _material.mainTextureOffset;
        _newOffset.Set(_newOffset.x - (_scrollSpeed * Time.deltaTime), 0);
        _material.mainTextureOffset = _newOffset;
    }

    public void Rightmove()
    {
        _newOffset = _material.mainTextureOffset;
        _newOffset.Set(_newOffset.x + (_scrollSpeed * Time.deltaTime), 0);
        _material.mainTextureOffset = _newOffset;
    }
}
