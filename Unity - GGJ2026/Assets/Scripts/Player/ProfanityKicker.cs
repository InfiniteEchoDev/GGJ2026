using TMPro;
using UnityEngine;

namespace com.ggj2026teamname.gamename
{
    public class ProfanityKicker : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private TextMeshProUGUI _profanityKickerText;

        public void KickProfanity(string text = null)
        {
            if (text != null)
            {
                _profanityKickerText.text = text;
            }
            _animator.SetTrigger("Kick");
        }
    }
}
