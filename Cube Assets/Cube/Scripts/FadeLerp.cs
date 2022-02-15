using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cube.Scripts
{
    public class FadeLerp : MonoBehaviour
    {
        public void FadeOutImage(Image image, float aVal)
        {
            StopCoroutine(FadeOut(image,aVal));
            StartCoroutine(FadeOut(image,aVal));
        }
    
        public void FadeOutText(TMP_Text tmpText)
        {
            StopCoroutine(FadeOut(tmpText));
            StartCoroutine(FadeOut(tmpText));
        }

    
        public void FadeInImage(Image image, float aVal)
        {
            StopCoroutine(FadeIn(image,aVal));
            StartCoroutine(FadeIn(image,aVal));
        }
    
        public void FadeInText(TMP_Text tmpText)
        {
            StopCoroutine(FadeIn(tmpText));
            StartCoroutine(FadeIn(tmpText));
        }
    
        public void FadeInMaterial(Material material, float aVal)
        {
            StopCoroutine(FadeIn(material,aVal));
            StartCoroutine(FadeIn(material,aVal));
        }
    
        public void FadeOutMaterial(Material material, float aVal)
        {
            StopCoroutine(FadeOut(material,aVal));
            StartCoroutine(FadeOut(material,aVal));
        }

        public void ChangeMaterial(Renderer objectRenderer, Material material, float changeMaterialTime)
        {
            StopCoroutine(MaterialChange(objectRenderer, material, changeMaterialTime));
            StartCoroutine(MaterialChange(objectRenderer, material, changeMaterialTime));
        }
    
        IEnumerator FadeOut(Image image, float aVal)
        {
            for (float i = aVal; i >= -0.05f; i -= 0.05f)
            {
                Color c = image.color;
                c.a = i;
                image.color = c;
                yield return new WaitForSeconds(0.05f);
            }
        }
    
        IEnumerator FadeOut(Material material, float aVal)
        {
            for (float i = aVal; i >= -0.05f; i -= 0.05f)
            {
                Color c = material.color;
                c.a = i;
                material.color = c;
                yield return new WaitForSeconds(0.05f);
            }
        }
    
        private IEnumerator MaterialChange(Renderer rendererObject, Material changeMatrial, float colorChangeTime)
        {
            float elapsedTime = 0;
            Material defaultMaterial = rendererObject.material;
        
            while (elapsedTime < colorChangeTime)
            {
                elapsedTime += Time.deltaTime;
                rendererObject.material.Lerp(defaultMaterial, changeMatrial, elapsedTime / colorChangeTime);
                yield return null;
            }
        }
    
        IEnumerator FadeOut(TMP_Text tmpText)
        {
            for (float i = 1f; i >= -0.05f; i -= 0.05f)
            {
                Color c = tmpText.color;
                c.a = i;
                tmpText.color = c;
                yield return new WaitForSeconds(0.05f);
            }
        }

        IEnumerator FadeIn(Image image, float aVal)
        {
            for (float i = 0.05f; i <= aVal; i += 0.05f)
            {
                Color c = image.color;
                c.a = i;
                image.color = c;
                yield return new WaitForSeconds(0.05f);
            }
        }
        
        IEnumerator FadeIn(Material material, float aVal)
        {
            for (float i = 0.05f; i <= aVal; i += 0.05f)
            {
                Color c = material.color;
                c.a = i;
                material.color = c;
                yield return new WaitForSeconds(0.05f);
            }
        }

        IEnumerator FadeIn(TMP_Text tmpText)
        {
            for (float i = 0.05f; i <= 1f; i += 0.05f)
            {
                Color c = tmpText.color;
                c.a = i;
                tmpText.color = c;
                yield return new WaitForSeconds(0.05f);
            }
        }
    }
}
