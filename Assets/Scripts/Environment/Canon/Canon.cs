using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Canon : MonoBehaviour
{
    [SerializeField] private List<GameObject> levels;
    [SerializeField] private AnimationCurve canonObjMoveAnimationCurve;
    [SerializeField] private float yMovementOffset;
    [SerializeField] private GameObject levelUpParticles;
    
    public int LevelIndex { get; private set; }
    
    // Start is called before the first frame update
    void Start()
    {
        SetLevel();
        canonObjMoveAnimationCurve.keys[1].value = yMovementOffset;
        ServiceSystem.GetService<SoundService>().PlaySound("Background");
    }

    private void SetLevel()
    {
        levels.ForEach(o=>o.SetActive(false));
        levels[LevelIndex].SetActive(true);
    }

    public void UpgradeCanon(GameObject canonUpgradeObject)
    {
        StartCoroutine(Routine());
        IEnumerator Routine()
        {
            canonUpgradeObject.transform.SetParent(null);
             canonUpgradeObject.GetComponent<Rotator>().enabled = false;
           canonUpgradeObject.transform.rotation =Quaternion.identity;
            float animTime = .65f;
            float timer =0;
            Vector3 startPos = canonUpgradeObject.transform.position;
            Vector3 endPos = transform.position;
            bool isParticlesSpawn =false;
            while (timer<animTime)
            {
                float t = timer / animTime;
                canonUpgradeObject.transform.position = Vector3.Lerp(startPos,
                    endPos + Vector3.up * canonObjMoveAnimationCurve.Evaluate(t),t );
                canonUpgradeObject.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 0.75f, t);
                timer += Time.deltaTime;
                if (t > 0.8f && !isParticlesSpawn)
                {
                    levelUpParticles.SetActive(true);
                    isParticlesSpawn = true;
                }
                yield return null;
            }
            // canonUpgradeObject.transform.DOJump(transform.position, 5,1,0.75f)..OnComplete((() =>
            // {
            //    
            // }));
            Destroy(canonUpgradeObject);
            LevelIndex++;
            SetLevel();
            ServiceSystem.GetService<UIService>().GamePlayCanvas.ShowLevelUpText(transform.position);
            ServiceSystem.GetService<SoundService>().PlaySound("LevelUp");
            yield return null;
        }
    }
}
