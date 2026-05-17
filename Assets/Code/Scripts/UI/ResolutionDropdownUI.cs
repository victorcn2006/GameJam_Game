using UnityEngine;
using TMPro;
using System.Collections.Generic;

[RequireComponent(typeof(TMP_Dropdown))]
public class ResolutionDropdownUI : MonoBehaviour
{
    private TMP_Dropdown _dropdown;
    private Resolution[] _resolutions;

    private void Awake()
    {
        _dropdown = GetComponent<TMP_Dropdown>();
    }

    private void Start()
    {
        PopulateResolutions();
    }

    private void PopulateResolutions()
    {
        _resolutions = Screen.resolutions;
        _dropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < _resolutions.Length; i++)
        {
            string option = _resolutions[i].width + " x " + _resolutions[i].height;
            options.Add(option);

            if (_resolutions[i].width == Screen.currentResolution.width &&
                _resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        _dropdown.AddOptions(options);
        _dropdown.value = currentResolutionIndex;
        _dropdown.RefreshShownValue();

        _dropdown.onValueChanged.AddListener(SetResolution);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = _resolutions[resolutionIndex];
        if (SettingsManager.instance != null)
        {
            SettingsManager.instance.SetResolution(resolution.width, resolution.height);
        }
        else
        {
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }
    }

    private void OnDestroy()
    {
        if (_dropdown != null)
        {
            _dropdown.onValueChanged.RemoveListener(SetResolution);
        }
    }
}
