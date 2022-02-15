using UnityEngine;

namespace Cube.Scripts
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class CubeClickEvent : MonoBehaviour
    {
        [SerializeField] private int index;
        [SerializeField] private CubeController cubeController;
    
        private void OnMouseDown()
        {
            cubeController.Clicked(index);
        }
    }
}
