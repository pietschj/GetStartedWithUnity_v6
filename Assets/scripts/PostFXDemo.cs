using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

// Post-processing demo: press 1 to 8 to switch individual effects on and off,
// 9 for grayscale, and 0 to switch all post-processing on and off for a before/after comparison.
// Put this on the same GameObject as a Global Volume.
public class PostFXDemo : MonoBehaviour
{
    [Tooltip("The Volume whose effects are toggled. Left empty, it uses the Volume on this GameObject.")]
    [SerializeField] Volume volume;

    [Tooltip("A second Global Volume with a higher Priority, holding only Color Adjustments with Saturation at -100.")]
    [SerializeField] Volume grayscaleVolume;

    [Tooltip("Show the on-screen list of effects and keys.")]
    [SerializeField] bool showHelp = true;

    VolumeComponent[] effects;
    string[] effectNames;
    bool grayscale;

    readonly Key[] keys =
    {
        Key.Digit1, Key.Digit2, Key.Digit3, Key.Digit4,
        Key.Digit5, Key.Digit6, Key.Digit7, Key.Digit8
    };

    void Start()
    {
        if (volume == null) volume = GetComponent<Volume>();
        if (volume == null)
        {
            Debug.LogWarning("PostFXDemo needs a Volume.");
            enabled = false;
            return;
        }

        // volume.profile (not sharedProfile) gives a copy for this play session,
        // so toggling effects in Play mode does not change the profile asset
        VolumeProfile profile = volume.profile;

        profile.TryGet(out Bloom bloom);
        profile.TryGet(out Tonemapping tonemapping);
        profile.TryGet(out ColorAdjustments colorAdjustments);
        profile.TryGet(out Vignette vignette);
        profile.TryGet(out ChromaticAberration chromaticAberration);
        profile.TryGet(out FilmGrain filmGrain);
        profile.TryGet(out LensDistortion lensDistortion);
        profile.TryGet(out DepthOfField depthOfField);

        effects = new VolumeComponent[]
        {
            bloom, tonemapping, colorAdjustments, vignette,
            chromaticAberration, filmGrain, lensDistortion, depthOfField
        };

        effectNames = new string[]
        {
            "Bloom", "Tonemapping", "Color Adjustments", "Vignette",
            "Chromatic Aberration", "Film Grain", "Lens Distortion", "Depth of Field"
        };

        // grayscale starts off
        if (grayscaleVolume != null) grayscaleVolume.enabled = false;
    }

    void Update()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard == null) return;

        for (int i = 0; i < keys.Length; i++)
        {
            if (keyboard[keys[i]].wasPressedThisFrame && effects[i] != null)
            {
                // active is the tick box next to the effect's name in the Volume Inspector
                effects[i].active = !effects[i].active;
            }
        }

        if (keyboard.digit9Key.wasPressedThisFrame)
        {
            grayscale = !grayscale;
        }

        if (keyboard.digit0Key.wasPressedThisFrame)
        {
            // switching the whole Volume off shows the scene with no post-processing
            volume.enabled = !volume.enabled;
        }

        // the grayscale Volume only shows while post-processing as a whole is on
        if (grayscaleVolume != null)
        {
            grayscaleVolume.enabled = grayscale && volume.enabled;
        }

        if (keyboard.hKey.wasPressedThisFrame)
        {
            showHelp = !showHelp;
        }
    }

    // OnGUI draws a quick debug panel without needing a Canvas.
    // It is drawn after post-processing, so the text itself is not affected by the effects.
    void OnGUI()
    {
        if (!showHelp || effects == null) return;

        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.fontSize = 18;
        style.richText = true;

        float width = 330f;
        float height = 50f + (keys.Length + 3) * 30f;
        Rect area = new Rect(Screen.width - width - 20f, 20f, width, height);

        GUI.Box(area, "");
        GUILayout.BeginArea(new Rect(area.x + 12f, area.y + 8f, width - 24f, height - 16f));

        GUILayout.Label("<b>Post-processing demo</b>", style);
        for (int i = 0; i < effects.Length; i++)
        {
            bool on = effects[i] != null && effects[i].active;
            GUILayout.Label((i + 1) + "  " + effectNames[i] + ":  " + OnOff(on), style);
        }
        if (grayscaleVolume != null)
        {
            GUILayout.Label("9  Grayscale:  " + OnOff(grayscale), style);
        }
        GUILayout.Label("0  All post-processing:  " + OnOff(volume.enabled), style);
        GUILayout.Label("H  hide this panel", style);

        GUILayout.EndArea();
    }

    string OnOff(bool on)
    {
        return on ? "<color=#7CFC7C>ON</color>" : "<color=#FF8080>off</color>";
    }
}
