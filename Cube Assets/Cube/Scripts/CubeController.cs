using System;
using System.Collections;
using UnityEngine;

namespace Cube.Scripts
{
    [Serializable]
    struct CubeData
    {
        public SpriteRenderer spriteRenderer;
        public ParticleSystem particleSystem;
        public bool particalStat;
    }

    public class CubeController : MonoBehaviour
    {
        [Header("Square Settings")] [Space]
        public Color purpleColor = new Color(0.765f, 0.694f, 0.883f);
        public Color greenColor = new Color(0.855f, 0.969f, 0.651f);
        [Range(1, 10)] [SerializeField] private float colorChangeTime = 5;
        [SerializeField] private Sprite filledCubeSprite;
        [SerializeField] private Sprite outlineCubeSprite;
        [SerializeField] private CubeData[] cubeDataArray;
    
        [NonSerialized] public bool taps;
        [NonSerialized] public int scores;
        [NonSerialized] public bool interactable;

        private void Update()
        {
            if (interactable)
                CubeParticles();
        }
        
        public void Interactable(bool stat)
        {
            interactable = stat;
            if (interactable)
                CubeCoroutins(true);
        }

        private void CubeCoroutins(bool stat)
        {
            for (int i = 0; i < cubeDataArray.Length; i++)
            {
                if (stat)
                    StartCoroutine(ColorChange(cubeDataArray[i].spriteRenderer));
                else
                    StopCoroutine(ColorChange(cubeDataArray[i].spriteRenderer));
            }
        }
    
        private void CubeParticles()
        {
            for (int i = 0; i < cubeDataArray.Length; i++)
            {
                if (cubeDataArray[i].spriteRenderer.color == greenColor && !cubeDataArray[i].particleSystem.isPlaying && !cubeDataArray[i].particalStat)
                {
                    StopCoroutine(ColorChange(cubeDataArray[i].spriteRenderer));
                    cubeDataArray[i].spriteRenderer.sprite = outlineCubeSprite;
                    cubeDataArray[i].particleSystem.Play();
                    cubeDataArray[i].particalStat = true;
                }
                else if (cubeDataArray[i].particalStat && !cubeDataArray[i].particleSystem.isPlaying)
                {
                    cubeDataArray[i].spriteRenderer.color = purpleColor;
                    cubeDataArray[i].spriteRenderer.sprite = filledCubeSprite;
                    cubeDataArray[i].particalStat = false;
                    StartCoroutine(ColorChange(cubeDataArray[i].spriteRenderer));
                }
            }
        }

        public void Clicked(int ind)
        {
            if (cubeDataArray[ind].particalStat)
            {
                taps = true;
                scores += 100;
                cubeDataArray[ind].spriteRenderer.color = purpleColor;
                cubeDataArray[ind].spriteRenderer.sprite = filledCubeSprite;
                cubeDataArray[ind].particleSystem.Stop();
                cubeDataArray[ind].particalStat = false;
                StopCoroutine(ColorChange(cubeDataArray[ind].spriteRenderer));
                StartCoroutine(ColorChange(cubeDataArray[ind].spriteRenderer));
            }
        }

        public void ResetCubes()
        {
            interactable = false;
            StopAllCoroutines();
            for (int i = 0; i < cubeDataArray.Length; i++)
            {
                cubeDataArray[i].particleSystem.Stop();
                cubeDataArray[i].particalStat = false;
                cubeDataArray[i].spriteRenderer.color = purpleColor;
                cubeDataArray[i].spriteRenderer.sprite = filledCubeSprite;
            }
        }
    
        private IEnumerator ColorChange(SpriteRenderer spriteRenderer)
        {
            float elapsedTime = 0;
            while (elapsedTime < colorChangeTime)
            {
                elapsedTime += Time.deltaTime;
                spriteRenderer.color = Color.Lerp(purpleColor, greenColor, elapsedTime / colorChangeTime);
                yield return null;
            }
        }
    }
}