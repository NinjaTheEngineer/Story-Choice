using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;
using TMPro;

public class StringWritter : NinjaMonoBehaviour {
    private string currentSentence;
    private bool isTypingSentence;
    public TextMeshProUGUI textHolder;
    public float typingSpeed = 0.01f;
    public void WriteSentence(string sentence) {
        var logId = "WriteSentence";
        if(isTypingSentence || string.IsNullOrEmpty(sentence)) {
            logd(logId, "IsTypingSentece="+isTypingSentence+" Sentence="+sentence.logf()+" => Clearing text");
            ClearText();
        }
        currentSentence = sentence;
        logd(logId,"Starting TypingRoutine for sentence="+sentence.logf());
        StartCoroutine(TypingRoutine(sentence));
    }
    public void ClearText() {
        StopAllCoroutines();
        currentSentence = "";
        textHolder.text = "";
        isTypingSentence = false;
    }
    IEnumerator TypingRoutine(string sentence) {
        string logId = "TypingRoutine";
        logd(logId, "Start typing routine for sentence="+sentence.logf());
        isTypingSentence = true;
        textHolder.text = "";
        var waitForSeconds = new WaitForSeconds(typingSpeed);
        var sentenceChars = sentence.ToCharArray();
        var charsCount = sentenceChars.Length;
        for (int i = 0; i < charsCount; i++) {
            var currentChar = sentenceChars[i];
            var isSlash = currentChar=='\\';
            var shouldFormat = i < charsCount-1 && sentenceChars[i+1]=='n';
            if(isSlash && shouldFormat) {
                textHolder.text += "\n";
                i++;
            } else {
                textHolder.text += currentChar;
            }
            yield return waitForSeconds;
        }
        isTypingSentence = false;
    }
    public void SkipTyping() {
        var logId = "SkipTyping";
        if(currentSentence!=null && isTypingSentence) {
            logd(logId, "Stopping all Routines and writing the whole text.");
            StopAllCoroutines();
            textHolder.text = currentSentence;
        } else {
            logd(logId, "Can't skip typing while CurrentSentece="+currentSentence.logf()+" IsTypingSentence="+isTypingSentence);
        }
    }
}
