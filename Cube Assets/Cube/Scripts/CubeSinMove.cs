using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Cube.Scripts
{
    public class CubeSinMove : MonoBehaviour
    {
        [NonSerialized] public Material[] materials;
        [NonSerialized] public Vector3 startPos;
        [NonSerialized] public float maxX;
        [NonSerialized] public float speedX = 2f;
        [NonSerialized] public float aAxisShiftY;
        [NonSerialized] public float bStrechAxisY;
        [NonSerialized] public float cStrechAxisX;
        [NonSerialized] public float dAxisShiftX;
        [NonSerialized] public Cubes3DController cubes3DController;
        [NonSerialized] public FadeLerp fadeLerp;
        [NonSerialized] public int cubeIndex;
        [NonSerialized] public float spawnDelay;
    
        private Renderer _renderer;
        private BoxCollider _boxCollider;
        private System.Random _random;
        private int _currentMaterial;
        private float _timeCount;
        private float _x;
        private float _y;
        private float _z;
    
        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _boxCollider = GetComponent<BoxCollider>();
            _random = new System.Random();
        }

        private void Start()
        {
            _x = startPos.x;
            _y = startPos.y;
            _z = Random.Range(-1, 1);
            _currentMaterial = _random.Next(0, materials.Length);
            _renderer.material = materials[_currentMaterial];
            _boxCollider.enabled = true;
        }

        private void Update()
        {
            if (cubes3DController.interactable3dCubes)
            {
                _timeCount += Time.deltaTime;
                if (_timeCount >= spawnDelay * cubeIndex)
                {
                    if (_x < maxX)
                    {
                        transform.position = new Vector3(_x, _y, _z);
                        _x += Time.deltaTime * speedX;
                        _y = aAxisShiftY + bStrechAxisY * Mathf.Sin(cStrechAxisX * _x + dAxisShiftX);
                    }
                    else if (_x >= maxX)
                    {
                        _x = startPos.x;
                        _y = startPos.y;
                        _z = Random.Range(-1, 1);
                        _currentMaterial = _random.Next(0, materials.Length);
                        _renderer.material = materials[_currentMaterial];
                        _boxCollider.enabled = true;
                        transform.position = new Vector3(_x, _y, _z);
                    }
                }

                if (!_boxCollider.enabled)
                {
                    if (_renderer.material.color.a <= 0)
                    {
                        _x = startPos.x;
                        _y = startPos.y;
                        _z = Random.Range(-1, 1);
                        _currentMaterial = _random.Next(0, materials.Length);
                        _renderer.material = materials[_currentMaterial];
                        _boxCollider.enabled = true;
                        transform.position = new Vector3(_x, _y, _z);
                    }
                }
            }
        }
    
        private void OnMouseDown()
        {
            if (cubes3DController.interactable3dCubes && _boxCollider.enabled)
            {
                _timeCount = 0;
                switch (_currentMaterial)
                {
                    case 0:
                        cubes3DController.Cube3DClicked(100);
                        break;
                    case 1:
                        cubes3DController.Cube3DClicked(200);
                        break;
                    case 2:
                        cubes3DController.Cube3DClicked(-300);
                        break;
                }
                _boxCollider.enabled = false;
                fadeLerp.FadeOutMaterial(_renderer.material, 1f);
            }
        }
    }
}
