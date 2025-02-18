using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Settings : MonoBehaviour
{
    private Button _btnClose;
    private Slider _sliderEnergy;
    private Slider _sliderFull;
    private Slider _sliderMood;
    private Toggle _toggleMasterBeside;
    
    private void Awake()
    {
        _btnClose = transform.Find("Btn_Close").GetComponent<Button>();
        _sliderEnergy = transform.Find("Slider_Energy").GetComponent<Slider>();
        _sliderFull = transform.Find("Slider_Full").GetComponent<Slider>();
        _sliderMood = transform.Find("Slider_Mood").GetComponent<Slider>();
        _toggleMasterBeside = transform.Find("Toggle_MasterBeside").GetComponent<Toggle>();

        _btnClose.onClick.AddListener(() => {
            this.gameObject.SetActive(false);
        });

        _sliderEnergy.value = GameManager.Instance.Energy;
        _sliderFull.value = GameManager.Instance.Full;
        _sliderMood.value = GameManager.Instance.Mood;
        _toggleMasterBeside.isOn = GameManager.Instance.MasterBeside;

        _sliderEnergy.onValueChanged.AddListener((value) => {
            GameManager.Instance.Energy = (int)value;
        });

        _sliderFull.onValueChanged.AddListener((value) => {
            GameManager.Instance.Full = (int)value;
        });

        _sliderMood.onValueChanged.AddListener((value) => {
            GameManager.Instance.Mood = (int)value;
        });

        _toggleMasterBeside.onValueChanged.AddListener((value) => {
            GameManager.Instance.MasterBeside = value;
        });
    }
}
