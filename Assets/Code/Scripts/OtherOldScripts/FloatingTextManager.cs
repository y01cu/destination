//
// Copyright (c) Umut Kaan Özdemir. All rights reserved.
//

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Scripts {
    public class FloatingTextManager : MonoBehaviour {
        public GameObject textContainer;
        public GameObject textPrefab;

        private List<FloatingText> floatingTexts = new List<FloatingText>();

        //private void Start() {
        //    DontDestroyOnLoad(gameObject);
        //}
        private void Update() {
            foreach (FloatingText text in floatingTexts) {
                text.UpdateFloatingText();
            }
        }

        public void Show(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration) {
            // I want to perform animation after show method.

            FloatingText floatingText = GetFloatingText();
            Debug.Log(msg);

            floatingText.txt.text = msg;
            floatingText.txt.fontSize = fontSize;
            floatingText.txt.color = color;
            // Transfer world space to screen space so we can use it in the UI:
            floatingText.go.transform.position = Camera.main.WorldToScreenPoint(position);
            floatingText.motion = motion;
            floatingText.duration = duration;

            floatingText.Show();

            // In animation I've set text component inactive. I want to reactivate it.

            //Animator animator = textContainer.GetComponent<Animator>();
            //animator.Play("FloatingTextShowAndHide");
        }

        private FloatingText GetFloatingText() {
            FloatingText txt = floatingTexts.Find(t => !t.active);

            if (txt == null) {
                txt = new FloatingText();
                txt.go = Instantiate(textPrefab);
                txt.go.transform.SetParent(textContainer.transform);
                txt.txt = txt.go.GetComponent<Text>();
                // Other properties is assigned in FloatingText class.

                floatingTexts.Add(txt);
            }

            return txt;
        }
    }
}