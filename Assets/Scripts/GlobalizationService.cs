using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

public class GlobalizationService  {

    #region singleton pattern
    private static GlobalizationService instance = null;

    public static GlobalizationService Instance
    {
        get
        {
            if (instance == null) instance = new GlobalizationService();
            return instance;
        }
    }
    #endregion

    private Dictionary<string, Dictionary<SystemLanguage, string>> multiLanguageDictionary; 

    public SystemLanguage CurrentLanguage { get; private set; }

    #region Globalization Constants
    public const string ContinueButton = "ContinueButton";
    public const string MaleButton = "MaleButton";
    public const string FemaleButton = "FemaleButton";
    public const string Session1Title = "Session1Title";
    public const string Session1SubTitle = "Session1SubTitle";
    public const string Session4Title = "Session4Title";
    public const string Session4SubTitle = "Session4SubTitle";
    public const string OpeningTitle = "OpeningTitle";
    public const string OpeningAText = "OpeningAText";
    public const string OpeningBText = "OpeningBText";
    public const string OpeningCText = "OpeningCText";
    public const string OpeningDText = "OpeningDText";
    public const string OpeningEText = "OpeningEText";
    public const string OpeningFText = "OpeningFText";
    public const string IntroducingOurselvesTitle = "IntroducingOurselvesTitle";
    public const string IntroducingOurselvesBackgroundText = "IntroducingOurselvesBackgroundText";
    public const string IntroducingOurselvesAvatarText = "IntroducingOurselvesAvatarText";
    public const string IBoxIntroductionTitle = "IBoxIntroductionTitle";
    public const string IBoxIntroductionAText = "IBoxIntroductionAText";
    public const string IBoxIntroductionBText = "IBoxIntroductionBText";
    public const string CandleCeremonyTitle = "CandleCeremonyTitle";
    public const string CandleCeremonyText = "CandleCeremonyText";
    public const string MinuteForMyselfTitle = "MinuteForMyselfTitle";
    public const string MinuteForMyselfAText = "MinuteForMyselfAText";
    public const string MinuteForMyselfBText = "MinuteForMyselfBText";
    public const string MinuteForMyselfCText = "MinuteForMyselfCText";
    public const string MinuteForMyselfDText = "MinuteForMyselfDText";
    public const string MeMeterText = "MeMeterText";
    public const string MeMeterClosingText = "MeMeterClosingText";
    public const string FacialMindfulnessTitle = "FacialMindfulnessTitle";
    public const string FacialMindfulnessA1Text = "FacialMindfulnessA1Text";
    public const string FacialMindfulnessA2Text = "FacialMindfulnessA2Text";
    public const string FacialMindfulnessB1Text = "FacialMindfulnessB1Text";
    public const string FacialMindfulnessB2Text = "FacialMindfulnessB2Text";
    public const string BreathingRegulationTitle = "BreathingRegulationTitle";
    public const string BreathingRegulationA1Text = "BreathingRegulationA1Text";
    public const string BreathingRegulationA2Text = "BreathingRegulationA2Text";
    public const string BreathingRegulationA3Text = "BreathingRegulationA3Text";
    public const string BreathingRegulationA4Text = "BreathingRegulationA4Text";
    public const string BreathingRegulationA5Text = "BreathingRegulationA5Text";
    public const string BreathingRegulationB1Text = "BreathingRegulationB1Text";
    public const string BreathingRegulationB2Text = "BreathingRegulationB2Text";
    public const string BreathingRegulationB3Text = "BreathingRegulationB3Text";
    public const string ActiveMeditationTitle = "ActiveMeditationTitle";
    public const string ActiveMeditationA1Text = "ActiveMeditationA1Text";
    public const string ActiveMeditationA2Text = "ActiveMeditationA2Text";
    public const string ActiveMeditationA3Text = "ActiveMeditationA3Text";
    public const string ActiveMeditationA4Text = "ActiveMeditationA4Text";
    public const string ActiveMeditationBText = "ActiveMeditationBText";
    public const string ActiveMeditationCText = "ActiveMeditationCText";
    public const string ActiveMeditationDText = "ActiveMeditationDText";
    public const string ProgressiveMuscleRelaxationTitle = "ProgressiveMuscleRelaxationTitle";
    public const string ProgressiveMuscleRelaxationAText = "ProgressiveMuscleRelaxationAText";
    public const string ProgressiveMuscleRelaxationBText = "ProgressiveMuscleRelaxationBText";
    public const string ProgressiveMuscleRelaxationC1Text = "ProgressiveMuscleRelaxationC1Text";
    public const string ProgressiveMuscleRelaxationC2Text = "ProgressiveMuscleRelaxationC2Text";
    public const string ProgressiveMuscleRelaxationC3Text = "ProgressiveMuscleRelaxationC3Text";
    public const string ProgressiveMuscleRelaxationC4Text = "ProgressiveMuscleRelaxationC4Text";
    public const string ProgressiveMuscleRelaxationC5Text = "ProgressiveMuscleRelaxationC5Text";
    public const string ProgressiveMuscleRelaxationC6Text = "ProgressiveMuscleRelaxationC6Text";
    public const string ProgressiveMuscleRelaxationC7Text = "ProgressiveMuscleRelaxationC7Text";
    public const string ProgressiveMuscleRelaxationD1Text = "ProgressiveMuscleRelaxationD1Text";
    public const string ProgressiveMuscleRelaxationD2Text = "ProgressiveMuscleRelaxationD2Text";
    public const string ProgressiveMuscleRelaxationD3Text = "ProgressiveMuscleRelaxationD3Text";
    public const string ProgressiveMuscleRelaxationD4Text = "ProgressiveMuscleRelaxationD4Text";
    public const string ProgressiveMuscleRelaxationD5Text = "ProgressiveMuscleRelaxationD5Text";
    public const string ProgressiveMuscleRelaxationD6Text = "ProgressiveMuscleRelaxationD6Text";
    public const string ProgressiveMuscleRelaxationD7Text = "ProgressiveMuscleRelaxationD7Text";
    public const string ProgressiveMuscleRelaxationE1Text = "ProgressiveMuscleRelaxationE1Text";
    public const string ProgressiveMuscleRelaxationE2Text = "ProgressiveMuscleRelaxationE2Text";
    public const string ProgressiveMuscleRelaxationE3Text = "ProgressiveMuscleRelaxationE3Text";
    public const string ProgressiveMuscleRelaxationE4Text = "ProgressiveMuscleRelaxationE4Text";
    public const string ProgressiveMuscleRelaxationE5Text = "ProgressiveMuscleRelaxationE5Text";
    public const string ProgressiveMuscleRelaxationE6Text = "ProgressiveMuscleRelaxationE6Text";
    public const string ProgressiveMuscleRelaxationF1Text = "ProgressiveMuscleRelaxationF1Text";
    public const string ProgressiveMuscleRelaxationF2Text = "ProgressiveMuscleRelaxationF2Text";
    public const string ProgressiveMuscleRelaxationF3Text = "ProgressiveMuscleRelaxationF3Text";
    public const string ProgressiveMuscleRelaxationF4Text = "ProgressiveMuscleRelaxationF4Text";
    public const string InternalSensationsTitle = "InternalSensationsTitle";
    public const string InternalSensationsAText = "InternalSensationsAText";
    public const string ClosingOfSessionTitle = "ClosingOfSessionTitle";
    public const string ClosingOfSessionAText = "ClosingOfSessionAText";
    public const string ClosingOfSessionCandleText = "ClosingOfSessionCandleText";
    public const string ClosingOfSessionCText = "ClosingOfSessionCText";
    
    #endregion

    public string Globalize(string globalizationKey)
    {
        //if the globalization key is not found, return the received key
        var dictionary = this.multiLanguageDictionary[globalizationKey];
        if (dictionary == null) return globalizationKey;

        var globalizedResult = dictionary[this.CurrentLanguage];
        //if there is no translation for the current language, return the translation for the firt language found
        if (globalizedResult == null) return dictionary.Values.FirstOrDefault();
        return globalizedResult;
    }

    private GlobalizationService()
    {
        this.CurrentLanguage = SystemLanguage.English;

        this.multiLanguageDictionary = new Dictionary<string, Dictionary<SystemLanguage, string>>();

        this.multiLanguageDictionary.Add(ContinueButton, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Continue"},
            {SystemLanguage.Italian, "Continuare"}
        });

        this.multiLanguageDictionary.Add(MaleButton, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Male"},
            {SystemLanguage.Italian, "Ragazzo"}
        });

        this.multiLanguageDictionary.Add(FemaleButton, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Female"},
            {SystemLanguage.Italian, "Ragazza"}
        });

        this.multiLanguageDictionary.Add(Session1Title, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Introduction"},
            {SystemLanguage.Italian, "Introduzione"}
        });

        this.multiLanguageDictionary.Add(Session1SubTitle, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Session 1"},
            {SystemLanguage.Italian, "Session 1"}
        });

        this.multiLanguageDictionary.Add(Session4Title, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Physical Regulation"},
            {SystemLanguage.Italian, "La Regolazione Fisica"}
        });

        this.multiLanguageDictionary.Add(Session4SubTitle, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Session 4"},
            {SystemLanguage.Italian, "Session 4"}
        });

        this.multiLanguageDictionary.Add(OpeningTitle, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Introduction"},
            {SystemLanguage.Italian, "Introduzione"}
        });

        this.multiLanguageDictionary.Add(OpeningAText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Hi!\nWelcome to Emoregulator, the game to learn how to handle stress and to regulate our emotions.\nOften we have to cope with situations that make us feeling stressed and that put us a little in trouble ...\nMaybe for an important exam, or because we discuss with our parents, or because we have too many things to do all at once!\nSo, we are getting nervous, we don't know what is the best to do ... we feel stomachache, headache and we don't find the solution..."},
            {SystemLanguage.Italian, "Ciao!\nBenvenuto(a) a Emoregulator, il gioco per imparare a gestire lo stress e le nostre emozioni.\n\nSpesso ci troviamo ad affrontare situazioni che ci agitano e ci mettono un po' in difficoltà...\nMagari per una verifica importante, oppure perchè discutiamo con i nostri genitori, o perchè abbiamo troppe cose da fare tutte insieme! Così, ci innervosiamo, non sappiamo cosa è meglio fare...cominciamo a sentire un gran mal di pancia, ci viene il mal di testa e non troviamo soluzione..."}
        });

        this.multiLanguageDictionary.Add(OpeningBText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Today we will learn to recognize what happens to our bodies when we are a bit nervous, and together we will see also how to better handle all these sensations.\n\nBut...we will do it having fun, playing!\n\nYes, in fact Emoregulator is a game developed for people of your age. So, today you will do different activities, some directly at the computer, other externally. Soon, you will know your avatar and he will do exactly the same as you do. You will give him the name that you want, in fact, the avatar is yourself."},
            {SystemLanguage.Italian, "Oggi impareremo a riconoscere ciò che succede al nostro corpo quando siamo un po' agitati, e vedremo insieme anche come gestire meglio tutte queste sensazioni.\n\nPerò...lo faremo divertendoci, giocando!\n\nEh sì, infatti Emoregulator è un gioco, sviluppato per i ragazzi della tua età. Oggi quindi farai diverse attività, alcune direttamente a computer, altre esternamente. Tra poco conoscerai il tuo avatar e lui farà esattamente lo stesso che farai tu(e). Potrai dargli il nome che vuoi, infatti l'avatar rappresenta te stesso(a). In altre parole, sarai tu(e), dentro al gioco!"}
        });

        this.multiLanguageDictionary.Add(OpeningCText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Before each level, you will find the written instructions of what you have to do.\nAlso, as you saw, you were equipped with some physiological sensors.\nWe will not do any medical examinations ;-).\nThrough the sensors you can see how you are improving in your ability to manage anxiety.\nYou will see your heart rate, and more you will be good, more points you'll get!\n\nThe game consists of several levels, to move to the next one, you should always complete the previous one and then click on the continue button."},
            {SystemLanguage.Italian, "Prima di ogni livello, troverai le istruzioni scritte di ciò che dovrai fare.\nInoltre, come hai visto, sei stato(a) dotato(a) di alcuni sensori fisiologici.\nNon vogliamo farti degli esami medici ;-)\nServiranno a te perchè così potrai vedere come stai migliorando nella tua capacità di gestire l'ansia.\nPotrai vedere il tuo battito cardiaco, e più sarai bravo, più punti guadagnerai!\n\nIl gioco si compone di diversi livelli, per passare al seguente, dovrai sempre completare quello che precedente e poi, cliccare in tasto continuare."}
        });

        this.multiLanguageDictionary.Add(OpeningDText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Each time you will complete an exercise, you'll earn points.\nAt each level the difficulty will increase, so you can earn more and more!\nAt the top left you will see the points that you can achieve and then, those that actually you have received.\nIn this way, your avatar will become stronger and more skilled in dealing with stress and manage emotions!"},
            {SystemLanguage.Italian, "Ogni volta che porterai a termine un esercizio, guadagnerai dei punti.\nAd ogni livello aumenterà la difficoltà, per cui potrai guadagnare sempre di più!\nIn alto a sinistra vedrai i punti che potresti raggiungere e poi, quelli che effettivamente riesci a prendere.\nIl tuo avatar diventerà così sempre più forte e più bravo nell'affrontare lo stress e gestire le emozioni!."}
        });

        this.multiLanguageDictionary.Add(OpeningEText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "At the beginning and at the end of the next session, you will find the M&M, your ME-METER. It it will be your thermomether to measure how much you will improve your ability to relax.\nLet's see how much good you will be!"},
            {SystemLanguage.Italian, "All'inizio ed alla fine della prossima sessione, troverai l' M&M, il tuo ME-METER. Questo sarà il tuo termometro per misurare quanto migliorerai la tua capacità di rilassamento.\nVediamo un po' quanto sarai bravo!"}
        });
        
        this.multiLanguageDictionary.Add(OpeningFText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Last information ...\nOnce you start exercising, you can always click on the icon in the lower right and read again the instructions.\nNow, it's time to have fun!\nCome on, let's start!"},
            {SystemLanguage.Italian, "Ultima informazione...\nUna volta iniziato l'esercizio, potrai sempre cliccare sull'icona in basso a destra e rileggere le istruzioni.\nOra, direi che è arrivato il momento di divertirci!\nForza, iniziamo!"}
        });

        this.multiLanguageDictionary.Add(IntroducingOurselvesTitle, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "My avatar"},
            {SystemLanguage.Italian, "Il mio avatar"}
        });

        this.multiLanguageDictionary.Add(IntroducingOurselvesBackgroundText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "First of all you have to choose the background that you want to give to the game.\nHere are various options.\nClick on the image.\nPerfect!"},
            {SystemLanguage.Italian, "Prima di tutto scegli lo sfondo che vuoi dare al gioco.\nQui trovi varie opzioni.\nClicca sopra l'immagine.\nPerfetto!"}
        });

        this.multiLanguageDictionary.Add(IntroducingOurselvesAvatarText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Now, we have to create your own avatar!\nThis is you.\nGive him/her a name!\nYou can choose what you want, your real name, or a different one.\nEvery time you will complete an exercise, you will become stronger.\nThen click on √ and let's continue our game!"},
            {SystemLanguage.Italian, "Ora, invece, creiamo il tuo avatar!\nQuesto sei tu.\nDagli un un nome!\nPuoi scegliere quello che vuoi, il tuo vero nome, o inventarne un altro.\nOgni volta che terminerai un esercizio diventerai più forte\nPoi clicca su √ e prosegui nel nostro gioco!"}
        });

        this.multiLanguageDictionary.Add(IBoxIntroductionTitle, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "I-Box"},
            {SystemLanguage.Italian, "I-Box"}
        });

        this.multiLanguageDictionary.Add(IBoxIntroductionAText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Another thing to know before starting our game ...\n\nThis is your I-box.\nIt's your personal space, where you collect points that you will be able to earn.\nHere also, you can put anything you will create during the exercises.\nYou can color the I-box as desired.\nMoreover it will always be at the top left.\nNext to the I-BOX you'll see your heartbeats, next to the hearth symbol."},
            {SystemLanguage.Italian, "Esther, please write down this part in italian please."}
        });

        this.multiLanguageDictionary.Add(IBoxIntroductionBText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Well!\nWe are ready to start having fun! Click to continue."},
            {SystemLanguage.Italian, "Esther, please write down this part in italian please."}
        });

        this.multiLanguageDictionary.Add(CandleCeremonyTitle, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Let's start: turn it on!"},
            {SystemLanguage.Italian, "Si inizia: accendiamola!"}
        });

        this.multiLanguageDictionary.Add(CandleCeremonyText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "We are finally in our game.\nBefore getting to the core, we must do something important.\n\nThis candle represents the beginning of our activities.\nTurn it on with the match!\n\nWhen we will finish, you will blow out this candle."},
            {SystemLanguage.Italian, "Eccoci finalmente al nostro gioco.\nPrima di entrare nel vivo dobbiamo fare una cosa importante.\n\nQuesta candela rappresenta l'inizio della nostra attività.\nAccendila con il fiammifero!\n\nQuando avremo finito, la spegnerai."}
        });

        this.multiLanguageDictionary.Add(MinuteForMyselfTitle, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "A Minute for Myself - M&M"},
            {SystemLanguage.Italian, "A Minute for Myself - M&M"}
        });

        this.multiLanguageDictionary.Add(MinuteForMyselfAText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Let's start now with the more practical exercises!"},
            {SystemLanguage.Italian, "Iniziamo ora con gli esercizi più pratici!"}
        });

        this.multiLanguageDictionary.Add(MinuteForMyselfBText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "The exercise is comprised of three stages:\n\nStage 1: Slow down your body and your thoughts\nTake a minute and focus on yourself, try to slow down your thoughts, let your mind and body relax, and pay attention to the natural rhythm of your breathing…You may close your eyes if you wish for a few seconds, take a slow deep breath. Just focus on the this natural action you are doing every day: breathing, and notice if it feels different to breath with focuse and attention."},
            {SystemLanguage.Italian, "L'M&M è composto da 3 fasi:\n\nFase 1: Calma il tuo corpo e I tuoi pensieri\nPrenditi un minuto e concentrati su te stesso, prova a calmare i tuoi pensieri, lascia rilassare il tuo corpo e i tuoi pensieri, e poni attenzione al ritmo naturale del tuo respiro… Se vuoi, puoi chiudere gli occhi per qualche secondo, fai un respiro lento e profondo. Semplicemente, focalizzati su questa naturale azione che fai ogni giorno: respira, e nota se senti un respiro diverso, quando ti focalizzi e poni attenzione."}
        });

        this.multiLanguageDictionary.Add(MinuteForMyselfCText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Stage 2: Orient - Focus on yourselves\nTry and focus yourselves in space and pay attention to what you feel, what you're doing, on the space, what is around you, and what is in the room … remind yourselves that you are in a safe and protected place."},
            {SystemLanguage.Italian, "Fase 2: Orient – Focalizzati su te stesso\nProva a concentrarti su te stesso nello spazio, e poni attenzione a cosa senti, a cosa fai, allo spazio, a ciò che è intorno a te, a ciò che c’è nella stanza...ricorda a te stesso che sei in uno spazio sicuro e protetto."}
        });

        this.multiLanguageDictionary.Add(MinuteForMyselfDText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Stage 3: Scan and rate yourselves\nTry and evaluate the amount of relaxation/tension you have in the moment according to the ME-Meter. Pay attention to how it feels in your body to be tense and what kind of thoughts come to your mind when you are tense."},
            {SystemLanguage.Italian, "Fase 3: Analizza e valuta te stesso\nProva a valutare il tuo livello di tensione, al momento, secondo il ME-METER. Poni attenzione a cosa senti a livello corporeo quando sei teso, e quali pensieri hai quando sei in tensione."}
        });

        this.multiLanguageDictionary.Add(MeMeterText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Now mark on the ME-METER your level of tension, from 1 to 10. (1 – very relaxed, 10 very tense)."},
            {SystemLanguage.Italian, "Adesso segna sul ME-METER il tuo livello di tensione, ora, da 1 a 10."}
        });

        this.multiLanguageDictionary.Add(FacialMindfulnessTitle, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Facial mindfulness"},
            {SystemLanguage.Italian, "Facial mindfulness"}
        });

        this.multiLanguageDictionary.Add(FacialMindfulnessA1Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Now let's focus on the face!"},
            {SystemLanguage.Italian, "Ora concentriamoci sulla faccia!"}
        });

        this.multiLanguageDictionary.Add(FacialMindfulnessA2Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Note the different parts of your face, your forehead, chin, mouth, eyes. The different parts of your face are relaxed or tense? Where do you feel more tense? Do you have some other feelings? What is your facial expression? Try to notice without changing expression."},
            {SystemLanguage.Italian, "Nota le diverse parti del tuo viso, la tua fronte, il mento, la bocca, gli occhi. Le diverse parti del tuo viso sono rilassate o tese? Dove le senti più tese? Hai altre sensazioni? Qual è la tua espressione facciale? Prova a notarlo senza cambiare espressione."}
        });

        this.multiLanguageDictionary.Add(FacialMindfulnessB1Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Now select from the list the parts of the face where you feel more tense: your avatar will be colored red in the corresponding areas."},
            {SystemLanguage.Italian, "Ora seleziona dall'elenco, le parti del viso dove ti senti più teso: il tuo avatar si colorerà di rosso nelle aree corrispondenti."}
        });

        this.multiLanguageDictionary.Add(FacialMindfulnessB2Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Then select the parts where you feel more relaxed: your avatar will be colored blue in the corresponding areas."},
            {SystemLanguage.Italian, "Seleziona poi le parti dove ti senti più rilassato: il tuo avatar si colorerà di azzurro nelle aree corrispondenti."}
        });

        this.multiLanguageDictionary.Add(BreathingRegulationTitle, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Let's regulate our breath!"},
            {SystemLanguage.Italian, "Regoliamo il respiro!"}
        });

        this.multiLanguageDictionary.Add(BreathingRegulationA1Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Now we will do another exercise: we will concentrate on our breathing!"},
            {SystemLanguage.Italian, "Ora invece faremo un altro esercizio: ci concentreremo sulla nostra respirazione!"}
        });

        this.multiLanguageDictionary.Add(BreathingRegulationA2Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Imagine that your stomach is a balloon."},
            {SystemLanguage.Italian, "Immagina che il tuo stomaco sia un palloncino."}
        });

        this.multiLanguageDictionary.Add(BreathingRegulationA3Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Put one hand on your stomach and the other on your chest.\nNow fill your Balloon-stomach with air (without moving the chest)."},
            {SystemLanguage.Italian, "Metti una mano sul tuo stomaco e l'altra sul petto.\nOra riempi il tuo palloncino-stomaco con l’aria (senza spostare il petto)."}
        });

        this.multiLanguageDictionary.Add(BreathingRegulationA4Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Hold for a few seconds, and let the air out."},
            {SystemLanguage.Italian, "Mantieni la posizione per alcuni secondi, e poi lascia che l'aria vada fuori."}
        });

        this.multiLanguageDictionary.Add(BreathingRegulationA5Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Notice how your stomach is getting bigger with each breath you take in and smaller with each breath you take out."},
            {SystemLanguage.Italian, "Nota come il tuo stomaco è sempre più grande con ogni respiro che prendi, e più piccolo con ogni respiro che fai uscire."}
        });

        this.multiLanguageDictionary.Add(BreathingRegulationB1Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "While you breathe in and out, some thoughts may come to mind-just notice them, without judging them and then let them go up in the air like a bubble."},
            {SystemLanguage.Italian, "Mentre inspiri ed espiri, possono venirti alla mente alcuni pensieri , semplicemente notali, senza giudicare e poi lasciarli andare nell’aria come una bolla."}
        });

        this.multiLanguageDictionary.Add(BreathingRegulationB2Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "And focus back on your breathing, an activity that you do every day automatically. Just breathe in and out, that’s right- just like that."},
            {SystemLanguage.Italian, "E concentrati di nuovo sulla respirazione. Questa è un'attività che fai ogni giorno automaticamente. Semplicemente inspira ed espira."}
        });

        this.multiLanguageDictionary.Add(BreathingRegulationB3Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "That's right! Good!"},
            {SystemLanguage.Italian, "Così giusto, proprio così! Bravo(a)."}
        });

        this.multiLanguageDictionary.Add(ActiveMeditationTitle, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Let's be active, let's moving ... and let's meditate!"},
            {SystemLanguage.Italian, "Attiviamoci, muoviamoci e...meditiamo!"}
        });

        this.multiLanguageDictionary.Add(ActiveMeditationA1Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Now a bit of movement!"},
            {SystemLanguage.Italian, "Ora un po' di movimento!"}
        });

        this.multiLanguageDictionary.Add(ActiveMeditationA2Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Now we will put a music, for 3-5 minutes.\nYou'll have to move, dance with the beat, with all the parts of your body.\nTry to use every part."},
            {SystemLanguage.Italian, "Adesso metteremo una musica, per 3-5 minuti.\nDovrai far muovere, ballare a ritmo, tutte le parti del tuo corpo.\nCerca di utilizzare ogni parte."}
        });

        this.multiLanguageDictionary.Add(ActiveMeditationA3Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Ready?"},
            {SystemLanguage.Italian, "Pronto(a)?"}
        });

        this.multiLanguageDictionary.Add(ActiveMeditationA4Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Let's start!"},
            {SystemLanguage.Italian, "Partiamo!"}
        });

        this.multiLanguageDictionary.Add(ActiveMeditationBText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Now you can stop.\nClose your eyes, relax and pay attention to your body, your breath, what you feel, for 1 minute, until you will hear a sound."},
            {SystemLanguage.Italian, "Ora puoi fermarti.\nChiudi gli occhi, rilassati e poni attenzione al tuo corpo, al tuo respiro, a ciò che senti, per 1 minuto, fino a che sentirai un suono."}
        });

        this.multiLanguageDictionary.Add(ActiveMeditationCText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Try to Focus attention to the differences in sensations between the activity of shaking and the relaxation experienced afterwards."},
            {SystemLanguage.Italian, "Cerca di cogliere le differenze tra quando hai ballato, hai mosso tutto il corpo, e quando invece ti sei  fermato e rilassato."}
        });

        this.multiLanguageDictionary.Add(ActiveMeditationDText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Did you notice the differences? For every part of your body, your breathing and your feelings."},
            {SystemLanguage.Italian, "Hai notato le differenze? Per ogni parte del tuo corpo, per il tuo respiro e per le tue sensazioni."}
        });

        this.multiLanguageDictionary.Add(ProgressiveMuscleRelaxationTitle, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Let's relax our muscles!"},
            {SystemLanguage.Italian, "Rilassiamo i muscoli!"}
        });

        this.multiLanguageDictionary.Add(ProgressiveMuscleRelaxationAText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "PMR is a technique for reducing anxiety by alternately tensing and relaxing the muscles.\n\nIt is an important and effective tool for relaxation and emotional regulating technique.\n\nYou can practice it at anytime and anywhere.\nIt's easy to master in a short span of time.\n\nThrough the process of first tensing then releasing muscles, you will aid your body in returning to a correct, more relaxed position.\nBy bringing on a state of relaxation, you will note that anxiety is diminished as well as the automatic physiological answer."},
            {SystemLanguage.Italian, "Il rilassamento muscolare progressivo è una tecnica per ridurre l’ansia alternando la tensione e il rilassamento muscolare.\n\nSi tratta di uno strumento importante ed efficace per il relax e la regolazione emotiva.\n\nPuoi praticarlo ovunque e in qualunque momento.\nÈ facile e veloce da imparare.\n\nAttraverso il processo di prima mettere in tensione, e poi rilassare i muscoli, aiuti il tuo corpo a tornare in una corretta posizione, più rilassata.\nPassando a uno stato di rilassamento, osserverai che l'ansia è diminuita, così come gli effetti negativi della risposta fisiologica automatica."}
        });

        this.multiLanguageDictionary.Add(ProgressiveMuscleRelaxationBText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Now let's start the exercise!\n\nIt will consist of 3 parts and you will have to use your imagination too."},
            {SystemLanguage.Italian, "Ora iniziamo l'esercizio!\nSarà composto da 3 parti e dovrai usare anche la tua immaginazione."}
        });

        this.multiLanguageDictionary.Add(ProgressiveMuscleRelaxationC1Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "First of all, you need to get a ball.\nHold it in your hand."},
            {SystemLanguage.Italian, "Prima di tutto, procurati una palla.\nTienila in mano."}
        });

        this.multiLanguageDictionary.Add(ProgressiveMuscleRelaxationC2Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Squeeze it really hard!"},
            {SystemLanguage.Italian, "Ora stringila fortissimo!"}
        });

        this.multiLanguageDictionary.Add(ProgressiveMuscleRelaxationC3Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Feel the pressure in your hand and in your arm as you squeeze it"},
            {SystemLanguage.Italian, "Senti la pressione nelle tue mani e nelle tue braccia mentre la spremi."}
        });

        this.multiLanguageDictionary.Add(ProgressiveMuscleRelaxationC4Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Now drop the ball and notice how your muscles are relaxed.\nSee how much better your hands feel."},
            {SystemLanguage.Italian, "Ora lascia cadere la palla e senti come i tuoi muscoli sono rilassati.\nVedi come le tue mani si sentono meglio?"}
        });

        this.multiLanguageDictionary.Add(ProgressiveMuscleRelaxationC5Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Try it again.\nSqueeze really, really hard… Just a little bit more."},
            {SystemLanguage.Italian, "Prova di nuovo. Stringila forte, forte, un po’ di più."}
        });

        this.multiLanguageDictionary.Add(ProgressiveMuscleRelaxationC6Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Now relax."},
            {SystemLanguage.Italian, "Ora rilassati!"}
        });

        this.multiLanguageDictionary.Add(ProgressiveMuscleRelaxationC7Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "How was that?"},
            {SystemLanguage.Italian, "Com’era?"}
        });

        this.multiLanguageDictionary.Add(ProgressiveMuscleRelaxationD1Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Okay, ready for the second exercise?"},
            {SystemLanguage.Italian, "Ok! Pronto per il secondo esercizio?"}
        });

        this.multiLanguageDictionary.Add(ProgressiveMuscleRelaxationD2Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Pretend that you just got up in the morning and you want to stretch so you can reach the sun…"},
            {SystemLanguage.Italian, "Fai finta che è mattina e ti sei appena svegliato.\nVuoi fare streching e raggiungere il sole con le mani"}});

        this.multiLanguageDictionary.Add(ProgressiveMuscleRelaxationD3Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Stretch your arms in front of you, up high over your head toward the sun… almost touching it… all the way up, stretch higher!"},
            {SystemLanguage.Italian, "Ora, stendi le braccia davanti a te, tirale.\nOra stendile verso l'alto, sopra la tua testa, e tirale! Vuoi raggiungere il sole....In alto, tirale!"}
        });

        this.multiLanguageDictionary.Add(ProgressiveMuscleRelaxationD4Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Now let your arms drop back to your side."},
            {SystemLanguage.Italian, "Ora lasciale cadere giù, rilassale."}
        });

        this.multiLanguageDictionary.Add(ProgressiveMuscleRelaxationD5Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Now stretch again up, up, up to the sun.\nHold tight and notice the tension in your hands and shoulders…"},
            {SystemLanguage.Italian, "Ora tirale di nuovo, in alto, in alto, verso il sole.\nNota come le tue spalle e le tue mani sono in tensione."}
        });

        this.multiLanguageDictionary.Add(ProgressiveMuscleRelaxationD6Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Now let them drop quickly."},
            {SystemLanguage.Italian, "Ora lasciale cadere di nuovo velocemente e rilassale!"}
        });

        this.multiLanguageDictionary.Add(ProgressiveMuscleRelaxationD7Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "It feels good right?\nGreat!"},
            {SystemLanguage.Italian, "Ti fa sentire bene, vero?\nFantastico!"}
        });

        this.multiLanguageDictionary.Add(ProgressiveMuscleRelaxationE1Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Now let’s try another exercise!"},
            {SystemLanguage.Italian, "Passiamo al prossimo esercizio!"}
        });

        this.multiLanguageDictionary.Add(ProgressiveMuscleRelaxationE2Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Pretend that you are a snail and you are sitting outside. Uh-oh, all of sudden you hear the alarm and you sense danger!"},
            {SystemLanguage.Italian, "Fai finta di essere una lumaca, ad un tratto senti un allarme e percepisci un pericolo!"}});

        this.multiLanguageDictionary.Add(ProgressiveMuscleRelaxationE3Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Curl back into your shell and hide…\npull your shoulder up to your ears and push your head down into your shoulders.\nHold it tight! Hold it!"},
            {SystemLanguage.Italian, "Piegati nel tuo guscio e nasconditi…\nTira su le spalle fino alle orecchie e metti la testa dentro le spalle.\nTienile strette! Tienile strette!"}
        });

        this.multiLanguageDictionary.Add(ProgressiveMuscleRelaxationE4Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Now let go, the danger is gone and you can play outside again. You feel relaxed and safe."},
            {SystemLanguage.Italian, "Ora lascia, il pericolo è passato e tu puoi di nuovo tornare fuori. Ti senti rilassato e sicuro."}
        });

        this.multiLanguageDictionary.Add(ProgressiveMuscleRelaxationE5Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Oh, watch out now, the alarm is on again! Push your shoulders way up to your ears and hold it!"},
            {SystemLanguage.Italian, "Oh, guarda fuori di nuovo! C'è di nuovo l’allarme!\nPorta le spalle in alto, alle orecchie e stringi…"}
        });

        this.multiLanguageDictionary.Add(ProgressiveMuscleRelaxationE6Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Ok you can relax now, bring your head out, and let your shoulders relax."},
            {SystemLanguage.Italian, "Ok, ora ti puoi rilassare, tira fuori la testa e rilassa le spalle."}
        });

        this.multiLanguageDictionary.Add(ProgressiveMuscleRelaxationF1Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Here is the last exercise."},
            {SystemLanguage.Italian, "Ora facciamo l’ultimo esercizio."}
        });

        this.multiLanguageDictionary.Add(ProgressiveMuscleRelaxationF2Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Pretend that you are standing barefooted on a warm, sandy beach.\nSquish your toes deep into the sand and feel how it goes through your toes."},
            {SystemLanguage.Italian, "Fai finta di essere al mare, a piedi nudi su una calda e morbida sabbia.\nPremi i piedi nella sabbia profondamente e senti come questa entra tra le dita."}
        });

        this.multiLanguageDictionary.Add(ProgressiveMuscleRelaxationF3Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Try to get your feet to the bottom, push down with your legs."},
            {SystemLanguage.Italian, "Cerca di spingere i piedi verso il basso, spingi con le gambe."}
        });

        this.multiLanguageDictionary.Add(ProgressiveMuscleRelaxationF4Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Now relax your feet, let your toes go loose and feel how nice and warm it is. Now you can slowly open your eyes.."},
            {SystemLanguage.Italian, "Ora rilassa i tuoi piedi, lascia sciogliere le tue dita e senti com'è piacevole, e come sono rilassate e calde."}
        });

        this.multiLanguageDictionary.Add(InternalSensationsTitle, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Color!"},
            {SystemLanguage.Italian, "Colore!"}
        });

        this.multiLanguageDictionary.Add(InternalSensationsAText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "We are coming to the end of our game.\n\nNow you can begin to customize a bit your Ibox!\n\nWe worked a lot on how to manage stress and on how to get relax.\n\nNow you will see a palette of colors.\nPick one that represents for you the sense of relaxation, calm.\nTake it and put it on 'Ibox to color!"},
            {SystemLanguage.Italian, "Stiamo giungendo alla fine del nostro gioco.\n\nFinalmente ora potrai cominciare a personalizzare un po' la tua Ibox!\n\nAbbiamo lavorato un bel po' sulla gestione dello stress e su come rilassarci.\n\nOra troverai una tavolozza di colori.\nScegline uno che rappresenti per te il senso di rilassamento, di calma.\nPrendilo e mettilo sull' Ibox per colorarla!"}
        });

        this.multiLanguageDictionary.Add(ClosingOfSessionTitle, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "We came to the end..."},
            {SystemLanguage.Italian, "Siamo giunti alla fine..."}
        });

        this.multiLanguageDictionary.Add(ClosingOfSessionAText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "We are arrived at the end of our journey today!\n\nToday we learned to be aware of our body and to regulate our sensations.\nWe learned to notice the difference between  “stress” and “relax.\nWe learned that sometimes we need to tense our muscles in order to feel it relaxed.\n\nYou were very good!\nYou worked hard and definitely you will be able to practice these exercises, when you need it!"},
            {SystemLanguage.Italian, "Siamo giunti alla fine del nostro percorso di oggi!\n\nAbbiamo imparato a diventare più consapevoli del nostro corpo ed a regolare le nostre sensazioni.\nAbbiamo imparato a notare la differenza tra “stress” e “relax”.\nAbbiamo imparato che a volte, dobbiamo mettere in tensione i nostri muscoli, per poterci poi rilassare.\n\nSei stato(a) molto bravo(a)! Ti sei impegnato(a) e sicuramente riuscirai a mettere in pratica questi esercizi, quando ne avrai bisogno!"}
        });

        this.multiLanguageDictionary.Add(ClosingOfSessionCandleText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Last but important step ....\nIt's up to our candle!\nWe came to the end and then we have to blow it out.\nBlow and click on it!"},
            {SystemLanguage.Italian, "Ultimo, ma importante passaggio...\nTocca alla nostra candela!\nSiamo giunti alla fine e quindi dobbiamo spegnerla.\nSoffia e clicca sopra!"}
        });

        this.multiLanguageDictionary.Add(ClosingOfSessionCText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Thank you for participating, I hope you enjoyed!"},
            {SystemLanguage.Italian, "Please Esther, write this in italian"}
        });
    }
}
