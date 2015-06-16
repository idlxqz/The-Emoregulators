using System.Collections.Generic;
using System.ComponentModel;
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

    public SystemLanguage CurrentLanguage { get; set; }

    #region Globalization Constants

    public const string StartButton = "StartButton";
    public const string QuitButton = "QuitButton";
    public const string UserCodePlaceholder = "UserCodePlaceholder";
    public const string ContinueButton = "ContinueButton";
    public const string MaleButton = "MaleButton";
    public const string FemaleButton = "FemaleButton";
    public const string Session1Title = "Session1Title";
    public const string Session1SubTitle = "Session1SubTitle";
    public const string Session4Title = "Session4Title";
    public const string Session4SubTitle = "Session4SubTitle";
    public const string PreBaselineText = "PreBaselineText";
    public const string PostBaselineText = "PostBaselineText";
    public const string OpeningTitle = "OpeningTitle";
    public const string OpeningActivityName = "OpeningActivityName";
    public const string OpeningAText = "OpeningAText";
    public const string OpeningBText = "OpeningBText";
    public const string OpeningCText = "OpeningCText";
    public const string OpeningDText = "OpeningDText";
    public const string OpeningEText = "OpeningEText";
    public const string OpeningFText = "OpeningFText";
    public const string IntroducingOurselvesTitle = "IntroducingOurselvesTitle";
    public const string IntroducingOurselvesActivityName = "IntroducingOurselvesActivityName";
    public const string IntroducingOurselvesBackgroundText = "IntroducingOurselvesBackgroundText";
    public const string IntroducingOurselvesAvatarText = "IntroducingOurselvesAvatarText";
    public const string IBoxIntroductionTitle = "IBoxIntroductionTitle";
    public const string IBoxIntroductionActivityName = "IBoxIntroductionActivityName";
    public const string IBoxIntroductionAText = "IBoxIntroductionAText";
    public const string IBoxIntroductionBText = "IBoxIntroductionBText";
    public const string IBoxIntroductionCText = "IBoxIntroductionCText";
    public const string CandleCeremonyTitle = "CandleCeremonyTitle";
    public const string CandleCeremonyActivityName = "CandleCeremonyActivityName";
    public const string CandleCeremonyText = "CandleCeremonyText";
    public const string MinuteForMyselfTitle = "MinuteForMyselfTitle";
    public const string MinuteForMyselfActivityName = "MinuteForMyselfActivityName";
    public const string MinuteForMyselfAText = "MinuteForMyselfAText";
    public const string MinuteForMyselfBText = "MinuteForMyselfBText";
    public const string MinuteForMyselfCText = "MinuteForMyselfCText";
    public const string MinuteForMyselfDText = "MinuteForMyselfDText";
    public const string MeMeterText = "MeMeterText";
    public const string MeMeterClosingText = "MeMeterClosingText";
    public const string FacialMindfulnessTitle = "FacialMindfulnessTitle";
    public const string FacialMindfulnessActivityName = "FacialMindfulnessActivityName";
    public const string FacialMindfulnessA1Text = "FacialMindfulnessA1Text";
    public const string FacialMindfulnessA2Text = "FacialMindfulnessA2Text";
    public const string FacialMindfulnessB1Text = "FacialMindfulnessB1Text";
    public const string FacialMindfulnessB2Text = "FacialMindfulnessB2Text";
    public const string BreathingRegulationTitle = "BreathingRegulationTitle";
    public const string BreathingRegulationActivityName = "BreathingRegulationActivityName";
    public const string BreathingRegulationA1Text = "BreathingRegulationA1Text";
    public const string BreathingRegulationA2Text = "BreathingRegulationA2Text";
    public const string BreathingRegulationA3Text = "BreathingRegulationA3Text";
    public const string BreathingRegulationA4Text = "BreathingRegulationA4Text";
    public const string BreathingRegulationA5Text = "BreathingRegulationA5Text";
    public const string BreathingRegulationB1Text = "BreathingRegulationB1Text";
    public const string BreathingRegulationB2Text = "BreathingRegulationB2Text";
    public const string BreathingRegulationB3Text = "BreathingRegulationB3Text";
    public const string ActiveMeditationTitle = "ActiveMeditationTitle";
    public const string ActiveMeditationActivityName = "ActiveMeditationActivityName";
    public const string ActiveMeditationA1Text = "ActiveMeditationA1Text";
    public const string ActiveMeditationA2Text = "ActiveMeditationA2Text";
    public const string ActiveMeditationA3Text = "ActiveMeditationA3Text";
    public const string ActiveMeditationA4Text = "ActiveMeditationA4Text";
    public const string ActiveMeditationBText = "ActiveMeditationBText";
    public const string ActiveMeditationCText = "ActiveMeditationCText";
    public const string ActiveMeditationDText = "ActiveMeditationDText";
    public const string ProgressiveMuscleRelaxationTitle = "ProgressiveMuscleRelaxationTitle";
    public const string ProgressiveMuscleRelaxationActivityName = "ProgressiveMuscleRelaxationActivityName";
    public const string ProgressiveMuscleRelaxationAText = "ProgressiveMuscleRelaxationAText";
    public const string ProgressiveMuscleRelaxationBText = "ProgressiveMuscleRelaxationBText";
    public const string ProgressiveMuscleRelaxationC1Text = "ProgressiveMuscleRelaxationC1Text";
    public const string ProgressiveMuscleRelaxationC2Text = "ProgressiveMuscleRelaxationC2Text";
    public const string ProgressiveMuscleRelaxationC3Text = "ProgressiveMuscleRelaxationC3Text";
    public const string ProgressiveMuscleRelaxationC4Text = "ProgressiveMuscleRelaxationC4Text";
    public const string ProgressiveMuscleRelaxationC5Text = "ProgressiveMuscleRelaxationC5Text";
    public const string ProgressiveMuscleRelaxationC6Text = "ProgressiveMuscleRelaxationC6Text";
    public const string ProgressiveMuscleRelaxationC7Text = "ProgressiveMuscleRelaxationC7Text";
    public const string ProgressiveMuscleRelaxationC8Text = "ProgressiveMuscleRelaxationC8Text";
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
    public const string ProgressiveMuscleRelaxationE7Text = "ProgressiveMuscleRelaxationE7Text";
    public const string ProgressiveMuscleRelaxationE8Text = "ProgressiveMuscleRelaxationE8Text";
    public const string ProgressiveMuscleRelaxationF1Text = "ProgressiveMuscleRelaxationF1Text";
    public const string ProgressiveMuscleRelaxationF2Text = "ProgressiveMuscleRelaxationF2Text";
    public const string ProgressiveMuscleRelaxationF3Text = "ProgressiveMuscleRelaxationF3Text";
    public const string ProgressiveMuscleRelaxationF4Text = "ProgressiveMuscleRelaxationF4Text";
    public const string InternalSensationsTitle = "InternalSensationsTitle";
    public const string InternalSensationsActivityName = "InternalSensationsActivityName";
    public const string InternalSensationsAText = "InternalSensationsAText";
    public const string ClosingOfSessionTitle = "ClosingOfSessionTitle";
    public const string ClosingOfSessionActivityName = "ClosingOfSessionActivityName";
    public const string ClosingOfSessionA1Text = "ClosingOfSessionA1Text";
    public const string ClosingOfSessionA2Text = "ClosingOfSessionA2Text";
    public const string ClosingOfSessionA3Text = "ClosingOfSessionA3Text";
    public const string ClosingOfSessionCandleText = "ClosingOfSessionCandleText";
    public const string ClosingOfSessionCText = "ClosingOfSessionCText";
    public const string HowDoesMyBodyFeelTitle = "HowDoesMyBodyFeelTitle";
    public const string HowDoesMyBodyFeelActivityName = "HowDoesMyBodyFeelActivityName";
    public const string HowDoesMyBodyFeelAText = "HowDoesMyBodyFeelAText";
    public const string HowDoesMyBodyFeelBText = "HowDoesMyBodyFeelBText";
    public const string HowDoesMyBodyFeelCText = "HowDoesMyBodyFeelCText";
    public const string HowDoesMyBodyFeelDText = "HowDoesMyBodyFeelDText";
    public const string HowDoesMyBodyFeelHappiness = "HowDoesMyBodyFeelHappiness";
    public const string HowDoesMyBodyFeelSadness = "HowDoesMyBodyFeelHappy";
    public const string HowDoesMyBodyFeelAnger = "HowDoesMyBodyFeelAnger";
    public const string HowDoesMyBodyFeelFear = "HowDoesMyBodyFeelFear";
    public const string HowDoesMyBodyFeelDisgust = "HowDoesMyBodyFeelDisgust";
    public const string HowDoesMyBodyFeelSurprise = "HowDoesMyBodyFeelSurprise";
    
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
        this.CurrentLanguage = SystemLanguage.Italian;

        this.multiLanguageDictionary = new Dictionary<string, Dictionary<SystemLanguage, string>>();

        this.multiLanguageDictionary.Add(StartButton, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Start"},
            {SystemLanguage.Italian, "Start"}
        });

        this.multiLanguageDictionary.Add(QuitButton, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Exit"},
            {SystemLanguage.Italian, "Exit"}
        });

        this.multiLanguageDictionary.Add(UserCodePlaceholder, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Enter User Code...(same as questionnaire)"},
            {SystemLanguage.Italian, "Inserire il Codice Utente...(come per i questionari)"}
        });


        this.multiLanguageDictionary.Add(ContinueButton, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Continue"},
            {SystemLanguage.Italian, "Continuare"}
        });

        this.multiLanguageDictionary.Add(MaleButton, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Male"},
            {SystemLanguage.Italian, "Maschio"}
        });

        this.multiLanguageDictionary.Add(FemaleButton, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Female"},
            {SystemLanguage.Italian, "Femmina"}
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
            {SystemLanguage.English, "Session 2"},
            {SystemLanguage.Italian, "Session 2"}
        });

        this.multiLanguageDictionary.Add(PreBaselineText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Now you will watch a video for 2 minutes, get comfortable and do not get up in the middle, trying to stay in the same position until the end, please.\n\nPlease sit with your back against the backrest, hands on legs, and eyes looking at the video, for all the time.\nWe would also like to ask you, not to talk or to change the position, during the video.\n\nWe need this to properly calibrate the sensors and to be sure that they are working correctly.\n\nNow, click “continue”, to watch the video."},
            {SystemLanguage.Italian, "Ora vedrai un video per 2 minuti, mettiti comodo/comoda e non alzarti nel mezzo, cercando di restare nella stessa posizione fino alla fine, perfavore.\n\nTi chiedo di sederti con la schiena appoggiata allo schienale, le mani sulle gambe, e lo sguardo rivolto al video, per tutta la durata.\nTi chiedo anche perfavore di non parlare, o cambiare posizione, durante il video.\n\nQuesto ci serve per calibrare bene i sensori ed essere sicuri che stiano funzionando correttamente.\n\nOra clicca “continuare” per vedere il video."}
        });

        this.multiLanguageDictionary.Add(PostBaselineText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Now we start our training!\n\nFirst, it will begin, the first session, with the general explanation, then the second session will follow, with the exercises.\n\nClick \"continue\"."},
            {SystemLanguage.Italian, "Ora iniziamo il nostro training!\n\nPrima inizierà la sessione 1, con la spiegazione generale, poi seguirà la sessione 2, con gli esercizi.\n\nClicca “continuare”"}
        });

        this.multiLanguageDictionary.Add(OpeningTitle, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Introduction"},
            {SystemLanguage.Italian, "Introduzione"}
        });

        this.multiLanguageDictionary.Add(OpeningActivityName, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Introduction"},
            {SystemLanguage.Italian, "Introduction"}
        });

        this.multiLanguageDictionary.Add(OpeningAText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Hi!\nWelcome to Emoregulators, the game to learn how to handle stress and to regulate our emotions.\n\nOften we have to cope with situations that make us feeling stressed and that put us a little in trouble ...\nMaybe for an important exam, or because we discuss with our parents, or because we have too many things to do all at once!\nSo, we are getting nervous, we don't know what is the best to do ... we feel stomachache, headache and we don't find the solution..."},
            {SystemLanguage.Italian, "Ciao!\nBenvenuto/Benvenuta a Emoregulators, il gioco per imparare a gestire lo stress e le nostre emozioni.\n\nSpesso ci troviamo ad affrontare situazioni che ci agitano e ci mettono un po' in difficoltà...\nMagari per una verifica importante, oppure perchè discutiamo con i nostri genitori, o perchè abbiamo troppe cose da fare tutte insieme!\nCosì, ci innervosiamo, non sappiamo cosa è meglio fare...cominciamo a sentire un gran mal di pancia, ci viene il mal di testa e non troviamo soluzione..."}
        });

        this.multiLanguageDictionary.Add(OpeningBText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Today we will learn to recognize what happens to our bodies when we are a bit nervous, and together we will see also how to better handle all these sensations.\n\nBut...we will do it having fun, playing!\n\nYes, in fact Emoregulator is a game developed for people of your age. So, today you will do different activities, some directly at the computer, other without.\nSoon, you will know your avatar and you can give him the name that you want, in fact, the avatar is yourself in the game!"},
            {SystemLanguage.Italian, "Oggi impareremo a riconoscere ciò che succede al nostro corpo quando siamo un po' agitati, e vedremo insieme anche come gestire meglio tutte queste sensazioni.\n\nPerò...lo faremo divertendoci, giocando!\n\nEh sì, infatti Emoregulator è un gioco, sviluppato per i ragazzi della tua età. Oggi quindi farai diverse attività, alcune direttamente a computer, altre senza.\nTra poco conoscerai il tuo avatar e potrai dargli il nome che vuoi, infatti l'avatar rappresenterà te stesso/ te stessa, dentro al gioco! "}
        });

        this.multiLanguageDictionary.Add(OpeningCText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Before each execise, you will find the written instructions of what you have to do.\nAlso, as you saw, you were equipped with some physiological sensors.\nWe will not do any medical examinations ;-).\nYou will use them, because you will be able to see your heart rate, and then, see your progress during the various exercises.\nYou can always see your heart rate, with this symbol, up, in the left corner:\n\nThe more you will be good, more points you'll get!"},
            {SystemLanguage.Italian, "Prima di ogni esercizio, troverai le istruzioni scritte di ciò che dovrai fare.\nInoltre, come hai visto, sei stato dotato/stata dotata di alcuni sensori fisiologici.\nNon vogliamo farti degli esami medici ;-)\nServiranno a te perchè così potrai vedere il tuo battito cardiaco, e quindi, vedere il tuo andamento, nel corso dei vari esercizi.\nPotrai sempre vedere il tuo battito cardiaco, con questo simbolo, in alto a sinistra:\n\nPiù migliorerai, più punti guadagnerai!"}
        });

        this.multiLanguageDictionary.Add(OpeningDText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "As every game, Emoregulators consists of several levels, and, to move to the next one, you should always complete the previous one and then click on the \"continue\" button. You can earn points, only if you will complete the exercise. You can earn more and more! \nAt the top left you will see the points that you have received.\nThe maximum points you can earn, in total, is 100.\nIn this way, your avatar will become stronger and more skilled in dealing with stress and manage emotions!"},
            {SystemLanguage.Italian, "Come ogni gioco, Emoregulators si compone di diversi livelli, e, per passare al seguente, dovrai sempre completare quello precedente e poi, cliccare in basso, sul tasto “continuare”.\nOgni esercizio sarà un livello.\n\nGuadagnerai dei punti, solo se porterai a termine l'esercizio. Potrai guadagnarne sempre di più! \nIn alto a sinistra vedrai i punti che riesci a prendere.\nIl massimo dei punti che potrai guadagnare, in totale, è 100.\nIl tuo avatar diventerà così sempre più forte e più bravo nell'affrontare lo stress e gestire le emozioni!."}
        });

        this.multiLanguageDictionary.Add(OpeningEText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "At the beginning and at the end of the next session, you will find the M&M, your ME-METER.\nIt it will be your thermomether to measure how much you will improve your ability to relax.\nLet's see how good will you be!"},
            {SystemLanguage.Italian, "All'inizio ed alla fine della prossima sessione, troverai l' M&M, il tuo ME-METER.\nQuesto sarà il tuo termometro per misurare quanto migliorerai la tua capacità di rilassamento.\nVediamo un po' quanto sarai bravo!"}
        });
        
        this.multiLanguageDictionary.Add(OpeningFText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Last information ...\nOnce you start exercising, sometimes, you will find this icon, in the lower right. You can click on it, and read again the instructions.\nNow, it's time to have fun!\n\nLet's start customizing a bit the game, before starting the exercise!"},
            {SystemLanguage.Italian, "Ultima informazione...\nUna volta iniziato l'esercizio, a volte troverai questa icona, in basso a destra. Potrai cliccarla e rileggere le istruzioni.\nOra, direi che è arrivato il momento di divertirci!\n\nIniziamo personalizzando un po' il gioco, prima di iniziare gli esercizi!"}
        });

        this.multiLanguageDictionary.Add(IntroducingOurselvesTitle, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "My personal space"},
            {SystemLanguage.Italian, "Il mio spazio personale"}
        });

        this.multiLanguageDictionary.Add(IntroducingOurselvesActivityName, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Introducing Ourselves"},
            {SystemLanguage.Italian, "Introducing Ourselves"}
        });

        this.multiLanguageDictionary.Add(IntroducingOurselvesBackgroundText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "First of all you have to choose the background that you want to give to the game, the one that makes you feel more relaxed.\nIn the next screen you will find various options. Click on “next” or on “previous” to go forward or to go back. \nBy clicking on each box, you can preview it.\nClick on the image that you prefer."},
            {SystemLanguage.Italian, "Prima di tutto scegli lo sfondo che vuoi dare al gioco, lo sfondo che più ti rilassa.\nNel prossimo screen troverai varie opzioni. Clicca su “next” o “previous” per andare avanti, o per tornare indietro.\nCliccando su ogni riquadro, potrai vederne l'anteprima.\nClicca sopra l'immagine che preferisci."}
        });

        this.multiLanguageDictionary.Add(IntroducingOurselvesAvatarText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Now, you have to choose your own avatar!\nClick on “Female” or “Male” to see them.\nThen give him/her a name!\nYou can choose what you want, your real name, or a different one.\nEvery time you will complete an exercise, you will become stronger.\nThen click on \"ok\" and let's continue!"},
            {SystemLanguage.Italian, "Ora, invece, scegli il tuo avatar!\nClicca su “femmina” o su “maschio”, per visualizzarli.\nPoi dagli anche un un nome!\nPuoi scegliere quello che vuoi, il tuo vero nome, o inventarne un altro.\nOgni volta che terminerai un esercizio diventerai più forte\nPoi clicca su \"ok\" e prosegui!"}
        });

        this.multiLanguageDictionary.Add(IBoxIntroductionTitle, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "I-Box"},
            {SystemLanguage.Italian, "I-Box"}
        });

        this.multiLanguageDictionary.Add(IBoxIntroductionActivityName, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "I-Box 1: Introduction"},
            {SystemLanguage.Italian, "I-Box 1: Introduction"}
        });

        this.multiLanguageDictionary.Add(IBoxIntroductionAText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Another thing to know before starting the game ...\n\nThis is your I-box.\nIt's your personal space, where you collect points that you will be able to earn.\nMoreover it will always be at the top left. \nIn the next screen you will find the instructions, the you can color the Ibox, as you want."},
            {SystemLanguage.Italian, "Altra cosa da sapere prima di iniziare il gioco...\n\nQuesta è la tua I-BOX.\nÈ il tuo spazio personale, dove raccoglierai i punti che riuscirai a guadagnare.\nInoltre sarà sempre visualizzabile in alto a sinistra. \nNel prossimo screen troverai delle istruzioni e poi, potrai colorare l'I-box come desideri"}
        });

        this.multiLanguageDictionary.Add(IBoxIntroductionBText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Now you can start to customize a bit, your Ibox!\n\nNow you will see a palette of colors.\nPick one that represents for you the sense of tension, anxiety.\nTake it and put it on the Ibox to color!\n\nThen watch your Ibox, top left: from now on, it will become the color you choose!"},
            {SystemLanguage.Italian, "Ora potrai cominciare a personalizzare un po' la tua Ibox!\n\nNel prossimo screen troverai una tavolozza di colori.\nScegline uno che rappresenti per te il senso di tensione, di ansia.\nPrendilo e mettilo sull' Ibox per colorarla!\n\nPoi guarda la tua Ibox, in alto a sinistra: diventerà del colore da te scelto"}
        });

        this.multiLanguageDictionary.Add(IBoxIntroductionCText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Well!\n\nNow, that you have created your personal space, we are ready to start to have fun with the next session!\nClick to continue with the game."},
            {SystemLanguage.Italian, "Bene!\nOra che hai creato il tuo spazio personale, direi che siamo pronti per iniziare a divertirci con la prossima sessione! Clicca per proseguire col gioco!"}
        });

        this.multiLanguageDictionary.Add(CandleCeremonyTitle, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Let's start: turn it on!"},
            {SystemLanguage.Italian, "Si inizia: accendiamola!"}
        });

        this.multiLanguageDictionary.Add(CandleCeremonyActivityName, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Candle Lighting Ceremony"},
            {SystemLanguage.Italian, "Candle Lighting Ceremony"}
        });

        this.multiLanguageDictionary.Add(CandleCeremonyText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "We are finally in our game.\nBefore getting to the core, we must do something important.\n\nThis candle represents the beginning of our activities.\nTurn it on with the match!\n\nTake it with the hand and move it on the candle.\n\nWhen we will finish, you will blow out this candle.\n\nFrom this point onward you will earn points, look at the upper left corner!"},
            {SystemLanguage.Italian, "Eccoci finalmente al nostro gioco.\nPrima di entrare nel vivo dobbiamo fare una cosa importante.\n\nQuesta candela rappresenta l'inizio della nostra attività.\nAccendila con il fiammifero!\n\nPrendilo con la mano e spostalo sulla candela.\n\nQuando avremo finito, la spegnerai.\n\nDa qui in poi guadagnerai dei punti, guarda in alto a sinistra!"}
        });

        this.multiLanguageDictionary.Add(MinuteForMyselfTitle, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "A Minute for Myself - M&M"},
            {SystemLanguage.Italian, "A Minute for Myself - M&M"}
        });

        this.multiLanguageDictionary.Add(MinuteForMyselfActivityName, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "A Minute for Myself"},
            {SystemLanguage.Italian, "A Minute for Myself"}
        });

        this.multiLanguageDictionary.Add(MinuteForMyselfAText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Let's start now with the first exercise! \n\nNow, and at the end of this session, you'll find the M&M, your ME-METER. This will be your thermometer to measure how you will improve your ability to relax.\nLet 's see how much good you will be! "},
            {SystemLanguage.Italian, "Iniziamo ora con il primo esercizio! Ora, e poi alla fine di questa sessione, troverai l' M&M, il tuo ME-METER.\n\nQuesto sarà il tuo termometro per misurare quanto migliorerai la tua capacità di rilassamento.\nVediamo un po' quanto sarai bravo!"}
        });

        this.multiLanguageDictionary.Add(MinuteForMyselfBText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "The exercise is comprised of three stages, look at how your heart beat will change.\n\nStage 1: Slow down your body and your thoughts\n\nTake a minute and focus on yourself, try to slow down your thoughts, let your mind and body relax, and pay attention to the natural rhythm of your breathing…You may close your eyes if you wish for a few seconds, take 1 slow deep breath. Just focus on this natural action you are doing every day: breathing, and notice if it feels different to breath with focus and attention."},
            {SystemLanguage.Italian, "L'M&M è composto da 3 fasi, osserva come il tuo battito varierà.\n\nFase 1: Calma il tuo corpo e I tuoi pensieri\n\nPrenditi un minuto e concentrati su te stesso, prova a calmare i tuoi pensieri, lascia rilassare il tuo corpo e i tuoi pensieri, e poni attenzione al ritmo naturale del tuo respiro… Se vuoi, puoi chiudere gli occhi per qualche secondo, fai 1 respiro lento e profondo. Semplicemente, focalizzati su questa naturale azione che fai ogni giorno: respira, e nota se senti un respiro diverso, quando ti focalizzi e poni attenzione."}
        });

        this.multiLanguageDictionary.Add(MinuteForMyselfCText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Stage 2: Orient - Focus on yourselves\n\nTry and focus yourselves in space and pay attention to what you feel, what you're doing, on the space, what is around you, and what is in the room … remind yourselves that you are in a safe and protected place.\n\nTake some seconds, until you hear a sound, and try to make 2 other deep breaths.\n\nInhales and exhales."},
            {SystemLanguage.Italian, "Fase 2: Orient – Focalizzati su te stesso\n\nProva a concentrarti su te stesso nello spazio, e poni attenzione a cosa senti, a cosa fai, allo spazio, a ciò che è intorno a te, a ciò che c’è nella stanza...ricorda a te stesso che sei in uno spazio sicuro e protetto.\n\nPrenditi qualche secondo, fino a quando sentirai un suono, e prova a fare altri 2 respiri profondi.\n\nInspira ed espira"}
        });

        this.multiLanguageDictionary.Add(MinuteForMyselfDText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Stage 3: Scan and rate yourselves\n\nTry and evaluate the amount of relaxation/tension you have in the moment according to the ME-Meter. Pay attention to how it feels in your body to be tense and what kind of thoughts come to your mind when you are tense.\n\nNow click on \"continue\" to see your ME-METER"},
            {SystemLanguage.Italian, "Fase 3: Analizza e valuta te stesso\n\nProva a valutare il tuo livello di tensione, al momento, secondo il ME-METER. Poni attenzione a cosa senti a livello corporeo quando sei teso, e quali pensieri hai quando sei in tensione.\n\nOra clicca su \"continuare\" per visualizzare il tuo ME-METER."}
        });

        this.multiLanguageDictionary.Add(MeMeterText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Now mark on the ME-METER your level of tension, from 1 to 10.\n(1 – very relaxed, 10 - very tense)."},
            {SystemLanguage.Italian, "Adesso segna sul ME-METER il tuo livello di tensione, ora, da 1 a 10.\n(1 - molto rilassato, 10 - molto teso)."}
        });

        this.multiLanguageDictionary.Add(FacialMindfulnessTitle, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Facial mindfulness"},
            {SystemLanguage.Italian, "Facial mindfulness"}
        });

        this.multiLanguageDictionary.Add(FacialMindfulnessActivityName, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Facial mindfulness"},
            {SystemLanguage.Italian, "Facial mindfulness"}
        });

        this.multiLanguageDictionary.Add(FacialMindfulnessA1Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Now let's focus on the face!"},
            {SystemLanguage.Italian, "Ora concentriamoci sul viso!"}
        });

        this.multiLanguageDictionary.Add(FacialMindfulnessA2Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Note the different parts of your face, your forehead, chin, mouth, eyes. The different parts of your face are relaxed or tense? Where do you feel more tense? Do you have some other feelings? What is your facial expression? Try to notice without changing expression.\nTake some seconds, then click on “continue”"},
            {SystemLanguage.Italian, "Nota le diverse parti del tuo viso, la tua fronte, il mento, la bocca, gli occhi. Le diverse parti del tuo viso sono rilassate o tese? Dove le senti più tese? Hai altre sensazioni? Qual è la tua espressione facciale? Prova a notarlo senza cambiare espressione.\nPrenditi qualche secondo, poi clicca “continuare”"}
        });

        this.multiLanguageDictionary.Add(FacialMindfulnessB1Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "In the next screen you will have to select the parts of the face where you feel more tense: your avatar will be colored red in the corresponding areas.\nClick 1 time.\nThen select the parts where you feel more relaxed: your avatar will be colored blue in the corresponding areas.\nClick 2 times.\n\nIf you want to change, click 3 times and then select again."},
            {SystemLanguage.Italian, "Nel prossimo screen dovrai selezionare le parti del viso dove ti senti più teso: il tuo avatar si colorerà di rosso nelle aree corrispondenti.\nDovrai cliccare 1 volta.\nSeleziona poi le parti dove ti senti più rilassato: il tuo avatar si colorerà di azzurro nelle aree corrispondenti.\nDovrai cliccare 2 volte.\n\nSe vuoi cambiare, clicca 3 volte e poi ri-seleziona."}
        });

        this.multiLanguageDictionary.Add(FacialMindfulnessB2Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Click \"continue\" and you will find the face of your avatar."},
            {SystemLanguage.Italian, "Clicca \"continuare\" e troverai la faccia del tuo avatar."}
        });

        this.multiLanguageDictionary.Add(BreathingRegulationTitle, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Let's regulate our breath!"},
            {SystemLanguage.Italian, "Regoliamo il respiro!"}
        });

        this.multiLanguageDictionary.Add(BreathingRegulationActivityName, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Breathing Regulation"},
            {SystemLanguage.Italian, "Breathing Regulation"}
        });

        this.multiLanguageDictionary.Add(BreathingRegulationA1Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Now we will do another exercise: we will concentrate on our breathing!\n\nPlease stand up."},
            {SystemLanguage.Italian, "Ora invece faremo un altro esercizio: ci concentreremo sulla nostra respirazione!\n\nAlzati in piedi."}
        });

        this.multiLanguageDictionary.Add(BreathingRegulationA2Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Imagine that your stomach is a balloon."},
            {SystemLanguage.Italian, "Immagina che il tuo stomaco sia un palloncino."}
        });

        this.multiLanguageDictionary.Add(BreathingRegulationA3Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Put one hand on your stomach and the other on your chest.\nNow fill your balloon-stomach with air (without moving the chest)."},
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
            {SystemLanguage.English, "And focus back on your breathing, an activity that you do every day automatically. Just breathe in and out, that’s right- just like that, 2 times."},
            {SystemLanguage.Italian, "E concentrati di nuovo sulla respirazione.\nQuesta è un'attività che fai ogni giorno automaticamente.\nSemplicemente inspira ed espira, 2 volte."}
        });

        this.multiLanguageDictionary.Add(BreathingRegulationB3Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "That's right! Good! Take a look at how many points you've gained and at your heart rate.\n\nLet's continue!"},
            {SystemLanguage.Italian, "Così giusto, proprio così! Bravo/Brava.\n\nGuarda quanti punti stai guadagnando e osserva il tuo battito.\n\nOra continuiamo!"}
        });

        this.multiLanguageDictionary.Add(ActiveMeditationTitle, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Let's be active, let's move ... and let's meditate!"},
            {SystemLanguage.Italian, "Attiviamoci, muoviamoci e...meditiamo!"}
        });

        this.multiLanguageDictionary.Add(ActiveMeditationActivityName, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Active/Shaking Meditation"},
            {SystemLanguage.Italian, "Active/Shaking Meditation"}
        });

        this.multiLanguageDictionary.Add(ActiveMeditationA1Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Now a bit of movement!"},
            {SystemLanguage.Italian, "Ora un po' di movimento!"}
        });

        this.multiLanguageDictionary.Add(ActiveMeditationA2Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Now we will put a music, for 3-5 minutes.\nYou'll have to move, dance with the beat, with all the parts of your body.\nTry to use every part.\n\nIf you want you can follow the movements of the avatar, or move freely.\n\nJust be careful not to turn off the sensor (and do not drop it) while moving ;-)."},
            {SystemLanguage.Italian, "Adesso metteremo una musica, per 3-5 minuti.\nDovrai far muovere, ballare a ritmo, tutte le parti del tuo corpo.\nCerca di utilizzare ogni parte.\n\n Se vuoi puoi seguire i movimenti dell'avatar, oppure muoverti liberamente.\n\nFai solo attenzione a non spegnere il sensore (e a non farlo cadere), mentre ti muovi ;-)."}
        });

        this.multiLanguageDictionary.Add(ActiveMeditationA3Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Ready? You can get a lot of points."},
            {SystemLanguage.Italian, "Pronto/Pronta?\n\nPuoi guadagnare un sacco di punti!"}
        });

        this.multiLanguageDictionary.Add(ActiveMeditationA4Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Let's start!"},
            {SystemLanguage.Italian, "Partiamo!"}
        });

        this.multiLanguageDictionary.Add(ActiveMeditationBText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Now you can stop and sit down.\nClose your eyes, relax and pay attention to your body, your breath, what you feel, for 1 minute, until you will hear a sound."},
            {SystemLanguage.Italian, "Ora puoi fermarti e sederti.\nChiudi gli occhi, rilassati e poni attenzione al tuo corpo, al tuo respiro, a ciò che senti, per 1 minuto, fino a che sentirai un suono."}
        });

        this.multiLanguageDictionary.Add(ActiveMeditationCText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Try to focus attention to the differences in sensations between the activity of shaking and the relaxation experienced afterwards.\n\nTake a few seconds, before continuing."},
            {SystemLanguage.Italian, "Cerca di cogliere le differenze tra quando hai ballato, hai mosso tutto il corpo, e quando invece ti sei  fermato e rilassato.\n\nPrenditi qualche secondo, prima di continuare."}
        });

        this.multiLanguageDictionary.Add(ActiveMeditationDText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Did you notice the differences? For every part of your body, your breathing and your feelings.Think about it, calmly."},
            {SystemLanguage.Italian, "Hai notato le differenze? Per ogni parte del tuo corpo, per il tuo respiro e per le tue sensazioni.\n\nRiflettici con calma."}
        });

        this.multiLanguageDictionary.Add(ProgressiveMuscleRelaxationTitle, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Let's relax our muscles!"},
            {SystemLanguage.Italian, "Rilassiamo i muscoli!"}
        });

        this.multiLanguageDictionary.Add(ProgressiveMuscleRelaxationActivityName, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Progressive Muscle Relaxation"},
            {SystemLanguage.Italian, "Progressive Muscle Relaxation"}
        });

        this.multiLanguageDictionary.Add(ProgressiveMuscleRelaxationAText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "The Progressive Muscle Relaxation is a technique for reducing anxiety by alternately tensing and relaxing the muscles.\n\nIt is an important and effective tool for relaxation and emotional regulating technique.\n\nYou can practice it at anytime and anywhere.\nIt's easy to master in a short span of time.\n\nThrough the process of first tensing then releasing muscles, you will aid your body in returning to a correct, more relaxed position.\nBy bringing on a state of relaxation, you will note that anxiety is diminished as well as the automatic physiological answer."},
            {SystemLanguage.Italian, "Il rilassamento muscolare progressivo è una tecnica per ridurre l’ansia alternando la tensione e il rilassamento muscolare.\n\nSi tratta di uno strumento importante ed efficace per il relax e la regolazione emotiva.\n\nPuoi praticarlo ovunque e in qualunque momento.\nÈ facile e veloce da imparare.\n\nAttraverso il processo di prima mettere in tensione, e poi rilassare i muscoli, aiuti il tuo corpo a tornare in una corretta posizione, più rilassata.\nPassando a uno stato di rilassamento, osserverai che l'ansia è diminuita, così come gli effetti negativi della risposta fisiologica automatica."}
        });

        this.multiLanguageDictionary.Add(ProgressiveMuscleRelaxationBText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Now let's start the exercise!\n\nIt will consist of 3 parts and you will have to use your imagination too.\nPlease stand up."},
            {SystemLanguage.Italian, "Ora iniziamo l'esercizio!\nSarà composto da 3 parti e dovrai usare anche la tua immaginazione.\nAlzati in piedi."}
        });

        this.multiLanguageDictionary.Add(ProgressiveMuscleRelaxationC1Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "First of all, you need to get a ball.\nHold it in your hand."},
            {SystemLanguage.Italian, "Prima di tutto, procurati una palla.\nTienila in mano."}
        });

        this.multiLanguageDictionary.Add(ProgressiveMuscleRelaxationC2Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Squeeze it really hard, with both your hands."},
            {SystemLanguage.Italian, "Ora stringila fortissimo, con entrambe le mani!"}
        });

        this.multiLanguageDictionary.Add(ProgressiveMuscleRelaxationC3Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Feel the pressure in your hands and in your arms as you squeeze it"},
            {SystemLanguage.Italian, "Senti la pressione nelle tue mani e nelle tue braccia mentre la spremi."}
        });

        this.multiLanguageDictionary.Add(ProgressiveMuscleRelaxationC4Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Now don't press with your hands, do as the avatar and relax your arms, holding the ball with one hand, so that it doesn't fall, and notice how your muscles are relaxed.\nSee how much better your hands feel."},
            {SystemLanguage.Italian, "Ora non premere più con le mani, fai come l'avatar e stendi le braccia, tenendo la palla con una mano, per non farla cadere, e senti come i tuoi muscoli sono rilassati.\nVedi come le tue mani si sentono meglio?"}
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

        this.multiLanguageDictionary.Add(ProgressiveMuscleRelaxationC8Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Let's continue!"},
            {SystemLanguage.Italian, "Ora continuiamo!"}
        });

        this.multiLanguageDictionary.Add(ProgressiveMuscleRelaxationD1Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Okay, ready for the second exercise?"},
            {SystemLanguage.Italian, "Ok! Pronto per il secondo esercizio?"}
        });

        this.multiLanguageDictionary.Add(ProgressiveMuscleRelaxationD2Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "It's morning and you just got up.\nYou want to stretch and to reach the sun with your hands"},
            {SystemLanguage.Italian, "E' mattina e ti sei appena svegliato.\nVuoi fare streching e raggiungere il sole con le mani"}});

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
            {SystemLanguage.English, "It feels good right? Great!"},
            {SystemLanguage.Italian, "Ti fa sentire bene, vero? Fantastico!"}
        });

        this.multiLanguageDictionary.Add(ProgressiveMuscleRelaxationE1Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Now let’s try another exercise!\n\nYou are a snail and you are sitting outside."},
            {SystemLanguage.Italian, "Passiamo al prossimo esercizio!\n\nSei una lumaca."}
        });

        this.multiLanguageDictionary.Add(ProgressiveMuscleRelaxationE2Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Uh-oh, all of sudden you hear the alarm and you sense danger!\nCurl back into your shell and hide…\nPull your shoulder up to your ears and push your head down into your shoulders."},
            {SystemLanguage.Italian, "Ad un tratto senti un allarme e percepisci un pericolo!\nPiegati nel tuo guscio e nasconditi…\nTira su le spalle fino alle orecchie e metti la testa dentro le spalle."}});

        this.multiLanguageDictionary.Add(ProgressiveMuscleRelaxationE3Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Hold it tight! Hold it!"},
            {SystemLanguage.Italian, "Tienile strette! Tienile strette!"}
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
            {SystemLanguage.English, "Hold it tight! Hold it!"},
            {SystemLanguage.Italian, "Tienile strette! Tienile strette!"}
        });

        this.multiLanguageDictionary.Add(ProgressiveMuscleRelaxationE7Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Ok you can relax now, bring your head out, and let your shoulders relax."},
            {SystemLanguage.Italian, "Ok, ora ti puoi rilassare, tira fuori la testa e rilassa le spalle."}
        });

        this.multiLanguageDictionary.Add(ProgressiveMuscleRelaxationE8Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Very good! Now click \"continue\"."},
            {SystemLanguage.Italian, "Bravo/Brava! Ora clicca \"continuare\"."}
        });

        this.multiLanguageDictionary.Add(ProgressiveMuscleRelaxationF1Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Here is the last exercise."},
            {SystemLanguage.Italian, "Ora facciamo l’ultimo esercizio."}
        });

        this.multiLanguageDictionary.Add(ProgressiveMuscleRelaxationF2Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "You are standing barefooted on a warm, sandy beach.\nSquish your toes deep into the sand and feel how it goes through your toes."},
            {SystemLanguage.Italian, "Sei al mare, a piedi nudi su una calda e morbida sabbia.\nPremi i piedi nella sabbia profondamente e senti come questa entra tra le dita."}
        });

        this.multiLanguageDictionary.Add(ProgressiveMuscleRelaxationF3Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Try to get your feet to the bottom, push down with your legs."},
            {SystemLanguage.Italian, "Cerca di spingere i piedi verso il basso, spingi con le gambe."}
        });

        this.multiLanguageDictionary.Add(ProgressiveMuscleRelaxationF4Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Now relax your feet, let your toes go loose and feel how nice and warm it is. Now you can slowly open your eyes.\n\nVery good! Now, let's continue!"},
            {SystemLanguage.Italian, "Ora rilassa i tuoi piedi, lascia sciogliere le tue dita e senti com'è piacevole, e come sono rilassate e calde.\n\nBravissimo/Bravissima! Proseguiamo!"}
        });

        this.multiLanguageDictionary.Add(InternalSensationsTitle, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Color!"},
            {SystemLanguage.Italian, "Colore!"}
        });

        this.multiLanguageDictionary.Add(InternalSensationsActivityName, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "IBox 2: Internal Sensations"},
            {SystemLanguage.Italian, "IBox 2: Internal Sensations"}
        });

        this.multiLanguageDictionary.Add(InternalSensationsAText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "We are coming to the end of our game.\n\nNow you can change the color to your Ibox!\n\nWe worked a lot on how to manage stress and on how to get relax.\n\nAs in the beginning of the game, now, in the next screen, you will see a palette of colors.\nPick one that represents for you the sense of relaxation, calm,and not of tension, as before..\nTake it and put it on the Ibox to color!\n\nEven now it will change color.\n\nClick \"continue\" to choose the color"},
            {SystemLanguage.Italian, "Stiamo giungendo alla fine del nostro gioco.\n\nOra potrai cambiare colore alla tua Ibox!\n\nAbbiamo lavorato un bel po' sulla gestione dello stress e su come rilassarci.\n\nCome all'inizio del gioco, troverai ora, nel prossimo screen, una tavolozza di colori.\nOra però dovrai sceglierne uno che rappresenti per te il senso di rilassamento, di calma, e non di tensione, come prima.\nPrendilo e mettilo sull' Ibox per colorarla!\n\nAnche ora cambierà colore.\n\nClicca “continuare” per scegliere il colore"}
        });

        this.multiLanguageDictionary.Add(ClosingOfSessionTitle, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "We came to the end..."},
            {SystemLanguage.Italian, "Siamo giunti alla fine..."}
        });

        this.multiLanguageDictionary.Add(ClosingOfSessionActivityName, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Closing of Session"},
            {SystemLanguage.Italian, "Closing of Session"}
        });

        this.multiLanguageDictionary.Add(MeMeterClosingText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Now we'll make the ME-METER, as you did at the beginning.\n\nMind your thoughts and notice your body.\n\nWhat is the level of tension you feel at this moment?\n\nDid it change from the beginning of the session?\n\nJust notice without judging.\nNot necessarily something must have changed.\n\nNow mark your level of tension on the ME-METER, from 1 to 10.\n(1 – very relaxed, 10 - very tense)"},
            {SystemLanguage.Italian, "Ora rifaremo il ME-METER, come hai fatto all'inizio.\n\nPoni attenzione ai tuoi pensieri e al tuo corpo.\n\nQual è il tuo livello di tensione in questo momento?\n\nC'è stato un cambiamento dall'inizio della sessione?\n\nSemplicemente riflettici, non giudicare.\nNon per forza deve essere cambiato qualcosa.\n\nOra segna il tuo livello di tensione sul ME-METER, da 1 a 10.\n(1 - molto rilassato, 10 - molto teso)"}
        });

        this.multiLanguageDictionary.Add(ClosingOfSessionA1Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "We are arrived at the end of our journey today!"},
            {SystemLanguage.Italian, "Siamo giunti alla fine del nostro percorso di oggi!"}
        });

        this.multiLanguageDictionary.Add(ClosingOfSessionA2Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Today we learned to be aware of our body and to regulate our sensations, through the breathing exercise, through the exercise when you have dance and shake all the body, and thanks to the attention you gave to your heartbeat.\nWe learned to notice the difference between  “stress” and “relax,through the Ibox, the ME-METER and through the exercise of mindfulness on your face.\nWe learned that sometimes we need to tense our muscles in order to feel it relaxed,through the Progressive Muscle Relaxation."},
            {SystemLanguage.Italian, "Abbiamo imparato a diventare più consapevoli del nostro corpo ed a regolare le nostre sensazioni,tramite l'esercizio di respirazione, tramite l'esercizio in cui hai ballato e mosso tutto il corpo, e grazie all'attenzione che hai posto ai tuoi battiti.\nAbbiamo imparato a notare la differenza tra “stress” e “relax”,tramite l'I-BOX, il ME-METER e tramite l'esercizio di mindfulness sul viso.\nAbbiamo imparato che a volte, dobbiamo mettere in tensione i nostri muscoli, per poterci poi rilassare,con il Rilassamento Muscolare Progressivo."}
        });

        this.multiLanguageDictionary.Add(ClosingOfSessionA3Text, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "You were very good!\nYou worked hard and definitely you will be able to practice these exercises, when you need it!"},
            {SystemLanguage.Italian, "Sei stato/stata molto bravo/brava! Ti sei impegnato/impegnata e sicuramente riuscirai a mettere in pratica questi esercizi, quando ne avrai bisogno!"}
        });

        this.multiLanguageDictionary.Add(ClosingOfSessionCandleText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Last but important step ....\nIt's up to our candle!\nWe came to the end and then we have to blow it out.\nBlow and click on it!"},
            {SystemLanguage.Italian, "Ultimo, ma importante passaggio...\nTocca alla nostra candela!\nSiamo giunti alla fine e quindi dobbiamo spegnerla.\nSoffia e clicca sopra!"}
        });

        this.multiLanguageDictionary.Add(ClosingOfSessionCText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Thank you for participating, I hope you enjoyed!"},
            {SystemLanguage.Italian, "Grazie per la partecipazione, spero tu ti sia divertito/divertita!"}
        });

        this.multiLanguageDictionary.Add(HowDoesMyBodyFeelTitle, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "How does my body feel?"},
            {SystemLanguage.Italian, "Cosa sento con il mio corpo?"}
        });

        this.multiLanguageDictionary.Add(HowDoesMyBodyFeelActivityName, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "How does my body feel?"},
            {SystemLanguage.Italian, "How does my body feel?"}
        });

        this.multiLanguageDictionary.Add(HowDoesMyBodyFeelAText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Now we will do another kind of exercise.\nYou can sit."},
            {SystemLanguage.Italian, "Ora passiamo ad un altro tipo di esercizio.\nPuoi sederti."}
        });

        this.multiLanguageDictionary.Add(HowDoesMyBodyFeelBText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Here beside you can see a list of the main emotions.\nRead them carefully and try to identify where in your body, you feel them.\nNow, think only, then click continue and you will find your avatar, together with the list, in order to do the exercise."},
            {SystemLanguage.Italian, "Qui di fianco vedi una lista delle emozioni principali.\nLeggile con attenzione e cerca di individuare dove nel tuo corpo, le senti.\nRiflettici soltanto ora, poi clicca continuare e troverai anche il tuo avatar, insieme alla lista, per poter svolgere l'esercizio."}
        });

        this.multiLanguageDictionary.Add(HowDoesMyBodyFeelCText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Now you have to mark on your avatar where you feel these emotions.\nClick on an emotion and then drag and drop it on the part of your body.\nIt will color in the same way.\n\nThink about every emotion, in general, even if you do not feel it now, in this moment.\n\nYou'll have to mark all the emotions, in order to earn points, remember that!\n\nDon't worry if you have to place two or more emotions, on the same side.\nIf you then change your mind, click 2 times on that part, and re-select."},
            {SystemLanguage.Italian, "Ora dovrai segnare sul tuo avatar dove senti queste emozioni.\nClicca sull'emozione e poi trascina la mano sulla parte del corpo, dove la senti.\nSi colorerà allo stesso modo.\n\nRagiona su ogni emozione, in generale, anche se non la senti ora, in questo momento.\n\nDovrai segnare tutte le emozioni, per poter guadagnare i punti, ricordatelo!\n\nNon ti preoccupare se dovrai posizionare due o più emozioni, sulla stessa parte.\nSe poi cambi idea, clicca 2 volte sulla parte, e riseleziona."}
        });

        this.multiLanguageDictionary.Add(HowDoesMyBodyFeelDText, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Good, great job!"},
            {SystemLanguage.Italian, "Bravo, ottimo lavoro!"}
        });


        this.multiLanguageDictionary.Add(HowDoesMyBodyFeelHappiness, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Happiness"},
            {SystemLanguage.Italian, "Felicità"}
        });

        this.multiLanguageDictionary.Add(HowDoesMyBodyFeelSadness, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Sadness"},
            {SystemLanguage.Italian, "Tristezza"}
        });

        this.multiLanguageDictionary.Add(HowDoesMyBodyFeelAnger, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Anger"},
            {SystemLanguage.Italian, "Rabbia"}
        });

        this.multiLanguageDictionary.Add(HowDoesMyBodyFeelFear, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Fear"},
            {SystemLanguage.Italian, "Paura"}
        });

        this.multiLanguageDictionary.Add(HowDoesMyBodyFeelDisgust, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Disgust"},
            {SystemLanguage.Italian, "Disgusto"}
        });

        this.multiLanguageDictionary.Add(HowDoesMyBodyFeelSurprise, new Dictionary<SystemLanguage, string>
        {
            {SystemLanguage.English, "Surprise"},
            {SystemLanguage.Italian, "Sorpresa"}
        });
        
    }
}
