using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;

namespace BattleCore.UI
{
    public class DamageIndicator : MonoBehaviour
    {
        [field: SerializeField]
        private TMP_Text DamageLabel { get; set; }

        public void Initialize (float value)
        {
            DamageLabel.text = value.ToString();
            DamageLabel.transform.localScale = Vector3.zero;
            DamageLabel.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InFlash).OnComplete(() => StartCoroutine(WaitAndDisappear()));
        }

        private IEnumerator WaitAndDisappear ()
        {
            yield return new WaitForSeconds(1.0f);
            DamageLabel.DOFade(0, 1.0f).OnComplete(Dispose);
        }

        private void Dispose ()
        {
            Destroy(gameObject);
        }
    }
}
