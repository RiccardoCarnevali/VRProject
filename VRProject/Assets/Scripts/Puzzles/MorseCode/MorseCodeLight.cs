using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MorseCodeLight : MonoBehaviour
{

    //The basic unit of time measurement, represented by a '.', whose duration is arbitrary
    [SerializeField] private float ditDurationSeconds;

    //Represented by a '-', dahs last 3 times the duration of a dit
    private float dahDurationSeconds;

    //The wait between dits and dahs, whose duration is equal to that of a dit
    private float waitBetweenDitsAndDahsSeconds;

    //The wait between characters of the same word, whose duration is equal to 3 times the duration of a dit
    private float waitBetweenCharactersSeconds;

    //The wait between words of the same sentence, whose duration is equal to 7 times the duration of a dit
    private float waitBetweenWordsSeconds;

    //The wait before the entire sentence repeats again. Its duration is not standardized but is generally between 10 and 14 times the duration of a dit. Here we use 10 times
    private float repeatWaitTimeSeconds;

    [SerializeField] private string message;

    //The dictionary mapping characters to a string representing their morse code representation
    private Dictionary<char, string> morseCodeEncoding = new Dictionary<char, string>
    {
        {'A', ".-"},
        {'B', "-..."},
        {'C', "-.-."},
        {'D', "-.."},
        {'E', "."},
        {'F', "..-."},
        {'G', "--."},
        {'H', "...."},
        {'I', ".."},
        {'J', ".---"},
        {'K', "-.-"},
        {'L', ".-.."},
        {'M', "--"},
        {'N', "-."},
        {'O', "---"},
        {'P', ".--."},
        {'Q', "--.-"},
        {'R', ".-."},
        {'S', "..."},
        {'T', "-"},
        {'U', "..-"},
        {'V', "...-"},
        {'W', ".--"},
        {'X', "-..-"},
        {'Y', "-.--"},
        {'Z', "--.."},
        {'0', "-----"},
        {'1', ".----"},
        {'2', "..---"},
        {'3', "...--"},
        {'4', "....-"},
        {'5', "....."},
        {'6', "-...."},
        {'7', "--..."},
        {'8', "---.."},
        {'9', "----."},
    };

    private Light blinkingLight;

    void Start()
    {
        blinkingLight = GetComponent<Light>();
        blinkingLight.enabled = false;

        dahDurationSeconds = ditDurationSeconds * 3;
        waitBetweenDitsAndDahsSeconds = ditDurationSeconds;
        waitBetweenCharactersSeconds = ditDurationSeconds * 3;
        waitBetweenWordsSeconds = ditDurationSeconds * 7;
        repeatWaitTimeSeconds = ditDurationSeconds * 10;

        message = message.ToUpper();
        StartCoroutine(Play(0));
    }

    private IEnumerator Play(int charIndex) {

        if (charIndex == message.Length) {
            charIndex = 0;
            yield return new WaitForSeconds(repeatWaitTimeSeconds);
        }

        char currentChar = message[charIndex];

        //We reached the end of the word
        if (currentChar == ' ') {
            yield return new WaitForSeconds(waitBetweenWordsSeconds);
        }
        else {
            string encoding = morseCodeEncoding[currentChar];

            float wait;
            for (int i = 0; i < encoding.Length; ++i) {
                wait = encoding[i] == '.' ? ditDurationSeconds : dahDurationSeconds;

                blinkingLight.enabled = true;
                yield return new WaitForSeconds(wait);
                blinkingLight.enabled = false;

                if (i != encoding.Length - 1) {
                    yield return new WaitForSeconds(waitBetweenDitsAndDahsSeconds);
                }
            }

            yield return new WaitForSeconds(waitBetweenCharactersSeconds);
        }

        ++charIndex;
        yield return Play(charIndex);
    }
}
