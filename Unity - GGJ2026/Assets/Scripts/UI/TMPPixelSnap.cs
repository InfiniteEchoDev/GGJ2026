using UnityEngine;

namespace com.ggj2026teamname.gamename
{
    using TMPro;
    using UnityEngine;

    [RequireComponent(typeof(TMP_Text))]
    public class TMPPixelSnap : MonoBehaviour
    {
        private TMP_Text tmp;
        private RectTransform rect;

        void Awake()
        {
            tmp = GetComponent<TMP_Text>();
            rect = tmp.rectTransform;
        }

        void LateUpdate()
        {
            if (!tmp) return;

            var uiCamera = tmp.canvas?.worldCamera;
            if (!uiCamera) return;
            
            tmp.ForceMeshUpdate(false, false);
            var textInfo = tmp.textInfo;

            for (int m = 0; m < textInfo.meshInfo.Length; m++)
            {
                var verts = textInfo.meshInfo[m].vertices;

                for (int i = 0; i < verts.Length; i++)
                {
                    // convert local vertex to world
                    Vector3 world = rect.TransformPoint(verts[i]);

                    // project to screen space
                    Vector3 screen = uiCamera.WorldToScreenPoint(world);

                    // snap to integer pixels
                    screen.x = Mathf.Round(screen.x);
                    screen.y = Mathf.Round(screen.y);

                    // back to world, preserving original z
                    Vector3 snappedWorld = uiCamera.ScreenToWorldPoint(screen);

                    // back to local
                    verts[i] = rect.InverseTransformPoint(snappedWorld);
                }

                tmp.UpdateGeometry(textInfo.meshInfo[m].mesh, m);
            }
        }
    }
}
