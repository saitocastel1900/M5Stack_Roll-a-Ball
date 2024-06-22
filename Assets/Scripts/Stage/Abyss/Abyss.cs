using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class Abyss : MonoBehaviour
{
   private void Start()
   {
      this.gameObject
         .OnTriggerEnterAsObservable()
         .Subscribe(hit =>
         {
            var dieable = hit.gameObject.GetComponent<IDieable>();
            if (dieable != null)
            {
               dieable.Kill();
            }
         });
   }
}
