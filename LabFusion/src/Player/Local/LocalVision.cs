using LabFusion.Data;
using LabFusion.Extensions;
using LabFusion.Utilities;

using UnityEngine;
using UnityEngine.UI;

namespace LabFusion.Player;

public static class LocalVision
{
    private static bool _blind = false;
    public static bool Blind
    {
        get
        {
            return _blind;
        }
        set
        {
            _blind = value;

            ApplyVisionSettings();
        }
    }

    private static Color _blindColor = Color.clear;
    public static Color BlindColor
    {
        get
        {
            return _blindColor;
        }
        set
        {
            _blindColor = value;

            ApplyVisionSettings();
        }
    }

    private static GameObject _blindnessGameObject = null;
    private static RawImage _blindnessImage = null;

    internal static void OnInitializeMelon()
    {
        MultiplayerHooking.OnMainSceneInitialized += OnMainSceneInitialized;
    }

    private static void OnMainSceneInitialized()
    {
    //
    }

    private static void ApplyVisionSettings()
    {
        if (_blindnessImage == null)
        {
            return;
        }

        _blindnessGameObject.SetActive(Blind);
        _blindnessImage.color = BlindColor;

    }
}
