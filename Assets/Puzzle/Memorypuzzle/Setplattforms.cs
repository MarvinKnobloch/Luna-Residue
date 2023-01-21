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
        public static int neededplattformnumber;

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
            neededplattformnumber = 0;
            for (int i = 0; i < currentplattforms.Length; i++)
            {
                neededplattformnumber++;
            }
        }
        private void OnEnable()
        {
            Checkplattform.resetplattforms += resetplattforms;
            Checkplattform.puzzlefinish += finishpuzzle;
            Memorystartcollider.startpuzzle += startcurrentpuzzle;
        }
        private void OnDisable()
        {
            Checkplattform.resetplattforms -= resetplattforms;
            Checkplattform.puzzlefinish -= finishpuzzle;
            Memorystartcollider.startpuzzle -= startcurrentpuzzle;
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
                //currentplattforms[f].GetComponent<Renderer>().material.color = Color.black;
            }
            foreach (GameObject form in currentplattforms)
            {
                form.gameObject.GetComponent<Renderer>().material.color = Color.black;
            }
            StopCoroutine(showplattforms());
        }
        private void resetplattforms()
        {
            foreach (GameObject form in currentplattforms)
            {
                form.GetComponent<Checkplattform>().plattformnumber = -2;
                form.gameObject.GetComponent<Renderer>().material.color = Color.black;
            }
        }
        private void startcurrentpuzzle()
        {
            StopAllCoroutines();
            foreach (GameObject form in currentplattforms)
            {
                form.gameObject.GetComponent<Renderer>().material.color = Color.black;
            }
        }
        private void finishpuzzle()
        {
            foreach (GameObject form in currentplattforms)
            {
                form.GetComponent<Renderer>().material.color = Color.green;
                form.GetComponent<Checkplattform>().plattformnumber = -2;
                currentplattformnumber = -1;
            }
        }
    }
}

