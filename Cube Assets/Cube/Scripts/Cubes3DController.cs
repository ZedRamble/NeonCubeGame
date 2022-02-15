using System;
using Unity.Mathematics;
using UnityEngine;

namespace Cube.Scripts
{
    public class Cubes3DController : MonoBehaviour
    {
        [Header("Cube Data")] [Space]
        [SerializeField] private ErrorPanel errorPanel;
        [SerializeField] private GameObject cubePrefab;
        [SerializeField] private Material[] cubeMaterials;
        [Range(1, 50)] [SerializeField] private float cubesCount = 5;
        [Range(0.1f, 3)] [SerializeField] private float cubeSpawnDelay = 0.5f;
        
        [Space] [Header("Cube Sin Movement Settings")] [Space]
        [SerializeField] private Vector3 startPos;
        [SerializeField] private float maxX;
        [SerializeField] private float speedX = 2f;
        [SerializeField] private float aAxisShiftY;
        [SerializeField] private float bStrechAxisY;
        [SerializeField] private float cStrechAxisX;
        [SerializeField] private float dAxisShiftX;

        public FadeLerp fadeLerp;
        [NonSerialized] public bool interactable3dCubes;
        [NonSerialized] public int scores3Dcubes;
        [NonSerialized] public bool taps3DCubes;
        
        private void Awake()
        {
            CubesPool();
        }

        public void Interactable()
        {
            interactable3dCubes = true;
        }

        public void Cube3DClicked(int score)
        {
            if (interactable3dCubes)
            {
                if (score == -300)
                    errorPanel.ErrorClick();
                scores3Dcubes = scores3Dcubes + score < 0 ? 0 : scores3Dcubes + score;
                taps3DCubes = true;
            }
        }
        
        private void CubesPool()
        {
            for (int i = 0; i < cubesCount; i++)
            {
                GameObject gt = Instantiate(cubePrefab, startPos, quaternion.identity, transform);
                CubeSinMove cubeSinMove = gt.GetComponent<CubeSinMove>();
                cubeSinMove.maxX = maxX;
                cubeSinMove.speedX = speedX;
                cubeSinMove.startPos = startPos;
                cubeSinMove.aAxisShiftY = aAxisShiftY;
                cubeSinMove.bStrechAxisY = bStrechAxisY;
                cubeSinMove.cStrechAxisX = cStrechAxisX;
                cubeSinMove.dAxisShiftX = dAxisShiftX;
                cubeSinMove.materials = cubeMaterials;
                cubeSinMove.fadeLerp = fadeLerp;
                cubeSinMove.spawnDelay = cubeSpawnDelay;
                cubeSinMove.cubeIndex = i;
                cubeSinMove.cubes3DController = this;
            }
        }
    }
}
