using System;
using System.Collections.Generic;
using System.Text;
using ION.Core.Extensions;

namespace FAtiMA.RemoteAgent
{
    public class EmotionalState
    {
        private float mood;
        private List<Emotion> emotions;

        public EmotionalState()
        {
            mood = 0;
            emotions = new List<Emotion>();
        }

        public float Mood
        {
            get
            {
                return this.mood;
            }
            set
            {
                this.mood = value;
            }
        }

        public List<Emotion> Emotions
        {
            get
            {
                return this.emotions;
            }
        }

        public void AddEmotion(Emotion e)
        {
            this.emotions.Add(e);
        }

        public Emotion GetStrongestEmotion()
        {
            Emotion strongest = null;
            foreach (Emotion emotion in emotions)
            {
                if ((strongest == null) || (emotion.Intensity > strongest.Intensity))
                {
                    strongest = emotion;
                }
            }
            return strongest;
        }

        public List<Emotion> GetTheThreeStrongestEmotions()
        {
            int i = 0;
            List<Emotion> result = new List<Emotion>();
            List<Emotion> listEmotions = emotions.ConvertAll<Emotion>(delegate(Emotion a) { return a; }); //Clone

            while ((listEmotions.Count != 0) && (i < 3))
            {
                Emotion strongest = null;
                int index = -1;
                for (int j = 0; j < listEmotions.Count; j++)
                {
                    Emotion emotion = listEmotions[j];
                    if ((strongest == null) || (emotion.Intensity > strongest.Intensity))
                    {
                        strongest = emotion;
                        index = j;
                    }
                }
                if (index != -1)
                {
                    result.Add(strongest);
                    listEmotions.RemoveAt(index);
                    i++;
                }
            }
            return result;
        }

        public string ToXml()
        {
            string emotionalState = "<EmotionalState><Mood>" + this.mood + "</Mood>";
            foreach (Emotion e in this.emotions)
            {
                emotionalState += e.ToXml();
            }
            emotionalState += "</EmotionalState>";

            return emotionalState;
        }
    }
}