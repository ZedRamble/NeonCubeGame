using UnityEngine;
using UnityEngine.UI;

namespace Cube.Scripts
{
    public class ErrorPanel : MonoBehaviour
    {
        [SerializeField] private FadeLerp fadeLerp;

        private Color _defaultColor;
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _defaultColor = _image.color;
        }

        private void Update()
        {
            if (_image.color.a <= 0)
            {
                _image.enabled = false;
                _image.color = _defaultColor;
            }
        }

        public void ErrorClick()
        {
            _image.enabled = true;
            fadeLerp.FadeOutImage(_image, _image.color.a);
        }

    }
}
