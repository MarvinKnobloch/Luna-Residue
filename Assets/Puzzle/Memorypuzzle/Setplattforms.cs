using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace setplattforms
{
    public class Setplattforms : MonoBehaviour, Interactioninterface
    {
        string Interactioninterface.Interactiontext => "Start";

        [SerializeField] private GameObject[] currentplattforms;
        public static int currentplattformnumber;

        public bool Interact(Closestinteraction interactor)
        {
            StopAllCoroutines();
            currentplattformnumber = 0;
            foreach (GameObject form in currentplattforms)
            {
                form.gameObject.GetComponent<Renderer>().material.color = Color.black;
            }
            StartCoroutine(showplattforms());
            return true;
        }

        private void Awake()
        {

        }
        IEnumerator showplattforms()
        {
            for (int n = 0; n < currentplattforms.Length; n++)
            {
                currentplattforms[n].GetComponent<Checkplattform>().plattformnumber = n;
            }
            for (int f = 0; f < currentplattforms.Length; f++)
            {
                currentplattforms[f].GetComponent<Renderer>().material.color = Color.red;
                yield return new WaitForSeconds(0.5f);
                currentplattforms[f].GetComponent<Renderer>().material.color = Color.black;
            }
            StopCoroutine(showplattforms());
        }
    }
}

