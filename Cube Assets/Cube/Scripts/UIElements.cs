using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cube.Scripts
{
    public class UIElements : MonoBehaviour
    {
        [Header("UI Elements Data")] [Space]
        [SerializeField] private GameObject touchPanel;
        [SerializeField] private float panelShowTime = 15f;
        [SerializeField] private CubeController cubeController;
        [SerializeField] private Cubes3DController cube3DController;
        [SerializeField] private TMP_Text scoreText;

        private int _scores;
        private FadeLerp _fadeLerp;
        private Image _touchPanelImage;
        private Button _touchPanelButton;
        private TMP_Text _touchPanelText;
        private float _alphaVal;
        private float _timeCounter;
        private bool _panelActive = true;
    
        private void Awake()
        {
            _fadeLerp = GetComponent<FadeLerp>();
            _touchPanelButton = touchPanel.GetComponent<Button>();
            _touchPanelImage = touchPanel.GetComponent<Image>();
            _touchPanelText = touchPanel.GetComponentInChildren<TMP_Text>();
            _alphaVal = _touchPanelImage.color.a;
        }

        private void Update()
        {
            if (!_panelActive)
            {
                if (scoreText.color.a <= 0)
                    _fadeLerp.FadeInText(scoreText);
                else
                {
                    _scores = cubeController.scores + cube3DController.scores3Dcubes;
                    scoreText.text = $"Очки: {_scores}";
                }
                if (cubeController.taps || cube3DController.taps3DCubes)
                {
                    _timeCounter = 0;
                    cubeController.taps = false;
                    cube3DController.taps3DCubes = false;
                }
                _timeCounter += Time.deltaTime;
                if (_timeCounter >= panelShowTime)
                {
                    _fadeLerp.FadeOutText(scoreText);
                    _fadeLerp.FadeInImage(_touchPanelImage, _alphaVal);
                    _fadeLerp.FadeInText(_touchPanelText);
                    _touchPanelButton.interactable = true;
                    _panelActive = true;
                    _timeCounter = 0;
                    _scores = 0;
                    cubeController.scores = 0;
                    cube3DController.scores3Dcubes = 0;
                    scoreText.text = $"Очки: {_scores}";
                    cubeController.ResetCubes();
                    cube3DController.interactable3dCubes = false;
                }
            }
        }
        
        public void PanelInNotActive()
        {
            StopAllCoroutines();
            _panelActive = false;
            _touchPanelButton.interactable = false;
            _fadeLerp.FadeOutImage(_touchPanelImage, _alphaVal);
            _fadeLerp.FadeOutText(_touchPanelText);
        }
    }
}
