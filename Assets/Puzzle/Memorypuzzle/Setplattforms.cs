using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace setplattforms
{
    public class Setplattforms : MonoBehaviour, Interactioninterface
    {
        string Interactioninterface.Interactiontext => "Start";

        [SerializeField] private GameObject nextpillar;
        [SerializeField] private GameObject reward;
        [SerializeField] private GameObject startcollider;

        [SerializeField] private GameObject[] currentplattforms;
        public static int currentplattformnumber;
        public static int neededplattformnumber;

        [SerializeField] private GameObject[] shownplattforms;

        [SerializeField] private GameObject redpillar;
        [SerializeField] private GameObject greenpillar;
        [SerializeField] private GameObject bluepillar;
        [SerializeField] private GameObject yellowpillar;

        [SerializeField] private Color[] switchcolors;
        private Color redcolor;
        private Color greencolor;
        private Color bluecolor;
        private Color yellowcolor;

        [SerializeField] private GameObject[] portals;
        [SerializeField] private GameObject[] portalendposi;

        public bool Interact(Closestinteraction interactor)
        {
            StopAllCoroutines();
            currentplattformnumber = 0;
            resetpillars();
            startcollider.SetActive(true);
            foreach (GameObject form in shownplattforms)
            {
                form.gameObject.GetComponent<Renderer>().material.color = Color.black;
            }
            foreach (GameObject form in currentplattforms)
            {
                form.gameObject.GetComponent<Renderer>().material.color = Color.black;
            }
            StartCoroutine(showplattforms());
            return true;
        }
        private void Awake()
        {
            ColorUtility.TryParseHtmlString("#B72020", out redcolor);
            ColorUtility.TryParseHtmlString("#2C6536", out greencolor);
            ColorUtility.TryParseHtmlString("#2944D6", out bluecolor);
            ColorUtility.TryParseHtmlString("#FFBE0D", out yellowcolor);
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
            Memorystartcollider.startpuzzle += enteredstartcollider;
        }
        private void OnDisable()
        {
            Checkplattform.resetplattforms -= resetplattforms;
            Checkplattform.puzzlefinish -= finishpuzzle;
            Memorystartcollider.startpuzzle -= enteredstartcollider;
        }
        IEnumerator showplattforms()
        {
            setportals();
            for (int n = 0; n < currentplattforms.Length; n++)
            {
                currentplattforms[n].GetComponent<Checkplattform>().plattformnumber = n;
            }
            for (int f = 0; f < shownplattforms.Length; f++)
            {
                shownplattforms[f].GetComponent<Renderer>().material.color = Color.red;
                yield return new WaitForSeconds(0.5f);
            }
            foreach (GameObject form in shownplattforms)
            {
                form.gameObject.GetComponent<Renderer>().material.color = Color.black;
            }
            switchpillars();
            StopCoroutine(showplattforms());
        }
        private void setportals()
        {
            for (int p = 0; p < portals.Length; p++)
            {
                portals[p].GetComponent<Checkplattform>().isportal = true;
                portals[p].GetComponent<Checkplattform>().portalend = portalendposi[p].transform.position;
            }
        }
        private void enteredstartcollider()
        {
            StopAllCoroutines();
            switchpillars();
            foreach (GameObject form in shownplattforms)
            {
                form.gameObject.GetComponent<Renderer>().material.color = Color.black;
            }
        }
        private void resetplattforms()
        {
            resetpillars();
            foreach (GameObject form in currentplattforms)
            {
                form.GetComponent<Checkplattform>().isportal = false;
                form.GetComponent<Checkplattform>().plattformnumber = -2;
                form.gameObject.GetComponent<Renderer>().material.color = Color.black;
            }
        }
        private void finishpuzzle()
        {
            resetpillars();
            gameObject.GetComponent<Detectinteractionobject>().enabled = false;
            foreach (GameObject form in currentplattforms)
            {
                form.GetComponent<Renderer>().material.color = Color.green;
                form.GetComponent<Checkplattform>().isportal = false;
                form.GetComponent<Checkplattform>().plattformnumber = -2;
                currentplattformnumber = -1;
            }
            Invoke("activatenextpillar", 1f);
        }
        private void activatenextpillar()
        {
            foreach (GameObject form in currentplattforms)
            {
                form.GetComponent<Renderer>().material.color = Color.black;
            }
            if(reward != null)
            {
                reward.GetComponent<Rewardinterface>().addrewardcount();
            }
            else
            {
                nextpillar.SetActive(true);
            }
            gameObject.SetActive(false);
        }
        private void resetpillars()
        {
            redpillar.GetComponent<Renderer>().material.color = redcolor;
            greenpillar.GetComponent<Renderer>().material.color = greencolor;
            bluepillar.GetComponent<Renderer>().material.color = bluecolor;
            yellowpillar.GetComponent<Renderer>().material.color = yellowcolor;
        }
        private void switchpillars()
        {
            redpillar.GetComponent<Renderer>().material.color = switchcolors[0];
            greenpillar.GetComponent<Renderer>().material.color = switchcolors[1];
            bluepillar.GetComponent<Renderer>().material.color = switchcolors[2];
            yellowpillar.GetComponent<Renderer>().material.color = switchcolors[3];
        }
    }
}

