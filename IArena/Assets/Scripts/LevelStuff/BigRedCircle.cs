using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace LevelElements
{
    public class BigRedCircle : MonoBehaviour
    {

        public Sprite sprite;
        [Range(0, 1)] public float sizeReductionFactor = 0.25f;
        public float shrinkSpeed;
        public int reductions;
        public int timeLimit;

        // Start is called before the first frame update
        void Start()
        {
            if(!sprite)
            {
                sprite = GetComponent<SpriteRenderer>().sprite;
            }
            if(reductions <= 0)
            {
                reductions = 4;
            }
            if(timeLimit <= 0)
            {
                timeLimit = 15;
            }
            StartMethod(reductions);
        }

        private async void StartMethod(int reductions)
        {
            while(reductions > 0)
            {
                await Shrink();
                reductions--;
            }
        }

        private GameObject Reference(Vector3 currentScale)
        {
            GameObject reference = Instantiate(new GameObject(), transform.position, Quaternion.identity);
            SpriteRenderer renderer = reference.AddComponent<SpriteRenderer>();
            renderer.sprite = sprite;
            if(reductions == 1)
            {
                reference.transform.localScale = new Vector3(currentScale.x - sizeReductionFactor * 0.75f, currentScale.y - sizeReductionFactor * 0.75f, currentScale.z);
            }
            else
            {
                reference.transform.localScale = new Vector3(currentScale.x - sizeReductionFactor, currentScale.y - sizeReductionFactor, currentScale.z);
            }
            return reference;
        }

        private async Task Shrink()
        {
            await Task.Delay(timeLimit);
            GameObject reference = Reference(currentScale: transform.localScale);
            float timer = 0f;

            while(transform.localScale.x > reference.transform.localScale.x)
            {
                timer += Time.deltaTime;
                transform.localScale -= new Vector3(1,1,1) * Time.deltaTime * sizeReductionFactor * shrinkSpeed;
                await Task.Yield();
            }
            Destroy(reference);
        }
    }
}
